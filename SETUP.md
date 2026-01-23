# Guía para correr el chatbot

Primero debe crear un .env con las API keys, y luego solo hace falta copiar todos los documentos y correr el archivo de python.

---

## Tabla de contenidos

En cada sección se explica la función de cada archivo en el repositorio.

1. [coffee_agent.py](#coffee_agent.py)
2. [requirements.txt](#requirements.txt)
3. [.env](#.env)
4. [Chinook.db](#chinook.db)

---

## coffee_agent.py

Este es el código principal en python que contiene todas las funcionalidades del agente de IA. Para explicaciones más detalladas puede revisar este [notebook](https://colab.research.google.com/drive/1ll1v516G7zXn6Kc-1Tt2bjma5pbZtifh?usp=sharing).

El código se comunica con OpenAI para usar el modelo de lenguaje, pide información de los documentos en Qdrant, y guarda las respuestas en LangSmith.

Al final del archivo puede ingresar su query (donde hay otras preguntas, cambiélas por la suya), y el chatbot imprime su mensaje.

---

## requirements.txt

Contiene las librerías necesarias para el código del agente.

```powershell
pip install -r requirements.txt
```

---

## .env

Este archivo debe crearlo con este nombre, y debe copiar esto en ese archivo.

```powershell
LangSmith_API_Key="your-api-key"
OpenAI_Key="your-api-key"
Qdrant_Key="your-api-key"
```

Debe agregar las API keys que se utilizan en el proyecto.

---

## Chinook.db

Esta es una base de datos SQL de prueba, contiene información de bandas de rock.

En lugar de este archivo, se debe incluir la base de datos real del beneficio. Esto se cambio en el código de python.