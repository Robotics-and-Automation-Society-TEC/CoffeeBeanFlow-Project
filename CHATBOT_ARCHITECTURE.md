# Diagrama de Arquitectura del Chatbot

## 🏗️ Arquitectura del Sistema

```
┌─────────────────────────────────────────────────────────────┐
│                     NAVEGADOR WEB                           │
│                                                             │
│  ┌────────────────────────────────────────────────────┐   │
│  │         Interfaz Angular (Frontend)                │   │
│  │                                                     │   │
│  │  ┌──────────────────────────────────────────┐     │   │
│  │  │  Componente Chatbot                      │     │   │
│  │  │  - Botón flotante 💬                    │     │   │
│  │  │  - Ventana de chat                       │     │   │
│  │  │  - Lista de mensajes                     │     │   │
│  │  │  - Input de texto                        │     │   │
│  │  └──────────────┬───────────────────────────┘     │   │
│  │                 │                                  │   │
│  │                 ↓                                  │   │
│  │  ┌──────────────────────────────────────────┐     │   │
│  │  │  ChatbotService                          │     │   │
│  │  │  - messages$ (Observable)                │     │   │
│  │  │  - sendMessage()                         │     │   │
│  │  │  - addMessage()                          │     │   │
│  │  └──────────────┬───────────────────────────┘     │   │
│  │                 │                                  │   │
│  └─────────────────┼──────────────────────────────────┘   │
│                    │                                      │
└────────────────────┼──────────────────────────────────────┘
                     │
                     │ HTTP POST /api/Chatbot/message
                     ↓
┌─────────────────────────────────────────────────────────────┐
│              SERVIDOR .NET (Backend)                        │
│                                                             │
│  ┌────────────────────────────────────────────────────┐   │
│  │  ChatbotController                                 │   │
│  │  - POST /api/Chatbot/message                       │   │
│  │  - GET /api/Chatbot/status                         │   │
│  │  - StartChatbotProcess()                           │   │
│  └──────────────┬─────────────────────────────────────┘   │
│                 │                                          │
│                 │ Process.Start()                          │
│                 │ StandardInput/Output                     │
│                 ↓                                          │
└─────────────────────────────────────────────────────────────┘
                  │
                  │
                  ↓
┌─────────────────────────────────────────────────────────────┐
│           PROCESO PYTHON (Agente IA)                        │
│                                                             │
│  ┌────────────────────────────────────────────────────┐   │
│  │  coffee_agent_api.py                               │   │
│  │                                                     │   │
│  │  ┌──────────────────────────────────────────┐     │   │
│  │  │  LangGraph Agent                         │     │   │
│  │  │  - Model: GPT-4.1-nano                   │     │   │
│  │  │  - Memory: InMemorySaver                 │     │   │
│  │  │  - System Prompt (Español)               │     │   │
│  │  └────────┬─────────────────────────────────┘     │   │
│  │           │                                        │   │
│  │           ├─────────────────┬──────────────────┐  │   │
│  │           ↓                 ↓                  ↓  │   │
│  │  ┌────────────────┐ ┌──────────────┐ ┌──────────────┐   │
│  │  │ Tool: RAG      │ │ Tool: SQL    │ │ Tool: SQL    │   │
│  │  │ retrieve_      │ │ list_tables  │ │ query_sql    │   │
│  │  │ context()      │ │              │ │              │   │
│  │  └────────┬───────┘ └──────┬───────┘ └──────┬───────┘   │
│  │           │                │                │           │
│  └───────────┼────────────────┼────────────────┼───────────┘
│              │                │                │           │
└──────────────┼────────────────┼────────────────┼───────────┘
               │                │                │
               ↓                ↓                ↓
┌──────────────────────┐ ┌─────────────────────────────┐
│   Qdrant Cloud       │ │   SQLite Database           │
│   Vector Database    │ │   (Chinook.db)              │
│   - RAG Collection   │ │   - Tables: Artist, Album,  │
│   - Coffee Docs      │ │     Track, etc.             │
└──────────────────────┘ └─────────────────────────────┘
```

## 📊 Flujo de Datos

### 1. Usuario Envía Mensaje

```
Usuario escribe: "¿Cómo secar café?"
         ↓
ChatbotComponent.sendMessage()
         ↓
ChatbotService.sendMessage(mensaje)
         ↓
HTTP POST → http://192.168.0.47:5253/api/Chatbot/message
         ↓
{ message: "¿Cómo secar café?" }
```

### 2. Backend Procesa

```
ChatbotController recibe request
         ↓
Verifica si proceso Python está activo
         ↓
Si NO → StartChatbotProcess()
         ↓
Envía mensaje a Python via StandardInput
         ↓
Espera respuesta de StandardOutput (max 30s)
         ↓
Lee líneas hasta encontrar "Input query:"
```

### 3. Python Procesa

```
coffee_agent_api.py recibe mensaje
         ↓
Agent.stream() procesa la consulta
         ↓
Evalúa qué herramienta(s) usar:
  - retrieve_context() para info sobre café
  - SQL tools para consultas de base de datos
         ↓
GPT-4.1-nano genera respuesta
         ↓
Retorna texto en español
```

### 4. Respuesta al Usuario

```
Python devuelve respuesta
         ↓
Backend captura output
         ↓
HTTP Response → { response: "...", success: true }
         ↓
ChatbotService recibe respuesta
         ↓
addMessage() agrega al historial
         ↓
ChatbotComponent muestra mensaje en UI
         ↓
Usuario ve respuesta en ventana de chat
```

## 🔄 Estados del Chatbot

### Estado Inicial
```
┌──────────────────┐
│  Bot Cerrado     │  ← Botón flotante visible
│  (isOpen=false)  │
└──────────────────┘
```

### Usuario Hace Clic
```
┌──────────────────┐
│  Bot Abierto     │  ← Ventana de chat visible
│  (isOpen=true)   │  ← Muestra mensaje de bienvenida
└──────────────────┘
```

### Usuario Envía Mensaje
```
┌──────────────────┐
│  Enviando...     │  ← Mensaje del usuario agregado
│  (isLoading=true)│  ← Indicador "escribiendo..."
└──────────────────┘
```

### Bot Responde
```
┌──────────────────┐
│  Bot Respondió   │  ← Mensaje del bot agregado
│ (isLoading=false)│  ← Listo para nuevo mensaje
└──────────────────┘
```

## 🔌 Endpoints API

### POST /api/Chatbot/message
**Request:**
```json
{
  "message": "¿Cómo secar café?"
}
```

**Response (Success):**
```json
{
  "response": "El secado del café es un proceso crucial...",
  "success": true,
  "error": null
}
```

**Response (Error):**
```json
{
  "response": "",
  "success": false,
  "error": "El chatbot no está disponible"
}
```

### GET /api/Chatbot/status
**Response:**
```json
{
  "isRunning": true,
  "message": "Chatbot activo"
}
```

## 🎨 Componentes Visuales

### Botón Flotante
- Posición: bottom-right (20px, 20px)
- Tamaño: 60px × 60px
- Color: Gradient café (#6B4423 → #8B5A3C)
- Icono: 💬 (cerrado) / ✕ (abierto)

### Ventana de Chat
- Tamaño: 380px × 550px
- Posición: sobre el botón
- Secciones:
  - Header (café oscuro)
  - Messages (fondo gris claro)
  - Input (fondo blanco)

### Mensajes
- Usuario: derecha, fondo café
- Bot: izquierda, fondo blanco
- Timestamp: debajo de cada mensaje
- Animación: slide-in desde abajo

## 📱 Responsive Design

### Desktop (> 480px)
- Ventana: 380px × 550px
- Posición: fixed bottom-right

### Mobile (≤ 480px)
- Ventana: calc(100vw - 40px)
- Altura: calc(100vh - 160px)
- Max-height: 600px

---

**Nota:** Este diagrama representa la arquitectura actual. 
Para integración con la base de datos real de CoffeeBeanFlow, 
reemplazar Chinook.db con PostgreSQL en futuras versiones.
