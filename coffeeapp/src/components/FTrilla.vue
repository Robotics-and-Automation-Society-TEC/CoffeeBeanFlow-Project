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
              <label class="required-label">Fecha Final de Reposo *</label>
              <input 
                type="date" 
                v-model="form.ffinalReposo" 
                required 
                class="input-field"
                :max="fechaMaxima"
                @blur="validarCampo('ffinalReposo')"
              />
              <span v-if="errors.ffinalReposo" class="error-text">{{ errors.ffinalReposo }}</span>
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
                  class="input-field percentage-input" 
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
                    v-model.number="form.rteoricoPelado" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rteoricoPelado')"
                  />
                  <span v-if="errors.rteoricoPelado" class="error-text">{{ errors.rteoricoPelado }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rfinalPelado" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rfinalPelado')"
                  />
                  <span v-if="errors.rfinalPelado" class="error-text">{{ errors.rfinalPelado }}</span>
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
                    v-model.number="form.rteoricoSeleccion" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rteoricoSeleccion')"
                  />
                  <span v-if="errors.rteoricoSeleccion" class="error-text">{{ errors.rteoricoSeleccion }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.rfinalSeleccion" 
                    required
                    step="0.01" 
                    min="0" 
                    max="100"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarRendimiento('rfinalSeleccion')"
                  />
                  <span v-if="errors.rfinalSeleccion" class="error-text">{{ errors.rfinalSeleccion }}</span>
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
                    v-model.number="form.wverdeTeorico" 
                    required
                    step="0.01" 
                    min="0" 
                    max="999999"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarPeso('wverdeTeorico')"
                  />
                  <span v-if="errors.wverdeTeorico" class="error-text">{{ errors.wverdeTeorico }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final (kg) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.wverdeFinal" 
                    required
                    step="0.01" 
                    min="0" 
                    max="999999"
                    class="input-field" 
                    placeholder="0.00"
                    @blur="validarPeso('wverdeFinal')"
                  />
                  <span v-if="errors.wverdeFinal" class="error-text">{{ errors.wverdeFinal }}</span>
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
                    v-model.number="form.hinicial" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarHumedad('hinicial')" 
                    placeholder="0.0" 
                    class="input-field" 
                  />
                  <span v-if="errors.hinicial" class="error-text">{{ errors.hinicial }}</span>
                </div>
                <div class="input-group">
                  <label class="required-label">Final - Verde (%) *</label>
                  <input 
                    type="number" 
                    v-model.number="form.hfinal" 
                    required
                    min="0" 
                    max="100" 
                    step="0.1" 
                    @blur="validarHumedad('hfinal')" 
                    placeholder="0.0" 
                    class="input-field" 
                  />
                  <span v-if="errors.hfinal" class="error-text">{{ errors.hfinal }}</span>
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
                v-model.number="form.wfinal" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-field" 
                placeholder="0.00"
                @blur="validarPeso('wfinal')"
              />
              <span v-if="errors.wfinal" class="error-text">{{ errors.wfinal }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Peso Verde Inferiores (kg) *</label>
              <input 
                type="number" 
                v-model.number="form.winferiores" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-field" 
                placeholder="0.00"
                @blur="validarPeso('winferiores')"
              />
              <span v-if="errors.winferiores" class="error-text">{{ errors.winferiores }}</span>
            </div>
            <div class="input-group">
              <label class="required-label">Peso Verde Final Inferior (kg) *</label>
              <input 
                type="number" 
                v-model.number="form.wfInferior" 
                required
                step="0.01" 
                min="0" 
                max="999999"
                class="input-field" 
                placeholder="0.00"
                @blur="validarPeso('wfInferior')"
              />
              <span v-if="errors.wfInferior" class="error-text">{{ errors.wfInferior }}</span>
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
  name: "RegistroTrilla",
  data() {
    return {
      form: {
        // Datos básicos
        nlote: "",
        ffinalReposo: "",

        // Porcentajes de calidades
        psegundas: null,
        pmenudos: null,
        pinferiores: null,
        pmadres: null,
        pprimera: null,
        pcaracolillo: null,
        pescogeduras: null,
        pbarreduras: null,
        pcataduras: null, // CORREGIDO: era "caraduras" ahora es "cataduras"

        // Rendimientos
        rteoricoPelado: null,
        rteoricoSeleccion: null,
        rfinalPelado: null,
        rfinalSeleccion: null,

        // Pesos en verde
        wverdeTeorico: null,
        wverdeFinal: null,

        // Humedad
        hinicial: null,
        hfinal: null,

        // Exportación (PesoVerde)
        wfinal: null,
        winferiores: null,
        wfInferior: null,
      },
      porcentajesCampos: [
        { key: 'pprimera', label: '% Primera' },
        { key: 'psegundas', label: '% Segundas' },
        { key: 'pcaracolillo', label: '% Caracolillo' },
        { key: 'pmenudos', label: '% Menudos' },
        { key: 'pinferiores', label: '% Inferiores' },
        { key: 'pmadres', label: '% Madres' },
        { key: 'pescogeduras', label: '% Escogeduras' },
        { key: 'pbarreduras', label: '% Barreduras' },
        { key: 'pcataduras', label: '% Cataduras' } // CORREGIDO
      ],
      errors: {},
      showSuccess: false,
      showError: false,
      errorMessage: '',
      totalPorcentajes: 0,
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
            .map(registro => registro.Nlote || registro.nlote)
            .filter(nlote => nlote && nlote.toString().trim() !== '')
        )];
        
        this.lotes = lotesUnicos.sort();
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

    // Método para crear registro de Trilla
    async crearTrilla() {
      const trillaData = {
        Nlote: this.form.nlote,
        FFinalReposo: new Date(this.form.ffinalReposo).toISOString(),
        Psegundas: this.form.psegundas,
        Pmenudos: this.form.pmenudos,
        Pinferiores: this.form.pinferiores,
        Pmadres: this.form.pmadres,
        Pprimera: this.form.pprimera,
        Pcaracolillo: this.form.pcaracolillo,
        Pescogeduras: this.form.pescogeduras,
        Pbarreduras: this.form.pbarreduras,
        Pcataduras: this.form.pcataduras,
        POinferiores: 0, // Campo que existe en el backend pero no en el formulario
        RTeoricoSeleccion: this.form.rteoricoSeleccion,
        RTeoricoPelado: this.form.rteoricoPelado,
        RfinalPelado: this.form.rfinalPelado,
        RfinalSeleccion: this.form.rfinalSeleccion,
        WVerdeFinal: this.form.wverdeFinal,
        WVerdeTeorico: this.form.wverdeTeorico,
        HFinal: this.form.hfinal,
        HInicial: this.form.hinicial
      };

      console.log("📋 Creando trilla:", trillaData);

      try {
        return await apiService.crear('TrillaApi', trillaData);
      } catch (error) {
        console.error("❌ Error al crear trilla:", trillaData, error);
        throw new Error(`Error al crear trilla: ${error.message || error}`);
      }
    },

    // Método para crear registro de PesoVerde
    async crearPesoVerde(idTrilla) {
      const pesoVerdeData = {
        Winferiores: this.form.winferiores,
        Wfinal: this.form.wfinal,
        WFinferior: this.form.wfInferior,
        ID_PesoTrilla: idTrilla
      };

      console.log("📦 Creando peso verde:", pesoVerdeData);

      try {
        return await apiService.crear('PesoVerdeApi', pesoVerdeData);
      } catch (error) {
        console.error("❌ Error al crear peso verde:", pesoVerdeData, error);
        throw new Error(`Error al crear peso verde: ${error.message || error}`);
      }
    },

    // Validación de campos de texto y fecha
    validarCampo(campo) {
      if (campo === 'nlote') {
        if (!this.form[campo] || this.form[campo].toString().trim() === '') {
          this.errors[campo] = 'Debe seleccionar o ingresar un lote';
        } else if (this.lotes.length > 0 && this.apiLotesDisponible) {
          const loteValido = this.lotes.includes(this.form[campo]);
          if (!loteValido) {
            this.errors[campo] = 'El lote seleccionado no es válido';
          } else {
            delete this.errors[campo];
          }
        } else {
          const lote = this.form[campo].toString().trim();
          if (lote.length < 2) {
            this.errors[campo] = 'El número de lote debe tener al menos 2 caracteres';
          } else if (lote.length > 50) {
            this.errors[campo] = 'El número de lote no puede exceder 50 caracteres';
          } else {
            delete this.errors[campo];
          }
        }
      } else if (campo === 'ffinalReposo') {
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
      this.errors = {};
      
      // Validar campos básicos
      ['nlote', 'ffinalReposo'].forEach(campo => {
        this.validarCampo(campo);
      });

      // Validar porcentajes
      this.porcentajesCampos.forEach(campo => {
        this.validarPorcentaje(campo.key);
      });

      // Validar rendimientos
      ['rteoricoPelado', 'rteoricoSeleccion', 'rfinalPelado', 'rfinalSeleccion'].forEach(campo => {
        this.validarRendimiento(campo);
      });

      // Validar pesos
      ['wverdeTeorico', 'wverdeFinal', 'wfinal', 'winferiores', 'wfInferior'].forEach(campo => {
        this.validarPeso(campo);
      });

      // Validar humedad
      ['hinicial', 'hfinal'].forEach(campo => {
        this.validarHumedad(campo);
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
        console.log("📋 Iniciando guardado de datos de trilla...");

        // 1. Crear el registro principal de Trilla
        const trillaCreada = await this.crearTrilla();
        
        // Obtener ID de la trilla creada
        const idTrilla = trillaCreada.ID_Trilla || trillaCreada.id_Trilla || trillaCreada.idTrilla;
        
        if (!idTrilla) {
          console.error("❌ No se pudo obtener el ID de la trilla creada:", trillaCreada);
          throw new Error("No se pudo obtener el ID del registro de trilla creado");
        }
        
        console.log("✅ Trilla creada con ID:", idTrilla);
        console.log("🔍 Objeto completo de la trilla:", trillaCreada);

        // 2. Crear el registro de PesoVerde usando el ID de la trilla
        const pesoVerdeCreado = await this.crearPesoVerde(idTrilla);
        console.log("✅ Peso Verde creado:", pesoVerdeCreado);

        console.log("🎉 Todos los datos de trilla guardados exitosamente");
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
        if (key === 'nlote' || key === 'ffinalReposo') {
          this.form[key] = '';
        } else {
          this.form[key] = null;
        }
      });
      
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

.percentages {
  border-left: 4px solid var(--verde-oscuro);
}

.rendimientos {
  border-left: 4px solid var(--cafe-oscuro);
}

.pesos {
  border-left: 4px solid var(--cafe-medio);
}

.humidity {
  border-left: 4px solid var(--verde-claro);
}

.exportacion {
  border-left: 4px solid var(--cafe-muy-oscuro);
}

/* Grid de formulario */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: 20px;
}

.percentages-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 15px;
}

.percentage-item {
  display: flex;
  flex-direction: column;
}

.rendimientos-grid, .pesos-grid {
  display: grid;
  grid-template-columns: 1fr;
  gap: 20px;
}

.exportacion-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(250px, 1fr));
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

.percentage-input {
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

/* Información adicional */
.percentage-info, .humidity-info {
  background: var(--beige-claro);
  padding: var(--space-md);
  border-radius: 8px;
  margin-bottom: var(--space-xl);
  display: flex;
  justify-content: space-between;
  align-items: center;
  gap: var(--space-lg);
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

.total-percentage {
  font-weight: 600;
  padding: var(--space-sm) var(--space-md);
  border-radius: 6px;
  font-size: var(--text-sm);
}

.total-valid {
  background: linear-gradient(135deg, var(--color-success), #388E3C);
  color: white;
}

.total-warning {
  background: var(--color-warning);
  color: white;
}

/* Mediciones */
.humidity-measurements {
  display: grid;
  grid-template-columns: 1fr;
  gap: var(--space-xl);
}

.rendimiento-pair, .peso-pair, .humidity-pair {
  background: linear-gradient(145deg, #ffffff, var(--beige-claro));
  padding: var(--space-lg);
  border-radius: 12px;
  border: 1px solid var(--cafe-claro);
  transition: var(--transition-all);
}

.rendimiento-pair:hover, .peso-pair:hover, .humidity-pair:hover {
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

  .percentages-grid {
    grid-template-columns: repeat(2, 1fr);
  }

  .exportacion-grid {
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

  .percentage-info, .humidity-info {
    flex-direction: column;
    text-align: center;
  }
}

@media (max-width: 480px) {
  .percentages-grid {
    grid-template-columns: 1fr;
  }

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