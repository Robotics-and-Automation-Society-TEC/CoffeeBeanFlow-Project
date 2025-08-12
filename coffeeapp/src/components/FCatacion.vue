<template>
  <div class="modal" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro de Catación</h2>
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
        <!-- Datos Generales -->
        <div class="section-card basic-info">
          <h3><i class="section-icon">📋</i>Datos Generales</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Número de Lote</label>
              <input type="text" v-model="form.lote" required class="input-field" 
                     placeholder="Ingrese el número de lote" />
            </div>
            <div class="input-group">
              <label>Fecha Final de Reposo</label>
              <input type="date" v-model="form.fechaReposo" required class="input-field" />
            </div>
            <div class="input-group">
              <label>Cantidad Defectuosas (Control)</label>
              <input type="number" v-model.number="form.defectuosas" min="0" class="input-field" 
                     placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Limpio</label>
              <input type="text" v-model="form.limpio" class="input-field" 
                     placeholder="Estado de limpieza" />
            </div>
          </div>
        </div>

        <!-- Características Sensoriales -->
        <div class="section-card sensory">
          <h3><i class="section-icon">👃</i>Características Sensoriales</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Olor en Verde</label>
              <select v-model="form.olorVerde" required class="input-field select-field">
                <option disabled value="">Selecciona olor</option>
                <option value="olor limpio">Olor limpio</option>
                <option value="olor extraño">Olor extraño</option>
              </select>
            </div>
            <div class="input-group">
              <label>Quaker</label>
              <input type="number" v-model.number="form.quaker" min="0" class="input-field" 
                     placeholder="Cantidad" />
            </div>
            <div class="input-group">
              <label>Clasificación Color en Verde</label>
              <select v-model="form.colorVerde" class="input-field select-field">
                <option disabled value="">Selecciona color</option>
                <option>Azulado verde</option>
                <option>Verde</option>
                <option>Verdoso</option>
                <option>Amarillo verde</option>
                <option>Amarillo pálido</option>
                <option>Amarillo</option>
                <option>Cafesoso</option>
              </select>
            </div>
          </div>
        </div>

        <!-- Características del Tostado -->
        <div class="section-card roasting">
          <h3><i class="section-icon">🔥</i>Características del Tostado</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Rendimiento Tostado (%)</label>
              <input type="number" v-model.number="form.rendimientoTostado" step="0.01" 
                     @blur="validarPorcentaje('rendimientoTostado')" class="input-field" 
                     placeholder="%" />
            </div>
            <div class="input-group">
              <label>Densidad de Tueste (g/ml)</label>
              <input type="number" v-model.number="form.densidadTueste" step="0.01" class="input-field" 
                     placeholder="Ej: 0.75" />
            </div>
            <div class="input-group">
              <label>Tonalidad Agtron</label>
              <input type="number" v-model.number="form.tonalidadAgtron" class="input-field" 
                     placeholder="Valor Agtron" />
            </div>
            <div class="input-group">
              <label>Clasificación Calidad</label>
              <select v-model="form.clasificacionCalidad" class="input-field select-field">
                <option disabled value="">Selecciona clase</option>
                <option>-80 Clase D</option>
                <option>80-83.99 Clase C</option>
                <option>84-84.99 Clase B</option>
                <option>85-85.99 Clase A</option>
                <option>86-88.99 Clase AA</option>
                <option>89+ Clase AAA</option>
              </select>
            </div>
          </div>
        </div>

        <!-- Clasificación Café Arábica - Categoría 1 -->
        <div class="section-card arabica-c1">
          <h3><i class="section-icon">☕</i>Clasificación Arábica - Categoría 1</h3>
          <div class="defects-grid">
            <div class="defect-pair">
              <div class="pair-header">Defectos Primarios</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label>C1 Negro</label>
                  <input type="number" v-model.number="form.c1Negro" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C1 Agrio</label>
                  <input type="number" v-model.number="form.c1Agrio" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
              </div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label>C1 Cereza Seca</label>
                  <input type="number" v-model.number="form.c1CerezaSeca" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C1 Daño por Hongos</label>
                  <input type="number" v-model.number="form.c1Hongos" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
              </div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label>C1 Insectos Severo</label>
                  <input type="number" v-model.number="form.c1InsectosSevero" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C1 Materia Extraña</label>
                  <input type="number" v-model.number="form.c1MateriaExtraña" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Clasificación Café Arábica - Categoría 2 -->
        <div class="section-card arabica-c2">
          <h3><i class="section-icon">🌱</i>Clasificación Arábica - Categoría 2</h3>
          <div class="defects-grid">
            <div class="defect-pair">
              <div class="pair-header">Defectos Secundarios</div>
              <div class="defects-full-grid">
                <div class="input-group">
                  <label>C2 Negro Parcial</label>
                  <input type="number" v-model.number="form.c2NegroParcial" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Agrio Parcial</label>
                  <input type="number" v-model.number="form.c2AgrioParcial" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Pergamino</label>
                  <input type="number" v-model.number="form.c2Pergamino" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Flotador</label>
                  <input type="number" v-model.number="form.c2Flotador" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Inmaduro</label>
                  <input type="number" v-model.number="form.c2Inmaduro" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Averanado</label>
                  <input type="number" v-model.number="form.c2Averanado" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Concha</label>
                  <input type="number" v-model.number="form.c2Concha" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Partido/Cortado</label>
                  <input type="number" v-model.number="form.c2Partido" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Cáscara/Pulpa</label>
                  <input type="number" v-model.number="form.c2Cascara" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
                <div class="input-group">
                  <label>C2 Insectos Ligeros</label>
                  <input type="number" v-model.number="form.c2InsectosLigeros" min="0" class="input-field defect-input" 
                         placeholder="0" />
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Granulometría -->
        <div class="section-card granulometry">
          <h3><i class="section-icon">⚖️</i>Granulometría</h3>
          <div class="sieve-grid">
            <div class="sieve-item">
              <label>Zaranda #20</label>
              <input type="number" v-model.number="form.z20" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #19</label>
              <input type="number" v-model.number="form.z19" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #18</label>
              <input type="number" v-model.number="form.z18" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #17</label>
              <input type="number" v-model.number="form.z17" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #15</label>
              <input type="number" v-model.number="form.z15" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #14</label>
              <input type="number" v-model.number="form.z14" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #13</label>
              <input type="number" v-model.number="form.z13" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Zaranda #3/16</label>
              <input type="number" v-model.number="form.z316" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
            </div>
            <div class="sieve-item">
              <label>Residuos</label>
              <input type="number" v-model.number="form.residuos" min="0" step="0.1" class="input-field sieve-input" 
                     placeholder="0.0" />
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
  name: "RegistroCatacion",
  data() {
    return {
      form: {
        lote: "",
        fechaReposo: "",
        defectuosas: null,
        limpio: "",
        olorVerde: "",
        quaker: null,
        colorVerde: "",
        rendimientoTostado: null,
        densidadTueste: null,
        tonalidadAgtron: null,
        clasificacionCalidad: "",

        // Clasificación café arábica - Categoría 1
        c1Negro: null,
        c1Agrio: null,
        c1CerezaSeca: null,
        c1Hongos: null,
        c1InsectosSevero: null,
        c1MateriaExtraña: null,

        // Clasificación café arábica - Categoría 2
        c2NegroParcial: null,
        c2AgrioParcial: null,
        c2Pergamino: null,
        c2Flotador: null,
        c2Inmaduro: null,
        c2Averanado: null,
        c2Concha: null,
        c2Partido: null,
        c2Cascara: null,
        c2InsectosLigeros: null,

        // Granulometría
        z20: null,
        z19: null,
        z18: null,
        z17: null,
        z15: null,
        z14: null,
        z13: null,
        z316: null,
        residuos: null,
      },
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false
    };
  },
  computed: {
    formularioValido() {
      // Validación básica: lote, fecha y olor en verde son obligatorios
      return this.form.lote.trim() !== '' && 
             this.form.fechaReposo !== '' && 
             this.form.olorVerde !== '' && 
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

      if (!this.form.fechaReposo) {
        this.mostrarError('La fecha final de reposo es obligatoria');
        return;
      }

      if (!this.form.olorVerde) {
        this.mostrarError('Debe seleccionar el olor en verde');
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

      console.log("📋 Datos de Catación guardados:", {
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
        if (key === 'lote' || key === 'fechaReposo' || key === 'limpio' || key === 'olorVerde' || 
            key === 'colorVerde' || key === 'clasificacionCalidad') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
      // Mostrar confirmación
      this.mostrarError('Formulario limpiado correctamente');
      setTimeout(() => {
        this.showError = false;
      }, 2000);
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