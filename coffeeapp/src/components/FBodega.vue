<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Formulario - Bodega</h2>
      <form @submit.prevent="submitForm">
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <input type="number" placeholder="Densidad bellota" v-model.number="form.densidadBellota" />
        <input type="number" placeholder="Densidad pergamino" v-model.number="form.densidadPergamino" />
        <input type="number" placeholder="Humedad inicial (%)" v-model.number="form.humedadInicial" />
        <input type="number" placeholder="Humedad final (%)" v-model.number="form.humedadFinal" />
        <input type="number" placeholder="Peso pergamino (kg)" v-model.number="form.pesoPergamino" />
        <input type="number" placeholder="Peso bellota (kg)" v-model.number="form.pesoBellota" />
        <input type="date" placeholder="Fecha inicio de reposo" v-model="form.fechaInicioReposo" />
        <input type="number" placeholder="Cantidad de sacos" v-model.number="form.cantidadSacos" />
        <input type="number" placeholder="Prom. mensual humedad relativa (%)" v-model.number="form.promHumedadRelativa" />
        <input type="number" placeholder="Prom. mensual temp. interna (°C)" v-model.number="form.promTempInterna" />
        <input type="number" placeholder="Prom. mensual temp. externa (°C)" v-model.number="form.promTempExterna" />

        <div class="form-buttons">
          <button type="submit">Guardar</button>
          <button type="button" @click="cancelar">Cancelar</button>
        </div>
      </form>
    </div>
  </div>
</template>

<script>
export default {
  name: "FBodega",
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
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos Bodega:", this.form);
      alert("Datos guardados correctamente");
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
  background-color: rgba(0, 0, 0, 0.4);
  display: flex;
  justify-content: center;
  align-items: center;
}
.modal-content {
  background:  rgb(137, 239, 114);
  padding: 20px;
  border-radius: 10px;
  max-height: 90%;
  overflow-y: auto;
  width: 90%;
  max-width: 600px;
}
input {
  padding: 8px;
  width: 100%;
  margin-bottom: 10px;
  border-radius: 6px;
  border: 1px solid #ccc;
  box-sizing: border-box;
}
.form-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
  margin-top: 20px;
}
button {
  padding: 10px 20px;
  border: none;
  border-radius: 6px;
  background-color: #4caf50;
  color: white;
  cursor: pointer;
}
button[type="button"] {
  background-color: #f44336;
}
</style>
