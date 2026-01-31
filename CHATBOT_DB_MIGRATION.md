# 🔄 Migración a Base de Datos Real - CoffeeBeanFlow

## 📌 Objetivo

Conectar el chatbot a la base de datos PostgreSQL real de CoffeeBeanFlow en lugar de la base de datos de prueba Chinook.db.

## 🗄️ Base de Datos Actual

El chatbot actualmente usa:
- **Base de datos**: Chinook.db (SQLite)
- **Contenido**: Información sobre bandas de rock, álbumes, canciones
- **Propósito**: Solo para demostración

## 🎯 Base de Datos Objetivo

Conectar a:
- **Base de datos**: CoffeeBeanFlowDB (PostgreSQL)
- **Tablas principales**:
  - AreaAcopio
  - Secado
  - Bodega
  - Trilla
  - Caracterizacion
  - Catacion
  - GuardarCafe
  - PesoVerde
  - EnviarMuestras
  - Y relaciones N-N

## 🔧 Pasos de Implementación

### 1. Actualizar Dependencias Python

Agregar a `requirements.txt`:
```txt
psycopg2-binary
sqlalchemy
langchain-community
```

Instalar:
```bash
pip install psycopg2-binary sqlalchemy
```

### 2. Modificar coffee_agent_api.py

Reemplazar la sección de SQL DATABASE:

#### Código Actual (líneas ~60-77):
```python
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
```

#### Código Nuevo:
```python
# SQL DATABASE - CoffeeBeanFlow PostgreSQL
from langchain_community.utilities import SQLDatabase
from sqlalchemy import create_engine

# Obtener connection string desde variables de entorno
# Formato: postgresql://usuario:password@host:puerto/database
connection_string = os.getenv('POSTGRES_CONNECTION_STRING')

if not connection_string:
    # Fallback para desarrollo local
    connection_string = "postgresql://postgres:password@localhost:5432/CoffeeBeanFlowDB"
    print("⚠️ Usando connection string por defecto. Configura POSTGRES_CONNECTION_STRING en .env")

# Crear conexión a PostgreSQL
db = SQLDatabase.from_uri(connection_string)

# Verificar conexión
try:
    tables = db.get_usable_table_names()
    print(f"✅ Conectado a PostgreSQL. Tablas disponibles: {len(tables)}")
except Exception as e:
    print(f"❌ Error al conectar a PostgreSQL: {e}")
    # Usar SQLite como fallback
    db = SQLDatabase.from_uri("sqlite:///Chinook.db")
    print("⚠️ Usando Chinook.db como fallback")
```

### 3. Actualizar System Prompt

Modificar el system_prompt para reflejar la base de datos de café:

#### Código Actual (líneas ~115-139):
```python
system_prompt = """
You are an agent tasked with helping people who work with coffee harvesting and processing in the countryside in Costa Rica. Speak accordingly, always in Spanish.

You have access to two tools, use both as needed.

The first tool retrieves context about coffee from different documents. Use this tool to help answer user queries when needed.

The second tool retrieves information from a SQL database about rock bands.
When interacting with the SQL database, create a syntactically correct {dialect} query
to run based on the given input question, then look at the results of the query and return the answer.
...
```

#### Código Nuevo:
```python
system_prompt = """
Eres un asistente de inteligencia artificial especializado en el procesamiento de café en Costa Rica, 
específicamente para el sistema CoffeeBeanFlow de gestión de microbeneficio.

Tienes acceso a dos herramientas principales:

1. **Herramienta RAG (Documentos)**: Recupera información de documentos sobre café, 
   procesamiento, cultivo, y mejores prácticas. Úsala para responder preguntas generales 
   sobre café.

2. **Herramienta SQL (Base de Datos)**: Accede a la base de datos del microbeneficio 
   CoffeeBeanFlow que contiene información sobre:
   - Área de Acopio: Recepción y acopio de café
   - Secado: Proceso de secado del café
   - Bodega: Almacenamiento de café seco
   - Trilla: Proceso de trilla
   - Caracterización: Análisis físico del café
   - Catación: Evaluación sensorial y puntuación
   - Relaciones y trazabilidad de lotes

Al usar la base de datos SQL:
- Crea consultas sintácticamente correctas en {dialect}
- Limita los resultados a {top_k} registros a menos que se especifique otro número
- Ordena los resultados por columnas relevantes
- Verifica tu consulta antes de ejecutarla
- Si hay un error, reescribe la consulta e intenta de nuevo
- NUNCA hagas statements DML (INSERT, UPDATE, DELETE, DROP)
- SIEMPRE empieza mirando las tablas disponibles
- Luego consulta el schema de las tablas más relevantes

**Instrucciones importantes:**
- Responde SIEMPRE en español
- Sé conciso y claro en tus respuestas
- Si no estás seguro, pregunta para clarificar
- Cuando muestres datos numéricos, usa formato apropiado (kg, %, etc.)
- Para fechas, usa formato DD/MM/YYYY
- Cuando hables de lotes, usa el formato LOTE-XXX

**Ejemplos de preguntas que puedes responder:**
- "¿Cuántos lotes tenemos en el área de acopio?"
- "¿Cuál es el peso promedio de los lotes en secado?"
- "Muéstrame las últimas cataciones con puntuación mayor a 85"
- "¿Qué lotes están listos para trilla?"
- "¿Cómo se hace el proceso de beneficiado húmedo?"
""".format(
    dialect=db.dialect,
    top_k=10,  # Aumentado a 10 para datos de café
)
```

### 4. Actualizar .env

Agregar al archivo `.env`:

```env
# Existing keys
LangSmith_API_Key=tu_clave_langsmith
OpenAI_Key=tu_clave_openai
Qdrant_Key=tu_clave_qdrant

# Nueva conexión PostgreSQL
POSTGRES_CONNECTION_STRING=postgresql://usuario:password@192.168.0.47:5432/CoffeeBeanFlowDB
```

**Importante:** Reemplazar `usuario` y `password` con las credenciales reales.

### 5. Probar la Conexión

Crear un script de prueba `test_db_connection.py`:

```python
import os
from dotenv import load_dotenv
from langchain_community.utilities import SQLDatabase

load_dotenv()

connection_string = os.getenv('POSTGRES_CONNECTION_STRING')
print(f"Connection string: {connection_string[:30]}...")

try:
    db = SQLDatabase.from_uri(connection_string)
    tables = db.get_usable_table_names()
    
    print(f"\n✅ Conexión exitosa!")
    print(f"\nTablas disponibles ({len(tables)}):")
    for table in sorted(tables):
        print(f"  - {table}")
    
    # Probar una consulta simple
    result = db.run("SELECT COUNT(*) FROM \"AreaAcopio\";")
    print(f"\nPrueba de consulta:")
    print(f"  Total de registros en AreaAcopio: {result}")
    
except Exception as e:
    print(f"\n❌ Error: {e}")
```

Ejecutar:
```bash
python test_db_connection.py
```

### 6. Consultas de Ejemplo

Una vez conectado, el chatbot podrá responder preguntas como:

#### Consultas de Inventario
- "¿Cuántos lotes hay en el área de acopio?"
- "Muéstrame todos los lotes en secado"
- "¿Cuánto café tenemos en bodega?"

#### Consultas de Calidad
- "¿Cuál es la puntuación promedio de nuestras cataciones?"
- "Muéstrame las cataciones con puntuación mayor a 85"
- "¿Qué lotes tienen mejor perfil de taza?"

#### Consultas de Trazabilidad
- "Dame el historial completo del lote LOTE-001"
- "¿Qué lotes están en proceso de trilla?"
- "Muéstrame el flujo del lote más reciente"

#### Consultas de Producción
- "¿Cuál es el peso total de café en el sistema?"
- "¿Cuántos kilos se han trillado este mes?"
- "¿Cuál es el rendimiento promedio de trilla?"

### 7. Optimizaciones Recomendadas

#### 7.1 Índices en la Base de Datos
Asegurar que existan índices en:
- Llaves primarias (ya existen)
- Campos de fecha para consultas temporales
- Campos de estado para filtros

#### 7.2 Vistas Materializadas
Crear vistas para consultas comunes:

```sql
-- Vista de resumen de lotes
CREATE VIEW vw_resumen_lotes AS
SELECT 
    aa.nlote,
    aa.fecha_recepcion,
    aa.peso_bruto,
    s.fecha_inicio_secado,
    b.fecha_almacenamiento,
    c.puntuacion_final
FROM "AreaAcopio" aa
LEFT JOIN "Secado" s ON aa.nlote = s.nlote
LEFT JOIN "GuardarCafe" gc ON s.secado_id = gc.secado_id
LEFT JOIN "Bodega" b ON gc.bodega_id = b.bodega_id
LEFT JOIN "Catacion" c ON aa.nlote = c.nlote;
```

#### 7.3 Caché de Consultas Frecuentes
Implementar caché para:
- Total de lotes por estado
- Resumen de inventario
- Estadísticas generales

### 8. Seguridad

⚠️ **IMPORTANTE**: 

1. **Usuario de Solo Lectura**
   Crear un usuario PostgreSQL con permisos de solo lectura:
   ```sql
   CREATE USER chatbot_readonly WITH PASSWORD 'password_seguro';
   GRANT CONNECT ON DATABASE CoffeeBeanFlowDB TO chatbot_readonly;
   GRANT USAGE ON SCHEMA public TO chatbot_readonly;
   GRANT SELECT ON ALL TABLES IN SCHEMA public TO chatbot_readonly;
   ```

2. **Usar este usuario en el connection string**:
   ```env
   POSTGRES_CONNECTION_STRING=postgresql://chatbot_readonly:password_seguro@host:5432/CoffeeBeanFlowDB
   ```

3. **Rate Limiting**
   Implementar límite de consultas por usuario/sesión

4. **Validación de Consultas**
   El agente ya tiene protección contra DML, pero monitorear logs

### 9. Testing Post-Migración

Probar todas las funcionalidades:

```bash
# 1. Verificar conexión
python test_db_connection.py

# 2. Probar agente standalone
python coffee_agent_api.py
# Pregunta: "¿Cuántos lotes hay en el sistema?"

# 3. Probar vía API
# Iniciar backend y frontend, luego usar el chatbot
```

### 10. Monitoreo

Implementar logging de:
- Consultas SQL ejecutadas
- Tiempo de respuesta
- Errores de conexión
- Uso de recursos

Agregar a `coffee_agent_api.py`:

```python
import logging

logging.basicConfig(
    filename='chatbot.log',
    level=logging.INFO,
    format='%(asctime)s - %(levelname)s - %(message)s'
)

# Antes de cada consulta SQL
logging.info(f"SQL Query: {query}")
logging.info(f"Response time: {elapsed_time}s")
```

## 📋 Checklist de Migración

- [ ] Instalar psycopg2-binary
- [ ] Configurar POSTGRES_CONNECTION_STRING en .env
- [ ] Crear usuario de solo lectura en PostgreSQL
- [ ] Actualizar coffee_agent_api.py
- [ ] Actualizar system_prompt
- [ ] Ejecutar test_db_connection.py
- [ ] Probar consultas básicas
- [ ] Probar desde la interfaz web
- [ ] Configurar logging
- [ ] Documentar consultas comunes
- [ ] Implementar monitoreo

## 🎯 Resultado Esperado

Después de la migración:
- ✅ Chatbot conectado a base de datos real
- ✅ Responde preguntas sobre el inventario real
- ✅ Provee trazabilidad de lotes
- ✅ Ayuda en análisis de calidad
- ✅ Mantiene seguridad (solo lectura)

---

**Próximo paso**: Una vez completada la migración, el chatbot será una herramienta 
poderosa para consultar información del microbeneficio en lenguaje natural.
