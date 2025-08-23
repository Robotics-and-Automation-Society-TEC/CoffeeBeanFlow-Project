<template>
  <div class="modal" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro de Acopio</h2>
        <div class="coffee-decoration">
          <div class="coffee-bean"></div>
          <div class="coffee-bean"></div>
        </div>
      </div>

      <!-- Mensajes de estado -->
      <transition name="fade">
        <div v-if="showSuccess" class="success-message">
          <i class="icon">✓</i>
          ¡Registro guardado exitosamente!
          <div v-if="idSecadoCreado" class="success-detail">
            ID de Secado asignado: {{ idSecadoCreado }}
          </div>
        </div>
      </transition>

      <transition name="fade">
        <div v-if="showError" class="error-message">
          <i class="icon">⚠</i>
          {{ errorMessage }}
        </div>
      </transition>

      <form @submit.prevent="submitForm" class="form-container">
        <!-- Información General -->
        <div class="section-card basic-info">
          <h3><i class="section-icon">📋</i>Información General</h3>
          <div class="form-grid">
            <div class="input-group">
              <label class="required-label">Número de Lote *</label>
              <input 
                type="text" 
                v-model="form.lote" 
                required 
                class="input-field" 
                placeholder="Ej: L001-2025"
                @blur="validarCampo('lote')"
              />
              <span v-if="errors.lote" class="error-text">{{ errors.lote }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Número de Recibo *</label>
              <input 
                type="number" 
                v-model.number="form.recibo" 
                required 
                class="input-field" 
                placeholder="Ej: 1234"
                @blur="validarCampo('recibo')"
              />
              <span v-if="errors.recibo" class="error-text">{{ errors.recibo }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Productor *</label>
              <input 
                type="text" 
                v-model="form.productor" 
                required 
                class="input-field" 
                placeholder="Nombre del productor"
                @blur="validarCampo('productor')"
              />
              <span v-if="errors.productor" class="error-text">{{ errors.productor }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Finca *</label>
              <input 
                type="text" 
                v-model="form.finca" 
                required 
                class="input-field" 
                placeholder="Nombre de la finca"
                @blur="validarCampo('finca')"
              />
              <span v-if="errors.finca" class="error-text">{{ errors.finca }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Zona *</label>
              <input 
                type="text" 
                v-model="form.zona" 
                required 
                class="input-field" 
                placeholder="Zona geográfica"
                @blur="validarCampo('zona')"
              />
              <span v-if="errors.zona" class="error-text">{{ errors.zona }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Altura (msnm) *</label>
              <input 
                type="number" 
                v-model.number="form.altura" 
                min="0" 
                required 
                class="input-field" 
                placeholder="1200"
                @blur="validarCampo('altura')"
              />
              <span v-if="errors.altura" class="error-text">{{ errors.altura }}</span>
            </div>
          </div>
        </div>

        <!-- Rango de Maduración -->
        <div class="section-card maturation">
          <h3><i class="section-icon">🍒</i>Rango de Maduración</h3>
          <div class="form-grid">
            <div class="input-group">
              <label class="required-label">Rendimiento Objetivo *</label>
              <input 
                type="number" 
                v-model.number="form.rangoObjetivo" 
                step="0.01" 
                min="0" 
                required 
                class="input-field" 
                placeholder="85.5"
                @blur="validarCampo('rangoObjetivo')"
              />
              <span v-if="errors.rangoObjetivo" class="error-text">{{ errors.rangoObjetivo }}</span>
            </div>
            <div class="input-group">
              <label>Rendimiento Sobre Objetivo</label>
              <input 
                type="number" 
                v-model.number="form.sobreObjetivo" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="90.0" 
              />
            </div>
            <div class="input-group">
              <label>Rendimiento Total</label>
              <input 
                type="number" 
                v-model.number="form.rendimientoTotal" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="88.2" 
              />
            </div>
            <div class="input-group">
              <label>Tipo de Despulpado</label>
              <select v-model="form.tipoDespulpado" class="input-field select-field">
                <option value="">Seleccionar tipo</option>
                <option value="despulpado">Despulpado</option>
                <option value="miel">Miel</option>
                <option value="lavado">Lavado</option>
                <option value="natural">Natural</option>
              </select>
            </div>
          </div>

          <!-- Porcentajes en grid separado -->
          <h4>Porcentajes de Calidad (Opcional)</h4>
          <div class="percentage-grid">
            <div class="input-group">
              <label>% Flote</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeFlote" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeFlote')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Vano</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeVano" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeVano')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Broca</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeBroca" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeBroca')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Verde</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeVerde" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeVerde')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Secos</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeSecos" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeSecos')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
          </div>
        </div>

        <!-- Estado del Producto -->
        <div class="section-card status">
          <h3><i class="section-icon">📦</i>Estado del Producto *</h3>
          <div class="status-selector">
            <div class="input-group">
              <label class="required-label">Estado Actual</label>
              <select 
                v-model="form.estadoProducto" 
                required 
                class="input-field select-field"
                @change="validarCampo('estadoProducto')"
              >
                <option disabled value="">Seleccionar estado</option>
                <option value="disponible">Disponible</option>
                <option value="vendido">Vendido</option>
                <option value="en_proceso">En Proceso</option>
              </select>
              <span v-if="errors.estadoProducto" class="error-text">{{ errors.estadoProducto }}</span>
            </div>
            <div class="input-group" v-if="form.estadoProducto === 'disponible'">
              <label>Cantidad Disponible (kg)</label>
              <input 
                type="number" 
                v-model.number="form.cantidadDisponible" 
                min="0" 
                step="0.01" 
                class="input-field" 
                placeholder="1000.5" 
              />
            </div>
          </div>
        </div>

        <!-- Pruebas Físicas -->
        <div class="section-card physical-tests">
          <h3><i class="section-icon">🔬</i>Pruebas Físicas - Beneficiado Húmeda</h3>
          <div class="percentage-grid">
            <div class="input-group">
              <label>% Segundas</label>
              <input 
                type="number" 
                v-model.number="form.porcentajeSegundas" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('porcentajeSegundas')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Daños Mecánicos</label>
              <input 
                type="number" 
                v-model.number="form.danosMecanicos" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('danosMecanicos')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Pulpa en Pergamino</label>
              <input 
                type="number" 
                v-model.number="form.pulpaEnPergamino" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('pulpaEnPergamino')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
            <div class="input-group">
              <label>% Pergamino en Pulpa</label>
              <input 
                type="number" 
                v-model.number="form.pergaminoEnPulpa" 
                min="0" 
                max="100" 
                step="0.1"
                @blur="validarPorcentaje('pergaminoEnPulpa')" 
                class="input-field" 
                placeholder="%" 
              />
            </div>
          </div>
        </div>

        <!-- Pruebas de Densidad -->
        <div class="section-card density">
          <h3><i class="section-icon">⚖️</i>Pruebas de Densidad</h3>
          <div class="form-grid">
            <div class="input-group">
              <label>Densidad de Fruta (g/cm³)</label>
              <input 
                type="number" 
                v-model.number="form.densidadFruta" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="1.05" 
              />
            </div>
            <div class="input-group">
              <label>Densidad Pergamino Húmedo (g/cm³)</label>
              <input 
                type="number" 
                v-model.number="form.densidadPergamino" 
                step="0.01" 
                min="0" 
                class="input-field" 
                placeholder="0.85" 
              />
            </div>
            <div class="input-group info-secado">
              <label>ID Secado</label>
              <div class="id-secado-info">
                <span class="auto-text">Se generará automáticamente</span>
                <i class="info-icon">ℹ️</i>
              </div>
              <small class="info-description">
                Se creará automáticamente un registro de secado para este lote. 
                Podrá completar los datos de secado más tarde.
              </small>
            </div>
          </div>
        </div>
        
        <!-- Botones -->
        <div class="form-buttons">
          <button type="submit" class="btn-base btn-primary" :disabled="!formularioValido || guardandoRegistro">
            <i class="btn-icon" v-if="!guardandoRegistro">💾</i>
            <i class="btn-icon loading-spinner" v-else>⏳</i>
            {{ guardandoRegistro ? 'Guardando...' : 'Guardar Registro' }}
          </button>
          <button type="button" @click="limpiarFormulario" class="btn-base btn-secondary" :disabled="guardandoRegistro">
            <i class="btn-icon">🔄</i>
            Limpiar
          </button>
          <button type="button" @click="cancelar" class="btn-base btn-cancel" :disabled="guardandoRegistro">
            <i class="btn-icon">✕</i>
            Cancelar
          </button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
import apiService from '@/services/apiService';

export default {
  name: "RegistroAcopio",
  data() {
    return {
      form: {
        // Campos principales
        lote: "",
        recibo: null,
        productor: "",
        finca: "",
        zona: "",
        proceso: "",
        altura: null,
        
        // Rendimientos
        rangoObjetivo: null,
        sobreObjetivo: null,
        rendimientoTotal: null,
        tipoDespulpado: "",
        
        // Porcentajes de calidad (opcionales)
        porcentajeFlote: null,
        porcentajeVano: null,
        porcentajeBroca: null,
        porcentajeVerde: null,
        porcentajeSecos: null,
        
        // Estado del producto
        estadoProducto: "",
        cantidadDisponible: null,
        
        // Pruebas físicas
        porcentajeSegundas: null,
        danosMecanicos: null,
        pulpaEnPergamino: null,
        pergaminoEnPulpa: null,
        
        // Densidades
        densidadFruta: null,
        densidadPergamino: null
      },
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false,
      idSecadoCreado: null // Para mostrar el ID creado
    };
  },
  computed: {
    formularioValido() {
      // Validamos los campos obligatorios
      return Object.keys(this.errors).length === 0 &&
             this.form.lote.trim() !== '' && 
             this.form.recibo !== null && 
             this.form.productor.trim() !== '' && 
             this.form.finca.trim() !== '' && 
             this.form.zona.trim() !== '' && 
             this.form.altura !== null && 
             this.form.rangoObjetivo !== null && 
             this.form.estadoProducto !== '';
    }
  },
  methods: {
    // Validación de campos obligatorios
    validarCampo(campo) {
      if (campo === 'lote') {
        if (!this.form.lote || this.form.lote.trim() === '') {
          this.errors.lote = 'El número de lote es requerido';
        } else if (this.form.lote.length > 50) {
          this.errors.lote = 'El número de lote no puede exceder 50 caracteres';
        } else {
          delete this.errors.lote;
        }
      } else if (campo === 'recibo') {
        if (!this.form.recibo || this.form.recibo <= 0) {
          this.errors.recibo = 'El número de recibo es requerido y debe ser mayor a 0';
        } else {
          delete this.errors.recibo;
        }
      } else if (['productor', 'finca', 'zona'].includes(campo)) {
        if (!this.form[campo] || this.form[campo].trim() === '') {
          this.errors[campo] = 'Este campo es requerido';
        } else {
          delete this.errors[campo];
        }
      } else if (campo === 'altura') {
        if (!this.form.altura || this.form.altura <= 0) {
          this.errors.altura = 'La altura es requerida y debe ser mayor a 0';
        } else {
          delete this.errors.altura;
        }
      } else if (campo === 'rangoObjetivo') {
        if (!this.form.rangoObjetivo || this.form.rangoObjetivo <= 0) {
          this.errors.rangoObjetivo = 'El rendimiento objetivo es requerido';
        } else {
          delete this.errors.rangoObjetivo;
        }
      } else if (campo === 'estadoProducto') {
        if (!this.form.estadoProducto) {
          this.errors.estadoProducto = 'Debe seleccionar un estado del producto';
        } else {
          delete this.errors.estadoProducto;
        }
      }
    },

    validarPorcentaje(campo) {
      const valor = this.form[campo];
      if (valor !== null && valor !== undefined) {
        this.form[campo] = Math.max(0, Math.min(100, valor));
      }
    },

    // Validar todos los campos obligatorios
    validarFormularioCompleto() {
      this.errors = {};
      
      // Validar todos los campos obligatorios
      ['lote', 'recibo', 'productor', 'finca', 'zona', 'altura', 'rangoObjetivo', 'estadoProducto'].forEach(campo => {
        this.validarCampo(campo);
      });
      
      return Object.keys(this.errors).length === 0;
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
      }, 4000);
    },

    limpiarFormulario() {
      // Limpiar todos los campos
      Object.keys(this.form).forEach(key => {
        if (typeof this.form[key] === 'string') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
      // Limpiar errores y estado
      this.errors = {};
      this.idSecadoCreado = null;
    },

    // Método para crear un registro de Secado placeholder
    async crearSecadoPlaceholder(nlote) {
      const fechaActual = new Date().toISOString();
      
      const secadoPlaceholder = {
        Nlote: nlote,
        Finicio: fechaActual, // Fecha placeholder
        Ffinal: fechaActual,   // Fecha placeholder  
        Dsecado: 0,           // Valor placeholder
        Psolar: 0,            // Valor placeholder
        Pmecanico: 0          // Valor placeholder
      };

      console.log("📋 Creando secado placeholder:", secadoPlaceholder);

      try {
        return await apiService.crear('SecadoApi', secadoPlaceholder);
      } catch (error) {
        console.error("❌ Error al crear secado placeholder:", error);
        throw new Error(`Error al crear registro de secado: ${error.message || error}`);
      }
    },

    mapearDatosParaAPI(idSecado) {
      // Mapear los datos del formulario al modelo de la API
      const datos = {
        nlote: this.form.lote,
        nrecibo: this.form.recibo || 0,
        nproductor: this.form.productor,
        nfinca: this.form.finca,
        zona: this.form.zona,
        altura: this.form.altura || 0,
        
        // Rendimientos
        robjetivo: this.form.rangoObjetivo || 0,
        rsobreobjetivo: this.form.sobreObjetivo || 0,
        rtotal: this.form.rendimientoTotal || 0,
        despulpado: this.form.tipoDespulpado || "",
        
        // Estado del producto
        vendido: this.form.estadoProducto === 'vendido',
        disponible: this.form.estadoProducto === 'disponible' ? (this.form.cantidadDisponible || 0) : 0,
        enproceso: this.form.estadoProducto === 'en_proceso' ? "Si" : "No",
        
        // Pruebas físicas
        psegundas: this.form.porcentajeSegundas || 0,
        pdmecanicos: this.form.danosMecanicos || 0,
        ppulpaPergamino: this.form.pulpaEnPergamino || 0,
        ppergaminoPulpa: this.form.pergaminoEnPulpa || 0,
        
        // Densidades
        dfruta: this.form.densidadFruta || 0,
        dpergamino_humedo: this.form.densidadPergamino || 0,
        
        // ID Secado generado automáticamente
        id_Secado: idSecado
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
        console.log("📋 Iniciando proceso de guardado...");
        
        // 1. Crear primero el registro de Secado placeholder
        const secadoCreado = await this.crearSecadoPlaceholder(this.form.lote);
        const idSecado = secadoCreado.ID_Secado || secadoCreado.id_Secado || secadoCreado.idSecado;
        
        if (!idSecado) {
          throw new Error("No se pudo obtener el ID del registro de secado creado");
        }
        
        console.log("✅ Secado placeholder creado con ID:", idSecado);
        this.idSecadoCreado = idSecado;
        
        // 2. Crear el registro de Area_Acopio con el ID_Secado
        const datosAPI = this.mapearDatosParaAPI(idSecado);
        console.log("📋 Enviando datos de Area_Acopio a API:", datosAPI);
        
        const resultadoAcopio = await apiService.crear('Area_Acopio', datosAPI);
        console.log("✅ Area_Acopio creado:", resultadoAcopio);
        
        console.log("🎉 Proceso completado exitosamente");
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
        }, 4000);
        
      } catch (error) {
        console.error("❌ Error al guardar:", error);
        
        let mensajeError = "Error al guardar el registro de acopio";
        
        // Manejar diferentes tipos de errores
        if (error.title) {
          mensajeError = error.title;
        } else if (error.errors) {
          // Errores de validación del modelo
          const errores = Object.values(error.errors).flat();
          mensajeError = errores.join(', ');
        } else if (typeof error === 'string') {
          mensajeError = error;
        } else if (error.message) {
          mensajeError = error.message;
        }
        
        this.mostrarError(mensajeError);
      } finally {
        this.guardandoRegistro = false;
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
:root {
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
  max-width: 900px;
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
.success-message, .error-message {
  padding: 15px 20px;
  border-radius: 12px;
  margin-bottom: 20px;
  display: flex;
  flex-direction: column;
  gap: 8px;
  font-weight: 500;
  animation: slideIn 0.5s ease;
}

.success-message {
  background: linear-gradient(135deg, #8FAD5A, #4A5D2E);
  color: white;
  box-shadow: 0 4px 12px rgba(143, 173, 90, 0.4);
}

.success-detail {
  font-size: 0.9rem;
  background: rgba(255, 255, 255, 0.2);
  padding: 8px 12px;
  border-radius: 6px;
  border-left: 3px solid rgba(255, 255, 255, 0.5);
}

.error-message {
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
  transition: all 0.3s ease;
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

.section-card h4 {
  color: #8B5A3C;
  font-size: 1.1rem;
  margin-bottom: 15px;
  margin-top: 20px;
}

.section-icon {
  font-size: 1.2rem;
}

/* Grids de formulario */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 20px;
}

.percentage-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 16px;
}

/* Grupos de inputs */
.input-group {
  display: flex;
  flex-direction: column;
  gap: 5px;
  min-width: 0;
  width: 100%;
  box-sizing: border-box;
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
  transition: all 0.3s ease;
  color: #2C1810;
  width: 100%;
  box-sizing: border-box;
  min-width: 0;
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
  padding: 8px;
  background: white;
  color: #2C1810;
}

/* Información del ID Secado */
.info-secado {
  background: linear-gradient(145deg, #f8f9fa, #e9ecef);
  border: 2px dashed #8FAD5A;
  border-radius: 12px;
  padding: 16px;
}

.id-secado-info {
  display: flex;
  align-items: center;
  gap: 8px;
  padding: 8px 12px;
  background: linear-gradient(135deg, #8FAD5A, #4A5D2E);
  color: white;
  border-radius: 8px;
  font-weight: 600;
  margin-bottom: 8px;
}

.auto-text {
  flex: 1;
}

.info-icon {
  font-size: 1rem;
}

.info-description {
  color: #4A5D2E;
  font-style: italic;
  font-size: 0.85rem;
  line-height: 1.4;
}

/* Mensajes de error */
.error-text {
  color: var(--color-error);
  font-size: var(--text-xs);
  margin-top: var(--space-xs);
  display: block;
}

/* Status selector especial */
.status-selector {
  display: flex;
  flex-direction: column;
  gap: 15px;
  align-items: center;
}

.status-selector .input-group {
  max-width: 300px;
  width: 100%;
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
  transition: all 0.3s ease;
  display: flex;
  align-items: center;
  gap: 8px;
  text-transform: uppercase;
  letter-spacing: 0.5px;
  min-height: 48px;
  justify-content: center;
  flex: 1;
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

.btn-base:active {
  transform: translateY(0);
}

.btn-base:disabled {
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

/* Estilos para diferentes tipos de sección */
.basic-info {
  border-left: 4px solid #A52A3D;
}

.maturation {
  border-left: 4px solid #8FAD5A;
}

.status {
  border-left: 4px solid #4A5D2E;
}

.physical-tests {
  border-left: 4px solid #8B5A3C;
}

.density {
  border-left: 4px solid #C8956F;
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

  .percentage-grid {
    grid-template-columns: repeat(2, 1fr);
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
  .percentage-grid {
    grid-template-columns: 1fr;
  }

  .form-buttons {
    flex-direction: column;
    align-items: stretch;
  }

  .btn-base {
    width: 100%;
  }
}

/* Scroll personalizado */
.modal-content::-webkit-scrollbar {
  width: 8px;
}

.modal-content::-webkit-scrollbar-track {
  background: #F4F0E6;
  border-radius: 4px;
}

.modal-content::-webkit-scrollbar-thumb {
  background: linear-gradient(#C8956F, #8B5A3C);
  border-radius: 4px;
}

.modal-content::-webkit-scrollbar-thumb:hover {
  background: linear-gradient(#8B5A3C, #4A2D1A);
}
</style>