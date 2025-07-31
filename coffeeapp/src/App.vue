<template>
  <div id="app">
    <!-- Header con título y barra de búsqueda -->
    <header class="header">
      <h1>Proyecto Cafe</h1>
      <input type="text" placeholder="🔍 Buscar" class="search-box" />
    </header>

    <h2>Crear nuevos registros</h2>

    <!-- Tarjetas como botones -->
    <div class="grid">
      <button
        class="card"
        v-for="item in sections"
        :key="item.title"
        @click="handleClick(item.title)"
      >
        <img :src="item.icon" alt="icono" class="icon" />
        <span>{{ item.title }}</span>
      </button>
    </div>
  </div>
</template>

<script>
export default {
  name: "App",
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
    };
  },
  methods: {
    handleClick(sectionName) {
      alert(`Has hecho clic en: ${sectionName}`);
      // Aquí puedes usar Vue Router o lógica personalizada
    },
  },
};
</script>

<style scoped>
/* Estilos generales */
#app {
  font-family: Arial, sans-serif;
  padding: 20px;
  background: linear-gradient(#a4e969, #dbf6a7);
  min-height: 100vh;
  box-sizing: border-box;
}

/* Header */
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

/* Input de búsqueda */
.search-box {
  padding: 8px;
  border-radius: 8px;
  border: none;
  width: 200px;
  max-width: 100%;
}

/* Grilla de tarjetas */
.grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(140px, 1fr));
  gap: 20px;
  margin-top: 30px;
}

/* Botón-tarjeta */
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

/* Iconos dentro del botón */
.icon {
  width: 50px;
  height: 50px;
  margin-bottom: 10px;
}

/* Responsive tip: más espacio en pantallas pequeñas */
@media (max-width: 600px) {
  h2 {
    font-size: 18px;
  }

  .card {
    padding: 16px;
    font-size: 14px;
  }

  .icon {
    width: 40px;
    height: 40px;
  }
}
</style>
