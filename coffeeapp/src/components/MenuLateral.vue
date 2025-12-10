<template>
  <div>
    <!-- BOTÓN HAMBURGUESA -->
    <button class="menu-btn" @click="toggleMenu">
      ☰
    </button>

    <!-- OVERLAY -->
    <div v-if="isOpen" class="overlay" @click="toggleMenu"></div>

    <!-- SIDEBAR A LA DERECHA -->
    <aside class="sidebar" :class="{ open: isOpen }">
      <div class="sidebar-header">
        <h2>Menú</h2>
        <button class="close-btn" @click="toggleMenu">✕</button>
      </div>

      <ul class="menu-list">
        <li @click="goTo('/')">
          <i>🏠</i> Inicio
        </li>

        <li @click="goTo('/formulario-nuevo')">
          <i>🏪</i> Área de Acopio
        </li>

        <li @click="goTo('/caracterizacion')">
          <i>🔬</i> Caracterización
        </li>

        <li @click="goTo('/secado')">
          <i>🌡️</i> Secado
        </li>

        <li @click="goTo('/bodega')">
          <i>📦</i> Bodega
        </li>

        <li @click="goTo('/trilla')">
          <i>⚙️</i> Trilla
        </li>

        <li @click="goTo('/catacion')">
          <i>☕</i> Catación
        </li>

        <li @click="goTo('/historial')">
          <i>📊</i> Historial General
        </li>
      </ul>
    </aside>
  </div>
</template>

<script>
export default {
  name: "MenuLateral",

  data() {
    return { isOpen: false };
  },

  methods: {
    toggleMenu() {
      this.isOpen = !this.isOpen;
    },

    goTo(path) {
      this.$router.push(path);
      this.isOpen = false;
    }
  }
};
</script>

<style scoped>
/* Botón hamburguesa a la DERECHA */
.menu-btn {
  position: fixed;
  top: 20px;
  right: 20px; /* ← MOVIDO A LA DERECHA */
  background: var(--gradient-header);
  color: white;
  border: none;
  padding: 12px 16px;
  border-radius: 12px;
  font-size: 20px;
  cursor: pointer;
  z-index: 3000;
  box-shadow: var(--shadow-lg);
}

/* Overlay */
.overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.4);
  z-index: 2500;
}

/* Sidebar AHORA A LA DERECHA */
.sidebar {
  position: fixed;
  top: 0;
  right: 0; /* ← MOVIDO A LA DERECHA */
  width: 270px;
  height: 100%;
  background: var(--gradient-card);
  padding: 20px;

  /* Animación inicial: está oculto hacia la derecha */
  transform: translateX(100%);
  transition: 0.3s ease-in-out;

  z-index: 2600;
  box-shadow: var(--shadow-xl);
}

.sidebar.open {
  /* Cuando está abierto, entra desde la derecha */
  transform: translateX(0);
}

.sidebar-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.sidebar-header h2 {
  color: var(--text-primary);
  margin: 0;
}

.close-btn {
  background: none;
  border: none;
  color: var(--text-primary);
  font-size: 22px;
  cursor: pointer;
}

.menu-list {
  margin-top: 25px;
  list-style: none;
  padding: 0;
}

.menu-list li {
  display: flex;
  align-items: center;
  gap: 10px;
  padding: 14px 10px;
  font-size: 18px;
  cursor: pointer;
  border-radius: var(--radius-xl);
  color: var(--text-secondary);
  transition: 0.2s;
}

.menu-list li i {
  font-size: 22px;
}

.menu-list li:hover {
  background: var(--gradient-primary);
  color: white;
  transform: translateX(6px);
}
</style>
