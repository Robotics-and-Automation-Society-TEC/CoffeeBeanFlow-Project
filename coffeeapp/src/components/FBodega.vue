<template>
  <div class="modal" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro de Bodega</h2>
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
        <!-- Datos básicos -->
        <div class="section-card basic-info">
          <h3><i class="section-icon">📋</i>Información Básica</h3>
          <div class="form-grid">
            <div class="input-group">
              <label class="required-label">Número de Lote *</label>
              
              <!-- Select cuando hay lotes cargados -->
              <select 
                v-if="lotes.length > 0"
                v-model="form.nlote" 
                required 
                class="input-field"
                @change="validarCampo('nlote')"
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
                v-model="form.nlote" 
                required 
                class="input-field"
                placeholder="Ej: LOTE-001, LOTE-2024-001"
                maxlength="50"
                @blur="validarCampo('nlote')"
                :disabled="cargandoLotes"
              />
              
              <span v-if="errors.nlote" class="error-text">{{ errors.nlote }}</span>
              
              <!-- Mensajes informativos -->
              <small v-if="cargandoLotes" class="info-text">
                <i class="info-icon">⏳</i>
                Cargando lotes disponibles...
              </small>
              <small v-else-if="!apiLotesDisponible && lotes.length === 0" class="info-text" style="color: var(--color-warning);">
                <i class="info-icon">⚠️</i>
                API de lotes no disponible. Ingrese el número de lote manualmente.
              </small>
              <small v-else-if="lotes.length === 0" class="info-text" style="color: var(--color-info);">
                <i class="info-icon">ℹ️</i>
                No hay lotes disponibles. Ingrese el número de lote manualmente.
              </small>
              <small v-else class="info-text" style="color: var(--color-success);">
                <i class="info-icon">✅</i>
                {{ lotes.length }} lote(s) disponible(s)
              </small>
            </div>
            <div class="input-group">
              <label class="required-label">Fecha Inicio de Reposo *</label>
              <input 
                type="date" 
                v-model="form.finicioReposo" 
                required 
                class="input-field"
                :max="fechaMaxima"
                @blur="validarCampo('finicioReposo')"
              />
              <span v-if="errors.finicioReposo" class="error-text">{{ errors.finicioReposo }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Cantidad de Sacos *</label>
              <input 
                type="number" 
                v-model.number="form.cantidadSacos" 
                required
                min="0" 
                class="input-field"
                placeholder="0"
                @blur="validarNumero('cantidadSacos')"
              />
              <span v-if="errors.cantidadSacos" class="error-text">{{ errors.cantidadSacos }}</span>
            </div>
          </div>
        </div>

        <!-- Densidades -->
        <div class="section-card densities">
          <h3><i class="section-icon">⚖️</i>Mediciones de Densidad</h3>
          <div class="density-measurements">
            <div class="density-pair">
              <div class="pair-header">Control de Densidades</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Densidad Bellota (g/ml) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.dBellota" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-field"
                    placeholder="0.00"
                    @blur="validarNumero('dBellota')"
                  />
                  <span v-if="errors.dBellota" class="error-text">{{ errors.dBellota }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Densidad Pergamino (g/ml) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.dPergamino" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-field"
                    placeholder="0.00"
                    @blur="validarNumero('dPergamino')"
                  />
                  <span v-if="errors.dPergamino" class="error-text">{{ errors.dPergamino }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Control de Humedad -->
        <div class="section-card humidity">
          <h3><i class="section-icon">💧</i>Control de Humedad</h3>
          <div class="humidity-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Los valores de humedad deben estar entre 0% y 100%
            </small>
          </div>
          <div class="humidity-measurements">
            <div class="humidity-pair">
              <div class="pair-header">Mediciones de Humedad</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Humedad Inicial (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.hinicial" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarPorcentaje('hinicial')" 
                    placeholder="0.0" 
                    class="input-field" 
                  />
                  <span v-if="errors.hinicial" class="error-text">{{ errors.hinicial }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Humedad Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.hfinal" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarPorcentaje('hfinal')" 
                    placeholder="0.0" 
                    class="input-field" 
                  />
                  <span v-if="errors.hfinal" class="error-text">{{ errors.hfinal }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Pesos -->
        <div class="section-card weights">
          <h3><i class="section-icon">🏋️</i>Control de Pesos</h3>
          <div class="weight-measurements">
            <div class="weight-pair">
              <div class="pair-header">Registro de Pesos</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Peso Pergamino (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.wPergamino" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-field"
                    placeholder="0.00"
                    @blur="validarNumero('wPergamino')"
                  />
                  <span v-if="errors.wPergamino" class="error-text">{{ errors.wPergamino }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Peso Bellota (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.wBellota" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-field"
                    placeholder="0.00"
                    @blur="validarNumero('wBellota')"
                  />
                  <span v-if="errors.wBellota" class="error-text">{{ errors.wBellota }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Condiciones Ambientales -->
        <div class="section-card environmental">
          <h3><i class="section-icon">🌡️</i>Condiciones Ambientales Promedio Mensual</h3>
          <div class="environmental-grid">
            <div class="environmental-item">
              <div class="input-group">
                <label class="required-label">Humedad Relativa (%) *</label>
                <input 
                  type="number" 
                  v-model.number="form.pmhRelativa" 
                  required
                  min="0" 
                  max="100" 
                  step="0.1" 
                  @blur="validarPorcentaje('pmhRelativa')" 
                  placeholder="0.0" 
                  class="input-field" 
                />
                <span v-if="errors.pmhRelativa" class="error-text">{{ errors.pmhRelativa }}</span>
              </div>
            </div>
            <div class="environmental-item">
              <div class="input-group">
                <label class="required-label">Temperatura Interna (°C) *</label>
                <input 
                  type="number" 
                  v-model.number="form.pmtInterna" 
                  required
                  step="0.1" 
                  placeholder="0.0" 
                  class="input-field"
                  @blur="validarNumero('pmtInterna')"
                />
                <span v-if="errors.pmtInterna" class="error-text">{{ errors.pmtInterna }}</span>
              </div>
            </div>
            <div class="environmental-item">
              <div class="input-group">
                <label class="required-label">Temperatura Externa (°C) *</label>
                <input 
                  type="number" 
                  v-model.number="form.pmtExterna" 
                  required
                  step="0.1" 
                  placeholder="0.0" 
                  class="input-field"
                  @blur="validarNumero('pmtExterna')"
                />
                <span v-if="errors.pmtExterna" class="error-text">{{ errors.pmtExterna }}</span>
              </div>
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
import apiService from '@/services/apiService' // Ajusta la ruta según tu estructura

export default {
  name: "RegistroBodega",
  data() {
    return {
      form: {
        nlote: "",
        dBellota: null,
        dPergamino: null,
        hinicial: null,
        hfinal: null,
        wPergamino: null,
        wBellota: null,
        finicioReposo: "",
        cantidadSacos: null,
        pmhRelativa: null,
        pmtInterna: null,
        pmtExterna: null,
      },
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false,
      lotes: [], // Lista de lotes disponibles
      cargandoLotes: false,
      apiLotesDisponible: true // Flag para saber si la API de lotes funciona
    };
  },
  computed: {
    fechaMaxima() {
      return new Date().toISOString().split('T')[0];
    },
    formularioValido() {
      return Object.keys(this.errors).length === 0 && 
             !this.guardandoRegistro && 
             !this.cargandoLotes;
    }
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
            .map(registro => registro.Nlote || registro.nlote) // CORREGIDO: priorizar Nlote en mayúsculas
            .filter(nlote => nlote && nlote.toString().trim() !== '') // Filtrar valores vacíos
        )];
        
        this.lotes = lotesUnicos.sort(); // Ordenar alfabéticamente
        this.apiLotesDisponible = true;
        
        console.log("✅ Lotes cargados desde Area_Acopio:", this.lotes.length, this.lotes);
        
      } catch (error) {
        console.error("❌ Error al cargar lotes desde Area_Acopio:", error);
        this.lotes = [];
        this.apiLotesDisponible = false;
        console.log("🔄 Cambiando a modo de entrada manual de lotes");
      } finally {
        this.cargandoLotes = false;
      }
    },

    // Método para crear registro de Bodega
    async crearBodega() {
      const bodegaData = {
        Nlote: this.form.nlote,
        D_Bellota: this.form.dBellota,
        D_Pergamino: this.form.dPergamino,
        Hinicial: this.form.hinicial,
        Hfinal: this.form.hfinal,
        W_pergamino: this.form.wPergamino,
        W_bellota: this.form.wBellota,
        FinicioReposo: new Date(this.form.finicioReposo).toISOString(),
        CantidadSacos: this.form.cantidadSacos,
        PMTexterna: this.form.pmtExterna,
        PMTinterna: this.form.pmtInterna,
        PMH_relativa: this.form.pmhRelativa
      };

      console.log("📋 Creando bodega:", bodegaData);

      try {
        return await apiService.crear('BodegaApi', bodegaData);
      } catch (error) {
        console.error("❌ Error al crear bodega:", bodegaData, error);
        throw new Error(`Error al crear bodega: ${error.message || error}`);
      }
    },

    // Validación de campos de texto y fecha
    validarCampo(campo) {
      if (campo === 'nlote') {
        if (!this.form[campo] || this.form[campo].toString().trim() === '') {
          this.errors[campo] = 'Debe seleccionar o ingresar un lote';
        } else if (this.lotes.length > 0 && this.apiLotesDisponible) {
          // Validación estricta cuando hay lotes disponibles en la API
          const loteValido = this.lotes.includes(this.form[campo]);
          if (!loteValido) {
            this.errors[campo] = 'El lote seleccionado no es válido';
          } else {
            delete this.errors[campo];
          }
        } else {
          // Validación básica para entrada manual
          const lote = this.form[campo].toString().trim();
          if (lote.length < 2) {
            this.errors[campo] = 'El número de lote debe tener al menos 2 caracteres';
          } else if (lote.length > 50) {
            this.errors[campo] = 'El número de lote no puede exceder 50 caracteres';
          } else {
            delete this.errors[campo];
          }
        }
      } else if (campo === 'finicioReposo') {
        if (!this.form[campo] || this.form[campo].toString().trim() === '') {
          this.errors[campo] = 'El campo es requerido';
        } else if (new Date(this.form[campo]) > new Date()) {
          this.errors[campo] = 'La fecha no puede ser futura';
        } else {
          delete this.errors[campo];
        }
      } else {
        if (!this.form[campo] || this.form[campo].toString().trim() === '') {
          this.errors[campo] = 'El campo es requerido';
        } else {
          delete this.errors[campo];
        }
      }
    },

    // Validación de porcentajes (0-100)
    validarPorcentaje(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'El porcentaje es requerido';
      } else if (valor < 0 || valor > 100) {
        this.errors[campo] = 'El porcentaje debe estar entre 0 y 100';
      } else {
        delete this.errors[campo];
      }
    },

    // Validación de números (no negativos)
    validarNumero(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'El campo es requerido';
      } else if (valor < 0) {
        this.errors[campo] = 'El valor no puede ser negativo';
      } else {
        delete this.errors[campo];
      }
    },

    // Validación completa del formulario
    validarFormularioCompleto() {
      this.errors = {};
      
      // Campos de texto/fecha
      ['nlote', 'finicioReposo'].forEach(campo => {
        this.validarCampo(campo);
      });

      // Campos de porcentaje
      ['hinicial', 'hfinal', 'pmhRelativa'].forEach(campo => {
        this.validarPorcentaje(campo);
      });

      // Campos numéricos
      ['dBellota', 'dPergamino', 'wPergamino', 'wBellota', 
       'cantidadSacos', 'pmtInterna', 'pmtExterna'].forEach(campo => {
        this.validarNumero(campo);
      });

      return Object.keys(this.errors).length === 0;
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

      try {
        console.log("📋 Iniciando guardado de datos de bodega...");

        // Crear el registro de Bodega
        const bodegaCreada = await this.crearBodega();
        
        // Verificar diferentes posibles nombres de la llave primaria
        const idBodega = bodegaCreada.ID_Bodega || bodegaCreada.id_Bodega || bodegaCreada.idBodega;
        
        if (!idBodega) {
          console.error("❌ No se pudo obtener el ID de la bodega creada:", bodegaCreada);
          throw new Error("No se pudo obtener el ID del registro de bodega creado");
        }
        
        console.log("✅ Bodega creada con ID:", idBodega);
        console.log("🔍 Objeto completo de la bodega:", bodegaCreada);

        console.log("🎉 Datos de bodega guardados exitosamente");
        this.mostrarExito();
        
        // Limpiar formulario después de 3 segundos
        setTimeout(() => {
          this.limpiarFormulario();
          this.guardandoRegistro = false;
          if (this.$router) {
            this.$router.push({ name: "HomeView" });
          }
        }, 3000);

      } catch (error) {
        console.error("❌ Error al guardar:", error);
        
        // Manejar diferentes tipos de errores
        let mensajeError = "Error desconocido";
        
        if (typeof error === 'string') {
          mensajeError = error;
        } else if (error.message) {
          mensajeError = error.message;
        } else if (error.response?.data) {
          mensajeError = error.response.data.message || JSON.stringify(error.response.data);
        } else if (error.data) {
          mensajeError = error.data.message || JSON.stringify(error.data);
        }
        
        this.mostrarError(`Error al guardar los datos: ${mensajeError}`);
        this.guardandoRegistro = false;
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

    limpiarFormulario() {
      // Limpiar todos los campos
      Object.keys(this.form).forEach(key => {
        if (key === 'nlote' || key === 'finicioReposo') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
      // Limpiar errores
      this.errors = {};
      
      // Recargar lotes solo si la API estaba disponible anteriormente
      if (this.apiLotesDisponible && this.lotes.length === 0) {
        this.cargarLotes();
      }
    },

    cancelar() {
      if (this.$router) {
        this.$router.push({ name: "HomeView" });
      }
    }
  },

  mounted() {
    this.cargarLotes(); // Cargar lotes al montar el componente
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

.densities {
  border-left: 4px solid var(--cafe-oscuro);
}

.humidity {
  border-left: 4px solid var(--verde-oscuro);
}

.weights {
  border-left: 4px solid var(--cafe-medio);
}

.environmental {
  border-left: 4px solid var(--cafe-muy-oscuro);
}

/* Grid de formulario */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 20px;
}

.environmental-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
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

/* Mensajes de error */
.error-text {
  color: var(--color-error);
  font-size: var(--text-xs);
  margin-top: var(--space-xs);
  display: block;
}

/* Información adicional */
.humidity-info {
  background: var(--beige-claro);
  padding: var(--space-md);
  border-radius: 8px;
  margin-bottom: var(--space-xl);
}

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

/* Mediciones */
.density-measurements, .humidity-measurements, .weight-measurements {
  display: grid;
  grid-template-columns: 1fr;
  gap: var(--space-xl);
}

.density-pair, .humidity-pair, .weight-pair {
  background: linear-gradient(145deg, #ffffff, var(--beige-claro));
  padding: var(--space-lg);
  border-radius: 12px;
  border: 1px solid var(--cafe-claro);
  transition: var(--transition-all);
}

.density-pair:hover, .humidity-pair:hover, .weight-pair:hover {
  border-color: var(--cafe-medio);
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(74, 45, 26, 0.15);
}

.pair-header {
  font-weight: 600;
  color: var(--cafe-oscuro);
  margin-bottom: var(--space-md);
  text-align: center;
  font-size: var(--text-sm);
}

.pair-inputs-horizontal {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: var(--space-md);
  align-items: end;
}

.environmental-item {
  display: flex;
  flex-direction: column;
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

.btn-secondary:hover {
  background: linear-gradient(135deg, #8B5A3C, #4A2D1A);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(200, 149, 111, 0.5);
}

.btn-cancel {
  background: linear-gradient(135deg, #A52A3D, #8B2332);
  color: white;
  box-shadow: 0 6px 15px rgba(165, 42, 61, 0.4);
}

.btn-cancel:hover {
  background: linear-gradient(135deg, #8B2332, #6B1B26);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(165, 42, 61, 0.5);
}

.btn:active {
  transform: translateY(0);
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

  .environmental-grid {
    grid-template-columns: 1fr;
  }

  .pair-inputs-horizontal {
    grid-template-columns: 1fr;
    gap: var(--space-sm);
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

  .pair-inputs-horizontal {
    grid-template-columns: 1fr;
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