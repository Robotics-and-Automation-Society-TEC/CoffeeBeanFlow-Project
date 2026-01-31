# ✅ Lista de Verificación - Chatbot CoffeeBeanFlow

## Pre-requisitos

- [ ] Python 3.x instalado
- [ ] Node.js y npm instalados
- [ ] .NET SDK instalado
- [ ] Angular CLI instalado (`npm install -g @angular/cli`)

## Configuración Inicial

### 1. Variables de Entorno
- [ ] Crear archivo `.env` en la raíz del proyecto
- [ ] Agregar `LangSmith_API_Key=...`
- [ ] Agregar `OpenAI_Key=...`
- [ ] Agregar `Qdrant_Key=...`
- [ ] Verificar que el archivo `.env` esté en `.gitignore`

### 2. Dependencias Python
```bash
cd /home/noemi/Documentos/Repo-Cafe/CoffeeBeanFlow-Project
pip install -r requirements.txt
```

- [ ] langchain instalado
- [ ] langchain-openai instalado
- [ ] langchain-qdrant instalado
- [ ] langgraph instalado
- [ ] qdrant-client instalado
- [ ] python-dotenv instalado
- [ ] requests instalado

### 3. Verificar Configuración

#### Backend
- [ ] Abrir `Backend/Controllers/ChatbotController.cs`
- [ ] Verificar ruta de Python (línea ~126):
  ```csharp
  var pythonPath = "/usr/bin/python3";
  ```
- [ ] Ajustar si es necesario: `which python3` en terminal

#### Frontend
- [ ] Abrir `Frontend/src/app/core/services/chatbot.service.ts`
- [ ] Verificar URL del API (línea ~18):
  ```typescript
  private apiUrl = 'http://192.168.0.47:5253/api/Chatbot';
  ```
- [ ] Cambiar a `http://localhost:5253/api/Chatbot` si usas desarrollo local

## Pruebas del Sistema

### 1. Prueba del Agente Python (Standalone)
```bash
cd /home/noemi/Documentos/Repo-Cafe/CoffeeBeanFlow-Project
python3 coffee_agent_api.py
```

Esperar a ver:
- [ ] "Chatbot ready!"
- [ ] Prompt "Input query:"
- [ ] Escribir "Hola" y presionar Enter
- [ ] Recibir respuesta en español
- [ ] Escribir "exit" para salir

### 2. Prueba del Backend
```bash
cd Backend
dotnet run
```

Verificar:
- [ ] Backend inicia sin errores
- [ ] Ver mensaje: "Now listening on: http://localhost:5253"
- [ ] Swagger UI disponible en http://localhost:5253/swagger

#### Probar Endpoint con curl
```bash
curl -X POST http://localhost:5253/api/Chatbot/message \
  -H "Content-Type: application/json" \
  -d '{"message":"Hola"}'
```

- [ ] Recibe respuesta JSON
- [ ] Campo "success" es true
- [ ] Campo "response" contiene texto

### 3. Prueba del Frontend
```bash
cd Frontend
ng serve
```

Verificar:
- [ ] Frontend compila sin errores
- [ ] Ver mensaje: "Angular Live Development Server is listening..."
- [ ] Aplicación disponible en http://localhost:4200

### 4. Prueba de Integración Completa

Abrir navegador en `http://localhost:4200`:

#### Visual
- [ ] El botón flotante 💬 aparece en esquina inferior derecha
- [ ] El botón tiene color café
- [ ] El botón tiene efecto hover (escala y sombra)

#### Funcionalidad - Abrir Chat
- [ ] Hacer clic en el botón 💬
- [ ] La ventana de chat aparece con animación
- [ ] El header tiene color café
- [ ] Aparece mensaje de bienvenida
- [ ] El botón cambia a ✕

#### Funcionalidad - Enviar Mensaje
- [ ] Escribir "Hola" en el input
- [ ] Presionar Enter o hacer clic en 📤
- [ ] El mensaje del usuario aparece a la derecha
- [ ] Aparece indicador "escribiendo..." (3 puntos)
- [ ] El bot responde en español
- [ ] El mensaje del bot aparece a la izquierda
- [ ] Scroll automático al último mensaje
- [ ] Input se limpia después de enviar

#### Funcionalidad - Preguntas sobre Café
Probar estas preguntas:
- [ ] "¿Cómo se seca el café?"
- [ ] "¿Qué es la catación?"
- [ ] "Explícame el beneficiado húmedo"
- [ ] "¿Cuál es la mejor altura para cultivar café?"

Para cada pregunta verificar:
- [ ] El bot responde en español
- [ ] La respuesta es coherente
- [ ] El tiempo de respuesta es razonable (< 30s)

#### Funcionalidad - Limpiar Chat
- [ ] Hacer clic en el icono 🗑️
- [ ] El historial se limpia
- [ ] Solo queda el mensaje de bienvenida

#### Funcionalidad - Cerrar Chat
- [ ] Hacer clic en ✕
- [ ] La ventana se cierra con animación
- [ ] El botón vuelve a mostrar 💬
- [ ] Los mensajes se mantienen al reabrir

### 5. Pruebas de Error

#### Sin Backend
- [ ] Detener el backend (Ctrl+C)
- [ ] Intentar enviar mensaje en el chat
- [ ] Verificar que aparece mensaje de error
- [ ] Mensaje de error es descriptivo

#### Mensaje Vacío
- [ ] Intentar enviar mensaje vacío
- [ ] Verificar que el botón está deshabilitado
- [ ] No se envía nada

#### Mensaje Muy Largo
- [ ] Enviar un mensaje de más de 500 caracteres
- [ ] Verificar que se envía correctamente
- [ ] La respuesta se muestra completa

### 6. Pruebas Responsive

#### Desktop (> 480px)
- [ ] Ventana de chat tiene 380px de ancho
- [ ] Botón está en esquina inferior derecha

#### Mobile (< 480px)
- [ ] Redimensionar navegador a < 480px
- [ ] Ventana de chat ocupa casi todo el ancho
- [ ] Ventana de chat es usable
- [ ] Input es accesible

### 7. Pruebas de Navegación

- [ ] Navegar a /area-acopio
- [ ] Verificar que el botón sigue visible
- [ ] Navegar a /secado
- [ ] Verificar que el botón sigue visible
- [ ] El estado del chat se mantiene entre páginas

## Problemas Comunes y Soluciones

### El bot no responde
- [ ] Verificar que el backend está corriendo
- [ ] Verificar logs del backend para errores de Python
- [ ] Verificar que las API keys están en `.env`
- [ ] Verificar conexión a internet (para OpenAI/Qdrant)

### Error de CORS
- [ ] Verificar que Frontend está en http://localhost:4200
- [ ] Verificar configuración CORS en `Backend/Program.cs`
- [ ] Verificar que la URL en `chatbot.service.ts` es correcta

### Proceso Python no inicia
- [ ] Verificar ruta de Python: `which python3`
- [ ] Verificar permisos de `coffee_agent_api.py`
- [ ] Verificar que todas las dependencias están instaladas
- [ ] Revisar logs de error del backend

### Frontend no compila
- [ ] Ejecutar `npm install` en carpeta Frontend
- [ ] Verificar versión de Angular: `ng version`
- [ ] Limpiar caché: `ng cache clean`

## Checklist de Producción

Antes de desplegar a producción:

- [ ] Cambiar URLs hardcoded a variables de entorno
- [ ] Configurar HTTPS
- [ ] Implementar rate limiting en el backend
- [ ] Agregar autenticación si es necesario
- [ ] Configurar logging apropiado
- [ ] Implementar monitoreo del proceso Python
- [ ] Configurar auto-restart si el proceso falla
- [ ] Optimizar timeout de respuestas
- [ ] Implementar caché de respuestas comunes
- [ ] Conectar a base de datos real (PostgreSQL)

## 📊 Resultados Esperados

Si todas las pruebas pasan:
- ✅ Chatbot completamente funcional
- ✅ Respuestas en español
- ✅ Interfaz responsive y atractiva
- ✅ Integración completa Frontend-Backend-Python
- ✅ Manejo de errores apropiado

## 🎉 ¡Listo!

Si todos los checkboxes están marcados, el chatbot está completamente
integrado y funcionando. ¡Felicidades! 🎊

---

**Última actualización:** 30 de enero de 2026
