<template>
  <div class="modal-overlay" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro de Trilla</h2>
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
              <input 
                type="text" 
                v-model="form.lote" 
                required 
                class="input-base"
                placeholder="Ej: 001 o LOTE-2024-001"
                maxlength="50"
                @blur="validarCampo('lote')"
              />
              <span v-if="errors.lote" class="error-text">{{ errors.lote }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Fecha Final de Reposo *</label>
              <input 
                type="date" 
                v-model="form.fechaReposo" 
                required 
                class="input-base"
                :max="fechaMaxima"
                @blur="validarCampo('fechaReposo')"
              />
              <span v-if="errors.fechaReposo" class="error-text">{{ errors.fechaReposo }}</span>
            </div>
          </div>
        </div>

        <!-- Porcentajes de Calidades -->
        <div class="section-card percentages">
          <h3><i class="section-icon">📊</i>Porcentajes de Calidades</h3>
          <div class="percentage-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Los porcentajes deben estar entre 0% y 100%. Total recomendado: 100%
            </small>
            <div class="total-percentage" :class="{ 'total-valid': totalPorcentajes === 100, 'total-warning': totalPorcentajes !== 100 }">
              Total: {{ totalPorcentajes.toFixed(1) }}%
            </div>
          </div>
          <div class="percentages-grid">
            <div v-for="(campo, index) in porcentajesCampos" :key="'perc-'+index" 
                 class="percentage-item">
              <div class="input-group">
                <label class="required-label">{{ campo.label }} *</label>
                <input 
                  type="number" 
                  v-model.number="form[campo.key]" 
                  required
                  :min="0" 
                  :max="100" 
                  step="0.1"
                  @blur="validarPorcentaje(campo.key)" 
                  @input="calcularTotalPorcentajes"
                  class="input-base percentage-input" 
                  placeholder="0.0"
                />
                <span v-if="errors[campo.key]" class="error-text">{{ errors[campo.key] }}</span>
              </div>
            </div>
          </div>
        </div>

        <!-- Rendimientos -->
        <div class="section-card rendimientos">
          <h3><i class="section-icon">⚖️</i>Rendimientos</h3>
          <div class="rendimientos-grid">
            <div class="rendimiento-pair">
              <div class="pair-header">Rendimiento de Pelado</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Teórico (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rendPeladoTeorico" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rendPeladoTeorico')"
                  />
                  <span v-if="errors.rendPeladoTeorico" class="error-text">{{ errors.rendPeladoTeorico }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rendPeladoFinal" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rendPeladoFinal')"
                  />
                  <span v-if="errors.rendPeladoFinal" class="error-text">{{ errors.rendPeladoFinal }}</span>
                </div>
              </div>
            </div>
            
            <div class="rendimiento-pair">
              <div class="pair-header">Rendimiento de Selección</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Teórico (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rendSeleccionTeorico" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rendSeleccionTeorico')"
                  />
                  <span v-if="errors.rendSeleccionTeorico" class="error-text">{{ errors.rendSeleccionTeorico }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rendSeleccionFinal" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rendSeleccionFinal')"
                  />
                  <span v-if="errors.rendSeleccionFinal" class="error-text">{{ errors.rendSeleccionFinal }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Pesos en Verde -->
        <div class="section-card pesos">
          <h3><i class="section-icon">🏋️</i>Pesos en Verde</h3>
          <div class="pesos-grid">
            <div class="peso-pair">
              <div class="pair-header">Peso Verde Básico</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Teórico (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.pesoVerdeTeorico" 
                    required
                    step="0.01" 
                    min="0" 
                    max="999999"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarPeso('pesoVerdeTeorico')"
                  />
                  <span v-if="errors.pesoVerdeTeorico" class="error-text">{{ errors.pesoVerdeTeorico }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.pesoVerdeFinal" 
                    required
                    step="0.01" 
                    min="0" 
                    max="999999"
                    class="input-base" 
                    placeholder="0.00"
                    @blur="validarPeso('pesoVerdeFinal')"
                  />
                  <span v-if="errors.pesoVerdeFinal" class="error-text">{{ errors.pesoVerdeFinal }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Humedad -->
        <div class="section-card humidity">
          <h3><i class="section-icon">💧</i>Control de Humedad</h3>
          <div class="humidity-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Ingrese los valores de humedad medidos durante el proceso
            </small>
          </div>
          <div class="humidity-measurements">
            <div class="humidity-pair">
              <div class="pair-header">Mediciones de Humedad</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label class="required-label">Inicial - Pergamino/Bellota (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.humedadInicial" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarHumedad('humedadInicial')" 
                    placeholder="0.0" 
                    class="input-base" 
                  />
                  <span v-if="errors.humedadInicial" class="error-text">{{ errors.humedadInicial }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final - Verde (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.humedadFinal" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarHumedad('humedadFinal')" 
                    placeholder="0.0" 
                    class="input-base" 
                  />
                  <span v-if="errors.humedadFinal" class="error-text">{{ errors.humedadFinal }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Peso Verde Exportación -->
        <div class="section-card exportacion">
          <h3><i class="section-icon">📦</i>Peso Verde para Exportación</h3>
          <div class="exportacion-grid">
            <div class="input-group">
              <label class="required-label">Peso Verde Final Exportación (kg) *</label>
              <input 
                type="number" 
                v-model.number="form.pesoVerdeExportacion" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-base" 
                placeholder="0.00"
                @blur="validarPeso('pesoVerdeExportacion')"
              />
              <span v-if="errors.pesoVerdeExportacion" class="error-text">{{ errors.pesoVerdeExportacion }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Peso Verde Inferiores (kg) *</label>
              <input 
                type="number" 
                v-model.number="form.pesoVerdeInferiores" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-base" 
                placeholder="0.00"
                @blur="validarPeso('pesoVerdeInferiores')"
              />
              <span v-if="errors.pesoVerdeInferiores" class="error-text">{{ errors.pesoVerdeInferiores }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Peso Verde Final Inferior (kg) *</label>
              <input 
                type="number" 
                v-model.number="form.pesoVerdeFinalInferior" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-base" 
                placeholder="0.00"
                @blur="validarPeso('pesoVerdeFinalInferior')"
              />
              <span v-if="errors.pesoVerdeFinalInferior" class="error-text">{{ errors.pesoVerdeFinalInferior }}</span>
            </div>
          </div>
        </div>

        <!-- Botones -->
        <div class="form-buttons">
          <button type="submit" class="btn-base btn-primary" :disabled="!formularioValido">
            <i class="btn-icon" v-if="!guardandoRegistro">💾</i>
            <i class="btn-icon loading-spinner" v-else>⏳</i>
            {{ guardandoRegistro ? 'Guardando...' : 'Guardar Registro' }}
          </button>
          <button type="button" @click="limpiarFormulario" class="btn-base btn-secondary">
            <i class="btn-icon">🔄</i>
            Limpiar
          </button>
          <button type="button" @click="cancelar" class="btn-base btn-cancel">
            <i class="btn-icon">✕</i>
            Cancelar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "RegistroTrilla",
  data() {
    return {
      form: {
        // Datos básicos
        lote: "",
        fechaReposo: "",

        // Porcentajes de calidades
        segundas: null,
        menudos: null,
        inferiores: null,
        madres: null,
        primera: null,
        caracolillo: null,
        escogeduras: null,
        barreduras: null,
        caraduras: null,

        // Rendimientos
        rendPeladoTeorico: null,
        rendSeleccionTeorico: null,
        rendPeladoFinal: null,
        rendSeleccionFinal: null,

        // Pesos en verde
        pesoVerdeTeorico: null,
        pesoVerdeFinal: null,

        // Humedad
        humedadInicial: null,
        humedadFinal: null,

        // Exportación
        pesoVerdeExportacion: null,
        pesoVerdeInferiores: null,
        pesoVerdeFinalInferior: null,
      },
      porcentajesCampos: [
        { key: 'primera', label: '% Primera' },
        { key: 'segundas', label: '% Segundas' },
        { key: 'caracolillo', label: '% Caracolillo' },
        { key: 'menudos', label: '% Menudos' },
        { key: 'inferiores', label: '% Inferiores' },
        { key: 'madres', label: '% Madres' },
        { key: 'escogeduras', label: '% Escogeduras' },
        { key: 'barreduras', label: '% Barreduras' },
        { key: 'caraduras', label: '% Caraduras' }
      ],
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      totalPorcentajes: 0,
      guardandoRegistro: false
    };
  },
  computed: {
    fechaMaxima() {
      return new Date().toISOString().split('T')[0];
    },
    // La validez del formulario ahora depende de que no haya errores
    formularioValido() {
      return Object.keys(this.errors).length === 0 && !this.guardandoRegistro;
    }
  },
  methods: {
    // Método genérico para validar campos de texto y fecha
    validarCampo(campo) {
      if (!this.form[campo] || this.form[campo].toString().trim() === '') {
        this.errors[campo] = `El campo es requerido`;
      } else if (campo === 'fechaReposo' && new Date(this.form[campo]) > new Date()) {
        this.errors[campo] = 'La fecha no puede ser futura';
      } else {
        // Eliminar el error si el campo es válido
        delete this.errors[campo];
      }
    },

    validarPorcentaje(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'El porcentaje es requerido';
      } else if (valor < 0 || valor > 100) {
        this.errors[campo] = 'El porcentaje debe estar entre 0 y 100';
      } else {
        delete this.errors[campo];
      }
      this.calcularTotalPorcentajes();
    },

    validarRendimiento(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'El rendimiento es requerido';
      } else if (valor < 0 || valor > 100) {
        this.errors[campo] = 'El rendimiento debe estar entre 0 y 100';
      } else {
        delete this.errors[campo];
      }
    },

    validarPeso(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'El peso es requerido';
      } else if (valor < 0) {
        this.errors[campo] = 'El peso no puede ser negativo';
      } else {
        delete this.errors[campo];
      }
    },
    
    validarHumedad(campo) {
      const valor = this.form[campo];
      if (valor === null || valor === undefined || valor === '') {
        this.errors[campo] = 'La humedad es requerida';
      } else if (valor < 0 || valor > 100) {
        this.errors[campo] = 'La humedad debe estar entre 0 y 100';
      } else {
        delete this.errors[campo];
      }
    },

    calcularTotalPorcentajes() {
      this.totalPorcentajes = this.porcentajesCampos.reduce((total, campo) => {
        const valor = this.form[campo.key];
        return total + (valor || 0);
      }, 0);
    },

    validarFormularioCompleto() {
      // Limpiar errores previos
      this.errors = {};
      
      // Validar todos los campos del formulario
      // Usamos Object.keys para iterar sobre todas las propiedades del objeto `form`
      Object.keys(this.form).forEach(campo => {
        // Aquí se valida cada tipo de campo con el método apropiado
        // Si un campo es de texto o fecha
        if (campo === 'lote' || campo === 'fechaReposo') {
            this.validarCampo(campo);
        // Si un campo es de porcentaje
        } else if (this.porcentajesCampos.some(p => p.key === campo)) {
            this.validarPorcentaje(campo);
        // Si es de rendimiento
        } else if (campo.includes('rend')) {
            this.validarRendimiento(campo);
        // Si es de peso
        } else if (campo.includes('pesoVerde')) {
            this.validarPeso(campo);
        // Si es de humedad
        } else if (campo.includes('humedad')) {
            this.validarHumedad(campo);
        }
      });
      
      // El formulario es válido si no hay errores en el objeto 'errors'
      return Object.keys(this.errors).length === 0;
    },

    submitForm() {
      // Prevenir doble envío
      if (this.guardandoRegistro) {
        return;
      }
      
      // Se llama al nuevo método de validación que revisa todos los campos
      if (!this.validarFormularioCompleto()) {
        this.mostrarError('Por favor, complete todos los campos obligatorios y corrija los errores.');
        return;
      }

      // Activar estado de guardado
      this.guardandoRegistro = true;

      // =========================================================================
      // CÓDIGO DE BACKEND:
      // Aquí es donde harías la llamada a tu API para guardar los datos.
      // El siguiente código es un ejemplo y DEBE ser reemplazado por tu lógica
      // de conexión a la base de datos (por ejemplo, una llamada HTTP con fetch o Axios).
      // =========================================================================

      // Ejemplo de la llamada a la base de datos:
      // guardarDatosEnBaseDeDatos(this.form)
      //   .then(response => {
      //      this.mostrarExito();
      //      setTimeout(() => {
      //        this.guardandoRegistro = false;
      //        if (this.$router) {
      //          this.$router.push({ name: "HomeView" });
      //        }
      //      }, 4000);
      //   })
      //   .catch(error => {
      //      this.guardandoRegistro = false;
      //      this.mostrarError('Ocurrió un error al guardar los datos.');
      //      console.error("Error al guardar:", error);
      //   });
      
      // Simulación de una llamada asíncrona
      console.log("📋 Datos de Trilla guardados:", {
        ...this.form,
        totalPorcentajes: this.totalPorcentajes,
        fechaRegistro: new Date().toISOString()
      });
      
      this.mostrarExito();
      
      setTimeout(() => {
        this.guardandoRegistro = false;
        if (this.$router) {
          this.$router.push({ name: "HomeView" });
        }
      }, 4000); 
    },

    mostrarError(mensaje) {
      this.errorMessage = mensaje;
      this.showError = true;
      setTimeout(() => {
        this.showError = false;
      }, 4000);
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
        if (key === 'lote' || key === 'fechaReposo') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
      // Limpiar errores
      this.errors = {};
      this.totalPorcentajes = 0;
    },

    cancelar() {
      if (this.$router) {
        this.$router.push({ name: "HomeView" });
      }
    }
  },

  mounted() {
    this.calcularTotalPorcentajes();
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
.modal-overlay {
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

.btn-base {
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

.btn-primary {
  background: linear-gradient(135deg, #8FAD5A, #4A5D2E);
  color: white;
  box-shadow: 0 6px 15px rgba(143, 173, 90, 0.4);
}

.btn-primary:hover:not(:disabled) {
  background: linear-gradient(135deg, #4A5D2E, #2e3d1a);
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(143, 173, 90, 0.5);
}

.btn-primary:disabled {
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

.btn-base:active {
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

  .form-buttons {
    flex-direction: row;
    flex-wrap: wrap;
    justify-content: center;
  }

  .btn-base {
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

  .btn-base {
    width: 100%;
  }
}
</style>