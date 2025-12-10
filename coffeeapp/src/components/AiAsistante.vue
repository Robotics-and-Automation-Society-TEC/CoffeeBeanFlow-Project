<template>
  <div>
    <!-- Botón flotante -->
    <button class="ai-button" @click="open = !open">
      🤖
    </button>

    <!-- Ventana flotante -->
    <div v-if="open" class="ai-window">
      <div class="ai-header">
        <h3>Asistente Inteligente</h3>
        <button class="close-btn" @click="open = false">✕</button>
      </div>

      <div class="ai-messages">
        <div
          v-for="(msg, index) in messages"
          :key="index"
          class="message"
          :class="msg.sender"
        >
          {{ msg.text }}
        </div>
      </div>

      <div class="ai-input">
        <input
          type="text"
          v-model="userText"
          placeholder="Escribe tu consulta..."
          @keyup.enter="sendMessage"
        />
        <button @click="sendMessage">➤</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "AiAsistante",

  data() {
    return {
      open: false,
      userText: "",
      messages: [
        { sender: "bot", text: "Hola 👋 ¿En qué puedo ayudarte hoy?" }
      ]
    };
  },

  methods: {
    sendMessage() {
      if (!this.userText.trim()) return;

      this.messages.push({ sender: "user", text: this.userText });

      // Respuesta generada provisional
      this.messages.push({
        sender: "bot",
        text: "Procesando tu consulta... (demo sin API)"
      });

      this.userText = "";
    }
  }
};
</script>

<style scoped>
.ai-button {
  position: fixed;
  bottom: 32px;
  right: 32px;
  background: #4d2c1e;
  color: white;
  font-size: 2rem;
  width: 70px;
  height: 70px;
  border-radius: 50%;
  border: none;
  cursor: pointer;
  box-shadow: 0 4px 12px rgba(0,0,0,0.3);
}

.ai-window {
  position: fixed;
  bottom: 120px;
  right: 32px;
  width: 330px;
  background: white;
  border-radius: 16px;
  box-shadow: 0 4px 20px rgba(0,0,0,0.3);
  overflow: hidden;
}

.ai-header {
  background: #4d2c1e;
  color: white;
  padding: 12px;
  display: flex;
  justify-content: space-between;
}

.ai-messages {
  padding: 12px;
  max-height: 340px;
  overflow-y: auto;
}

.message {
  padding: 8px 12px;
  margin-bottom: 10px;
  border-radius: 12px;
  max-width: 80%;
}

.message.user {
  background: #e7e7e7;
  margin-left: auto;
}

.message.bot {
  background: #4d2c1e;
  color: white;
  margin-right: auto;
}

.ai-input {
  display: flex;
  border-top: 1px solid #ddd;
}

.ai-input input {
  flex: 1;
  padding: 10px;
  border: none;
}

.ai-input button {
  background: #4d2c1e;
  color: white;
  padding: 0 16px;
  border: none;
}
</style>
