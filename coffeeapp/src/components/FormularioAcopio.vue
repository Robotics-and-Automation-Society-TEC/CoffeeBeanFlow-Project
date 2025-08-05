<template>
  <div class="modal">
    <div class="modal-content" @click.stop>
      <h2>Nuevo Registro - Área de Acopio</h2>
      <form @submit.prevent="submitForm">
        <!-- Información General -->
        <h3>Información General</h3>
        <input type="text" placeholder="Número de lote" v-model="form.lote" required />
        <input type="text" placeholder="Número de recibo" v-model="form.recibo" required />
        <input type="text" placeholder="Productor" v-model="form.productor" required />
        <input type="text" placeholder="Finca" v-model="form.finca" required />
        <input type="text" placeholder="Zona" v-model="form.zona" required />
        <input type="number" placeholder="Altura (msnm)" v-model.number="form.altura" min="0" required />

        <!-- Rango de Maduración -->
        <h3>Rango de Maduración</h3>
        <input type="text" placeholder="Rango Objetivo" v-model="form.rangoObjetivo" required />
        <input type="text" placeholder="Sobre Objetivo" v-model="form.sobreObjetivo" />
        <input type="text" placeholder="Rango" v-model="form.rango" />
        <input type="number" placeholder="% Flote" v-model.number="form.porcentajeFlote" min="0" max="100" />
        <input type="number" placeholder="% Vano" v-model.number="form.porcentajeVano" min="0" max="100" />
        <input type="number" placeholder="% Broca" v-model.number="form.porcentajeBroca" min="0" max="100" />
        <input type="number" placeholder="pH Miel" v-model.number="form.phMiel" step="0.01" min="0" max="14" />
        <input type="number" placeholder="% Verde" v-model.number="form.porcentajeVerde" min="0" max="100" />
        <input type="number" placeholder="% Secos" v-model.number="form.porcentajeSecos" min="0" max="100" />

        <!-- Estado de Producto -->
        <h3>Estado del Producto</h3>
        <select v-model="form.estadoProducto" required>
          <option disabled value="">Seleccionar estado</option>
          <option value="disponible">Disponible</option>
          <option value="vendido">Vendido</option>
          <option value="en_proceso">En proceso</option>
        </select>

        <!-- Pruebas Físicas Beneficiado Húmeda -->
        <h3>Pruebas Físicas - Beneficiado Húmeda</h3>
        <input type="number" placeholder="% Segundas" v-model.number="form.porcentajeSegundas" min="0" max="100" />
        <input type="number" placeholder="% Daños Mecánicos" v-model.number="form.danosMecanicos" min="0" max="100" />
        <input type="number" placeholder="% Pulpa en Pergamino" v-model.number="form.pulpaEnPergamino" min="0" max="100" />
        <input type="number" placeholder="% Pergamino en Pulpa" v-model.number="form.pergaminoEnPulpa" min="0" max="100" />

        <!-- Pruebas de Densidad -->
        <h3>Pruebas de Densidad</h3>
        <input type="number" placeholder="Densidad de fruta" v-model.number="form.densidadFruta" step="0.01" min="0" />
        <input type="number" placeholder="Densidad pergamino humedad" v-model.number="form.densidadPergamino" step="0.01" min="0" />

        <!-- Botones -->
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
  name: "FormularioAcopio",
  data() {
    return {
      form: {
        lote: "",
        recibo: "",
        productor: "",
        finca: "",
        zona: "",
        altura: null,

        rangoObjetivo: "",
        sobreObjetivo: "",
        rango: "",
        porcentajeFlote: null,
        porcentajeVano: null,
        porcentajeBroca: null,
        phMiel: null,
        porcentajeVerde: null,
        porcentajeSecos: null,

        estadoProducto: "",

        porcentajeSegundas: null,
        danosMecanicos: null,
        pulpaEnPergamino: null,
        pergaminoEnPulpa: null,

        densidadFruta: null,
        densidadPergamino: null,
      },
    };
  },
  methods: {
    submitForm() {
      console.log("📋 Datos Centro de Acopio:", this.form);
      alert("Formulario enviado exitosamente");
      // Aquí puedes agregar lógica para enviar el formulario a un backend o store

      // Luego rediriges a la pantalla principal:
      this.$router.push({ name: "HomeView" });
    },
    cancelar() {
      // Simplemente regresa a la ventana principal
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
h3 {
  margin-top: 20px;
  font-size: 18px;
  color: #333;
}
.form-buttons {
  display: flex;
  justify-content: flex-end;
  gap: 10px;
}
</style>
