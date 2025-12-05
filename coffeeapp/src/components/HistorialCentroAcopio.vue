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
            <i class="section-icon">🏪</i>
          </div>
          <div>
            <h1>Historial - Centro de Acopio</h1>
            <p class="subtitle">Consulta de registros y reportes completos</p>
          </div>
        </div>
      </div>
    </header>

    <!-- Selector de tipo de búsqueda (SOLO para Centro de Acopio) -->
    <div class="search-type-selector" v-if="seccion === 'Área de Acopio'">
      <div class="selector-container">
        <button 
          @click="tipoBusqueda = 'acopio'"
          :class="['selector-btn', { active: tipoBusqueda === 'acopio' }]"
        >
          <i class="btn-icon">📋</i>
          <div>
            <div class="btn-title">Solo Acopio</div>
            <div class="btn-desc">Ver registros de centro de acopio únicamente</div>
          </div>
        </button>
        
        <button 
          @click="tipoBusqueda = 'completo'"
          :class="['selector-btn', { active: tipoBusqueda === 'completo' }]"
        >
          <i class="btn-icon">📄</i>
          <div>
            <div class="btn-title">Reporte Completo</div>
            <div class="btn-desc">Ver todo el proceso del lote (Acopio, Caracterización, Trilla...)</div>
          </div>
        </button>
      </div>
    </div>

    <!-- Barra de búsqueda -->
    <div class="search-section">
      <div class="search-container">
        <i class="search-icon">🔍</i>
        <input 
          v-model="busqueda" 
          type="text" 
          :placeholder="tipoBusqueda === 'completo' ? 'Buscar por número de lote (ej: LOT-2024-001)' : 'Buscar por lote, productor, fecha...'"
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
            <div class="stat-label">Registros encontrados</div>
          </div>
        </div>
        <div class="stat-card">
          <div class="stat-icon">⚖️</div>
          <div class="stat-info">
            <div class="stat-value">{{ totalKilos }} kg</div>
            <div class="stat-label">Total procesado</div>
          </div>
        </div>
        <div class="stat-card">
          <div class="stat-icon">📅</div>
          <div class="stat-info">
            <div class="stat-value">{{ registrosHoy }}</div>
            <div class="stat-label">Registros hoy</div>
          </div>
        </div>
      </div>

      <!-- Vista de registros individuales (Solo Acopio u otras secciones) -->
      <div v-if="tipoBusqueda === 'acopio' || seccion !== 'Área de Acopio'" class="registros-grid">
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
            <div class="registro-status" :class="registro.estado">
              {{ registro.estado }}
            </div>
          </div>

          <div class="registro-body">
            <div class="info-row">
              <span class="label">Productor:</span>
              <span class="value">{{ registro.productor }}</span>
            </div>
            <div class="info-row">
              <span class="label">Peso:</span>
              <span class="value">{{ registro.peso }} kg</span>
            </div>
            <div class="info-row">
              <span class="label">Calidad:</span>
              <span class="value">{{ registro.calidad }}</span>
            </div>
            <div class="info-row" v-if="registro.observaciones">
              <span class="label">Observaciones:</span>
              <span class="value">{{ registro.observaciones }}</span>
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

      <!-- Vista de reporte completo por lote (Solo Centro de Acopio) -->
      <div v-else class="reportes-completos">
        <div 
          v-for="lote in lotesCompletos" 
          :key="lote.numeroLote"
          class="reporte-card"
        >
          <div class="reporte-header">
            <div class="reporte-title">
              <div class="lote-info">
                <h3>{{ lote.numeroLote }}</h3>
                <span class="productor-name">{{ lote.productor }}</span>
              </div>
              <span class="fecha-badge">{{ formatearFecha(lote.fechaInicio) }}</span>
            </div>
            <div class="proceso-progress">
              <div class="progress-bar">
                <div class="progress-fill" :style="{ width: lote.progreso + '%' }"></div>
              </div>
              <span class="progress-text">{{ lote.progreso }}% completado</span>
            </div>
          </div>

          <div class="reporte-body">
            <div class="proceso-timeline">
              <div 
                v-for="proceso in lote.procesos" 
                :key="proceso.tipo"
                :class="['timeline-item', { completed: proceso.completado }]"
              >
                <div class="timeline-icon">{{ proceso.icon }}</div>
                <div class="timeline-content">
                  <div class="timeline-title">{{ proceso.tipo }}</div>
                  <div class="timeline-date">{{ proceso.fecha || 'Pendiente' }}</div>
                  <div class="timeline-data" v-if="proceso.datos">
                    {{ proceso.datos }}
                  </div>
                </div>
                <div class="timeline-status">
                  <span v-if="proceso.completado" class="status-check">✓</span>
                  <span v-else class="status-pending">⏳</span>
                </div>
              </div>
            </div>
          </div>

          <div class="reporte-footer">
            <button @click="verReporteCompleto(lote)" class="btn-action btn-primary">
              <i class="icon">📄</i>
              Ver Reporte Completo
            </button>
            <button @click="imprimirReporteCompleto(lote)" class="btn-action btn-print">
              <i class="icon">🖨️</i>
              Imprimir PDF
            </button>
            <button @click="descargarReporte(lote)" class="btn-action btn-download">
              <i class="icon">⬇️</i>
              Descargar
            </button>
          </div>
        </div>

        <!-- Estado vacío para reportes completos -->
        <div v-if="lotesCompletos.length === 0" class="empty-state">
          <div class="empty-icon">📋</div>
          <h3>No se encontraron lotes</h3>
          <p>Busca por número de lote para ver el reporte completo</p>
        </div>
      </div>
    </main>

    <!-- Modal de detalle -->
    <transition name="fade">
      <div v-if="modalDetalle" class="modal-overlay" @click="cerrarModal">
        <div class="modal-content modal-large" @click.stop>
          <div class="modal-header">
            <h3>Detalle del Registro</h3>
            <button @click="cerrarModal" class="btn-close">✕</button>
          </div>
          
          <div class="modal-body" v-if="registroSeleccionado">
            <div class="detail-section">
              <h4>Información General</h4>
              <div class="detail-grid">
                <div class="detail-item">
                  <span class="detail-label">Número de Lote:</span>
                  <span class="detail-value">{{ registroSeleccionado.lote }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Fecha:</span>
                  <span class="detail-value">{{ formatearFecha(registroSeleccionado.fecha) }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Productor:</span>
                  <span class="detail-value">{{ registroSeleccionado.productor }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Peso Total:</span>
                  <span class="detail-value">{{ registroSeleccionado.peso }} kg</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Calidad:</span>
                  <span class="detail-value">{{ registroSeleccionado.calidad }}</span>
                </div>
                <div class="detail-item">
                  <span class="detail-label">Estado:</span>
                  <span class="detail-value">{{ registroSeleccionado.estado }}</span>
                </div>
              </div>
            </div>

            <div class="detail-section" v-if="registroSeleccionado.observaciones">
              <h4>Observaciones</h4>
              <p class="observaciones-text">{{ registroSeleccionado.observaciones }}</p>
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
  name: "HistorialCentroAcopio",
  data() {
    return {
      seccion: '',
      tipoBusqueda: 'acopio',
      busqueda: '',
      filtroActivo: null,
      modalDetalle: false,
      registroSeleccionado: null,
      
      filtrosRapidos: [
        { id: 'hoy', label: 'Hoy' },
        { id: 'semana', label: 'Esta semana' },
        { id: 'mes', label: 'Este mes' },
        { id: 'todos', label: 'Todos' }
      ],
      
      registros: [
        {
          id: 1,
          lote: 'LOT-2024-001',
          fecha: '2024-12-03',
          productor: 'Juan Pérez',
          peso: 150,
          calidad: 'Premium',
          estado: 'Procesado',
          observaciones: 'Café de excelente calidad, granos uniformes'
        },
        {
          id: 2,
          lote: 'LOT-2024-002',
          fecha: '2024-12-02',
          productor: 'María González',
          peso: 200,
          calidad: 'Estándar',
          estado: 'En proceso',
          observaciones: 'Requiere clasificación adicional'
        },
        {
          id: 3,
          lote: 'LOT-2024-003',
          fecha: '2024-12-01',
          productor: 'Carlos Rodríguez',
          peso: 180,
          calidad: 'Premium',
          estado: 'Procesado',
          observaciones: ''
        }
      ],

      lotes: [
        {
          numeroLote: 'LOT-2024-001',
          productor: 'Juan Pérez',
          fechaInicio: '2024-12-03',
          progreso: 100,
          procesos: [
            { 
              tipo: 'Acopio', 
              icon: '🏪', 
              completado: true, 
              fecha: '2024-12-03 08:00',
              datos: 'Peso: 150kg, Calidad: Premium'
            },
            { 
              tipo: 'Caracterización', 
              icon: '🔬', 
              completado: true, 
              fecha: '2024-12-03 10:30',
              datos: 'Brix: 22°, Humedad: 11%'
            },
            { 
              tipo: 'Secado', 
              icon: '🌡️', 
              completado: true, 
              fecha: '2024-12-04 14:00',
              datos: 'Temperatura: 45°C, Tiempo: 6h'
            },
            { 
              tipo: 'Bodega', 
              icon: '📦', 
              completado: true, 
              fecha: '2024-12-05 09:00',
              datos: 'Ubicación: B-12, Sacos: 3'
            },
            { 
              tipo: 'Trilla', 
              icon: '⚙️', 
              completado: true, 
              fecha: '2024-12-06 11:00',
              datos: 'Rendimiento: 82%'
            },
            { 
              tipo: 'Catación', 
              icon: '☕', 
              completado: true, 
              fecha: '2024-12-07 15:00',
              datos: 'Puntuación: 88/100'
            }
          ]
        },
        {
          numeroLote: 'LOT-2024-002',
          productor: 'María González',
          fechaInicio: '2024-12-02',
          progreso: 66,
          procesos: [
            { 
              tipo: 'Acopio', 
              icon: '🏪', 
              completado: true, 
              fecha: '2024-12-02 07:30',
              datos: 'Peso: 200kg, Calidad: Estándar'
            },
            { 
              tipo: 'Caracterización', 
              icon: '🔬', 
              completado: true, 
              fecha: '2024-12-02 09:15',
              datos: 'Brix: 20°, Humedad: 12%'
            },
            { 
              tipo: 'Secado', 
              icon: '🌡️', 
              completado: true, 
              fecha: '2024-12-03 13:00',
              datos: 'Temperatura: 42°C, Tiempo: 7h'
            },
            { 
              tipo: 'Bodega', 
              icon: '📦', 
              completado: true, 
              fecha: '2024-12-04 10:00',
              datos: 'Ubicación: A-08, Sacos: 4'
            },
            { 
              tipo: 'Trilla', 
              icon: '⚙️', 
              completado: false, 
              fecha: null,
              datos: null
            },
            { 
              tipo: 'Catación', 
              icon: '☕', 
              completado: false, 
              fecha: null,
              datos: null
            }
          ]
        }
      ],

      registrosFiltrados: []
    };
  },
  
  computed: {
    totalKilos() {
      return this.registrosFiltrados.reduce((sum, r) => sum + r.peso, 0);
    },
    
    registrosHoy() {
      const hoy = new Date().toISOString().split('T')[0];
      return this.registrosFiltrados.filter(r => r.fecha === hoy).length;
    },

    lotesCompletos() {
      if (!this.busqueda) return this.lotes;
      
      const busquedaLower = this.busqueda.toLowerCase();
      return this.lotes.filter(lote => 
        lote.numeroLote.toLowerCase().includes(busquedaLower) ||
        lote.productor.toLowerCase().includes(busquedaLower)
      );
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
        registro.productor.toLowerCase().includes(busquedaLower) ||
        registro.calidad.toLowerCase().includes(busquedaLower) ||
        registro.estado.toLowerCase().includes(busquedaLower)
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
    
    verDetalle(registro) {
      this.registroSeleccionado = registro;
      this.modalDetalle = true;
    },
    
    cerrarModal() {
      this.modalDetalle = false;
      this.registroSeleccionado = null;
    },
    
    imprimirRegistro(registro) {
      alert(`Imprimiendo registro ${registro.lote}...`);
    },

    verReporteCompleto(lote) {
      alert(`Abriendo reporte completo del lote ${lote.numeroLote}...`);
    },
    
    imprimirReporteCompleto(lote) {
      alert(`Imprimiendo reporte completo del lote ${lote.numeroLote}...`);
    },

    descargarReporte(lote) {
      alert(`Descargando reporte del lote ${lote.numeroLote}...`);
    }
  },
  
  mounted() {
    this.seccion = this.$route.query.seccion || 'Área de Acopio';
    
    if (this.seccion !== 'Área de Acopio') {
      this.tipoBusqueda = 'acopio';
    }
    
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
  background: linear-gradient(135deg, #6F4E37, #A0826D);
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
  background: linear-gradient(135deg, #8FBC8F, #556B2F);
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

.search-type-selector {
  background: white;
  padding: 2rem;
  margin: 2rem auto;
  max-width: 1400px;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.selector-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1.5rem;
}

.selector-btn {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 1.5rem;
  border: 3px solid #e0e0e0;
  border-radius: 12px;
  background: white;
  cursor: pointer;
  transition: all 0.3s;
  text-align: left;
}

.selector-btn:hover {
  border-color: #A0826D;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  transform: translateY(-2px);
}

.selector-btn.active {
  border-color: #6F4E37;
  background: linear-gradient(135deg, #fff5ee, #ffffff);
  box-shadow: 0 4px 16px rgba(111,78,55,0.2);
}

.btn-icon {
  font-size: 2.5rem;
}

.btn-title {
  font-size: 1.2rem;
  font-weight: bold;
  color: #333;
  margin-bottom: 0.25rem;
}

.btn-desc {
  font-size: 0.9rem;
  color: #666;
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
  border-color: #A0826D;
  outline: none;
  box-shadow: 0 0 0 3px rgba(160,130,109,0.1);
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
  border-color: #A0826D;
  background: #f8f8f8;
}

.chip.active {
  background: linear-gradient(135deg, #6F4E37, #A0826D);
  color: white;
  border-color: #6F4E37;
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
  background: linear-gradient(135deg, #8FBC8F, #556B2F);
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
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
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
}

.registro-title {
  display: flex;
  flex-direction: column;
  gap: 0.5rem;
}

.lote-badge {
  font-weight: bold;
  color: #6F4E37;
  font-size: 1.1rem;
}

.fecha {
  font-size: 0.85rem;
  color: #999;
}

.registro-status {
  padding: 0.5rem 1rem;
  border-radius: 20px;
  font-size: 0.85rem;
  font-weight: 600;
}

.registro-status.Procesado {
  background: #d4edda;
  color: #155724;
}

.registro-status.En.proceso {
  background: #fff3cd;
  color: #856404;
}

.registro-body {
  padding: 1.25rem;
}

.info-row {
  display: flex;
  justify-content: space-between;
  padding: 0.5rem 0;
  border-bottom: 1px solid #f0f0f0;
}

.info-row:last-child {
  border-bottom: none;
}

.label {
  font-weight: 600;
  color: #666;
}

.value {
  color: #333;
  text-align: right;
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
  background: linear-gradient(135deg, #6F4E37, #A0826D);
  color: white;
}

.btn-detail:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(111,78,55,0.3);
}

.btn-print {
  background: white;
  border: 2px solid #6F4E37;
  color: #6F4E37;
}

.btn-print:hover {
  background: #6F4E37;
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

.reportes-completos {
  display: flex;
  flex-direction: column;
  gap: 2rem;
}

.reporte-card {
  background: white;
  border-radius: 12px;
  box-shadow: 0 2px 8px rgba(0,0,0,0.1);
  overflow: hidden;
}

.reporte-header {
  padding: 1.5rem;
  background: linear-gradient(135deg, #f8f8f8, #ffffff);
  border-bottom: 2px solid #e0e0e0;
}

.reporte-title {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 1rem;
}

.lote-info h3 {
  margin: 0 0 0.25rem 0;
  color: #6F4E37;
  font-size: 1.5rem;
}

.productor-name {
  color: #666;
  font-size: 1rem;
}

.fecha-badge {
  background: #6F4E37;
  color: white;
  padding: 0.5rem 1rem;
  border-radius: 20px;
  font-size: 0.9rem;
}

.proceso-progress {
  margin-top: 1rem;
}

.progress-bar {
  height: 12px;
  background: #e0e0e0;
  border-radius: 6px;
  overflow: hidden;
  margin-bottom: 0.5rem;
}

.progress-fill {
  height: 100%;
  background: linear-gradient(90deg, #8FBC8F, #556B2F);
  transition: width 0.5s;
}

.progress-text {
  font-size: 0.9rem;
  color: #666;
  font-weight: 600;
}

.reporte-body {
  padding: 2rem;
}

.proceso-timeline {
  display: flex;
  flex-direction: column;
  gap: 1rem;
}

.timeline-item {
  display: grid;
  grid-template-columns: 60px 1fr 40px;
  gap: 1rem;
  padding: 1rem;
  border-radius: 8px;
  background: #f8f8f8;
  transition: all 0.3s;
}

.timeline-item.completed {
  background: linear-gradient(135deg, #e8f5e9, #f1f8f4);
  border-left: 4px solid #4caf50;
}

.timeline-icon {
  font-size: 2rem;
  display: flex;
  align-items: center;
  justify-content: center;
}

.timeline-content {
  display: flex;
  flex-direction: column;
  gap: 0.25rem;
}

.timeline-title {
  font-weight: bold;
  color: #333;
  font-size: 1.1rem;
}

.timeline-date {
  font-size: 0.85rem;
  color: #999;
}

.timeline-data {
  font-size: 0.9rem;
  color: #666;
  margin-top: 0.25rem;
}

.timeline-status {
  display: flex;
  align-items: center;
  justify-content: center;
}

.status-check {
  width: 32px;
  height: 32px;
  background: #4caf50;
  color: white;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
}

.status-pending {
  font-size: 1.5rem;
  opacity: 0.5;
}

.reporte-footer {
  padding: 1.5rem;
  background: #f8f8f8;
  display: flex;
  gap: 1rem;
  border-top: 2px solid #e0e0e0;
}

.btn-primary {
  background: linear-gradient(135deg, #6F4E37, #A0826D);
  color: white;
  flex: 1;
}

.btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(111,78,55,0.3);
}

.btn-download {
  background: linear-gradient(135deg, #8FBC8F, #556B2F);
  color: white;
}

.btn-download:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(85,107,47,0.3);
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
  max-width: 600px;
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
}

.modal-header h3 {
  margin: 0;
  color: #333;
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
}

.detail-section h4 {
  color: #6F4E37;
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
  background: #f8f8f8;
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
}

.observaciones-text {
  background: #f8f8f8;
  padding: 1rem;
  border-radius: 8px;
  color: #333;
  line-height: 1.6;
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
  background: linear-gradient(135deg, #6F4E37, #A0826D);
  color: white;
}

.btn-base.btn-secondary {
  background: white;
  border: 2px solid #6F4E37;
  color: #6F4E37;
}

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.3s;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

@media (max-width: 768px) {
  .selector-container {
    grid-template-columns: 1fr;
  }

  .registros-grid {
    grid-template-columns: 1fr;
  }

  .reporte-footer {
    flex-direction: column;
  }

  .timeline-item {
    grid-template-columns: 50px 1fr 35px;
  }
}
</style>