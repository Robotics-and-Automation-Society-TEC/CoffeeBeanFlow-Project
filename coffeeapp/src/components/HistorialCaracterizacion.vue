<template>
  <div class="historial-container">
    <!-- Header -->
    <header class="header">
      <div class="header-content">
        <button @click="volverAtras" class="btn-back">
          <i class="icon">←</i>
          Volver
        </button>
        <div class="title-section">
          <div class="icon-container">
            <i class="section-icon">🔬</i>
          </div>
          <div>
            <h1>Historial - Caracterización</h1>
            <p class="subtitle">Análisis refractométrico y control de calidad</p>
          </div>
        </div>
      </div>
    </header>

    <!-- Barra de búsqueda -->
    <div class="search-section">
      <div class="search-container">
        <i class="search-icon">🔍</i>
        <input 
          v-model="busqueda" 
          type="text" 
          placeholder="Buscar por lote, rango de °Brix, escala de maduración..."
          class="search-input"
          @input="filtrarRegistros"
        />
        <button v-if="busqueda" @click="limpiarBusqueda" class="clear-btn">✕</button>
      </div>
      
      <div class="filter-chips">
        <button 
          v-for="filtro in filtrosRapidos" 
          :key="filtro.id"
          @click="aplicarFiltroRapido(filtro)"
          :class="['chip', { active: filtroActivo === filtro.id }]"
        >
          {{ filtro.label }}
        </button>
      </div>
    </div>

    <!-- Resultados -->
    <main class="main-content">
      <!-- Estadísticas rápidas -->
      <div class="stats-grid">
        <div class="stat-card">
          <div class="stat-icon">📊</div>
          <div class="stat-info">
            <div class="stat-value">{{ registrosFiltrados.length }}</div>
            <div class="stat-label">Análisis realizados</div>
          </div>
        </div>
        <div class="stat-card">
          <div class="stat-icon">🍒</div>
          <div class="stat-info">
            <div class="stat-value">{{ promedioCalidad }}%</div>
            <div class="stat-label">Calidad promedio</div>
          </div>
        </div>
        <div class="stat-card">
          <div class="stat-icon">📈</div>
          <div class="stat-info">
            <div class="stat-value">{{ promedioBrix }}°</div>
            <div class="stat-label">°Brix promedio</div>
          </div>
        </div>
        <div class="stat-card">
          <div class="stat-icon">📅</div>
          <div class="stat-info">
            <div class="stat-value">{{ registrosHoy }}</div>
            <div class="stat-label">Análisis hoy</div>
          </div>
        </div>
      </div>

      <!-- Vista de registros -->
      <div class="registros-grid">
        <div 
          v-for="registro in registrosFiltrados" 
          :key="registro.id"
          class="registro-card"
        >
          <div class="registro-header">
            <div class="registro-title">
              <span class="lote-badge">{{ registro.lote }}</span>
              <span class="fecha">{{ formatearFecha(registro.fecha) }}</span>
            </div>
            <div class="registro-calidad" :class="getCalidadClass(registro.calidadGeneral)">
              {{ registro.calidadGeneral }}
            </div>
          </div>

          <div class="registro-body">
            <!-- Análisis principal -->
            <div class="analisis-section">
              <h4 class="subsection-title">
                <i class="subsection-icon">🔬</i>
                Análisis Refractométrico
              </h4>
              <div class="info-grid">
                <div class="info-item">
                  <span class="label">Rango Óptimo:</span>
                  <span class="value highlight">{{ registro.rangoOptimo }}°Brix</span>
                </div>
                <div class="info-item">
                  <span class="label">Escala Maduración:</span>
                  <span class="value">{{ registro.escalaMaduracion }}/10</span>
                </div>
                <div class="info-item">
                  <span class="label">Densidad:</span>
                  <span class="value">{{ registro.densidad }} g/ml</span>
                </div>
              </div>
            </div>

            <!-- Distribución de cerezas -->
            <div class="analisis-section">
              <h4 class="subsection-title">
                <i class="subsection-icon">🍒</i>
                Distribución de Cerezas
              </h4>
              <div class="progress-bars">
                <div class="progress-item">
                  <div class="progress-header">
                    <span class="progress-label">Objetivo (Óptimas)</span>
                    <span class="progress-value">{{ registro.porcObjetivo }}%</span>
                  </div>
                  <div class="progress-bar-container">
                    <div class="progress-bar objetivo" :style="{ width: registro.porcObjetivo + '%' }"></div>
                  </div>
                </div>
                
                <div class="progress-item">
                  <div class="progress-header">
                    <span class="progress-label">Verdes</span>
                    <span class="progress-value">{{ registro.porcVerdes }}%</span>
                  </div>
                  <div class="progress-bar-container">
                    <div class="progress-bar verdes" :style="{ width: registro.porcVerdes + '%' }"></div>
                  </div>
                </div>
                
                <div class="progress-item">
                  <div class="progress-header">
                    <span class="progress-label">Secas</span>
                    <span class="progress-value">{{ registro.porcSecas }}%</span>
                  </div>
                  <div class="progress-bar-container">
                    <div class="progress-bar secas" :style="{ width: registro.porcSecas + '%' }"></div>
                  </div>
                </div>
              </div>
            </div>

            <!-- Defectos -->
            <div class="analisis-section defectos">
              <h4 class="subsection-title">
                <i class="subsection-icon">⚠️</i>
                Control de Defectos
              </h4>
              <div class="defectos-grid">
                <div class="defecto-item">
                  <span class="defecto-icon">🐛</span>
                  <span class="defecto-label">Broca</span>
                  <span class="defecto-value">{{ registro.broca }}%</span>
                </div>
                <div class="defecto-item">
                  <span class="defecto-icon">⚪</span>
                  <span class="defecto-label">Vanos</span>
                  <span class="defecto-value">{{ registro.vanos }}%</span>
                </div>
                <div class="defecto-item">
                  <span class="defecto-icon">🌵</span>
                  <span class="defecto-label">Secos</span>
                  <span class="defecto-value">{{ registro.secos }}%</span>
                </div>
              </div>
            </div>
          </div>

          <div class="registro-footer">
            <button @click="verDetalle(registro)" class="btn-action btn-detail">
              <i class="icon">👁️</i>
              Ver detalle
            </button>
            <button @click="imprimirRegistro(registro)" class="btn-action btn-print">
              <i class="icon">🖨️</i>
              Imprimir
            </button>
          </div>
        </div>

        <!-- Estado vacío -->
        <div v-if="registrosFiltrados.length === 0" class="empty-state">
          <div class="empty-icon">📭</div>
          <h3>No se encontraron registros</h3>
          <p>Intenta ajustar los filtros de búsqueda</p>
        </div>
      </div>
    </main>

    <!-- Modal de detalle -->
    <transition name="fade">
      <div v-if="modalDetalle" class="modal-overlay" @click="cerrarModal">
        <div class="modal-content modal-large" @click.stop>
          <div class="modal-header">
            <h3>Detalle del Análisis - {{ registroSeleccionado?.lote }}</h3>
            <button @click="cerrarModal" class="btn-close">✕</button>
          </div>
          
          <div class="modal-body" v-if="registroSeleccionado">
            <!-- Información general -->
            <div class="detail-section">
              <h4>Información General</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">Número de Lote:</span>
                  <span class="detail-value">{{ registroSeleccionado.lote }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Fecha de Análisis:</span>
                  <span class="detail-value">{{ formatearFecha(registroSeleccionado.fecha) }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Calidad General:</span>
                  <span class="detail-value">{{ registroSeleccionado.calidadGeneral }}</span>
                </div>
              </div>
            </div>

            <!-- Mediciones refractométricas -->
            <div class="detail-section">
              <h4>Mediciones Refractométricas</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">Rango Óptimo de Maduras:</span>
                  <span class="detail-value highlight-value">{{ registroSeleccionado.rangoOptimo }}°Brix</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Escala de Maduración:</span>
                  <span class="detail-value">{{ registroSeleccionado.escalaMaduracion }}/10</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Densidad del Grano:</span>
                  <span class="detail-value">{{ registroSeleccionado.densidad }} g/ml</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Muestreo Tabla:</span>
                  <span class="detail-value">{{ registroSeleccionado.muestreoTabla }}</span>
                </div>
              </div>
            </div>

            <!-- Cantidades de cerezas -->
            <div class="detail-section">
              <h4>Cantidades de Cerezas</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">Inmaduras:</span>
                  <span class="detail-value">{{ registroSeleccionado.inmaduras }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Sobremaduras:</span>
                  <span class="detail-value">{{ registroSeleccionado.sobremaduras }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Verdes:</span>
                  <span class="detail-value">{{ registroSeleccionado.verdes }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Objetivo (Óptimas):</span>
                  <span class="detail-value">{{ registroSeleccionado.objetivo }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Secas:</span>
                  <span class="detail-value">{{ registroSeleccionado.secas }}</span>
                </div>
              </div>
            </div>

            <!-- Porcentajes -->
            <div class="detail-section">
              <h4>Análisis Porcentual</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">% Objetivo:</span>
                  <span class="detail-value success-value">{{ registroSeleccionado.porcObjetivo }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Verdes:</span>
                  <span class="detail-value">{{ registroSeleccionado.porcVerdes }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Secas:</span>
                  <span class="detail-value">{{ registroSeleccionado.porcSecas }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Debajo Objetivo:</span>
                  <span class="detail-value">{{ registroSeleccionado.porcDebajoObjetivo }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Encima Objetivo:</span>
                  <span class="detail-value">{{ registroSeleccionado.porcEncimaObjetivo }}%</span>
                </div>
              </div>
            </div>

            <!-- Defectos -->
            <div class="detail-section">
              <h4>Control de Defectos</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">% Broca:</span>
                  <span class="detail-value warning-value">{{ registroSeleccionado.broca }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Vanos:</span>
                  <span class="detail-value">{{ registroSeleccionado.vanos }}%</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">% Secos:</span>
                  <span class="detail-value">{{ registroSeleccionado.secos }}%</span>
                </div>
              </div>
            </div>
          </div>

          <div class="modal-footer">
            <button @click="imprimirRegistro(registroSeleccionado)" class="btn-base btn-primary">
              <i class="icon">🖨️</i>
              Imprimir
            </button>
            <button @click="cerrarModal" class="btn-base btn-secondary">
              Cerrar
            </button>
          </div>
        </div>
      </div>
    </transition>
  </div>
</template>

<script>
export default {
  name: "HistorialCaracterizacion",
  data() {
    return {
      busqueda: '',
      filtroActivo: null,
      modalDetalle: false,
      registroSeleccionado: null,
      
      filtrosRapidos: [
        { id: 'hoy', label: 'Hoy' },
        { id: 'semana', label: 'Esta semana' },
        { id: 'mes', label: 'Este mes' },
        { id: 'premium', label: 'Calidad Premium' },
        { id: 'todos', label: 'Todos' }
      ],
      
      // Datos de ejemplo
      registros: [
        {
          id: 1,
          lote: 'LOT-2024-001',
          fecha: '2024-12-03',
          calidadGeneral: 'Premium',
          rangoOptimo: 22.5,
          escalaMaduracion: 8.5,
          densidad: 1.25,
          muestreoTabla: 100,
          inmaduras: 5,
          sobremaduras: 8,
          verdes: 3,
          objetivo: 78,
          secas: 6,
          porcVerdes: 3,
          porcSecas: 6,
          porcObjetivo: 78,
          porcDebajoObjetivo: 5,
          porcEncimaObjetivo: 8,
          broca: 2.5,
          vanos: 1.2,
          secos: 3.8
        },
        {
          id: 2,
          lote: 'LOT-2024-002',
          fecha: '2024-12-02',
          calidadGeneral: 'Estándar',
          rangoOptimo: 20.0,
          escalaMaduracion: 7.0,
          densidad: 1.20,
          muestreoTabla: 100,
          inmaduras: 12,
          sobremaduras: 15,
          verdes: 8,
          objetivo: 55,
          secas: 10,
          porcVerdes: 8,
          porcSecas: 10,
          porcObjetivo: 55,
          porcDebajoObjetivo: 12,
          porcEncimaObjetivo: 15,
          broca: 4.2,
          vanos: 2.8,
          secos: 5.5
        },
        {
          id: 3,
          lote: 'LOT-2024-003',
          fecha: '2024-12-01',
          calidadGeneral: 'Premium',
          rangoOptimo: 23.0,
          escalaMaduracion: 9.0,
          densidad: 1.28,
          muestreoTabla: 100,
          inmaduras: 2,
          sobremaduras: 5,
          verdes: 2,
          objetivo: 85,
          secas: 6,
          porcVerdes: 2,
          porcSecas: 6,
          porcObjetivo: 85,
          porcDebajoObjetivo: 2,
          porcEncimaObjetivo: 5,
          broca: 1.5,
          vanos: 0.8,
          secos: 2.5
        }
      ],

      registrosFiltrados: []
    };
  },
  
  computed: {
    promedioCalidad() {
      if (this.registrosFiltrados.length === 0) return 0;
      const suma = this.registrosFiltrados.reduce((acc, r) => acc + r.porcObjetivo, 0);
      return (suma / this.registrosFiltrados.length).toFixed(1);
    },
    
    promedioBrix() {
      if (this.registrosFiltrados.length === 0) return 0;
      const suma = this.registrosFiltrados.reduce((acc, r) => acc + r.rangoOptimo, 0);
      return (suma / this.registrosFiltrados.length).toFixed(1);
    },
    
    registrosHoy() {
      const hoy = new Date().toISOString().split('T')[0];
      return this.registrosFiltrados.filter(r => r.fecha === hoy).length;
    }
  },
  
  methods: {
    volverAtras() {
      this.$router.go(-1);
    },
    
    filtrarRegistros() {
      if (!this.busqueda) {
        this.registrosFiltrados = [...this.registros];
        return;
      }
      
      const busquedaLower = this.busqueda.toLowerCase();
      this.registrosFiltrados = this.registros.filter(registro => 
        registro.lote.toLowerCase().includes(busquedaLower) ||
        registro.calidadGeneral.toLowerCase().includes(busquedaLower) ||
        registro.rangoOptimo.toString().includes(busquedaLower) ||
        registro.escalaMaduracion.toString().includes(busquedaLower)
      );
    },
    
    limpiarBusqueda() {
      this.busqueda = '';
      this.filtrarRegistros();
    },
    
    aplicarFiltroRapido(filtro) {
      this.filtroActivo = filtro.id;
      const hoy = new Date();
      
      switch(filtro.id) {
        case 'hoy': {
          const hoyStr = hoy.toISOString().split('T')[0];
          this.registrosFiltrados = this.registros.filter(r => r.fecha === hoyStr);
          break;
        }
        case 'semana': {
          const semanaAtras = new Date(hoy.getTime() - 7 * 24 * 60 * 60 * 1000);
          this.registrosFiltrados = this.registros.filter(r => 
            new Date(r.fecha) >= semanaAtras
          );
          break;
        }
        case 'mes': {
          const mesAtras = new Date(hoy.getTime() - 30 * 24 * 60 * 60 * 1000);
          this.registrosFiltrados = this.registros.filter(r => 
            new Date(r.fecha) >= mesAtras
          );
          break;
        }
        case 'premium':
          this.registrosFiltrados = this.registros.filter(r => 
            r.calidadGeneral === 'Premium'
          );
          break;
        case 'todos':
          this.registrosFiltrados = [...this.registros];
          break;
        default:
          break;
      }
    },
    
    formatearFecha(fecha) {
      const date = new Date(fecha);
      const opciones = { year: 'numeric', month: 'long', day: 'numeric' };
      return date.toLocaleDateString('es-ES', opciones);
    },
    
    getCalidadClass(calidad) {
      return calidad.toLowerCase().replace(/\s+/g, '-');
    },
    
    verDetalle(registro) {
      this.registroSeleccionado = registro;
      this.modalDetalle = true;
    },
    
    cerrarModal() {
      this.modalDetalle = false;
      this.registroSeleccionado = null;
    },
    
    imprimirRegistro(registro) {
      alert(`Imprimiendo análisis del lote ${registro.lote}...`);
    }
  },
  
  mounted() {
    this.registrosFiltrados = [...this.registros];
  }
};
</script>

<style scoped>
.historial-container {
  min-height: 100vh;
  background: linear-gradient(135deg, #8FBC8F 1%, #556B2F 50%, #3d4f1f 99%);
}

.header {
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  color: white;
  box-shadow: 0 4px 20px rgba(0,0,0,0.3);
  position: sticky;
  top: 0;
  z-index: 100;
}

.header-content {
  display: flex;
  align-items: center;
  gap: 2rem;
  padding: 1.5rem 2rem;
  max-width: 1400px;
  margin: 0 auto;
}

.btn-back {
  background: rgba(255,255,255,0.2);
  color: white;
  border: 2px solid rgba(255,255,255,0.3);
  padding: 0.75rem 1.5rem;
  border-radius: 12px;
  font-weight: bold;
  cursor: pointer;
  transition: all 0.3s;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.btn-back:hover {
  background: rgba(255,255,255,0.3);
  transform: translateX(-4px);
}

.title-section {
  display: flex;
  align-items: center;
  gap: 1rem;
  flex: 1;
}

.icon-container {
  width: 60px;
  height: 60px;
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  border-radius: 16px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 2rem;
  box-shadow: 0 4px 12px rgba(0,0,0,0.2);
}

.title-section h1 {
  margin: 0;
  font-size: 2rem;
  font-weight: bold;
}

.subtitle {
  margin: 0;
  opacity: 0.9;
  font-size: 0.95rem;
}

.search-section {
  background: white;
  padding: 2rem;
  margin: 2rem auto;
  max-width: 1400px;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.search-container {
  position: relative;
  margin-bottom: 1.5rem;
}

.search-icon {
  position: absolute;
  left: 1.25rem;
  top: 50%;
  transform: translateY(-50%);
  font-size: 1.25rem;
  z-index: 1;
}

.search-input {
  width: 100%;
  padding: 1rem 3.5rem 1rem 3.5rem;
  border: 2px solid #e0e0e0;
  border-radius: 12px;
  font-size: 1rem;
  transition: all 0.3s;
}

.search-input:focus {
  border-color: #C8956F;
  outline: none;
  box-shadow: 0 0 0 3px rgba(200,149,111,0.1);
}

.clear-btn {
  position: absolute;
  right: 1rem;
  top: 50%;
  transform: translateY(-50%);
  background: #f0f0f0;
  border: none;
  width: 32px;
  height: 32px;
  border-radius: 50%;
  cursor: pointer;
  display: flex;
  align-items: center;
  justify-content: center;
  transition: all 0.3s;
}

.clear-btn:hover {
  background: #e0e0e0;
}

.filter-chips {
  display: flex;
  gap: 1rem;
  flex-wrap: wrap;
}

.chip {
  padding: 0.5rem 1.25rem;
  border: 2px solid #e0e0e0;
  background: white;
  border-radius: 24px;
  cursor: pointer;
  transition: all 0.3s;
  font-size: 0.9rem;
  font-weight: 500;
}

.chip:hover {
  border-color: #C8956F;
  background: #f8f8f8;
}

.chip.active {
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  color: white;
  border-color: #8B5A3C;
}

.main-content {
  max-width: 1400px;
  margin: 0 auto 3rem;
  padding: 0 2rem;
}

.stats-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1.5rem;
  margin-bottom: 2rem;
}

.stat-card {
  background: white;
  padding: 1.5rem;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  display: flex;
  align-items: center;
  gap: 1rem;
}

.stat-icon {
  font-size: 2.5rem;
  width: 60px;
  height: 60px;
  display: flex;
  align-items: center;
  justify-content: center;
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  border-radius: 12px;
}

.stat-info {
  flex: 1;
}

.stat-value {
  font-size: 2rem;
  font-weight: bold;
  color: #333;
  line-height: 1;
  margin-bottom: 0.25rem;
}

.stat-label {
  font-size: 0.9rem;
  color: #666;
}

.registros-grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(450px, 1fr));
  gap: 1.5rem;
}

.registro-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  transition: all 0.3s;
  overflow: hidden;
}

.registro-card:hover {
  box-shadow: 0 4px 16px rgba(0,0,0,0.15);
  transform: translateY(-4px);
}

.registro-header {
  padding: 1.25rem;
  border-bottom: 2px solid #f0f0f0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: linear-gradient(135deg, #fef9f5, #ffffff);
}

.registro-title {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.lote-badge {
  font-weight: bold;
  color: #8B5A3C;
  font-size: 1.1rem;
}

.fecha {
  font-size: 0.85rem;
  color: #999;
}

.registro-calidad {
  padding: 0.5rem 1rem;
  border-radius: 20px;
  font-size: 0.85rem;
  font-weight: 600;
}

.registro-calidad.premium {
  background: #d4edda;
  color: #155724;
}

.registro-calidad.estándar {
  background: #fff3cd;
  color: #856404;
}

.registro-body {
  padding: 1.5rem;
}

.analisis-section {
  margin-bottom: 1.5rem;
  padding-bottom: 1.5rem;
  border-bottom: 1px solid #f0f0f0;
}

.analisis-section:last-child {
  margin-bottom: 0;
  padding-bottom: 0;
  border-bottom: none;
}

.subsection-title {
  font-size: 1rem;
  font-weight: bold;
  color: #8B5A3C;
  margin-bottom: 1rem;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.subsection-icon {
  font-size: 1.1rem;
}

.info-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(180px, 1fr));
  gap: 0.75rem;
}

.info-item {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.label {
  font-size: 0.8rem;
  color: #666;
  font-weight: 600;
}

.value {
  font-size: 1rem;
  color: #333;
  font-weight: 500;
}

.value.highlight {
  color: #8B5A3C;
  font-weight: bold;
  font-size: 1.1rem;
}

.progress-bars {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.progress-item {
  width: 100%;
}

.progress-header {
  display: flex;
  justify-content: space-between;
  margin-bottom: 0.5rem;
}

.progress-label {
  font-size: 0.85rem;
  color: #666;
  font-weight: 600;
}

.progress-value {
  font-size: 0.85rem;
  color: #333;
  font-weight: bold;
}

.progress-bar-container {
  height: 10px;
  background: #f0f0f0;
  border-radius: 5px;
  overflow: hidden;
}

.progress-bar {
  height: 100%;
  transition: width 0.5s ease;
  border-radius: 5px;
}

.progress-bar.objetivo {
  background: linear-gradient(90deg, #4caf50, #66bb6a);
}

.progress-bar.verdes {
  background: linear-gradient(90deg, #ff9800, #ffb74d);
}

.progress-bar.secas {
  background: linear-gradient(90deg, #f44336, #ef5350);
}

.defectos-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
  gap: 1rem;
}

.defecto-item {
  display: flex;
  flex-direction: column;
  align-items: center;
  gap: 0.5rem;
  padding: 1rem;
  background: #fef9f5;
  border-radius: 8px;
  border: 2px solid #f0f0f0;
}

.defecto-icon {
  font-size: 1.5rem;
}

.defecto-label {
  font-size: 0.85rem;
  color: #666;
  font-weight: 600;
}

.defecto-value {
  font-size: 1.1rem;
  color: #8B5A3C;
  font-weight: bold;
}

.registro-footer {
  padding: 1rem 1.25rem;
  background: #f8f8f8;
  display: flex;
  gap: 1rem;
}

.btn-action {
  flex: 1;
  padding: 0.75rem 1rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 0.5rem;
}

.btn-detail {
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  color: white;
}

.btn-detail:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(139,90,60,0.3);
}

.btn-print {
  background: white;
  border: 2px solid #C8956F;
  color: #8B5A3C;
}

.btn-print:hover {
  background: #C8956F;
  color: white;
}

.empty-state {
  grid-column: 1 / -1;
  text-align: center;
  padding: 4rem 2rem;
  background: white;
  border-radius: 12px;
}

.empty-icon {
  font-size: 4rem;
  margin-bottom: 1rem;
}

.empty-state h3 {
  color: #333;
  margin-bottom: 0.5rem;
}

.empty-state p {
  color: #666;
}

.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0,0,0,0.6);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 2rem;
}

.modal-content {
  background: white;
  border-radius: 16px;
  max-width: 900px;
  width: 100%;
  max-height: 90vh;
  overflow-y: auto;
  box-shadow: 0 8px 32px rgba(0,0,0,0.3);
}

.modal-header {
  padding: 2rem;
  border-bottom: 2px solid #e0e0e0;
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: linear-gradient(135deg, #fef9f5, #ffffff);
}

.modal-header h3 {
  margin: 0;
  color: #8B5A3C;
  font-size: 1.5rem;
}

.btn-close {
  width: 36px;
  height: 36px;
  border: none;
  background: #f0f0f0;
  border-radius: 50%;
  cursor: pointer;
  font-size: 1.25rem;
  transition: all 0.3s;
}

.btn-close:hover {
  background: #e0e0e0;
  transform: rotate(90deg);
}

.modal-body {
  padding: 2rem;
}

.detail-section {
  margin-bottom: 2rem;
  padding-bottom: 2rem;
  border-bottom: 2px solid #f0f0f0;
}

.detail-section:last-child {
  margin-bottom: 0;
  padding-bottom: 0;
  border-bottom: none;
}

.detail-section h4 {
  color: #8B5A3C;
  margin-bottom: 1rem;
  font-size: 1.2rem;
}

.detail-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
  gap: 1rem;
}

.detail-item {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
  padding: 0.75rem;
  background: #fef9f5;
  border-radius: 8px;
}

.detail-label {
  font-size: 0.85rem;
  color: #666;
  font-weight: 600;
}

.detail-value {
  font-size: 1rem;
  color: #333;
  font-weight: 500;
}

.highlight-value {
  color: #8B5A3C;
  font-weight: bold;
  font-size: 1.2rem;
}

.success-value {
  color: #4caf50;
  font-weight: bold;
}

.warning-value {
  color: #ff9800;
  font-weight: bold;
}

.modal-footer {
  padding: 1.5rem 2rem;
  background: #f8f8f8;
  border-top: 2px solid #e0e0e0;
  display: flex;
  gap: 1rem;
  justify-content: flex-end;
}

.btn-base {
  padding: 0.75rem 1.5rem;
  border: none;
  border-radius: 8px;
  cursor: pointer;
  font-weight: 600;
  transition: all 0.3s;
  display: flex;
  align-items: center;
  gap: 0.5rem;
}

.btn-base.btn-primary {
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  color: white;
}

.btn-base.btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(139,90,60,0.3);
}

.btn-base.btn-secondary {
  background: white;
  border: 2px solid #C8956F;
  color: #8B5A3C;
}

.btn-base.btn-secondary:hover {
  background: #fef9f5;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

@media (max-width: 768px) {
  .registros-grid {
    grid-template-columns: 1fr;
  }

  .info-grid {
    grid-template-columns: 1fr;
  }

  .detail-grid {
    grid-template-columns: 1fr;
  }

  .defectos-grid {
    grid-template-columns: 1fr;
  }

  .modal-content {
    margin: 1rem;
  }
}
</style>