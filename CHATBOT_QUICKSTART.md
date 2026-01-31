# Guía Rápida - Chatbot CoffeeBeanFlow

## ⚡ Inicio Rápido

### 1. Configurar Variables de Entorno

Crea un archivo `.env` en la raíz del proyecto:

```env
LangSmith_API_Key=tu_clave_langsmith
OpenAI_Key=tu_clave_openai
Qdrant_Key=tu_clave_qdrant
```

### 2. Instalar Dependencias Python

```bash
pip install -r requirements.txt
```

### 3. Iniciar el Backend

```bash
cd Backend
dotnet run
```

### 4. Iniciar el Frontend

En otra terminal:

```bash
cd Frontend
ng serve
```

### 5. Usar el Chatbot

1. Abre tu navegador en `http://localhost:4200`
2. Busca el botón flotante 💬 en la esquina inferior derecha
3. Haz clic para abrir el chat
4. ¡Empieza a conversar!

## 🎨 Vista Previa

El chatbot aparecerá como un botón flotante con:
- Icono de chat (💬)
- Color café (#6B4423)
- Animación al pasar el mouse
- Ventana de chat moderna al hacer clic

## ✨ Funcionalidades

- ✅ Respuestas en español
- ✅ Contexto sobre café de Costa Rica
- ✅ Memoria de conversación
- ✅ Interfaz responsive
- ✅ Animaciones suaves

## 📝 Ejemplo de Uso

**Tú**: "¿Cómo se seca el café?"

**Bot**: "El secado del café es un proceso crucial que puede realizarse de dos maneras principales: secado natural al sol o secado mecánico. En el secado natural, el café se extiende en patios o camas elevadas por aproximadamente 8-15 días, removiéndolo regularmente..."

## 🔧 Configuración Personalizada

### Cambiar URL del Backend

Edita `Frontend/src/app/core/services/chatbot.service.ts`:

```typescript
private apiUrl = 'http://TU_IP:5253/api/Chatbot';
```

### Cambiar Ruta de Python

Edita `Backend/Controllers/ChatbotController.cs`:

```csharp
var pythonPath = "/ruta/a/tu/python3";
```

## 🐛 Solución de Problemas

### El bot no responde
- Verifica que el backend esté corriendo
- Revisa la consola del navegador (F12)
- Asegúrate de que las API keys estén configuradas

### Error de Python
- Verifica la instalación: `python3 --version`
- Instala dependencias: `pip install -r requirements.txt`
- Revisa el archivo `.env`

## 📚 Documentación Completa

Para más detalles, consulta: [CHATBOT_INTEGRATION.md](./CHATBOT_INTEGRATION.md)

---

**¿Necesitas ayuda?** El chatbot está diseñado para responder preguntas sobre café en español. ¡Pruébalo!
