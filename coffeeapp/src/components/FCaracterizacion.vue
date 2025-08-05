<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Registro Refractómetro</h2>
      <form @submit.prevent="submitForm">
        
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <label>Proceso:</label>
        <select v-model="form.proceso" required>
          <option disabled value="">Selecciona un proceso</option>
          <option value="despulpado">Despulpado</option>
          <option value="miel">Miel</option>
          <option value="lavado">Lavado</option>
          <option value="natural">Natural</option>
        </select>

        <input type="number" placeholder="Cerezas inmaduras" v-model.number="form.inmaduras" min="0" />
        <input type="number" placeholder="Cerezas sobremaduras" v-model.number="form.sobremaduras" min="0" />
        <input type="number" placeholder="Cerezas verdes" v-model.number="form.verdes" min="0" />
        <input type="number" placeholder="Cerezas objetivo (óptimas)" v-model.number="form.objetivo" min="0" />
        <input type="number" placeholder="Cerezas secas" v-model.number="form.secas" min="0" />
        <input type="text" placeholder="Rango óptimo de maduras" v-model="form.rangoOptimo" />
        <input type="number" placeholder="% Cerezas verdes" v-model.number="form.porcVerdes" min="0" max="100" />
        <input type="number" placeholder="% Cerezas secas" v-model.number="form.porcSecas" min="0" max="100" />
        <input type="number" placeholder="% Cerezas objetivo" v-model.number="form.porcObjetivo" min="0" max="100" />
        <input type="number" placeholder="% Por debajo madurez" v-model.number="form.porcDebajoObjetivo" min="0" max="100" />
        <input type="text" placeholder="Estado de maduración" v-model="form.estadoMaduracion" />
        <input type="number" placeholder="% Broca" v-model.number="form.broca" min="0" max="100" />
        <input type="number" placeholder="% Vanos" v-model.number="form.vanos" min="0" max="100" />
        <input type="number" placeholder="% Secos" v-model.number="form.secos" min="0" max="100" />
        <input type="number" placeholder="Densidad del grano" v-model.number="form.densidadGrano" step="0.01" min="0" />

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
  name: "FCaracterizacion",
  data() {
    return {
      form: {
        lote:"",
        
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
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos Caracterización:", this.form);
      alert("Formulario de caracterización enviado exitosamente");
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
  top: 0; left: 0;
  width: 100%; height: 100%;
  background-color: rgba(0,0,0,0.5);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: 1000;
}
.modal-content {
  background: rgb(137, 239, 114);
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
form button {
  margin-top: 10px;
  padding: 10px 20px;
  background-color: #4caf50;
  color: white;
  border: none;
  border-radius: 6px;
  margin-right: 10px;
  cursor: pointer;
}
form button[type="button"] {
  background-color: #f44336;
}
.form-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
</style>
