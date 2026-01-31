# Integración del Chatbot de Café - CoffeeBeanFlow

## 📋 Descripción

Se ha integrado un chatbot de inteligencia artificial en la interfaz web de CoffeeBeanFlow. Este asistente virtual está diseñado para ayudar a las personas que trabajan con café en Costa Rica, proporcionando información sobre el cultivo, procesamiento y gestión del café.

## 🎯 Características

- **Botón flotante**: Un botón de chat visible en la esquina inferior derecha de todas las páginas
- **Interfaz amigable**: Ventana de chat moderna con tema café
- **Respuestas en español**: El bot está configurado para responder en español
- **Memoria de conversación**: Mantiene el contexto de la conversación
- **Acceso a dos fuentes de datos**:
  - Base de documentos sobre café (RAG)
  - Base de datos SQL (para consultas estructuradas)

## 🏗️ Arquitectura

### Frontend (Angular)

1. **Componente**: `ChatbotComponent`
   - Ubicación: `/Frontend/src/app/shared/components/chatbot/`
   - Archivos:
     - `chatbot.ts` - Lógica del componente
     - `chatbot.html` - Template del chat
     - `chatbot.css` - Estilos personalizados

2. **Servicio**: `ChatbotService`
   - Ubicación: `/Frontend/src/app/core/services/chatbot.service.ts`
   - Funciones:
     - `sendMessage()` - Envía mensajes al backend
     - `addMessage()` - Agrega mensajes al historial
     - `clearMessages()` - Limpia el historial
     - `getChatbotStatus()` - Verifica el estado del bot

3. **Integración**: El componente se agrega automáticamente en `app.ts`

### Backend (.NET)

1. **Controlador**: `ChatbotController`
   - Ubicación: `/Backend/Controllers/ChatbotController.cs`
   - Endpoints:
     - `POST /api/Chatbot/message` - Enviar mensaje al bot
     - `GET /api/Chatbot/status` - Obtener estado del bot

2. **Proceso Python**: El controlador inicia y gestiona el proceso de Python que ejecuta el agente de IA

### Python Agent

1. **Script principal**: `coffee_agent_api.py`
   - Versión optimizada para uso con API
   - Mantiene el agente en memoria
   - Procesa consultas de forma eficiente

## 🚀 Instalación y Configuración

### 1. Dependencias de Python

Asegúrate de tener instaladas todas las dependencias necesarias:

```bash
cd /home/noemi/Documentos/Repo-Cafe/CoffeeBeanFlow-Project
pip install -r requirements.txt
```

### 2. Variables de Entorno

Crea un archivo `.env` en el directorio raíz con las siguientes claves:

```env
LangSmith_API_Key=tu_clave_langsmith
OpenAI_Key=tu_clave_openai
Qdrant_Key=tu_clave_qdrant
```

### 3. Configuración del Backend

El controlador está configurado para usar Python 3. Si tu instalación de Python está en otra ubicación, actualiza la ruta en `ChatbotController.cs`:

```csharp
var pythonPath = "/usr/bin/python3"; // Cambiar según tu sistema
```

### 4. Configuración del Frontend

El servicio está configurado para conectarse a la API. Si necesitas cambiar la URL del backend, edita:

`/Frontend/src/app/core/services/chatbot.service.ts`:

```typescript
private apiUrl = 'http://192.168.0.47:5253/api/Chatbot';
```

## 📝 Uso

### Para Usuarios

1. **Abrir el chat**: Haz clic en el botón flotante 💬 en la esquina inferior derecha
2. **Escribir mensaje**: Escribe tu pregunta en el campo de texto
3. **Enviar**: Presiona Enter o haz clic en el botón de enviar 📤
4. **Recibir respuesta**: El bot procesará tu consulta y responderá
5. **Limpiar chat**: Haz clic en el icono de basura 🗑️ para limpiar el historial
6. **Cerrar chat**: Haz clic en la X para minimizar el chat

### Ejemplos de Consultas

- "¿Cuál es la mejor temperatura para secar café?"
- "¿Cómo se caracteriza el café de alta calidad?"
- "Explícame el proceso de catación"
- "¿Qué es el beneficiado del café?"

## 🛠️ Desarrollo

### Modificar Estilos

Edita `/Frontend/src/app/shared/components/chatbot/chatbot.css` para personalizar:
- Colores (actualmente tema café)
- Tamaño de la ventana
- Posición del botón
- Animaciones

### Modificar Comportamiento

Edita `/Frontend/src/app/shared/components/chatbot/chatbot.ts` para:
- Cambiar mensajes de bienvenida
- Ajustar timeout de respuestas
- Agregar funcionalidades adicionales

### Actualizar el Agente

Edita `coffee_agent_api.py` para:
- Cambiar el modelo de IA
- Modificar el prompt del sistema
- Agregar nuevas herramientas
- Conectar a diferentes bases de datos

## 🔧 Troubleshooting

### El bot no responde

1. Verifica que el proceso de Python esté ejecutándose:
   ```bash
   ps aux | grep python
   ```

2. Revisa los logs del backend para errores

3. Verifica que las variables de entorno estén configuradas correctamente

### Errores de conexión

1. Verifica que el backend esté ejecutándose en el puerto correcto (5253)
2. Asegúrate de que CORS esté configurado correctamente en `Program.cs`
3. Verifica la URL en `chatbot.service.ts`

### El proceso de Python no inicia

1. Verifica la ruta de Python en `ChatbotController.cs`
2. Asegúrate de que `coffee_agent_api.py` tenga permisos de ejecución
3. Verifica que todas las dependencias de Python estén instaladas

## 📊 Estado Actual

✅ Componente de chatbot creado
✅ Servicio de comunicación implementado
✅ Endpoint de API configurado
✅ Wrapper de Python creado
✅ Integración en la aplicación principal
✅ Estilos personalizados aplicados

## 🔮 Futuras Mejoras

- [ ] Conectar a la base de datos real de CoffeeBeanFlow en lugar de Chinook.db
- [ ] Agregar soporte para archivos adjuntos
- [ ] Implementar historial persistente de conversaciones
- [ ] Agregar sugerencias de preguntas frecuentes
- [ ] Implementar autenticación de usuarios
- [ ] Mejorar el manejo de errores y reintentos
- [ ] Agregar indicadores de estado del bot
- [ ] Implementar notificaciones push

## 📞 Soporte

Para problemas o preguntas sobre el chatbot, revisa:
1. Los logs del backend (.NET)
2. Los logs de Python (stderr/stdout)
3. La consola del navegador (F12)

---

**Última actualización**: 30 de enero de 2026
**Versión**: 1.0.0
