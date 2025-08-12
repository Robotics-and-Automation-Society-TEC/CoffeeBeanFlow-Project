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
              <label>Número de Lote</label>
              <input type="text" v-model="form.lote" required class="input-field" 
                     placeholder="Ingrese el número de lote" />
            </div>
            <div class="input-group">
              <label>Proceso</label>
              <select v-model="form.proceso" required class="input-field select-field">
                <option disabled value="">Selecciona un proceso</option>
                <option value="despulpado">Despulpado</option>
                <option value="miel">Miel</option>
                <option value="lavado">Lavado</option>
                <option value="natural">Natural</option>
              </select>
            </div>
          </div>
        </div>

        <!-- Análisis de cerezas -->
        <div class="section-card cherries">
          <h3><i class="section-icon">🍒</i>Análisis de Cerezas</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Cerezas Inmaduras</label>
              <input type="number" v-model.number="form.inmaduras" min="0" 
                     class="input-field" placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Cerezas Sobremaduras</label>
              <input type="number" v-model.number="form.sobremaduras" min="0" 
                     class="input-field" placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Cerezas Verdes</label>
              <input type="number" v-model.number="form.verdes" min="0" 
                     class="input-field" placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Cerezas Objetivo (Óptimas)</label>
              <input type="number" v-model.number="form.objetivo" min="0" 
                     class="input-field" placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Cerezas Secas</label>
              <input type="number" v-model.number="form.secas" min="0" 
                     class="input-field" placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Rango Óptimo de Maduras</label>
              <input type="text" v-model="form.rangoOptimo" 
                     class="input-field" placeholder="Ej: 18-22 °Brix" />
            </div>
          </div>
        </div>

        <!-- Porcentajes de análisis -->
        <div class="section-card percentages">
          <h3><i class="section-icon">📊</i>Porcentajes de Análisis</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>% Cerezas Verdes</label>
              <input type="number" v-model.number="form.porcVerdes" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('porcVerdes')" 
                     class="input-field" placeholder="%" />
            </div>
            <div class="input-group">
              <label>% Cerezas Secas</label>
              <input type="number" v-model.number="form.porcSecas" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('porcSecas')" 
                     class="input-field" placeholder="%" />
            </div>
            <div class="input-group">
              <label>% Cerezas Objetivo</label>
              <input type="number" v-model.number="form.porcObjetivo" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('porcObjetivo')" 
                     class="input-field" placeholder="%" />
            </div>
            <div class="input-group">
              <label>% Por Debajo de Madurez</label>
              <input type="number" v-model.number="form.porcDebajoObjetivo" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('porcDebajoObjetivo')" 
                     class="input-field" placeholder="%" />
            </div>
          </div>
        </div>

        <!-- Estado y defectos -->
        <div class="section-card defects">
          <h3><i class="section-icon">🔍</i>Estado y Defectos</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Estado de Maduración</label>
              <input type="text" v-model="form.estadoMaduracion" 
                     class="input-field" placeholder="Ej: Óptimo, Inmaduro, Sobremaduro" />
            </div>
            <div class="input-group">
              <label>% Broca</label>
              <input type="number" v-model.number="form.broca" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('broca')" 
                     class="input-field" placeholder="%" />
            </div>
            <div class="input-group">
              <label>% Vanos</label>
              <input type="number" v-model.number="form.vanos" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('vanos')" 
                     class="input-field" placeholder="%" />
            </div>
            <div class="input-group">
              <label>% Secos</label>
              <input type="number" v-model.number="form.secos" min="0" max="100" 
                     step="0.1" @blur="validarPorcentaje('secos')" 
                     class="input-field" placeholder="%" />
            </div>
          </div>
        </div>

        <!-- Densidad -->
        <div class="section-card density">
          <h3><i class="section-icon">⚖️</i>Análisis de Densidad</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Densidad del Grano (g/ml)</label>
              <input type="number" v-model.number="form.densidadGrano" step="0.01" min="0" 
                     class="input-field" placeholder="Ej: 1.25" />
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
  name: "FCaracterizacion",
  data() {
    return {
      form: {
        lote: "",
        proceso: "",
        inmaduras: null,
        sobremaduras: null,
        verdes: null,
        objetivo: null,
        secas: null,
        rangoOptimo: "",
        porcVerdes: null,
        porcSecas: null,
        porcObjetivo: null,
        porcDebajoObjetivo: null,
        estadoMaduracion: "",
        broca: null,
        vanos: null,
        secos: null,
        densidadGrano: null,
      },
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false
    };
  },
  computed: {
    formularioValido() {
      // Validación básica: lote y proceso son obligatorios
      return this.form.lote.trim() !== '' && 
             this.form.proceso !== '' && 
             !this.guardandoRegistro;
    }
  },
  methods: {
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
      }, 4000);
    },

    mostrarExito() {
      this.showSuccess = true;
      setTimeout(() => {
        this.showSuccess = false;
      }, 3000);
    },

    submitForm() {
      // Prevenir doble envío
      if (this.guardandoRegistro) {
        return;
      }

      // Validaciones básicas
      if (!this.form.lote.trim()) {
        this.mostrarError('El número de lote es obligatorio');
        return;
      }

      if (!this.form.proceso) {
        this.mostrarError('Debe seleccionar un proceso');
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

      console.log("📋 Datos Refractómetro guardados:", {
        ...this.form,
        fechaRegistro: new Date().toISOString()
      });
      
      this.mostrarExito();
      
      // Opcional: redirigir después de un delay
      setTimeout(() => {
        this.guardandoRegistro = false;
        if (this.$router) {
          this.$router.push({ name: "HomeView" });
        }
      }, 4000);
    },

    limpiarFormulario() {
      // Limpiar todos los campos
      Object.keys(this.form).forEach(key => {
        if (key === 'lote' || key === 'proceso' || key === 'rangoOptimo' || key === 'estadoMaduracion') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
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