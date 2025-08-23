<template>
  <div class="modal" @click="cancelar">
    <div class="modal-content" @click.stop>
      <div class="header-section">
        <h2>Registro de Secado</h2>
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
              <label class="required-label">Fecha Inicio de Secado *</label>
              <input 
                type="date" 
                v-model="form.finicio" 
                @change="calcularDiasSecado" 
                required 
                class="input-field"
                :max="fechaMaxima"
                @blur="validarCampo('finicio')"
              />
              <span v-if="errors.finicio" class="error-text">{{ errors.finicio }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Fecha Final *</label>
              <input 
                type="date" 
                v-model="form.ffinal" 
                @change="calcularDiasSecado" 
                required 
                class="input-field"
                :max="fechaMaxima"
                @blur="validarCampo('ffinal')"
              />
              <span v-if="errors.ffinal" class="error-text">{{ errors.ffinal }}</span>
            </div>
            <div class="input-group">
              <label>Días de Secado</label>
              <input 
                type="number" 
                :value="diasSecado" 
                readonly 
                class="input-field readonly" 
                placeholder="Calculado automáticamente"
              />
            </div>
          </div>
        </div>

        <!-- Porcentajes -->
        <div class="section-card percentages">
          <h3><i class="section-icon">📊</i>Porcentajes de Proceso</h3>
          <div class="percentage-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Los porcentajes deben estar entre 0% y 100%
            </small>
            <div class="total-percentage" :class="{ 'total-valid': totalPorcentajes === 100, 'total-warning': totalPorcentajes !== 100 && totalPorcentajes > 0 }">
              Total: {{ totalPorcentajes.toFixed(1) }}%
            </div>
          </div>
          <div class="form-grid">
            <div class="input-group">
              <label class="required-label">Porcentaje Mecánico (%) *</label>
              <input 
                type="number" 
                v-model.number="form.pmecanico" 
                required
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('pmecanico')" 
                @input="calcularTotalPorcentajes"
                class="input-field percentage-input"
                placeholder="0.0"
              />
              <span v-if="errors.pmecanico" class="error-text">{{ errors.pmecanico }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Porcentaje Solar (%) *</label>
              <input 
                type="number" 
                v-model.number="form.psolar" 
                required
                min="0" 
                max="100" 
                step="0.1" 
                @blur="validarPorcentaje('psolar')" 
                @input="calcularTotalPorcentajes"
                class="input-field percentage-input"
                placeholder="0.0"
              />
              <span v-if="errors.psolar" class="error-text">{{ errors.psolar }}</span>
            </div>
          </div>
        </div>

        <!-- Temperaturas de secado -->
        <div class="section-card temperatures">
          <h3><i class="section-icon">🌡️</i>Temperaturas de Secado (°C)</h3>
          <div class="temp-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Registre las temperaturas medidas durante el proceso de secado
            </small>
          </div>
          <div class="measurement-grid">
            <div v-for="(temp, index) in form.temperaturasSecado" :key="'temp-'+index" class="measurement-item">
              <div class="input-group">
                <label>Medición {{ index + 1 }}</label>
                <input 
                  type="number" 
                  v-model.number="form.temperaturasSecado[index]" 
                  step="0.1" 
                  placeholder="0.0°C" 
                  class="input-field temp-input"
                  @blur="validarTemperatura(index)"
                />
                <span v-if="errors['temp_' + index]" class="error-text">{{ errors['temp_' + index] }}</span>
              </div>
            </div>
          </div>
        </div>

        <!-- Mediciones de humedad -->
        <div class="section-card humidity">
          <h3><i class="section-icon">💧</i>Humedad y Temperatura</h3>
          <div class="humidity-info">
            <small class="info-text">
              <i class="info-icon">ℹ️</i>
              Registre las mediciones de humedad y temperatura durante el secado
            </small>
          </div>
          <div class="humidity-measurements">
            <div v-for="(medicion, index) in form.medicionesHumedad" :key="'med-'+index" 
                 class="humidity-pair">
              <div class="pair-header">Medición {{ index + 1 }}</div>
              <div class="pair-inputs-horizontal">
                <div class="input-group">
                  <label>Humedad (%)</label>
                  <input 
                    type="number" 
                    v-model.number="medicion.phumedad" 
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarHumedadMedicion(index, 'phumedad')" 
                    placeholder="0.0%" 
                    class="input-field" 
                  />
                  <span v-if="errors['humedad_' + index]" class="error-text">{{ errors['humedad_' + index] }}</span>
                </div>
                <div class="input-group">
                  <label>Temperatura (°C)</label>
                  <input 
                    type="number" 
                    v-model.number="medicion.temperatura" 
                    step="0.1" 
                    placeholder="0.0°C" 
                    class="input-field"
                    @blur="validarTemperaturaMedicion(index)"
                  />
                  <span v-if="errors['temp_med_' + index]" class="error-text">{{ errors['temp_med_' + index] }}</span>
                </div>
              </div>
            </div>
          </div>
        </div>

        <!-- Termohigrometría -->
        <div class="section-card thermo">
          <h3><i class="section-icon">🔬</i>Termohigrometría</h3>
          
          <div class="thermo-section">
            <div class="thermo-info">
              <small class="info-text">
                <i class="info-icon">ℹ️</i>
                Mediciones de humedad relativa y temperaturas internas/externas
              </small>
            </div>
            <h4>Mediciones de Humedad Relativa y Temperaturas</h4>
            <div class="thermo-measurements">
              <div v-for="(medicion, index) in form.termoHigro.mediciones" :key="'thermo-'+index" 
                   class="thermo-pair">
                <div class="pair-header">Medición {{ index + 1 }}</div>
                <div class="thermo-inputs">
                  <div class="input-group">
                    <label>Humedad Relativa (%)</label>
                    <input 
                      type="number" 
                      v-model.number="medicion.hrelativa" 
                      min="0" 
                      max="100" 
                      step="0.1" 
                      @blur="validarHumedadRelativa(index)" 
                      placeholder="0.0%" 
                      class="input-field" 
                    />
                    <span v-if="errors['hr_' + index]" class="error-text">{{ errors['hr_' + index] }}</span>
                  </div>
                  <div class="input-group">
                    <label>Temperatura Interna (°C)</label>
                    <input 
                      type="number" 
                      v-model.number="medicion.tinterna" 
                      step="0.1" 
                      placeholder="0.0°C" 
                      class="input-field"
                      @blur="validarTempInterna(index)"
                    />
                    <span v-if="errors['temp_int_' + index]" class="error-text">{{ errors['temp_int_' + index] }}</span>
                  </div>
                  <div class="input-group">
                    <label>Temperatura Externa (°C)</label>
                    <input 
                      type="number" 
                      v-model.number="medicion.texterna" 
                      step="0.1" 
                      placeholder="0.0°C" 
                      class="input-field"
                      @blur="validarTempExterna(index)"
                    />
                    <span v-if="errors['temp_ext_' + index]" class="error-text">{{ errors['temp_ext_' + index] }}</span>
                  </div>
                </div>
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
  name: "RegistroSecado",
  data() {
    return {
      form: {
        nlote: "",
        finicio: "",
        ffinal: "",
        pmecanico: null,
        psolar: null,
        temperaturasSecado: Array.from({ length: 6 }, () => null),
        medicionesHumedad: Array.from({ length: 6 }, () => ({ 
          phumedad: null, 
          temperatura: null 
        })),
        termoHigro: {
          mediciones: Array.from({ length: 6 }, () => ({
            hrelativa: null,
            tinterna: null,
            texterna: null,
          })),
        },
      },
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      guardandoRegistro: false,
      totalPorcentajes: 0,
      lotes: [], // Lista de lotes disponibles
      cargandoLotes: false,
      apiLotesDisponible: true // Flag para saber si la API de lotes funciona
    };
  },
  computed: {
    diasSecado() {
      if (this.form.finicio && this.form.ffinal) {
        const inicio = new Date(this.form.finicio);
        const final = new Date(this.form.ffinal);
        const diferencia = Math.ceil((final - inicio) / (1000 * 60 * 60 * 24));
        return diferencia >= 0 ? diferencia : '';
      }
      return '';
    },
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
    // Validación de campos básicos
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
      } else {
        if (!this.form[campo] || this.form[campo].toString().trim() === '') {
          this.errors[campo] = 'El campo es requerido';
        } else if (campo.includes('fecha') && new Date(this.form[campo]) > new Date()) {
          this.errors[campo] = 'La fecha no puede ser futura';
        } else {
          delete this.errors[campo];
        }
      }
    },

    // Validación de porcentajes
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

    // Validación de temperaturas individuales
    validarTemperatura(index) {
      const valor = this.form.temperaturasSecado[index];
      const key = 'temp_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < -50 || valor > 200) {
          this.errors[key] = 'Temperatura fuera del rango válido (-50°C a 200°C)';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    // Validación de humedad en mediciones
    validarHumedadMedicion(index, tipo) {
      const valor = this.form.medicionesHumedad[index][tipo];
      const key = 'humedad_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < 0 || valor > 100) {
          this.errors[key] = 'La humedad debe estar entre 0 y 100%';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    // Validación de temperatura en mediciones
    validarTemperaturaMedicion(index) {
      const valor = this.form.medicionesHumedad[index].temperatura;
      const key = 'temp_med_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < -50 || valor > 200) {
          this.errors[key] = 'Temperatura fuera del rango válido';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    // Validación de humedad relativa en termohigrometría
    validarHumedadRelativa(index) {
      const valor = this.form.termoHigro.mediciones[index].hrelativa;
      const key = 'hr_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < 0 || valor > 100) {
          this.errors[key] = 'La humedad relativa debe estar entre 0 y 100%';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    // Validación de temperatura interna
    validarTempInterna(index) {
      const valor = this.form.termoHigro.mediciones[index].tinterna;
      const key = 'temp_int_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < -50 || valor > 200) {
          this.errors[key] = 'Temperatura fuera del rango válido';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    // Validación de temperatura externa
    validarTempExterna(index) {
      const valor = this.form.termoHigro.mediciones[index].texterna;
      const key = 'temp_ext_' + index;
      
      if (valor !== null && valor !== undefined && valor !== '') {
        if (valor < -50 || valor > 200) {
          this.errors[key] = 'Temperatura fuera del rango válido';
        } else {
          delete this.errors[key];
        }
      } else {
        delete this.errors[key];
      }
    },

    calcularTotalPorcentajes() {
      this.totalPorcentajes = (this.form.pmecanico || 0) + (this.form.psolar || 0);
    },

    calcularDiasSecado() {
      if (this.form.finicio && this.form.ffinal) {
        const inicio = new Date(this.form.finicio);
        const final = new Date(this.form.ffinal);
        
        if (final < inicio) {
          this.errors.ffinal = 'La fecha final debe ser posterior a la fecha de inicio';
        } else {
          delete this.errors.ffinal;
          delete this.errors.finicio;
        }
      }
    },

    // Validación completa del formulario
    validarFormularioCompleto() {
      this.errors = {};
      
      // Validar campos básicos obligatorios
      ['nlote', 'finicio', 'ffinal'].forEach(campo => {
        this.validarCampo(campo);
      });

      // Validar porcentajes obligatorios
      ['pmecanico', 'psolar'].forEach(campo => {
        this.validarPorcentaje(campo);
      });

      // Validar fechas
      this.calcularDiasSecado();

      // Validar todas las temperaturas que tienen valor
      this.form.temperaturasSecado.forEach((temp, index) => {
        this.validarTemperatura(index);
      });

      // Validar mediciones de humedad
      this.form.medicionesHumedad.forEach((medicion, index) => {
        this.validarHumedadMedicion(index, 'phumedad');
        this.validarTemperaturaMedicion(index);
      });

      // Validar termohigrometría
      this.form.termoHigro.mediciones.forEach((medicion, index) => {
        this.validarHumedadRelativa(index);
        this.validarTempInterna(index);
        this.validarTempExterna(index);
      });

      return Object.keys(this.errors).length === 0;
    },

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

    // Método para crear registro de Secado
    async crearSecado() {
      const secadoData = {
        Nlote: this.form.nlote, // CORREGIDO: usar Nlote en mayúsculas
        Finicio: new Date(this.form.finicio).toISOString(), // CORREGIDO: usar Finicio en mayúsculas
        Ffinal: new Date(this.form.ffinal).toISOString(), // CORREGIDO: usar Ffinal en mayúsculas
        Dsecado: this.diasSecado, // CORREGIDO: usar Dsecado en mayúsculas
        Psolar: this.form.psolar, // CORREGIDO: usar Psolar en mayúsculas
        Pmecanico: this.form.pmecanico // CORREGIDO: usar Pmecanico en mayúsculas
      };

      console.log("📋 Creando secado:", secadoData);

      try {
        return await apiService.crear('SecadoApi', secadoData);
      } catch (error) {
        console.error("❌ Error al crear secado:", secadoData, error);
        throw new Error(`Error al crear secado: ${error.message || error}`);
      }
    },

    // Método para crear temperaturas de secado
    async crearTemperaturasSecado(idSecado) {
      const temperaturas = this.form.temperaturasSecado.filter(temp => 
        temp !== null && temp !== undefined && temp !== ''
      );

      const promises = temperaturas.map(async (lectura) => {
        const tempData = {
          lectura: parseInt(lectura),
          ID_Secado: idSecado // CORREGIDO: usar ID_Secado en mayúsculas
        };

        console.log("🌡️ Creando temperatura:", tempData);

        try {
          return await apiService.crear('TemperaturaSecadoApi', tempData);
        } catch (error) {
          console.error("❌ Error al crear temperatura:", tempData, error);
          throw new Error(`Error al crear temperatura: ${error.message || error}`);
        }
      });

      return Promise.all(promises);
    },

    // Método para crear mediciones de humedad
    async crearMedicionesHumedad(idSecado) {
      const mediciones = this.form.medicionesHumedad.filter(medicion => 
        (medicion.phumedad !== null && medicion.phumedad !== undefined && medicion.phumedad !== '') ||
        (medicion.temperatura !== null && medicion.temperatura !== undefined && medicion.temperatura !== '')
      );

      const promises = mediciones.map(async (medicion) => {
        const humedadData = {
          PHumedad: medicion.phumedad || 0, // CORREGIDO: usar PHumedad en mayúsculas
          Temperatura: parseInt(medicion.temperatura || 0), // CORREGIDO: usar Temperatura en mayúsculas
          ID_Secado: idSecado // CORREGIDO: usar ID_Secado en mayúsculas
        };

        console.log("💧 Creando humedad:", humedadData);

        try {
          return await apiService.crear('HumedadApi', humedadData);
        } catch (error) {
          console.error("❌ Error al crear humedad:", humedadData, error);
          throw new Error(`Error al crear humedad: ${error.message || error}`);
        }
      });

      return Promise.all(promises);
    },

    // Método para crear termohigrometría
    async crearTermoHigrometria(idSecado) {
      const mediciones = this.form.termoHigro.mediciones.filter(medicion => 
        (medicion.hrelativa !== null && medicion.hrelativa !== undefined && medicion.hrelativa !== '') ||
        (medicion.tinterna !== null && medicion.tinterna !== undefined && medicion.tinterna !== '') ||
        (medicion.texterna !== null && medicion.texterna !== undefined && medicion.texterna !== '')
      );

      const promises = mediciones.map(async (medicion) => {
        const termoData = {
          Hrelativa: medicion.hrelativa || 0, // CORREGIDO: usar Hrelativa en mayúsculas
          Tinterna: parseInt(medicion.tinterna || 0), // CORREGIDO: usar Tinterna en mayúsculas
          Texterna: parseInt(medicion.texterna || 0), // CORREGIDO: usar Texterna en mayúsculas
          ID_Secado: idSecado // CORREGIDO: usar ID_Secado en mayúsculas
        };

        console.log("🔬 Creando termohigrometría:", termoData);

        try {
          return await apiService.crear('TermoHigrometriaApi', termoData);
        } catch (error) {
          console.error("❌ Error al crear termohigrometría:", termoData, error);
          throw new Error(`Error al crear termohigrometría: ${error.message || error}`);
        }
      });

      return Promise.all(promises);
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
        console.log("📋 Iniciando guardado de datos de secado...");

        // 1. Crear el registro principal de Secado
        const secadoCreado = await this.crearSecado();
        
        // IMPORTANTE: Verificar diferentes posibles nombres de la llave primaria
        const idSecado = secadoCreado.ID_Secado || secadoCreado.id_Secado || secadoCreado.idSecado;
        
        if (!idSecado) {
          console.error("❌ No se pudo obtener el ID del secado creado:", secadoCreado);
          throw new Error("No se pudo obtener el ID del registro de secado creado");
        }
        
        console.log("✅ Secado creado con ID:", idSecado);
        console.log("🔍 Objeto completo del secado:", secadoCreado);

        // 2. Crear temperaturas de secado (si existen)
        const temperaturasCreadas = await this.crearTemperaturasSecado(idSecado);
        console.log("✅ Temperaturas creadas:", temperaturasCreadas.length);

        // 3. Crear mediciones de humedad (si existen)
        const humedadesCreadas = await this.crearMedicionesHumedad(idSecado);
        console.log("✅ Mediciones de humedad creadas:", humedadesCreadas.length);

        // 4. Crear termohigrometría (si existen)
        const termohigrometriaCreada = await this.crearTermoHigrometria(idSecado);
        console.log("✅ Termohigrometría creada:", termohigrometriaCreada.length);

        console.log("🎉 Todos los datos guardados exitosamente");
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
      // Limpiar campos básicos
      this.form.nlote = '';
      this.form.finicio = '';
      this.form.ffinal = '';
      this.form.pmecanico = null;
      this.form.psolar = null;

      // Limpiar temperaturas de secado
      this.form.temperaturasSecado = Array.from({ length: 6 }, () => null);

      // Limpiar mediciones de humedad
      this.form.medicionesHumedad = Array.from({ length: 6 }, () => ({ 
        phumedad: null, 
        temperatura: null 
      }));

      // Limpiar termohigrometría
      this.form.termoHigro.mediciones = Array.from({ length: 6 }, () => ({
        hrelativa: null,
        tinterna: null,
        texterna: null,
      }));

      // Limpiar errores y totales
      this.errors = {};
      this.totalPorcentajes = 0;
      
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
    this.calcularTotalPorcentajes();
    this.cargarLotes(); // Cargar lotes al montar el componente
  }
};
</script>

<style scoped>
/* Variables de la paleta de colores de café basada en la imagen */
* {
  --burgundy: #A52A3D;
  --verde-claro: #8FAD5A; 
  --verde-oscuro: #4A5D2E;
  --verde-muy-oscuro: #2e4b03;
  --cafe-claro: #E5C29F;
  --cafe-medio: #C8956F;
  --cafe-oscuro: #8B5A3C;
  --cafe-muy-oscuro: #4A2D1A;
  --negro-cafe: #2C1810;
  --beige-claro: #F4F0E6;
  --blanco-crema: #FEFAF5;
  --color-success: #8FAD5A;
  --color-error: #A52A3D;
  --color-warning: #C8956F;
  --color-info: #4A5D2E;
  --text-primary: #2C1810;
  --text-secondary: #8B5A3C;
  --text-muted: #C8956F;
  --bg-card: #FEFAF5;
  --bg-secondary: #F4F0E6;
  --shadow-sm: 0 2px 4px rgba(74, 45, 26, 0.1);
  --shadow-md: 0 4px 8px rgba(74, 45, 26, 0.15);
  --shadow-lg: 0 8px 16px rgba(74, 45, 26, 0.2);
  --shadow-xl: 0 12px 24px rgba(74, 45, 26, 0.25);
  --shadow-2xl: 0 20px 40px rgba(74, 45, 26, 0.3);
  --radius-sm: 6px;
  --radius-md: 8px;
  --radius-lg: 12px;
  --radius-xl: 16px;
  --radius-2xl: 20px;
  --radius-3xl: 24px;
  --space-xs: 4px;
  --space-sm: 8px;
  --space-md: 12px;
  --space-lg: 16px;
  --space-xl: 24px;
  --space-2xl: 32px;
  --space-3xl: 48px;
  --text-xs: 0.75rem;
  --text-sm: 0.875rem;
  --text-base: 1rem;
  --text-lg: 1.125rem;
  --text-xl: 1.25rem;
  --text-2xl: 1.5rem;
  --text-3xl: 1.875rem;
  --text-4xl: 2.25rem;
  --transition-fast: 0.15s ease;
  --transition-normal: 0.3s ease;
  --transition-slow: 0.5s ease;
  --transition-all: all 0.3s ease;
}

.modal {
  position: fixed;
  top: 0; left: 0;
  width: 100%; height: 100%;
  background: linear-gradient(135deg, var(--verde-claro) 1%, var(--verde-oscuro) 50%, var(--verde-muy-oscuro) 99%);
  backdrop-filter: blur(8px);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
  animation: fadeIn var(--transition-normal);
}

.modal-content {
  background: linear-gradient(145deg, var(--blanco-crema), var(--beige-claro));
  padding: var(--space-3xl);
  border-radius: var(--radius-3xl);
  max-height: 90vh;
  overflow-y: auto;
  width: 95%;
  max-width: 800px;
  box-shadow: var(--shadow-2xl);
  color: var(--text-primary);
  position: relative;
  animation: slideUp var(--transition-slow);
  scroll-behavior: smooth;
}

.header-section {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: var(--space-3xl);
  padding-bottom: var(--space-lg);
  border-bottom: 3px solid var(--cafe-medio);
}

.header-section h2 {
  color: var(--text-primary);
  font-size: var(--text-4xl);
  font-weight: bold;
  margin: 0;
  text-shadow: 2px 2px 4px rgba(165, 42, 61, 0.1);
}

.coffee-decoration {
  display: flex;
  gap: var(--space-sm);
}

.coffee-bean {
  width: 20px;
  height: 30px;
  background: linear-gradient(45deg, var(--cafe-oscuro), var(--cafe-muy-oscuro));
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
  background: var(--cafe-claro);
  border-radius: 1px;
}

/* Mensajes de estado mejorados */
.message-base {
  padding: var(--space-lg) var(--space-xl);
  border-radius: var(--radius-lg);
  margin-bottom: var(--space-xl);
  display: flex;
  align-items: center;
  gap: var(--space-md);
  font-weight: 600;
  font-size: var(--text-lg);
  animation: slideIn 0.5s ease, pulse 0.6s ease 0.2s;
}

.message-success {
  background: linear-gradient(135deg, var(--color-success), var(--verde-oscuro));
  color: white;
  box-shadow: var(--shadow-md);
}

.message-error {
  background: linear-gradient(135deg, var(--color-error), #8B2332);
  color: white;
  box-shadow: var(--shadow-md);
}

.icon {
  font-size: 1.2rem;
  font-weight: bold;
}

/* Secciones de formulario */
.section-card {
  background: var(--bg-card);
  border-radius: var(--radius-2xl);
  padding: var(--space-2xl);
  margin-bottom: var(--space-2xl);
  box-shadow: var(--shadow-md);
  border: 2px solid transparent;
  transition: var(--transition-all);
  animation: slideIn 0.3s ease;
}

.section-card:hover {
  border-color: var(--cafe-medio);
  transform: translateY(-2px);
  box-shadow: var(--shadow-lg);
}

.section-card h3 {
  color: var(--text-primary);
  font-size: var(--text-xl);
  font-weight: bold;
  margin-bottom: var(--space-xl);
  display: flex;
  align-items: center;
  gap: var(--space-sm);
}

.section-card h4 {
  color: var(--text-secondary);
  font-size: var(--text-lg);
  margin-bottom: var(--space-lg);
  margin-top: var(--space-lg);
}

.section-icon {
  font-size: var(--text-lg);
}

/* Información adicional */
.percentage-info, .humidity-info, .temp-info, .thermo-info {
  background: var(--beige-claro);
  padding: var(--space-md);
  border-radius: var(--radius-lg);
  margin-bottom: var(--space-xl);
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: var(--space-lg);
}

.info-text {
  color: var(--text-secondary);
  font-style: italic;
  display: flex;
  align-items: center;
  gap: var(--space-sm);
}

.info-icon {
  font-size: var(--text-sm);
}

.total-percentage {
  font-weight: 600;
  padding: var(--space-sm) var(--space-md);
  border-radius: var(--radius-md);
  font-size: var(--text-sm);
}

.total-valid {
  background: linear-gradient(135deg, var(--color-success), var(--verde-oscuro));
  color: white;
}

.total-warning {
  background: var(--color-warning);
  color: white;
}

/* Grids de formulario */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: var(--space-xl);
}

.measurement-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(150px, 1fr));
  gap: var(--space-lg);
}

/* Grupos de inputs */
.input-group {
  display: flex;
  flex-direction: column;
  gap: var(--space-xs);
  min-width: 0;
  width: 100%;
  box-sizing: border-box;
}

.input-group label {
  color: var(--text-primary);
  font-weight: 600;
  font-size: var(--text-sm);
}

.required-label::after {
  content: " *";
  color: var(--color-error);
  font-weight: bold;
}

.input-field {
  padding: var(--space-md) var(--space-lg);
  border: 2px solid var(--cafe-claro);
  border-radius: var(--radius-lg);
  font-size: var(--text-base);
  background: white;
  transition: var(--transition-all);
  color: var(--text-primary);
  width: 100%;
  box-sizing: border-box;
  min-width: 0;
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
  color: var(--text-muted);
  cursor: not-allowed;
  background-image: url("data:image/svg+xml,%3csvg xmlns='http://www.w3.org/2000/svg' fill='none' viewBox='0 0 20 20'%3e%3cpath stroke='%23C8956F' stroke-linecap='round' stroke-linejoin='round' stroke-width='1.5' d='m6 8 4 4 4-4'/%3e%3c/svg%3e");
}

.input-field:focus {
  outline: none;
  border-color: var(--cafe-oscuro);
  box-shadow: 0 0 12px rgba(139, 90, 60, 0.3);
  transform: scale(1.02);
}

.input-field:hover {
  border-color: var(--cafe-medio);
}

.input-field:valid {
  border-color: var(--color-success);
}

.input-field:invalid:not(:focus) {
  border-color: var(--color-error);
}

.input-field.readonly {
  background: var(--beige-claro);
  color: var(--text-secondary);
  font-weight: 600;
  cursor: not-allowed;
}

.input-field::placeholder {
  color: var(--text-muted);
  font-style: italic;
  opacity: 0.7;
}

.temp-input, .percentage-input {
  text-align: center;
  font-weight: 500;
}

/* Mensajes de error */
.error-text {
  color: var(--color-error);
  font-size: var(--text-xs);
  margin-top: var(--space-xs);
  display: block;
}

/* Mediciones de humedad */
.humidity-measurements {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(300px, 1fr));
  gap: var(--space-xl);
}

.humidity-pair {
  background: linear-gradient(145deg, #ffffff, var(--beige-claro));
  padding: var(--space-lg);
  border-radius: var(--radius-xl);
  border: 1px solid var(--cafe-claro);
  overflow: hidden;
  width: 100%;
  box-sizing: border-box;
  transition: var(--transition-all);
}

.humidity-pair:hover {
  border-color: var(--cafe-medio);
  transform: translateY(-2px);
  box-shadow: var(--shadow-md);
}

.pair-header {
  font-weight: 600;
  color: var(--text-secondary);
  margin-bottom: var(--space-md);
  text-align: center;
  font-size: var(--text-sm);
}

.pair-inputs-horizontal {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: var(--space-md);
  align-items: end;
  width: 100%;
  box-sizing: border-box;
}

/* Termohigrometría */
.thermo-measurements {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(350px, 1fr));
  gap: var(--space-xl);
}

.thermo-pair {
  background: linear-gradient(145deg, #ffffff, var(--beige-claro));
  padding: var(--space-lg);
  border-radius: var(--radius-xl);
  border: 1px solid var(--cafe-claro);
  overflow: hidden;
  width: 100%;
  box-sizing: border-box;
  transition: var(--transition-all);
}

.thermo-pair:hover {
  border-color: var(--cafe-medio);
  transform: translateY(-2px);
  box-shadow: var(--shadow-md);
}

.thermo-inputs {
  display: grid;
  grid-template-columns: 1fr 1fr 1fr;
  gap: var(--space-md);
  align-items: end;
  width: 100%;
  box-sizing: border-box;
}

.measurement-item {
  display: flex;
  flex-direction: column;
  gap: var(--space-xs);
}

.measurement-item label {
  font-size: var(--text-sm);
  color: var(--text-secondary);
  font-weight: 500;
  text-align: center;
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
  padding: var(--space-lg) var(--space-2xl);
  border: none;
  border-radius: var(--radius-xl);
  font-size: var(--text-base);
  font-weight: bold;
  cursor: pointer;
  transition: var(--transition-all);
  display: flex;
  align-items: center;
  gap: var(--space-sm);
  text-transform: uppercase;
  letter-spacing: 0.5px;
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
  color: var(--text-muted);
  cursor: not-allowed;
  transform: none;
  box-shadow: none;
}

.btn-secondary {
  background: linear-gradient(135deg, var(--cafe-medio), var(--cafe-oscuro));
  color: white;
  box-shadow: 0 6px 15px rgba(200, 149, 111, 0.4);
}

.btn-secondary:hover {
  background: linear-gradient(135deg, var(--cafe-oscuro), var(--cafe-muy-oscuro));
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(200, 149, 111, 0.5);
}

.btn-cancel {
  background: linear-gradient(135deg, #A52A3D, #8B2332);
  color: white;
  box-shadow: 0 6px 15px rgba(165, 42, 61, 0.4);
}

.btn-cancel:hover {
  background: linear-gradient(135deg, var(--cafe-muy-oscuro), var(--negro-cafe));
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(139, 90, 60, 0.5);
}

.btn:active {
  transform: translateY(0);
}

.btn-icon {
  font-size: var(--text-base);
}

/* Estados de carga */
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
  transition: var(--transition-slow);
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
  transform: translateY(-10px);
}

/* Estilos para diferentes tipos de sección */
.basic-info {
  border-left: 4px solid var(--burgundy);
}

.percentages {
  border-left: 4px solid var(--verde-oscuro);
}

.temperatures {
  border-left: 4px solid var(--cafe-oscuro);
}

.humidity {
  border-left: 4px solid var(--cafe-medio);
}

.thermo {
  border-left: 4px solid var(--cafe-muy-oscuro);
}

/* Efectos de accesibilidad */
.btn:focus {
  outline: 3px solid var(--color-info);
  outline-offset: 2px;
}

.input-field:focus {
  outline: none;
  border-color: var(--cafe-oscuro);
  box-shadow: 0 0 12px rgba(139, 90, 60, 0.3);
}

/* Responsive */
@media (max-width: 768px) {
  .modal-content {
    padding: var(--space-xl);
    margin: var(--space-lg);
    max-width: 95%;
  }

  .header-section h2 {
    font-size: var(--text-2xl);
  }

  .form-grid {
    grid-template-columns: 1fr;
  }

  .measurement-grid {
    grid-template-columns: repeat(2, 1fr);
  }

  .humidity-measurements {
    grid-template-columns: 1fr;
  }

  .pair-inputs-horizontal {
    grid-template-columns: 1fr;
    gap: var(--space-sm);
  }

  .thermo-measurements {
    grid-template-columns: 1fr;
  }

  .thermo-inputs {
    grid-template-columns: 1fr;
    gap: var(--space-sm);
  }

  .form-buttons {
    flex-direction: row !important;
    flex-wrap: wrap;
    justify-content: center;
  }

  .btn {
    flex: 1;
    min-width: 120px;
    max-width: 150px;
    justify-content: center;
  }

  .percentage-info, .humidity-info, .temp-info, .thermo-info {
    flex-direction: column;
    text-align: center;
  }
}

@media (max-width: 480px) {
  .measurement-grid {
    grid-template-columns: 1fr;
  }
  
  .pair-inputs-horizontal {
    grid-template-columns: 1fr;
  }
  
  .thermo-inputs {
    grid-template-columns: 1fr;
  }

  .form-buttons {
    flex-direction: column !important;
    align-items: stretch;
  }

  .btn {
    width: 100%;
    justify-content: center;
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

/* Animaciones de entrada para las secciones */
.section-card:nth-child(1) { animation-delay: 0.1s; }
.section-card:nth-child(2) { animation-delay: 0.2s; }
.section-card:nth-child(3) { animation-delay: 0.3s; }
.section-card:nth-child(4) { animation-delay: 0.4s; }
.section-card:nth-child(5) { animation-delay: 0.5s; }
.section-card:nth-child(6) { animation-delay: 0.6s; }
</style>