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
          <div class="icon-container" :style="{ background: seccionActual.gradiente }">
            <i class="section-icon">{{ seccionActual.icon }}</i>
          </div>
          <div>
            <h1>Historial de Registros</h1>
            <p class="subtitle">Consulta y gestión de registros del sistema</p>
          </div>
        </div>
      </div>
    </header>

    <!-- Selector de Tipo de Formulario -->
    <div class="selector-formulario-section">
      <div class="selector-container">
        <div class="selector-header">
          <i class="selector-icon">📂</i>
          <h3>Seleccione el tipo de registro a consultar</h3>
        </div>
        
        <div class="formularios-grid">
          <button
            v-for="tipo in tiposFormulario"
            :key="tipo.id"
            @click="cambiarTipoFormulario(tipo)"
            :class="['formulario-btn', { active: tipoSeleccionado === tipo.id }]"
          >
            <div class="formulario-icon" :style="{ background: tipo.gradiente }">
              {{ tipo.icon }}
            </div>
            <div class="formulario-info">
              <div class="formulario-nombre">{{ tipo.nombre }}</div>
              <div class="formulario-desc">{{ tipo.descripcion }}</div>
              <div class="formulario-count">{{ tipo.totalRegistros || 0 }} registros</div>
            </div>
            <div class="formulario-check" v-if="tipoSeleccionado === tipo.id">✓</div>
          </button>
        </div>
      </div>
    </div>

    <!-- SOLO para Área de Acopio: opción de reporte completo -->
    <div class="search-type-selector" v-if="tipoSeleccionado === 'acopio'">
      <div class="selector-mode-container">
        <button 
          @click="modoVista = 'individual'"
          :class="['mode-btn', { active: modoVista === 'individual' }]"
        >
          <i class="mode-icon">📋</i>
          <div>
            <div class="mode-title">Registros Individuales</div>
            <div class="mode-desc">Ver solo registros de acopio</div>
          </div>
        </button>
        
        <button 
          @click="modoVista = 'completo'"
          :class="['mode-btn', { active: modoVista === 'completo' }]"
        >
          <i class="mode-icon">📄</i>
          <div>
            <div class="mode-title">Seguimiento Completo</div>
            <div class="mode-desc">Ver todo el proceso del lote</div>
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
          :placeholder="getPlaceholderBusqueda()"
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
        <div class="stat-card" :style="{ borderLeft: `4px solid ${seccionActual.color}` }">
          <div class="stat-icon" :style="{ background: seccionActual.gradiente }">📊</div>
          <div class="stat-info">
            <div class="stat-value">{{ registrosFiltrados.length }}</div>
            <div class="stat-label">Registros encontrados</div>
          </div>
        </div>
        <div class="stat-card" :style="{ borderLeft: `4px solid ${seccionActual.color}` }">
          <div class="stat-icon" :style="{ background: seccionActual.gradiente }">{{ seccionActual.statIcon }}</div>
          <div class="stat-info">
            <div class="stat-value">{{ estadisticaPrincipal }}</div>
            <div class="stat-label">{{ estadisticaLabel }}</div>
          </div>
        </div>
        <div class="stat-card" :style="{ borderLeft: `4px solid ${seccionActual.color}` }">
          <div class="stat-icon" :style="{ background: seccionActual.gradiente }">📅</div>
          <div class="stat-info">
            <div class="stat-value">{{ registrosHoy }}</div>
            <div class="stat-label">Registros hoy</div>
          </div>
        </div>
      </div>

      <!-- Vista de registros individuales -->
      <div v-if="modoVista === 'individual'" class="registros-grid">
        <div 
          v-for="registro in registrosFiltrados" 
          :key="registro.id"
          class="registro-card"
          :style="{ borderTop: `4px solid ${seccionActual.color}` }"
        >
          <div class="registro-header">
            <div class="registro-title">
              <span class="lote-badge" :style="{ color: seccionActual.color }">{{ registro.lote }}</span>
              <span class="fecha">{{ formatearFecha(registro.fecha) }}</span>
            </div>
            <div class="registro-status" :class="registro.estado">
              {{ registro.estado }}
            </div>
          </div>

          <div class="registro-body">
            <!-- Contenido dinámico según el tipo de formulario -->
            <component 
              :is="'div'" 
              v-html="renderizarContenidoRegistro(registro)"
            ></component>
          </div>

          <div class="registro-footer">
            <button @click="verDetalle(registro)" class="btn-action btn-detail" :style="{ background: seccionActual.gradiente }">
              <i class="icon">👁️</i>
              Ver detalle
            </button>
            <button @click="imprimirRegistro(registro)" class="btn-action btn-print" :style="{ borderColor: seccionActual.color, color: seccionActual.color }">
              <i class="icon">🖨️</i>
              Imprimir
            </button>
          </div>
        </div>

        <!-- Estado vacío -->
        <div v-if="registrosFiltrados.length === 0" class="empty-state">
          <div class="empty-icon">📭</div>
          <h3>No se encontraron registros</h3>
          <p>Intenta seleccionar otro tipo de formulario o ajustar los filtros</p>
        </div>
      </div>

      <!-- Vista de seguimiento completo (solo Acopio) -->
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
                  <div class="timeline-data" v-if="proceso.datos">{{ proceso.datos }}</div>
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
              Ver Reporte
            </button>
            <button @click="imprimirReporteCompleto(lote)" class="btn-action btn-print">
              <i class="icon">🖨️</i>
              Imprimir PDF
            </button>
          </div>
        </div>
      </div>
    </main>

    <!-- Modal de detalle (simplificado) -->
    <transition name="fade">
      <div v-if="modalDetalle" class="modal-overlay" @click="cerrarModal">
        <div class="modal-content" @click.stop>
          <div class="modal-header">
            <h3>Detalle del Registro</h3>
            <button @click="cerrarModal" class="btn-close">✕</button>
          </div>
          
          <div class="modal-body" v-if="registroSeleccionado">
            <p class="modal-placeholder">Aquí se mostrará el detalle completo del registro {{ registroSeleccionado.lote }}</p>
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
  name: "HistorialGeneral",
  data() {
    return {
      tipoSeleccionado: 'acopio',
      modoVista: 'individual',
      busqueda: '',
      filtroActivo: null,
      modalDetalle: false,
      registroSeleccionado: null,
      
      tiposFormulario: [
        {
          id: 'acopio',
          nombre: 'Área de Acopio',
          icon: '🏪',
          descripcion: 'Recepción y pesaje',
          gradiente: 'linear-gradient(135deg, #8FBC8F, #556B2F)',
          totalRegistros: 15
        },
        {
          id: 'caracterizacion',
          nombre: 'Caracterización',
          icon: '🔬',
          descripcion: 'Análisis refractométrico',
          gradiente: 'linear-gradient(135deg, #C8956F, #8B5A3C)',
          totalRegistros: 12
        },
        {
          id: 'secado',
          nombre: 'Secado',
          icon: '🌡️',
          descripcion: 'Control de temperatura',
          gradiente: 'linear-gradient(135deg, #E5C29F, #C8956F)',
          totalRegistros: 10
        },
        {
          id: 'bodega',
          nombre: 'Bodega',
          icon: '📦',
          descripcion: 'Gestión de inventario',
          gradiente: 'linear-gradient(135deg, #8B5A3C, #4A2D1A)',
          totalRegistros: 18
        },
        {
          id: 'trilla',
          nombre: 'Trilla',
          icon: '⚙️',
          descripcion: 'Proceso de trillado',
          gradiente: 'linear-gradient(135deg, #A0826D, #8B5A3C)',
          totalRegistros: 8
        },
        {
          id: 'catacion',
          nombre: 'Catación',
          icon: '☕',
          descripcion: 'Evaluación sensorial',
          gradiente: 'linear-gradient(135deg, #4A2D1A, #2C1810)',
          totalRegistros: 6
        }
      ],
      
      filtrosRapidos: [
        { id: 'hoy', label: 'Hoy' },
        { id: 'semana', label: 'Esta semana' },
        { id: 'mes', label: 'Este mes' },
        { id: 'todos', label: 'Todos' }
      ],
      
      // Datos de ejemplo por tipo
      registrosPorTipo: {
        acopio: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-03', productor: 'Juan Pérez', peso: 150, estado: 'Procesado' },
          { id: 2, lote: 'LOT-2024-002', fecha: '2024-12-02', productor: 'María González', peso: 200, estado: 'En proceso' }
        ],
        caracterizacion: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-03', rangoOptimo: 22.5, escalaMaduracion: 8.5, estado: 'Completado' },
          { id: 2, lote: 'LOT-2024-002', fecha: '2024-12-02', rangoOptimo: 20.0, escalaMaduracion: 7.0, estado: 'Completado' }
        ],
        secado: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-04', temperatura: 45, tiempo: 6, humedad: 11, estado: 'Completado' },
          { id: 2, lote: 'LOT-2024-002', fecha: '2024-12-03', temperatura: 42, tiempo: 7, humedad: 12, estado: 'En proceso' }
        ],
        bodega: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-05', ubicacion: 'B-12', sacos: 3, peso: 120, estado: 'Almacenado' },
          { id: 2, lote: 'LOT-2024-002', fecha: '2024-12-04', ubicacion: 'A-08', sacos: 4, peso: 160, estado: 'Almacenado' }
        ],
        trilla: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-06', rendimiento: 82, pesoEntrada: 120, pesoSalida: 98, estado: 'Completado' }
        ],
        catacion: [
          { id: 1, lote: 'LOT-2024-001', fecha: '2024-12-07', puntuacion: 88, perfil: 'Afrutado', estado: 'Certificado' }
        ]
      },

      lotes: [
        {
          numeroLote: 'LOT-2024-001',
          productor: 'Juan Pérez',
          fechaInicio: '2024-12-03',
          progreso: 100,
          procesos: [
            { tipo: 'Acopio', icon: '🏪', completado: true, fecha: '2024-12-03 08:00', datos: 'Peso: 150kg' },
            { tipo: 'Caracterización', icon: '🔬', completado: true, fecha: '2024-12-03 10:30', datos: 'Brix: 22°' },
            { tipo: 'Secado', icon: '🌡️', completado: true, fecha: '2024-12-04 14:00', datos: 'Temp: 45°C' },
            { tipo: 'Bodega', icon: '📦', completado: true, fecha: '2024-12-05 09:00', datos: 'Ubicación: B-12' },
            { tipo: 'Trilla', icon: '⚙️', completado: true, fecha: '2024-12-06 11:00', datos: 'Rend: 82%' },
            { tipo: 'Catación', icon: '☕', completado: true, fecha: '2024-12-07 15:00', datos: 'Punt: 88/100' }
          ]
        }
      ],

      registrosFiltrados: []
    };
  },
  
  computed: {
    seccionActual() {
      const tipo = this.tiposFormulario.find(t => t.id === this.tipoSeleccionado);
      return {
        ...tipo,
        color: this.getColorPrincipal(tipo.gradiente),
        statIcon: this.getStatIcon(tipo.id)
      };
    },

    estadisticaPrincipal() {
      const registros = this.registrosFiltrados;
      if (registros.length === 0) return '0';
      
      switch(this.tipoSeleccionado) {
        case 'acopio':
          return registros.reduce((sum, r) => sum + (r.peso || 0), 0) + ' kg';
        case 'caracterizacion': {
          const avgBrix = registros.reduce((sum, r) => sum + (r.rangoOptimo || 0), 0) / registros.length;
          return avgBrix.toFixed(1) + '°';
        }
        case 'secado': {
          const avgTemp = registros.reduce((sum, r) => sum + (r.temperatura || 0), 0) / registros.length;
          return avgTemp.toFixed(1) + '°C';
        }
        case 'bodega':
          return registros.reduce((sum, r) => sum + (r.sacos || 0), 0) + ' sacos';
        case 'trilla': {
          const avgRend = registros.reduce((sum, r) => sum + (r.rendimiento || 0), 0) / registros.length;
          return avgRend.toFixed(1) + '%';
        }
        case 'catacion': {
          const avgPunt = registros.reduce((sum, r) => sum + (r.puntuacion || 0), 0) / registros.length;
          return avgPunt.toFixed(1) + '/100';
        }
        default:
          return '0';
      }
    },

    estadisticaLabel() {
      const labels = {
        acopio: 'Total procesado',
        caracterizacion: '°Brix promedio',
        secado: 'Temperatura promedio',
        bodega: 'Total sacos',
        trilla: 'Rendimiento promedio',
        catacion: 'Puntuación promedio'
      };
      return labels[this.tipoSeleccionado] || 'Estadística';
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
    getColorPrincipal(gradiente) {
      const match = gradiente.match(/#[0-9A-F]{6}/i);
      return match ? match[0] : '#8FBC8F';
    },

    getStatIcon(tipo) {
      const icons = {
        acopio: '⚖️',
        caracterizacion: '📈',
        secado: '🌡️',
        bodega: '📦',
        trilla: '⚙️',
        catacion: '⭐'
      };
      return icons[tipo] || '📊';
    },

    cambiarTipoFormulario(tipo) {
      this.tipoSeleccionado = tipo.id;
      this.modoVista = 'individual';
      this.busqueda = '';
      this.filtroActivo = null;
      this.cargarRegistros();
    },

    cargarRegistros() {
      this.registrosFiltrados = [...(this.registrosPorTipo[this.tipoSeleccionado] || [])];
    },

    getPlaceholderBusqueda() {
      const placeholders = {
        acopio: 'Buscar por lote, productor, peso...',
        caracterizacion: 'Buscar por lote, °Brix, escala...',
        secado: 'Buscar por lote, temperatura, tiempo...',
        bodega: 'Buscar por lote, ubicación, sacos...',
        trilla: 'Buscar por lote, rendimiento...',
        catacion: 'Buscar por lote, puntuación, perfil...'
      };
      return placeholders[this.tipoSeleccionado] || 'Buscar...';
    },

    renderizarContenidoRegistro(registro) {
      const templates = {
        acopio: `
          <div class="info-row"><span class="label">Productor:</span><span class="value">${registro.productor}</span></div>
          <div class="info-row"><span class="label">Peso:</span><span class="value">${registro.peso} kg</span></div>
        `,
        caracterizacion: `
          <div class="info-row"><span class="label">Rango Óptimo:</span><span class="value">${registro.rangoOptimo}°Brix</span></div>
          <div class="info-row"><span class="label">Escala Maduración:</span><span class="value">${registro.escalaMaduracion}/10</span></div>
        `,
        secado: `
          <div class="info-row"><span class="label">Temperatura:</span><span class="value">${registro.temperatura}°C</span></div>
          <div class="info-row"><span class="label">Tiempo:</span><span class="value">${registro.tiempo}h</span></div>
          <div class="info-row"><span class="label">Humedad:</span><span class="value">${registro.humedad}%</span></div>
        `,
        bodega: `
          <div class="info-row"><span class="label">Ubicación:</span><span class="value">${registro.ubicacion}</span></div>
          <div class="info-row"><span class="label">Sacos:</span><span class="value">${registro.sacos}</span></div>
          <div class="info-row"><span class="label">Peso:</span><span class="value">${registro.peso} kg</span></div>
        `,
        trilla: `
          <div class="info-row"><span class="label">Rendimiento:</span><span class="value">${registro.rendimiento}%</span></div>
          <div class="info-row"><span class="label">Peso Entrada:</span><span class="value">${registro.pesoEntrada} kg</span></div>
          <div class="info-row"><span class="label">Peso Salida:</span><span class="value">${registro.pesoSalida} kg</span></div>
        `,
        catacion: `
          <div class="info-row"><span class="label">Puntuación:</span><span class="value">${registro.puntuacion}/100</span></div>
          <div class="info-row"><span class="label">Perfil:</span><span class="value">${registro.perfil}</span></div>
        `
      };
      return templates[this.tipoSeleccionado] || '';
    },
    
    volverAtras() {
      this.$router.go(-1);
    },
    
    filtrarRegistros() {
      if (!this.busqueda) {
        this.cargarRegistros();
        return;
      }
      
      const busquedaLower = this.busqueda.toLowerCase();
      const registrosBase = this.registrosPorTipo[this.tipoSeleccionado] || [];
      this.registrosFiltrados = registrosBase.filter(registro => 
        Object.values(registro).some(valor => 
          String(valor).toLowerCase().includes(busquedaLower)
        )
      );
    },
    
    limpiarBusqueda() {
      this.busqueda = '';
      this.filtrarRegistros();
    },
    
    aplicarFiltroRapido(filtro) {
      this.filtroActivo = filtro.id;
      const hoy = new Date();
      const registrosBase = this.registrosPorTipo[this.tipoSeleccionado] || [];
      
      switch(filtro.id) {
        case 'hoy': {
          const hoyStr = hoy.toISOString().split('T')[0];
          this.registrosFiltrados = registrosBase.filter(r => r.fecha === hoyStr);
          break;
        }
        case 'semana': {
          const semanaAtras = new Date(hoy.getTime() - 7 * 24 * 60 * 60 * 1000);
          this.registrosFiltrados = registrosBase.filter(r => new Date(r.fecha) >= semanaAtras);
          break;
        }
        case 'mes': {
          const mesAtras = new Date(hoy.getTime() - 30 * 24 * 60 * 60 * 1000);
          this.registrosFiltrados = registrosBase.filter(r => new Date(r.fecha) >= mesAtras);
          break;
        }
        case 'todos':
          this.registrosFiltrados = [...registrosBase];
          break;
        default:
          this.registrosFiltrados = [...registrosBase];
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
    }
  },
  
  mounted() {
    const seccionQuery = this.$route.query.seccion;
    if (seccionQuery) {
      const mapa = {
        'Área de Acopio': 'acopio',
        'Caracterización': 'caracterizacion',
        'Secado': 'secado',
        'Bodega': 'bodega',
        'Trilla': 'trilla',
        'Catación': 'catacion'
      };
      this.tipoSeleccionado = mapa[seccionQuery] || 'acopio';
    }
    this.cargarRegistros();
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

/* Selector de formulario */
.selector-formulario-section {
  background: white;
  padding: 2rem;
  margin: 2rem auto;
  max-width: 1400px;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.selector-header {
  display: flex;
  align-items: center;
  gap: 1rem;
  margin-bottom: 1.5rem;
}

.selector-icon {
  font-size: 2rem;
}

.selector-header h3 {
  margin: 0;
  color: #4A2D1A;
  font-size: 1.3rem;
}

.formularios-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: 1rem;
}

.formulario-btn {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 1.25rem;
  border: 3px solid #e0e0e0;
  border-radius: 12px;
  background: white;
  cursor: pointer;
  transition: all 0.3s;
  position: relative;
}

.formulario-btn:hover {
  border-color: #C8956F;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
  transform: translateY(-2px);
}

.formulario-btn.active {
  border-color: #6F4E37;
  background: linear-gradient(135deg, #fff5ee, #ffffff);
  box-shadow: 0 4px 16px rgba(111,78,55,0.2);
}

.formulario-icon {
  width: 50px;
  height: 50px;
  border-radius: 12px;
  display: flex;
  align-items: center;
  justify-content: center;
  font-size: 1.8rem;
  flex-shrink: 0;
}

.formulario-info {
  flex: 1;
}

.formulario-nombre {
  font-weight: bold;
  color: #333;
  font-size: 1.05rem;
  margin-bottom: 0.25rem;
}

.formulario-desc {
  font-size: 0.85rem;
  color: #666;
  margin-bottom: 0.25rem;
}

.formulario-count {
  font-size: 0.75rem;
  color: #999;
  font-weight: 600;
}

.formulario-check {
  position: absolute;
  top: 0.5rem;
  right: 0.5rem;
  width: 24px;
  height: 24px;
  background: #4caf50;
  color: white;
  border-radius: 50%;
  display: flex;
  align-items: center;
  justify-content: center;
  font-weight: bold;
  font-size: 0.9rem;
}

/* Selector de modo (individual/completo) */
.search-type-selector {
  background: white;
  padding: 1.5rem 2rem;
  margin: 0 auto 2rem;
  max-width: 1400px;
  border-radius: 16px;
  box-shadow: 0 4px 12px rgba(0,0,0,0.1);
}

.selector-mode-container {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 1rem;
}

.mode-btn {
  display: flex;
  align-items: center;
  gap: 1rem;
  padding: 1rem;
  border: 3px solid #e0e0e0;
  border-radius: 12px;
  background: white;
  cursor: pointer;
  transition: all 0.3s;
}

.mode-btn:hover {
  border-color: #8FBC8F;
  transform: translateY(-2px);
}

.mode-btn.active {
  border-color: #556B2F;
  background: linear-gradient(135deg, #f0f8f0, #ffffff);
  box-shadow: 0 4px 12px rgba(85,107,47,0.2);
}

.mode-icon {
  font-size: 2rem;
}

.mode-title {
  font-weight: bold;
  color: #333;
  font-size: 1rem;
  margin-bottom: 0.25rem;
}

.mode-desc {
  font-size: 0.85rem;
  color: #666;
}

/* Búsqueda */
.search-section {
  background: white;
  padding: 2rem;
  margin: 0 auto 2rem;
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

/* Contenido principal */
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

/* Registros */
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

.registro-status.Procesado,
.registro-status.Completado,
.registro-status.Almacenado,
.registro-status.Certificado {
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
  color: white;
}

.btn-detail:hover {
  transform: translateY(-2px);
  box-shadow: 0 4px 12px rgba(0,0,0,0.2);
}

.btn-print {
  background: white;
  border: 2px solid;
}

.btn-print:hover {
  opacity: 0.8;
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

/* Reportes completos */
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

/* Modal */
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

.modal-placeholder {
  color: #666;
  text-align: center;
  padding: 2rem;
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
  .formularios-grid {
    grid-template-columns: 1fr;
  }

  .selector-mode-container {
    grid-template-columns: 1fr;
  }

  .registros-grid {
    grid-template-columns: 1fr;
  }

  .timeline-item {
    grid-template-columns: 50px 1fr 35px;
  }
}
</style>