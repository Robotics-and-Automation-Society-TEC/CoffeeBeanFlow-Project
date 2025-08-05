<template> 
  <div>
    <!-- Header con título y barra de búsqueda -->
    <header class="header">
      <h1>Proyecto Cafe</h1>
      <input type="text" placeholder="🔍 Buscar" class="search-box" />
    </header>

    <h2>Bienvenidos a la ventana principal</h2>

    <!-- Tarjetas como botones -->
    <div class="grid">
      <button
        class="card"
        v-for="item in sections"
        :key="item.title"
        @click="handleClick(item)"
      >
        <img :src="item.icon" alt="icono" class="icon" />
        <span>{{ item.title }}</span>
      </button>
    </div>

    <!-- Modal de opciones -->
    <div v-if="modalVisible" class="modal">
      <div class="modal-content">
        <h3>¿Qué desea hacer en "{{ selectedSection.title }}"?</h3>
        <button @click="nuevoRegistro">Nuevo registro</button>
        <button @click="verRegistroViejo">Ver registros anteriores</button>
        <button @click="cerrarModal">Cancelar</button>
      </div>
    </div>
  </div>
</template>

<script>
export default {
  name: "HomeView",
  data() {
    return {
      sections: [
        { title: "Área de acopio", icon: "icons/acopio.svg" },
        { title: "Caracterización", icon: "icons/caracterizacion.svg" },
        { title: "Secado", icon: "icons/secado.svg" },
        { title: "Bodega", icon: "icons/bodega.svg" },
        { title: "Trilla", icon: "icons/trilla.svg" },
        { title: "Catación", icon: "icons/catacion.svg" },
      ],
      modalVisible: false,
      selectedSection: null,
    };
  },
  methods: {
    handleClick(section) {
      this.selectedSection = section;
      this.modalVisible = true;
    },
    nuevoRegistro() {
      this.modalVisible = false;
      if (this.selectedSection.title === "Área de acopio") {
        this.$router.push("/formulario-nuevo");
      } else if (this.selectedSection.title === "Caracterización") {
        this.$router.push("/caracterizacion");
      }else if(this.selectedSection.title === "Secado") {
        this.$router.push("/secado");
      }else if(this.selectedSection.title === "Bodega") {
        this.$router.push("/bodega");
      }else if(this.selectedSection.title === "Trilla") {
        this.$router.push("/trilla");
      }else if(this.selectedSection.title === "Catación") {
        this.$router.push("/catacion");
      } else {
        alert("Espere un momento, tenemos un pequeño desafio");
      }
    },
    verRegistroViejo() {
      this.modalVisible = false;
      this.$router.push({
        name: "RegistroViejo",
        query: { seccion: this.selectedSection.title },
      });
    },
    cerrarModal() {
      this.modalVisible = false;
      this.selectedSection = null;
    },
  },
  mounted() {
    // ✅ Se asegura que el modal esté cerrado al cargar
    this.modalVisible = false;
    this.selectedSection = null;
  }
};
</script>

<style scoped>
.header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  background: #4caf50;
  padding: 10px 20px;
  color: white;
  flex-wrap: wrap;
  gap: 10px;
}

.search-box {
  padding: 8px;
  border-radius: 8px;
  border: none;
  width: 200px;
  max-width: 100%;
}

.grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(140px, 1fr));
  gap: 20px;
  margin-top: 30px;
}

.card {
  background-color: #4caf50;
  border: none;
  border-radius: 16px;
  padding: 20px;
  color: white;
  text-align: center;
  font-size: 16px;
  font-weight: bold;
  box-shadow: 2px 4px 12px rgba(0, 0, 0, 0.2);
  transition: transform 0.2s ease;
  cursor: pointer;
  display: flex;
  flex-direction: column;
  align-items: center;
  justify-content: center;
}

.card:hover {
  transform: scale(1.05);
}

.icon {
  width: 50px;
  height: 50px;
  margin-bottom: 10px;
}

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
  z-index: 1000;
}

.modal-content {
  background: white;
  padding: 30px;
  border-radius: 12px;
  width: 300px;
  text-align: center;
}

.modal-content button {
  margin: 10px 0;
  padding: 10px 15px;
  width: 100%;
  border: none;
  background-color: #4caf50;
  color: white;
  border-radius: 8px;
  cursor: pointer;
}
div {
  background-color: #18491c; /* verde claro */
  min-height: 100vh;
  padding: 20px;
}

</style>
