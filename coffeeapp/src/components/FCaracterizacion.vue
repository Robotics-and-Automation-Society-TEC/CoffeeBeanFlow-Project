<template>
  <div class="modal" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro Refractómetro</h2>
        <div class="coffee-decoration">
          <div class="coffee-bean"></div>
          <div class="coffee-bean"></div>
        </div>
      </div>

      <!-- Mensajes de estado -->
      <transition name="fade">
        <div v-if="showSuccess" class="message-base message-success">
          <i class="icon">✓</i>
          ¡Su registro ha sido guardado exitosamente!
        </div>
      </transition>

      <transition name="fade">
        <div v-if="showError" class="message-base message-error">
          <i class="icon">⚠</i>
          {{ errorMessage }}
        </div>
      </transition>

      <form @submit.prevent="submitForm" class="form-container">
        <!-- Información básica -->
        <div class="section-card basic-info">
          <h3><i class="section-icon">📋</i>Información Básica</h3>
          <div class="form-grid">
            <div class="input-group">
              <label class="required-label">Número de Lote *</label>
              
              <!-- Select cuando hay lotes cargados -->
              <select 
                v-if="lotes.length > 0"
                v-model="form.nloteAreaAcopio" 
                required 
                class="input-field select-field"
                @change="validarCampo('nloteAreaAcopio')"
                :disabled="cargandoLotes"
              >
                <option value="" disabled>
                  {{ cargandoLotes ? 'Cargando lotes...' : 'Seleccione un lote' }}
                </option>
                <option 
                  v-for="lote in lotes" 
                  :key="lote" 
                  :value="lote"
                >
                  {{ lote }}
                </option>
              </select>
              
              <!-- Input manual cuando no hay lotes o falla la API -->
              <input 
                v-else
                type="text" 
                v-model="form.nloteAreaAcopio" 
                required 
                class="input-field"
                placeholder="Ej: LOTE-001, LOTE-2024-001"
                maxlength="50"
                @blur="validarCampo('nloteAreaAcopio')"
                :disabled="cargandoLotes"
              />
              
              <span v-if="errors.nloteAreaAcopio" class="error-text">{{ errors.nloteAreaAcopio }}</span>
              
              <!-- Mensajes informativos -->
              <small v-if="cargandoLotes" class="info-text">
                <i class="info-icon">⏳</i>
                Cargando lotes disponibles...
              </small>
              <small v-else-if="lotes.length === 0" class="info-text" style="color: var(--color-warning);">
                <i class="info-icon">⚠️</i>
                API de lotes no disponible. Ingrese el número de lote manualmente.
              </small>
              <small v-else class="info-text" style="color: var(--color-success);">
                <i class="info-icon">✅</i>
                {{ lotes.length }} lote(s) disponible(s)
              </small>
            </div>
            
            <!-- CAMPO PROCESO AGREGADO -->
            <div class="input-group">
              <label class="required-label">Tipo de Proceso *</label>
              <select 
                v-model="form.proceso" 
                required 
                class="input-field select-field"
                @change="validarCampo('proceso')"
              >
                <option value="" disabled>Seleccione el proceso</option>
                <option value="Lavado">Lavado</option>
                <option value="Miel">Miel</option>
                <option value="Natural">Natural</option>
                <option value="Despulpado">Despulpado</option>
                <option value="Semi-Lavado">Semi-Lavado</option>
              </select>
              <span v-if="errors.proceso" class="error-text">{{ errors.proceso }}</span>
            </div>
          </div>
        </div>

        <!-- Análisis de cerezas -->
        <div class="section-card cherries">
          <h3><i class="section-icon">🍒</i>Análisis de Cerezas</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Cerezas Inmaduras</label>
              <input 
                type="number" 
                v-model.number="form.cinmaduras" 
                min="0" 
                class="input-field" 
                placeholder="Cantidad" 
              />
            </div>
            <div class="input-group">
              <label>Cerezas Sobremaduras</label>
              <input 
                type="number" 
                v-model.number="form.csobremaduras" 
                min="0" 
                class="input-field" 
                placeholder="Cantidad" 
              />
            </div>
            <div class="input-group">
              <label>Cerezas Verdes</label>
              <input 
                type="number" 
                v-model.number="form.cverdes" 
                min="0" 
                class="input-field" 
                placeholder="Cantidad" 
              />
            </div>
            <div class="input-group">
              <label>Cerezas Objetivo (Óptimas)</label>
              <input 
                type="number" 
                v-model.number="form.cobjetivo" 
                min="0" 
                class="input-field" 
                placeholder="Cantidad" 
              />
            </div>
            <div class="input-group">
              <label>Cerezas Secas</label>
              <input 
                type="number" 
                v-model.number="form.csecas" 
                min="0" 
                class="input-field" 
                placeholder="Cantidad" 
              />
            </div>
            <div class="input-group">
              <label>Rango Óptimo de Maduras (°Brix)</label>
              <input 
                type="number" 
                v-model.number="form.drmaduras" 
                step="0.1" 
                min="0" 
                class="input-field" 
                placeholder="20.5" 
              />
            </div>
          </div>
        </div>

        <!-- Porcentajes de análisis -->
        <div class="section-card percentages">
          <h3><i class="section-icon">📊</i>Porcentajes de Análisis</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>% Cerezas Verdes</label>
              <input 
                type="number" 
                v-model.number="form.pcverdes" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pcverdes')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Cerezas Secas</label>
              <input 
                type="number" 
                v-model.number="form.pcsecas" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pcsecas')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Cerezas Objetivo</label>
              <input 
                type="number" 
                v-model.number="form.pcobjetivo" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pcobjetivo')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Por Debajo de Madurez</label>
              <input 
                type="number" 
                v-model.number="form.pcdebajo" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pcdebajo')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Por Encima de Madurez</label>
              <input 
                type="number" 
                v-model.number="form.pcencima" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pcencima')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
          </div>
        </div>

        <!-- Estado y defectos -->
        <div class="section-card defects">
          <h3><i class="section-icon">🔍</i>Estado y Defectos</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Escala de Maduración (1-10)</label>
              <input 
                type="number" 
                v-model.number="form.emaduracion" 
                min="1" 
                max="10" 
                step="0.1" 
                class="input-field" 
                placeholder="8.5" 
              />
            </div>
            <div class="input-group">
              <label>% Broca</label>
              <input 
                type="number" 
                v-model.number="form.broca" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('broca')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Vanos</label>
              <input 
                type="number" 
                v-model.number="form.vanos" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('vanos')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Secos</label>
              <input 
                type="number" 
                v-model.number="form.secos" 
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('secos')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>Muestreo Tabla</label>
              <input 
                type="number" 
                v-model.number="form.mtabla" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="100.0" 
              />
            </div>
          </div>
        </div>

        <!-- Densidad -->
        <div class="section-card density">
          <h3><i class="section-icon">⚖️</i>Análisis de Densidad</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Densidad del Grano (g/ml)</label>
              <input 
                type="number" 
                v-model.number="form.densidad" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="Ej: 1.25" 
              />
            </div>
          </div>
        </div>

        <!-- Botones -->
        <div class="form-buttons">
          <button type="submit" class="btn btn-submit" :disabled="!formularioValido">
            <i class="btn-icon" v-if="!guardandoRegistro">💾</i>
            <i class="btn-icon loading-spinner" v-else>⏳</i>
            {{ guardandoRegistro ? 'Guardando...' : 'Guardar Registro' }}
          </button>
          <button type="button" @click="limpiarFormulario" class="btn btn-secondary">
            <i class="btn-icon">🔄</i>
            Limpiar
          </button>
          <button type="button" @click="cancelar" class="btn btn-cancel">
            <i class="btn-icon">✕</i>
            Cancelar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import apiService from '@/services/apiService'

export default {
  name: "FCaracterizacion",
  data() {
    return {
      form: {
        nloteAreaAcopio: "",
        proceso: "",
        cinmaduras: null,
        csobremaduras: null,
        cverdes: null,
        cobjetivo: null,
        csecas: null,
        drmaduras: null,
        pcverdes: null,
        pcsecas: null,
        pcobjetivo: null,
        pcdebajo: null,
        pcencima: null,
        emaduracion: null,
        broca: null,
        vanos: null,
        secos: null,
        densidad: null,
        mtabla: 100.0, // Valor por defecto
      },
      lotes: [], // Lista de lotes disponibles
      cargandoLotes: false,
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false
    };
  },
  computed: {
    formularioValido() {
      // Validación básica: lote y proceso son obligatorios
      return Object.keys(this.errors).length === 0 &&
             this.form.nloteAreaAcopio && 
             this.form.nloteAreaAcopio.trim() !== '' && 
             this.form.proceso !== '' && 
             !this.guardandoRegistro &&
             !this.cargandoLotes;
    }
  },
  async mounted() {
    await this.cargarLotes();
  },
  methods: {
    // Método para cargar lotes desde Area_Acopio
    async cargarLotes() {
      this.cargandoLotes = true;
      try {
        console.log("📦 Cargando lotes desde Area_Acopio...");
        
        // Obtener todos los registros de Area_Acopio
        const registrosAreaAcopio = await apiService.obtenerTodos('Area_Acopio');
        
        console.log("📊 Registros de Area_Acopio obtenidos:", registrosAreaAcopio);
        
        // Extraer números de lote únicos y válidos
        const lotesUnicos = [...new Set(
          registrosAreaAcopio
            .map(registro => registro.Nlote || registro.nlote)
            .filter(nlote => nlote && nlote.toString().trim() !== '')
        )];
        
        this.lotes = lotesUnicos.sort();
        
        console.log("✅ Lotes cargados desde Area_Acopio:", this.lotes.length, this.lotes);
        
      } catch (error) {
        console.error("❌ Error al cargar lotes desde Area_Acopio:", error);
        this.lotes = [];
        console.log("🔄 Cambiando a modo de entrada manual de lotes");
      } finally {
        this.cargandoLotes = false;
      }
    },

    // Validación de campos obligatorios
    validarCampo(campo) {
      if (campo === 'nloteAreaAcopio') {
        if (!this.form.nloteAreaAcopio || this.form.nloteAreaAcopio.toString().trim() === '') {
          this.errors.nloteAreaAcopio = 'Debe seleccionar o ingresar un lote';
        } else if (this.lotes.length > 0) {
          const loteValido = this.lotes.includes(this.form.nloteAreaAcopio);
          if (!loteValido) {
            this.errors.nloteAreaAcopio = 'El lote seleccionado no es válido';
          } else {
            delete this.errors.nloteAreaAcopio;
          }
        } else {
          const lote = this.form.nloteAreaAcopio.toString().trim();
          if (lote.length < 2) {
            this.errors.nloteAreaAcopio = 'El número de lote debe tener al menos 2 caracteres';
          } else {
            delete this.errors.nloteAreaAcopio;
          }
        }
      } else if (campo === 'proceso') {
        if (!this.form.proceso || this.form.proceso.trim() === '') {
          this.errors.proceso = 'Debe seleccionar un tipo de proceso';
        } else {
          delete this.errors.proceso;
        }
      }
    },

    validarPorcentaje(campo) {
      const valor = this.form[campo];
      if (valor !== null && valor !== undefined) {
        this.form[campo] = Math.max(0, Math.min(100, valor));
      }
    },

    mostrarError(mensaje) {
      this.errorMessage = mensaje;
      this.showError = true;
      setTimeout(() => {
        this.showError = false;
      }, 6000);
    },

    mostrarExito() {
      this.showSuccess = true;
      setTimeout(() => {
        this.showSuccess = false;
      }, 3000);
    },

    // Validar todos los campos obligatorios
    validarFormularioCompleto() {
      this.errors = {};
      
      // Validar campos obligatorios
      this.validarCampo('nloteAreaAcopio');
      this.validarCampo('proceso');
      
      return Object.keys(this.errors).length === 0;
    },

    mapearDatosParaAPI() {
      // Mapear los datos del formulario al modelo de la API según tu backend
      const datos = {
        Tiempo: new Date().toISOString(), // Timestamp actual como llave primaria
        Nlote_AreaAcopio: this.form.nloteAreaAcopio,
        Proceso: this.form.proceso,
        
        // Cantidades de cerezas
        Cinmaduras: this.form.cinmaduras || 0,
        Csobremaduras: this.form.csobremaduras || 0,
        Cverdes: this.form.cverdes || 0,
        Cobjetivo: this.form.cobjetivo || 0,
        Csecas: this.form.csecas || 0,
        
        // Rango óptimo y muestreo
        DRmaduras: this.form.drmaduras || 0,
        Mtabla: this.form.mtabla || 100.0,
        
        // Porcentajes
        PCverdes: this.form.pcverdes || 0,
        PCsecas: this.form.pcsecas || 0,
        PCobjetivo: this.form.pcobjetivo || 0,
        PCdebajo: this.form.pcdebajo || 0,
        PCencima: this.form.pcencima || 0,
        
        // Escala de maduración y defectos
        Emaduracion: this.form.emaduracion || 0,
        Broca: this.form.broca || 0,
        Vanos: this.form.vanos || 0,
        Secos: this.form.secos || 0,
        
        // Densidad
        Densidad: this.form.densidad || 0
      };
      
      return datos;
    },

    async submitForm() {
      // Prevenir doble envío
      if (this.guardandoRegistro) {
        return;
      }
      
      // Validar formulario completo
      if (!this.validarFormularioCompleto()) {
        this.mostrarError('Por favor, complete todos los campos obligatorios y corrija los errores.');
        return;
      }

      // Activar estado de guardado
      this.guardandoRegistro = true;
      this.showError = false;
      this.showSuccess = false;

      try {
        console.log("📋 Iniciando guardado de caracterización...");
        
        // Mapear datos para la API
        const datosAPI = this.mapearDatosParaAPI();
        console.log("📋 Enviando datos de caracterización a API:", datosAPI);
        
        // Llamar a la API
        const resultado = await apiService.crear('FormularioCaracterizacionApi', datosAPI);
        console.log("✅ Respuesta de la API:", resultado);
        
        this.mostrarExito();
        
        // Limpiar formulario después de guardar exitosamente
        setTimeout(() => {
          this.limpiarFormulario();
        }, 2000);
        
        // Redirigir después de guardar (opcional)
        setTimeout(() => {
          if (this.$router) {
            this.$router.push({ name: "HomeView" });
          }
        }, 3000);
        
      } catch (error) {
        console.error("❌ Error al guardar caracterización:", error);
        
        let mensajeError = "Error al guardar el registro de caracterización";
        
        // Manejar diferentes tipos de errores
        if (typeof error === 'string') {
          mensajeError = error;
        } else if (error.message) {
          mensajeError = error.message;
        } else if (error.response?.data) {
          mensajeError = error.response.data.message || JSON.stringify(error.response.data);
        } else if (error.data) {
          mensajeError = error.data.message || JSON.stringify(error.data);
        }
        
        this.mostrarError(mensajeError);
      } finally {
        this.guardandoRegistro = false;
      }
    },

    limpiarFormulario() {
      // Limpiar todos los campos excepto valores por defecto
      Object.keys(this.form).forEach(key => {
        if (key === 'nloteAreaAcopio' || key === 'proceso') {
          this.form[key] = '';
        } else if (key === 'mtabla') {
          this.form[key] = 100.0; // Mantener valor por defecto
        } else {
          this.form[key] = null;
        }
      });
      
      // Limpiar errores
      this.errors = {};
      
      // Recargar lotes si es necesario
      if (this.lotes.length === 0) {
        this.cargarLotes();
      }
    },

    cancelar() {
      if (this.$router) {
        this.$router.push({ name: "HomeView" });
      }
    }
  }
};
</script>

<style scoped>
/* Variables de la paleta de colores de café */
* {
  --burgundy: #A52A3D;
  --verde-claro: #8FAD5A; 
  --verde-oscuro: #4A5D2E;
  --verde-muy-oscuro: #2e3d1a;
  --cafe-claro: #E5C29F;
  --cafe-medio: #C8956F;
  --cafe-oscuro: #8B5A3C;
  --cafe-muy-oscuro: #4A2D1A;
  --negro-cafe: #2C1810;
  --beige-claro: #F4F0E6;
  --blanco-crema: #FEFAF5;
  
  /* Colores de sistema */
  --color-success: #4CAF50;
  --color-error: #F44336;
  --color-warning: #FF9800;
  --color-info: #2196F3;
  
  /* Espaciado */
  --space-xs: 4px;
  --space-sm: 8px;
  --space-md: 12px;
  --space-lg: 16px;
  --space-xl: 24px;
  --space-2xl: 32px;
  --space-3xl: 48px;
  
  /* Tamaños de texto */
  --text-xs: 0.75rem;
  --text-sm: 0.875rem;
  --text-base: 1rem;
  --text-lg: 1.125rem;
  --text-xl: 1.25rem;
  --text-2xl: 1.5rem;
  
  /* Transiciones */
  --transition-all: all 0.3s ease;
}

.modal {
  position: fixed;
  top: 0; left: 0;
  width: 100%; height: 100%;
  background: linear-gradient(135deg, var(--verde-claro) 1%, var(--verde-oscuro) 50%, var(--verde-muy-oscuro) 99%);
  backdrop-filter: blur(5px);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  animation: fadeIn 0.3s ease;
}

.modal-content {
  background: linear-gradient(145deg, #FEFAF5, #F4F0E6);
  padding: 32px;
  border-radius: 20px;
  max-height: 90vh;
  overflow-y: auto;
  width: 95%;
  max-width: 800px;
  box-shadow: 
    0 20px 40px rgba(74, 45, 26, 0.3),
    0 8px 16px rgba(165, 42, 61, 0.1);
  color: #2C1810;
  position: relative;
  animation: slideUp 0.4s ease;
}

.header-section {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 30px;
  padding-bottom: 15px;
  border-bottom: 3px solid #C8956F;
}

.header-section h2 {
  color: #4A2D1A;
  font-size: 2rem;
  font-weight: bold;
  margin: 0;
  text-shadow: 2px 2px 4px rgba(165, 42, 61, 0.1);
}

.coffee-decoration {
  display: flex;
  gap: 8px;
}

.coffee-bean {
  width: 20px;
  height: 30px;
  background: linear-gradient(45deg, #8B5A3C, #4A2D1A);
  border-radius: 50%;
  position: relative;
  box-shadow: inset 0 2px 4px rgba(0,0,0,0.3);
}

.coffee-bean::after {
  content: '';
  position: absolute;
  top: 50%;
  left: 50%;
  transform: translate(-50%, -50%);
  width: 2px;
  height: 60%;
  background: #E5C29F;
  border-radius: 1px;
}

/* Mensajes de estado */
.message-base {
  padding: 15px 20px;
  border-radius: 12px;
  margin-bottom: 20px;
  display: flex;
  align-items: center;
  gap: 10px;
  font-weight: 600;
  font-size: var(--text-lg);
  animation: slideIn 0.5s ease, pulse 0.6s ease 0.2s;
}

.message-success {
  background: linear-gradient(135deg, #8FAD5A, #4A5D2E);
  color: white;
  box-shadow: 0 4px 12px rgba(143, 173, 90, 0.4);
}

.message-error {
  background: linear-gradient(135deg, #A52A3D, #8B2332);
  color: white;
  box-shadow: 0 4px 12px rgba(165, 42, 61, 0.4);
}

.icon {
  font-size: 1.2rem;
  font-weight: bold;
}

/* Información adicional */
.info-text {
  color: var(--cafe-oscuro);
  font-style: italic;
  display: flex;
  align-items: center;
  gap: var(--space-sm);
  font-size: var(--text-sm);
}

.info-icon {
  font-size: var(--text-sm);
}

/* Mensajes de error */
.error-text {
  color: var(--color-error);
  font-size: var(--text-xs);
  margin-top: var(--space-xs);
  display: block;
}

/* Secciones de formulario */
.section-card {
  background: #FEFAF5;
  border-radius: 16px;
  padding: 24px;
  margin-bottom: 24px;
  box-shadow: 
    0 8px 16px rgba(74, 45, 26, 0.15),
    0 4px 8px rgba(165, 42, 61, 0.05);
  border: 2px solid transparent;
  transition: var(--transition-all);
}

.section-card:hover {
  border-color: #C8956F;
  transform: translateY(-2px);
  box-shadow: 
    0 12px 24px rgba(74, 45, 26, 0.2),
    0 6px 12px rgba(165, 42, 61, 0.1);
}

.section-card h3 {
  color: #4A2D1A;
  font-size: 1.3rem;
  font-weight: bold;
  margin-bottom: 20px;
  display: flex;
  align-items: center;
  gap: 8px;
}

.section-icon {
  font-size: 1.2rem;
}

/* Bordes laterales por sección */
.basic-info {
  border-left: 4px solid var(--burgundy);
}

.cherries {
  border-left: 4px solid var(--cafe-oscuro);
}

.percentages {
  border-left: 4px solid var(--verde-oscuro);
}

.defects {
  border-left: 4px solid var(--cafe-medio);
}

.density {
  border-left: 4px solid var(--cafe-muy-oscuro);
}

/* Grid de formulario */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 20px;
}

/* Grupos de inputs */
.input-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
}

.input-group label {
  color: #4A2D1A;
  font-weight: 600;
  font-size: 0.9rem;
}

.required-label::after {
  content: " *";
  color: var(--color-error);
  font-weight: bold;
}

.input-field {
  padding: 12px 16px;
  border: 2px solid #E5C29F;
  border-radius: 10px;
  font-size: 1rem;
  background: white;
  transition: var(--transition-all);
  color: #2C1810;
  width: 100%;
  box-sizing: border-box;
}

/* Estilos específicos para select */
select.input-field {
  cursor: pointer;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%236b7280' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='m6 8 4 4 4-4'/%3e%3c/svg%3e");
  background-position: right 0.5rem center;
  background-repeat: no-repeat;
  background-size: 1.5em 1.5em;
  padding-right: 2.5rem;
}

select.input-field:disabled {
  background-color: var(--beige-claro);
  color: var(--cafe-medio);
  cursor: not-allowed;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%23C8956F' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='m6 8 4 4 4-4'/%3e%3c/svg%3e");
}

.input-field:focus {
  outline: none;
  border-color: #8B5A3C;
  box-shadow: 0 0 12px rgba(139, 90, 60, 0.3);
  transform: scale(1.02);
}

.input-field:hover {
  border-color: #C8956F;
}

.input-field::placeholder {
  color: #C8956F;
  font-style: italic;
}

.select-field {
  cursor: pointer;
}

.select-field option {
  color: #2C1810;
  background: white;
  padding: 8px;
}

/* Botones */
.form-buttons {
  display: flex;
  justify-content: space-between;
  gap: 15px;
  margin-top: 30px;
  padding-top: 20px;
  border-top: 2px solid #E5C29F;
}

.btn {
  padding: 14px 28px;
  border: none;
  border-radius: 12px;
  font-size: 1rem;
  font-weight: bold;
  cursor: pointer;
  transition: var(--transition-all);
  display: flex;
  align-items: center;
  gap: 8px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  min-height: 48px;
  justify-content: center;
}

.btn-submit {
  background: linear-gradient(135deg, #8FAD5A, #4A5D2E);
  color: white;
  box-shadow: 0 6px 15px rgba(143, 173, 90, 0.4);
}

.btn-submit:hover:not(:disabled) {
  background: linear-gradient(135deg, #4A5D2E, #2e3d1a);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(143, 173, 90, 0.5);
}

.btn-submit:disabled {
  background: var(--cafe-claro);
  color: #8B5A3C;
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.btn-secondary {
  background: linear-gradient(135deg, #C8956F, #8B5A3C);
  color: white;
  box-shadow: 0 6px 15px rgba(200, 149, 111, 0.4);
}

.btn-secondary:hover:not(:disabled) {
  background: linear-gradient(135deg, #8B5A3C, #4A2D1A);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(200, 149, 111, 0.5);
}

.btn-cancel {
  background: linear-gradient(135deg, #A52A3D, #8B2332);
  color: white;
  box-shadow: 0 6px 15px rgba(165, 42, 61, 0.4);
}

.btn-cancel:hover:not(:disabled) {
  background: linear-gradient(135deg, #8B2332, #6B1B26);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(165, 42, 61, 0.5);
}

.btn:active {
  transform: translateY(0);
}

.btn:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.btn-icon {
  font-size: 1rem;
}

.loading-spinner {
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

/* Animaciones */
@keyframes fadeIn {
  from { opacity: 0; }
  to { opacity: 1; }
}

@keyframes slideUp {
  from {
    opacity: 0;
    transform: translateY(50px);
  }
  to {
    opacity: 1;
    transform: translateY(0);
  }
}

@keyframes slideIn {
  from {
    opacity: 0;
    transform: translateX(-20px);
  }
  to {
    opacity: 1;
    transform: translateX(0);
  }
}

@keyframes pulse {
  0%, 100% {
    transform: scale(1);
  }
  50% {
    transform: scale(1.02);
  }
}

/* Transiciones Vue */
.fade-enter-active, .fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

/* Responsive */
@media (max-width: 768px) {
  .modal-content {
    padding: 20px;
    margin: 10px;
    max-width: 95%;
  }

  .header-section h2 {
    font-size: 1.5rem;
  }

  .form-grid {
    grid-template-columns: 1fr;
  }

  .form-buttons {
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: center;
  }

  .btn {
    flex: 1;
    min-width: 120px;
    max-width: 150px;
  }
}

@media (max-width: 480px) {
  .form-buttons {
    flex-direction: column;
    align-items: stretch;
  }

  .btn {
    width: 100%;
  }
}

/* Scroll personalizado */
.modal-content::-webkit-scrollbar {
  width: 8px;
}

.modal-content::-webkit-scrollbar-track {
  background: var(--beige-claro);
  border-radius: 4px;
}

.modal-content::-webkit-scrollbar-thumb {
  background: linear-gradient(var(--cafe-medio), var(--cafe-oscuro));
  border-radius: 4px;
}

.modal-content::-webkit-scrollbar-thumb:hover {
  background: linear-gradient(var(--cafe-oscuro), var(--cafe-muy-oscuro));
}
</style>