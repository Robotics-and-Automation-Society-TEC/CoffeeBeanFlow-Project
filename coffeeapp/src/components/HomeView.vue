<template>
  <div class="app-container">

    <MenuLateral />

    <!-- Header elegante con tema de café -->
    <header class="header">
      <div class="header-content">
        <div class="title-section">
          <div class="coffee-logo">
            <div class="coffee-bean"></div>
            <div class="coffee-bean"></div>
          </div>
          <div class="title-info">
            <h1>Proyecto Café</h1>
            <p class="subtitle">Sistema de Gestión de Calidad</p>
          </div>
        </div>
      </div>
    </header>

    <!-- Sección principal de tarjetas -->
    <main class="main-content">
      <div class="welcome-section text-center">
        <h2>Módulos de Gestión</h2>
        <p>Seleccione el proceso que desea gestionar</p>
      </div>

      <!-- Grid de tarjetas -->
      <div class="process-grid">
        <button
          class="process-card-large"
          v-for="item in sections"
          :key="item.title"
          @click="handleClick(item)"
          :class="item.className"
        >
          <div class="card-icon-section">
            <div class="large-icon-container">
              <i class="process-icon">{{ item.icon }}</i>
            </div>
            <div class="card-badge">{{ item.badge }}</div>
          </div>

          <div class="card-content">
            <h3>{{ item.title }}</h3>
            <p class="card-description">{{ item.description }}</p>
            <div class="card-stats">
              <span class="stat-item">
                <i class="stat-icon">📊</i>
                {{ item.recordCount || 0 }} registros
              </span>
            </div>
          </div>

          <div class="card-action">
            <span class="action-button">
              <i class="action-icon">▶</i>
              Gestionar
            </span>
          </div>
        </button>
      </div>
    </main>

    <!-- Modal -->
    <transition name="fade">
      <div v-if="modalVisible" class="modal-overlay" @click="cerrarModal">
        <div class="modal-content" @click.stop>
          <div class="modal-header text-center">
            <div class="modal-icon-container">
              <i class="modal-icon">{{ selectedSection?.icon }}</i>
            </div>
            <h3>{{ selectedSection?.title }}</h3>
            <p class="text-muted">¿Qué acción desea realizar?</p>
          </div>

          <div class="modal-actions">
            <button @click="nuevoRegistro" class="btn-base btn-primary">
              <i class="btn-icon">📝</i>
              Nuevo Registro
            </button>

            <button @click="verRegistroViejo" class="btn-base btn-secondary">
              <i class="btn-icon">📊</i>
              Ver Historial
            </button>
          </div>

          <div class="modal-footer text-center">
            <button @click="cerrarModal" class="btn-base btn-cancel">
              <i class="btn-icon">✕</i>
              Cancelar
            </button>
          </div>
        </div>
      </div>
    </transition>

    <!-- ⚡ ASISTENTE FLOTANTE DE IA -->
    <AiAsistante />
  </div>
</template>

<script>
import MenuLateral from "@/components/MenuLateral.vue";
import AiAsistante from "@/components/AiAsistante.vue";

export default {
  name: "HomeView",

  components: {
    MenuLateral,
    AiAsistante
  },

  data() {
    return {
      sections: [
        { title: "Área de Acopio", icon: "🏪", badge: "Recepción", description: "Registro y control de entrada de café cereza.", className: "acopio-card" },
        { title: "Caracterización", icon: "🔬", badge: "Análisis", description: "Análisis y características físicas del café.", className: "caracterizacion-card" },
        { title: "Secado", icon: "🌡️", badge: "Proceso", description: "Control de temperatura y humedad.", className: "secado-card" },
        { title: "Bodega", icon: "📦", badge: "Almacén", description: "Gestión de inventarios.", className: "bodega-card" },
        { title: "Trilla", icon: "⚙️", badge: "Proceso", description: "Proceso de trillado.", className: "trilla-card" },
        { title: "Catación", icon: "☕", badge: "Calidad", description: "Evaluación sensorial del café.", className: "catacion-card" },
      ],
      modalVisible: false,
      selectedSection: null,
    };
  },

  methods: {
    handleClick(section) {
      this.selectedSection = section;
      this.modalVisible = true;
    },

    nuevoRegistro() {
      this.modalVisible = false;
      const routes = {
        "Área de Acopio": "/formulario-nuevo",
        "Caracterización": "/caracterizacion",
        "Secado": "/secado",
        "Bodega": "/bodega",
        "Trilla": "/trilla",
        "Catación": "/catacion"
      };
      const route = routes[this.selectedSection.title];
      if (route) this.$router.push(route);
      else alert("Módulo en desarrollo");
    },

    verRegistroViejo() {
      this.modalVisible = false;
      this.$router.push({ name: "HistorialGeneral", query: { seccion: this.selectedSection.title } });
    },

    cerrarModal() {
      this.modalVisible = false;
      this.selectedSection = null;
    }
  }
};
</script>

<style scoped>
  
/* ===== USANDO CONSTANTES CSS GLOBALES ===== */

/* Container principal con fondo verde requerido */
.app-container {
  min-height: 100vh;
  background: linear-gradient(135deg, var(--verde-claro) 1%, var(--verde-oscuro) 50%, var(--verde-muy-oscuro) 99%);
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
}

/* Header con constantes */
.header {
  background: var(--gradient-header);
  color: var(--text-white);
  box-shadow: var(--shadow-xl);
  position: relative;
  overflow: hidden;
}

.header::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: url('data:image/svg+xml,<svg xmlns="http://www.w3.org/2000/svg" viewBox="0 0 100 100"><circle cx="20" cy="20" r="2" fill="rgba(229,194,159,0.1)"/><circle cx="80" cy="40" r="1.5" fill="rgba(200,149,111,0.1)"/><circle cx="60" cy="80" r="2.5" fill="rgba(229,194,159,0.1)"/></svg>') repeat;
  pointer-events: none;
}

.header-content {
  display: flex;
  justify-content: space-between;
  align-items: center;
  padding: var(--space-2xl) var(--space-3xl);
  max-width: 1400px;
  margin: 0 auto;
  position: relative;
  z-index: 1;
}

.title-section {
  display: flex;
  align-items: center;
  gap: var(--space-lg);
}

.coffee-logo {
  display: flex;
  gap: var(--space-xs);
  flex-direction: column;
}

.title-info h1 {
  font-size: var(--text-4xl);
  font-weight: bold;
  margin: 0;
  text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
  letter-spacing: -0.5px;
}

.subtitle {
  font-size: var(--text-sm);
  color: var(--cafe-claro);
  margin: 0;
  font-style: italic;
}

.search-container {
  position: relative;
  display: flex;
  align-items: center;
}

.search-icon {
  position: absolute;
  left: var(--space-md);
  font-size: var(--text-lg);
  color: var(--cafe-oscuro);
  z-index: 1;
}

.search-input {
  padding-left: var(--space-4xl);
  width: 320px;
  background: rgba(255,255,255,0.95);
  border: 2px solid transparent;
  backdrop-filter: blur(10px);
}

.search-input:focus {
  background: white;
  border-color: var(--cafe-medio);
}

/* Contenido principal */
.main-content {
  max-width: 1400px;
  margin: 0 auto;
  padding: var(--space-4xl) var(--space-3xl);
}

.welcome-section {
  margin-bottom: var(--space-4xl);
}

.welcome-section h2 {
  font-size: var(--text-3xl);
  color: var(--text-white);
  margin-bottom: var(--space-sm);
  font-weight: bold;
  text-shadow: 2px 2px 4px rgba(0,0,0,0.3);
}

.welcome-section p {
  color: var(--beige-claro);
  font-size: var(--text-lg);
  margin: 0;
}

/* Grid de procesos con tarjetas más grandes */
.process-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(380px, 1fr));
  gap: var(--space-3xl);
  margin-top: var(--space-4xl);
}

.process-card-large {
  background: var(--gradient-card);
  border: 3px solid transparent;
  border-radius: var(--radius-3xl);
  padding: var(--space-3xl);
  cursor: pointer;
  transition: var(--transition-all);
  box-shadow: var(--shadow-lg);
  position: relative;
  overflow: hidden;
  text-align: left;
  min-height: 280px;
  display: flex;
  flex-direction: column;
  justify-content: space-between;
}

.process-card-large::before {
  content: '';
  position: absolute;
  top: 0;
  left: 0;
  right: 0;
  height: 6px;
  background: var(--gradient-coffee);
  opacity: 0;
  transition: var(--transition-normal);
}

.process-card-large:hover::before {
  opacity: 1;
}

.process-card-large:hover {
  transform: translateY(-8px) scale(1.03);
  border-color: var(--cafe-medio);
  box-shadow: var(--shadow-2xl);
}

/* Sección del icono más grande */
.card-icon-section {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: var(--space-2xl);
}

.large-icon-container {
  width: 80px;
  height: 80px;
  border-radius: var(--radius-3xl);
  background: var(--gradient-primary);
  display: flex;
  align-items: center;
  justify-content: center;
  box-shadow: var(--shadow-success);
}

.process-icon {
  font-size: 3rem;
  filter: drop-shadow(0 4px 8px rgba(0,0,0,0.1));
}

.card-badge {
  background: var(--gradient-secondary);
  color: white;
  padding: var(--space-sm) var(--space-lg);
  border-radius: var(--radius-xl);
  font-size: var(--text-xs);
  font-weight: bold;
  text-transform: uppercase;
  letter-spacing: 0.5px;
}

/* Contenido de la tarjeta */
.card-content {
  flex: 1;
  margin-bottom: var(--space-2xl);
}

.card-content h3 {
  color: var(--text-primary);
  font-size: var(--text-2xl);
  font-weight: bold;
  margin-bottom: var(--space-md);
  line-height: 1.2;
}

.card-description {
  color: var(--text-secondary);
  font-size: var(--text-base);
  line-height: 1.5;
  margin-bottom: var(--space-lg);
}

.card-stats {
  display: flex;
  align-items: center;
  gap: var(--space-lg);
}

.stat-item {
  display: flex;
  align-items: center;
  gap: var(--space-sm);
  color: var(--text-muted);
  font-size: var(--text-sm);
  font-weight: 500;
}

.stat-icon {
  font-size: var(--text-base);
}

/* Acción de la tarjeta */
.card-action {
  display: flex;
  justify-content: flex-end;
}

.action-button {
  display: flex;
  align-items: center;
  gap: var(--space-sm);
  color: var(--burgundy);
  font-weight: bold;
  font-size: var(--text-lg);
  transition: var(--transition-normal);
}

.process-card-large:hover .action-button {
  transform: translateX(var(--space-sm));
  color: var(--burgundy-dark);
}

.action-icon {
  font-size: var(--text-base);
  transition: var(--transition-normal);
}

.process-card-large:hover .action-icon {
  transform: scale(1.2);
}

/* Estilos específicos por tipo de proceso */
.acopio-card .large-icon-container { 
  background: linear-gradient(135deg, var(--verde-claro), var(--verde-oscuro)); 
}
.caracterizacion-card .large-icon-container { 
  background: var(--gradient-secondary); 
}
.secado-card .large-icon-container { 
  background: linear-gradient(135deg, var(--cafe-medio), var(--cafe-oscuro)); 
}
.bodega-card .large-icon-container { 
  background: linear-gradient(135deg, var(--cafe-muy-oscuro), var(--negro-cafe)); 
}
.trilla-card .large-icon-container { 
  background: linear-gradient(135deg, var(--cafe-claro), var(--cafe-medio)); 
}
.catacion-card .large-icon-container { 
  background: linear-gradient(135deg, var(--cafe-oscuro), var(--cafe-muy-oscuro)); 
}

/* Modal usando constantes */
.modal-header {
  margin-bottom: var(--space-2xl);
}

.modal-icon-container {
  width: 80px;
  height: 80px;
  border-radius: var(--radius-3xl);
  background: var(--gradient-primary);
  display: flex;
  align-items: center;
  justify-content: center;
  margin: 0 auto var(--space-lg);
  font-size: 2.5rem;
  box-shadow: var(--shadow-success);
}

.modal-header h3 {
  color: var(--text-primary);
  font-size: var(--text-2xl);
  font-weight: bold;
  margin-bottom: var(--space-sm);
}

.modal-actions {
  display: flex;
  flex-direction: column;
  gap: var(--space-md);
  margin-bottom: var(--space-2xl);
}

.modal-footer {
  padding-top: var(--space-lg);
  border-top: 2px solid var(--cafe-claro);
}

/* Responsive usando constantes */
@media (max-width: 768px) {
  .header-content {
    flex-direction: column;
    gap: var(--space-xl);
    padding: var(--space-xl);
  }

  .title-info h1 {
    font-size: var(--text-3xl);
  }

  .search-input {
    width: 100%;
    max-width: 320px;
  }

  .main-content {
    padding: var(--space-2xl) var(--space-lg);
  }

  .process-grid {
    grid-template-columns: 1fr;
    gap: var(--space-xl);
  }

  .welcome-section h2 {
    font-size: var(--text-2xl);
  }
}

@media (max-width: 480px) {
  .process-card-large {
    padding: var(--space-xl);
    min-height: 240px;
  }

  .large-icon-container {
    width: 64px;
    height: 64px;
  }

  .process-icon {
    font-size: 2.2rem;
  }

  .card-content h3 {
    font-size: var(--text-xl);
  }

  .modal-content {
    margin: var(--space-lg);
  }
}
</style>