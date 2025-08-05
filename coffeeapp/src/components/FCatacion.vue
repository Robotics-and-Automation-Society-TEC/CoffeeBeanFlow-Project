<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Registro de Catación</h2>
      <form @submit.prevent="submitForm">
        <!-- DATOS GENERALES -->
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <input type="date" placeholder="Fecha final de reposo" v-model="form.fechaReposo" required />
        <input type="number" placeholder="Cantidad defectuosas (Control)" v-model.number="form.defectuosas" min="0" />
        <input type="text" placeholder="Limpio" v-model="form.limpio" />

        <label>Olor en verde</label>
        <select v-model="form.olorVerde" required>
          <option disabled value="">Selecciona olor</option>
          <option value="olor limpio">Olor limpio</option>
          <option value="olor extraño">Olor extraño</option>
        </select>

        <input type="number" placeholder="Quaker" v-model.number="form.quaker" min="0" />

        <label>Clasificación color en verde</label>
        <select v-model="form.colorVerde">
          <option disabled value="">Selecciona color</option>
          <option>Azulado verde</option>
          <option>Verde</option>
          <option>Verdoso</option>
          <option>Amarillo verde</option>
          <option>Amarillo pálido</option>
          <option>Amarillo</option>
          <option>Cafesoso</option>
        </select>

        <input type="number" placeholder="Rendimiento tostado" v-model.number="form.rendimientoTostado" step="0.01" />
        <input type="number" placeholder="Densidad de tueste" v-model.number="form.densidadTueste" step="0.01" />
        <input type="number" placeholder="Tonalidad Agtron" v-model.number="form.tonalidadAgtron" />

        <label>Clasificación calidad</label>
        <select v-model="form.clasificacionCalidad">
          <option disabled value="">Selecciona clase</option>
          <option>-80 Clase D</option>
          <option>80-83.99 Clase C</option>
          <option>84-84.99 Clase B</option>
          <option>85-85.99 Clase A</option>
          <option>86-88.99 Clase AA</option>
          <option>89+ Clase AAA</option>
        </select>

        <!-- CLASIFICACIÓN CAFÉ ARÁBICA -->
        <h3>Clasificación Café Arábica</h3>
        <input type="number" placeholder="C1 Negro" v-model.number="form.c1Negro" min="0" />
        <input type="number" placeholder="C1 Agrio" v-model.number="form.c1Agrio" min="0" />
        <input type="number" placeholder="C1 Cereza seca" v-model.number="form.c1CerezaSeca" min="0" />
        <input type="number" placeholder="C1 Daño por hongos" v-model.number="form.c1Hongos" min="0" />
        <input type="number" placeholder="C1 Daño severo de insectos" v-model.number="form.c1InsectosSevero" min="0" />
        <input type="number" placeholder="C1 Materia extraña" v-model.number="form.c1MateriaExtraña" min="0" />

        <input type="number" placeholder="C2 Negro parcial" v-model.number="form.c2NegroParcial" min="0" />
        <input type="number" placeholder="C2 Agrio parcial" v-model.number="form.c2AgrioParcial" min="0" />
        <input type="number" placeholder="C2 Pergamino" v-model.number="form.c2Pergamino" min="0" />
        <input type="number" placeholder="C2 Flotador" v-model.number="form.c2Flotador" min="0" />
        <input type="number" placeholder="C2 Inmaduro" v-model.number="form.c2Inmaduro" min="0" />
        <input type="number" placeholder="C2 Averanado" v-model.number="form.c2Averanado" min="0" />
        <input type="number" placeholder="C2 Concha" v-model.number="form.c2Concha" min="0" />
        <input type="number" placeholder="C2 Partido/cortado/molido" v-model.number="form.c2Partido" min="0" />
        <input type="number" placeholder="C2 Cáscara/pulpa" v-model.number="form.c2Cascara" min="0" />
        <input type="number" placeholder="C2 Daño ligero de insectos" v-model.number="form.c2InsectosLigeros" min="0" />

        <!-- GRANULOMETRÍA -->
        <h3>Gramulometría</h3>
        <input type="number" placeholder="Zaranda #20" v-model.number="form.z20" min="0" />
        <input type="number" placeholder="Zaranda #19" v-model.number="form.z19" min="0" />
        <input type="number" placeholder="Zaranda #18" v-model.number="form.z18" min="0" />
        <input type="number" placeholder="Zaranda #17" v-model.number="form.z17" min="0" />
        <input type="number" placeholder="Zaranda #15" v-model.number="form.z15" min="0" />
        <input type="number" placeholder="Zaranda #14" v-model.number="form.z14" min="0" />
        <input type="number" placeholder="Zaranda #13" v-model.number="form.z13" min="0" />
        <input type="number" placeholder="Zaranda #3/16" v-model.number="form.z316" min="0" />
        <input type="number" placeholder="Residuos" v-model.number="form.residuos" min="0" />

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
  name: "FCatacion",
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

        // Clasificación café arábica
        c1Negro: null,
        c1Agrio: null,
        c1CerezaSeca: null,
        c1Hongos: null,
        c1InsectosSevero: null,
        c1MateriaExtraña: null,
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
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos de Catación:", this.form);
      alert("Formulario de Catación enviado exitosamente");
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
  color: #5c4200;
}
form button {
  margin-top: 10px;
  padding: 10px 20px;
  background-color: #b77b1e;
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
