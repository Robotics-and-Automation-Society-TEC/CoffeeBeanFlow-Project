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
              <label class="required-label">Fecha Inicio de Reposo *</label>
              <input 
                type="date" 
                v-model="form.fechaInicioReposo" 
                required 
                class="input-base"
                :max="fechaMaxima"
                @blur="validarCampo('fechaInicioReposo')"
              />
              <span v-if="errors.fechaInicioReposo" class="error-text">{{ errors.fechaInicioReposo }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Cantidad de Sacos *</label>
              <input 
                type="number" 
                v-model.number="form.cantidadSacos" 
                required
                min="0" 
                class="input-base"
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
                    v-model.number="form.densidadBellota" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-base"
                    placeholder="0.00"
                    @blur="validarNumero('densidadBellota')"
                  />
                  <span v-if="errors.densidadBellota" class="error-text">{{ errors.densidadBellota }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Densidad Pergamino (g/ml) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.densidadPergamino" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-base"
                    placeholder="0.00"
                    @blur="validarNumero('densidadPergamino')"
                  />
                  <span v-if="errors.densidadPergamino" class="error-text">{{ errors.densidadPergamino }}</span>
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
                    v-model.number="form.humedadInicial" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarPorcentaje('humedadInicial')" 
                    placeholder="0.0" 
                    class="input-base" 
                  />
                  <span v-if="errors.humedadInicial" class="error-text">{{ errors.humedadInicial }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Humedad Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.humedadFinal" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarPorcentaje('humedadFinal')" 
                    placeholder="0.0" 
                    class="input-base" 
                  />
                  <span v-if="errors.humedadFinal" class="error-text">{{ errors.humedadFinal }}</span>
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
                    v-model.number="form.pesoPergamino" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-base"
                    placeholder="0.00"
                    @blur="validarNumero('pesoPergamino')"
                  />
                  <span v-if="errors.pesoPergamino" class="error-text">{{ errors.pesoPergamino }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Peso Bellota (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.pesoBellota" 
                    required
                    step="0.01" 
                    min="0" 
                    class="input-base"
                    placeholder="0.00"
                    @blur="validarNumero('pesoBellota')"
                  />
                  <span v-if="errors.pesoBellota" class="error-text">{{ errors.pesoBellota }}</span>
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
                  v-model.number="form.promHumedadRelativa" 
                  required
                  min="0" 
                  max="100" 
                  step="0.1" 
                  @blur="validarPorcentaje('promHumedadRelativa')" 
                  placeholder="0.0" 
                  class="input-base" 
                />
                <span v-if="errors.promHumedadRelativa" class="error-text">{{ errors.promHumedadRelativa }}</span>
              </div>
            </div>
            <div class="environmental-item">
              <div class="input-group">
                <label class="required-label">Temperatura Interna (°C) *</label>
                <input 
                  type="number" 
                  v-model.number="form.promTempInterna" 
                  required
                  step="0.1" 
                  placeholder="0.0" 
                  class="input-base"
                  @blur="validarNumero('promTempInterna')"
                />
                <span v-if="errors.promTempInterna" class="error-text">{{ errors.promTempInterna }}</span>
              </div>
            </div>
            <div class="environmental-item">
              <div class="input-group">
                <label class="required-label">Temperatura Externa (°C) *</label>
                <input 
                  type="number" 
                  v-model.number="form.promTempExterna" 
                  required
                  step="0.1" 
                  placeholder="0.0" 
                  class="input-base"
                  @blur="validarNumero('promTempExterna')"
                />
                <span v-if="errors.promTempExterna" class="error-text">{{ errors.promTempExterna }}</span>
              </div>
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
  name: "RegistroBodega",
  data() {
    return {
      form: {
        lote: "",
        densidadBellota: null,
        densidadPergamino: null,
        humedadInicial: null,
        humedadFinal: null,
        pesoPergamino: null,
        pesoBellota: null,
        fechaInicioReposo: "",
        cantidadSacos: null,
        promHumedadRelativa: null,
        promTempInterna: null,
        promTempExterna: null,
      },
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false
    };
  },
  computed: {
    fechaMaxima() {
      return new Date().toISOString().split('T')[0];
    },
    formularioValido() {
      return Object.keys(this.errors).length === 0 && !this.guardandoRegistro;
    }
  },
  methods: {
    // Validación de campos de texto y fecha
    validarCampo(campo) {
      if (!this.form[campo] || this.form[campo].toString().trim() === '') {
        this.errors[campo] = 'El campo es requerido';
      } else if (campo === 'fechaInicioReposo' && new Date(this.form[campo]) > new Date()) {
        this.errors[campo] = 'La fecha no puede ser futura';
      } else {
        delete this.errors[campo];
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
      ['lote', 'fechaInicioReposo'].forEach(campo => {
        this.validarCampo(campo);
      });

      // Campos de porcentaje
      ['humedadInicial', 'humedadFinal', 'promHumedadRelativa'].forEach(campo => {
        this.validarPorcentaje(campo);
      });

      // Campos numéricos
      ['densidadBellota', 'densidadPergamino', 'pesoPergamino', 'pesoBellota', 
       'cantidadSacos', 'promTempInterna', 'promTempExterna'].forEach(campo => {
        this.validarNumero(campo);
      });

      return Object.keys(this.errors).length === 0;
    },

    submitForm() {
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

      // =========================================================================
      // CÓDIGO DE BACKEND:
      // Aquí es donde harías la llamada a tu API para guardar los datos.
      // El siguiente código es un ejemplo y DEBE ser reemplazado por tu lógica
      // de conexión a la base de datos.
      // =========================================================================

      console.log("📋 Datos de Bodega guardados:", {
        ...this.form,
        fechaRegistro: new Date().toISOString()
      });
      
      this.mostrarExito();
      
      // Simulación de guardado
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
        if (key === 'lote' || key === 'fechaInicioReposo') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
      // Limpiar errores
      this.errors = {};
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