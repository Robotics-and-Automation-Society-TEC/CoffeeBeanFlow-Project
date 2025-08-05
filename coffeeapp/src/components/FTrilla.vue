<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Registro Trilla</h2>
      <form @submit.prevent="submitForm">
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <input type="date" placeholder="Fecha final de reposo" v-model="form.fechaReposo" required />

        <h3>Porcentajes de calidades</h3>
        <input type="number" placeholder="% Segundas" v-model.number="form.segundas" min="0" max="100" />
        <input type="number" placeholder="% Menudos" v-model.number="form.menudos" min="0" max="100" />
        <input type="number" placeholder="% Inferiores" v-model.number="form.inferiores" min="0" max="100" />
        <input type="number" placeholder="% Madres" v-model.number="form.madres" min="0" max="100" />
        <input type="number" placeholder="% Primera" v-model.number="form.primera" min="0" max="100" />
        <input type="number" placeholder="% Caracolillo" v-model.number="form.caracolillo" min="0" max="100" />
        <input type="number" placeholder="% Escogeduras" v-model.number="form.escogeduras" min="0" max="100" />
        <input type="number" placeholder="% Barreduras" v-model.number="form.barreduras" min="0" max="100" />
        <input type="number" placeholder="% Caraduras" v-model.number="form.caraduras" min="0" max="100" />

        <h3>Rendimientos</h3>
        <input type="number" placeholder="Rendimiento teórico de pelado" v-model.number="form.rendPeladoTeorico" step="0.01" min="0" />
        <input type="number" placeholder="Rendimiento teórico de selección" v-model.number="form.rendSeleccionTeorico" step="0.01" min="0" />
        <input type="number" placeholder="Rendimiento final de pelado" v-model.number="form.rendPeladoFinal" step="0.01" min="0" />
        <input type="number" placeholder="Rendimiento final de selección" v-model.number="form.rendSeleccionFinal" step="0.01" min="0" />

        <input type="number" placeholder="Peso en verde teórico" v-model.number="form.pesoVerdeTeorico" step="0.01" min="0" />
        <input type="number" placeholder="Peso verde final" v-model.number="form.pesoVerdeFinal" step="0.01" min="0" />

        <input type="number" placeholder="Humedad inicial (pergamino o bellota)" v-model.number="form.humedadInicial" step="0.1" min="0" max="100" />
        <input type="number" placeholder="Humedad final (verde)" v-model.number="form.humedadFinal" step="0.1" min="0" max="100" />

        <h3>Peso en verde Exportación</h3>
        <input type="number" placeholder="Peso verde final exportación" v-model.number="form.pesoVerdeExportacion" step="0.01" min="0" />
        <input type="number" placeholder="Peso verde inferiores" v-model.number="form.pesoVerdeInferiores" step="0.01" min="0" />
        <input type="number" placeholder="Peso verde final inferior" v-model.number="form.pesoVerdeFinalInferior" step="0.01" min="0" />

        <div class="form-buttons">
          <button type="submit">Guardar</button>
          <button type="button" @click.prevent="cancelar">Cancelar</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "FTrilla",
  data() {
    return {
      form: {
        lote: "",
        fechaReposo: "",

        segundas: null,
        menudos: null,
        inferiores: null,
        madres: null,
        primera: null,
        caracolillo: null,
        escogeduras: null,
        barreduras: null,
        caraduras: null,

        rendPeladoTeorico: null,
        rendSeleccionTeorico: null,
        rendPeladoFinal: null,
        rendSeleccionFinal: null,

        pesoVerdeTeorico: null,
        pesoVerdeFinal: null,

        humedadInicial: null,
        humedadFinal: null,

        pesoVerdeExportacion: null,
        pesoVerdeInferiores: null,
        pesoVerdeFinalInferior: null,
      },
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos Trilla:", this.form);
      alert("Formulario de Trilla enviado exitosamente");
      this.$router.push({ name: "HomeView" });
    },
    cancelar() {
      this.$router.push({ name: "HomeView" });
    },
  },
};
</script>

<style scoped>
.modal {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background-color: rgba(0, 0, 0, 0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}
.modal-content {
  background:rgb(137, 239, 114);
  padding: 20px;
  border-radius: 12px;
  width: 90%;
  max-height: 90%;
  overflow-y: auto;
}
form input,
form select {
  width: 100%;
  margin: 8px 0;
  padding: 10px;
  border-radius: 8px;
  border: 1px solid #ccc;
  box-sizing: border-box;
}
form h3 {
  margin-top: 20px;
  color: #0a3c59;
}
form button {
  margin-top: 10px;
  padding: 10px 20px;
  background-color: #007acc;
  color: white;
  border: none;
  border-radius: 6px;
  margin-right: 10px;
  cursor: pointer;
}
form button[type="button"] {
  background-color: #d9534f;
}
.form-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
</style>
