# CoffeeBeanFlow API Wrapper
# Este wrapper permite usar el agente de café a través de una API

import sys
import json
from io import StringIO

# Importar el agente
import os
from dotenv import load_dotenv

# Import from .env file
load_dotenv()

# LangSmith
os.environ["LANGSMITH_TRACING"] = "true"
os.environ["LANGSMITH_API_KEY"] = os.getenv("LangSmith_API_Key")
# OpenAI
os.environ["OPENAI_API_KEY"] = os.getenv('OpenAI_Key')

# CHOOSE MODELS - OPENAI
from langchain_openai import ChatOpenAI
from langchain_openai import OpenAIEmbeddings

# Using gpt-4.1-nano as LLM
model = ChatOpenAI(model="gpt-4.1-nano")

# Using text-embedding-3-small for embeddings
embeddings = OpenAIEmbeddings(model="text-embedding-3-small")

# QDRANT VECTOR DATABASE
from qdrant_client.models import Distance, VectorParams
from langchain_qdrant import QdrantVectorStore
from qdrant_client import QdrantClient

# Load cloud api
client = QdrantClient(
    url="https://6fceb271-9e8c-4e12-82c0-f244c9c3aa3e.us-east4-0.gcp.cloud.qdrant.io:6333",
    api_key=os.getenv('Qdrant_Key'),
)

# Get length of embedding vector
vector_size = len(embeddings.embed_query("sample text"))

# Create RAG collection
if not client.collection_exists("RAG"):
    client.create_collection(
        collection_name="RAG",
        vectors_config=VectorParams(size=vector_size, distance=Distance.COSINE)
    )

# Load cluster
vector_store = QdrantVectorStore(
    client=client,
    collection_name="RAG",
    embedding=embeddings,
)

# SQL DATABASE
import requests, pathlib
from langchain_community.utilities import SQLDatabase

# Download from web page - Test only
url = "https://storage.googleapis.com/benchmarks-artifacts/chinook/Chinook.db"
local_path = pathlib.Path("Chinook.db")

# Create path
if local_path.exists():
    pass
else:
    response = requests.get(url)
    if response.status_code == 200:
        local_path.write_bytes(response.content)

db = SQLDatabase.from_uri("sqlite:///Chinook.db")

# CREATE TOOLS
from langchain.tools import tool
from langchain_community.agent_toolkits import SQLDatabaseToolkit

# Create first tool - Retrieve info from pdfs (RAG)
@tool(response_format="content_and_artifact")
def retrieve_context(query: str):
    "Retrieve information to help answer a query."
    retrieved_docs = vector_store.similarity_search(query, k=3)
    serialized = "\n\n".join(
        (f"Source: {doc.metadata}\nContent: {doc.page_content}")
        for doc in retrieved_docs
    )
    return serialized, retrieved_docs

# Create second tool - Retrieve info from sql database
toolkit = SQLDatabaseToolkit(db=db, llm=model)
sql_tools = toolkit.get_tools()

# CREATE AGENT
from langchain.agents import create_agent
from langgraph.checkpoint.memory import InMemorySaver

# Allow for short-term memory
checkpointer = InMemorySaver()

# Load tools
tools = [retrieve_context] + sql_tools

# Custom system prompt
system_prompt = """
You are an agent tasked with helping people who work with coffee harvesting and processing in the countryside in Costa Rica. Speak accordingly, always in Spanish.

You have access to two tools, use both as needed.

The first tool retrieves context about coffee from different documents. Use this tool to help answer user queries when needed.

The second tool retrieves information from a SQL database about rock bands.
When interacting with the SQL database, create a syntactically correct {dialect} query
to run based on the given input question, then look at the results of the query and return the answer.
Unless the user specifies a specific number of examples they wish to obtain, always limit your
query to at most {top_k} results.
You can order the results by a relevant column to return the most interesting
examples in the database. Never query for all the columns from a specific table,
only ask for the relevant columns given the question.
You MUST double check your query before executing it. If you get an error while
executing a query, rewrite the query and try again.
DO NOT make any DML statements (INSERT, UPDATE, DELETE, DROP etc.) to the
database.
To start you should ALWAYS look at the tables in the database to see what you
can query. Do NOT skip this step.
Then you should query the schema of the most relevant tables.
""".format(
    dialect=db.dialect,
    top_k=5,
)

# Initialize agent
agent = create_agent(model, tools, checkpointer=checkpointer, system_prompt=system_prompt)

def get_response(query):
    """Get response from the agent for a given query."""
    try:
        # Capture the response
        response_text = ""
        
        for event in agent.stream(
            {
                "messages": [{
                    "role": "user",
                    "content": query
                }]
            },
            {"configurable": {"thread_id": "1"}},
            stream_mode="values",
        ):
            # Get the last message
            last_message = event["messages"][-1]
            if hasattr(last_message, 'content'):
                response_text = last_message.content
        
        return response_text
    except Exception as e:
        return f"Error: {str(e)}"

# Main loop for CLI interface
if __name__ == "__main__":
    print("\nChatbot ready!", flush=True)
    
    while True:
        try:
            query = input("\nInput query: ")
            if query.strip().lower() in ['exit', 'quit', 'salir']:
                print("Goodbye!")
                break
                
            response = get_response(query)
            print(f"\nBot: {response}", flush=True)
        except EOFError:
            break
        except KeyboardInterrupt:
            print("\nGoodbye!")
            break
