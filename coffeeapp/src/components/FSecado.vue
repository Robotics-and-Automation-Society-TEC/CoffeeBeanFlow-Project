<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Registro de Secado</h2>
      <form @submit.prevent="submitForm">
        <!-- Datos básicos -->
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <input type="date" placeholder="Fecha inicio de secado" v-model="form.fechaInicio" required />
        <input type="number" placeholder="Días de secado" v-model.number="form.diasSecado" />
        <input type="date" placeholder="Fecha final" v-model="form.fechaFinal" required />
        <input type="number" placeholder="Porcentaje mecánico (%)" v-model.number="form.porcMecanico" />
        <input type="number" placeholder="Porcentaje solar (%)" v-model.number="form.porcSolar" />

        <!-- Temperaturas de secado -->
        <h3>Temperaturas de Secado (6)</h3>
        <div class="input-row" v-for="(t, i) in form.temperaturasSecado" :key="'temp-'+i">
          <input type="number" placeholder="°C" v-model.number="form.temperaturasSecado[i]" />
        </div>

        <!-- Mediciones de humedad -->
        <h3>Humedad y Temperatura (6)</h3>
        <div class="input-row" v-for="(m, i) in form.medicionesHumedad" :key="'med-'+i">
          <input type="number" placeholder="Humedad (%)" v-model.number="m.humedad" />
          <input type="number" placeholder="Temp (°C)" v-model.number="m.temperatura" />
        </div>

        <!-- Termohigrometría -->
        <h3>TermoHigrometría</h3>
        <div class="input-row" v-for="(rh, i) in form.termoHigro.humedadRelativa" :key="'rh-'+i">
          <input type="number" placeholder="Humedad relativa (%)" v-model.number="form.termoHigro.humedadRelativa[i]" />
        </div>
        <input type="number" placeholder="Temp interna (°C)" v-model.number="form.termoHigro.tempInterna" />
        <input type="number" placeholder="Temp externa (°C)" v-model.number="form.termoHigro.tempExterna" />

        <!-- Botones -->
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
  name: "FSecado",
  data() {
    return {
      form: {
        lote: "",
        fechaInicio: "",
        diasSecado: null,
        fechaFinal: "",
        porcMecanico: null,
        porcSolar: null,
        temperaturasSecado: Array.from({ length: 6 }, () => null),
        medicionesHumedad: Array.from({ length: 6 }, () => ({ humedad: null, temperatura: null })),
        termoHigro: {
          humedadRelativa: Array.from({ length: 6 }, () => null),
          tempInterna: null,
          tempExterna: null,
        },
      }
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos de secado:", this.form);
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
  background-color: rgba(0,0,0,0.4);
  display: flex;
  justify-content: center;
  align-items: center;
}
.modal-content {
  background: rgb(137, 239, 114);
  padding: 20px;
  border-radius: 10px;
  max-height: 90%;
  overflow-y: auto;
  width: 90%;
  max-width: 600px;
}
.input-row {
  display: flex;
  gap: 10px;
  flex-wrap: wrap;
  margin-bottom: 10px;
}
input {
  padding: 8px;
  width: 100%;
  box-sizing: border-box;
  border-radius: 6px;
  border: 1px solid #ccc;
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
