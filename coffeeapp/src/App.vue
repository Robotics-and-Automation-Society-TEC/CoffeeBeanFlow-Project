<!-- src/App.vue -->
<template>
    <router-view />
</template>

<script>
export default {
  name: "App",
};
</script>

<style>
/* ========================================
   SISTEMA DE CONSTANTES GLOBALES - TEMA CAFÉ
   ======================================== */

/* Variables CSS Globales */
:root {
  /* === PALETA DE COLORES CAFÉ === */
  --burgundy: #A52A3D;
  --burgundy-dark: #8B2332;
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
  
  /* === COLORES FUNCIONALES === */
  --color-success: var(--verde-claro);
  --color-success-dark: var(--verde-oscuro);
  --color-error: var(--burgundy);
  --color-error-dark: var(--burgundy-dark);
  --color-warning: #f39c12;
  --color-info: var(--cafe-medio);
  
  /* === COLORES DE TEXTO === */
  --text-primary: var(--negro-cafe);
  --text-secondary: var(--cafe-oscuro);
  --text-muted: var(--cafe-medio);
  --text-light: var(--cafe-claro);
  --text-white: var(--blanco-crema);
  
  /* === FONDOS === */
  --bg-primary: var(--blanco-crema);
  --bg-secondary: var(--beige-claro);
  --bg-dark: var(--negro-cafe);
  --bg-card: var(--blanco-crema);
  
  /* === GRADIENTES === */
  --gradient-primary: linear-gradient(135deg, var(--verde-claro), var(--verde-oscuro));
  --gradient-secondary: linear-gradient(135deg, var(--burgundy), var(--burgundy-dark));
  --gradient-background: linear-gradient(145deg, var(--blanco-crema), var(--beige-claro));
  --gradient-card: linear-gradient(145deg, var(--blanco-crema), var(--beige-claro));
  --gradient-header: linear-gradient(135deg, var(--cafe-muy-oscuro), var(--negro-cafe));
  --gradient-coffee: linear-gradient(90deg, var(--burgundy), var(--verde-claro), var(--cafe-oscuro));
  
  /* === SOMBRAS === */
  --shadow-xs: 0 2px 4px rgba(74, 45, 26, 0.1);
  --shadow-sm: 0 4px 8px rgba(74, 45, 26, 0.15);
  --shadow-md: 0 8px 16px rgba(74, 45, 26, 0.2);
  --shadow-lg: 0 12px 24px rgba(74, 45, 26, 0.25);
  --shadow-xl: 0 16px 32px rgba(74, 45, 26, 0.3);
  --shadow-2xl: 0 20px 40px rgba(74, 45, 26, 0.35);
  
  /* Sombras con color */
  --shadow-success: 0 6px 15px rgba(143, 173, 90, 0.4);
  --shadow-error: 0 6px 15px rgba(165, 42, 61, 0.4);
  --shadow-coffee: 0 6px 15px rgba(139, 90, 60, 0.4);
  
  /* === TRANSICIONES === */
  --transition-fast: 0.15s ease;
  --transition-normal: 0.3s ease;
  --transition-slow: 0.4s ease;
  --transition-very-slow: 0.6s ease;
  
  /* Transiciones específicas */
  --transition-transform: transform 0.3s ease;
  --transition-opacity: opacity 0.3s ease;
  --transition-colors: background-color 0.3s ease, border-color 0.3s ease, color 0.3s ease;
  --transition-shadow: box-shadow 0.3s ease;
  --transition-all: all 0.3s ease;
  
  /* === ESPACIADO === */
  --space-xs: 4px;
  --space-sm: 8px;
  --space-md: 12px;
  --space-lg: 16px;
  --space-xl: 20px;
  --space-2xl: 24px;
  --space-3xl: 32px;
  --space-4xl: 40px;
  --space-5xl: 48px;
  
  /* === BORDER RADIUS === */
  --radius-xs: 4px;
  --radius-sm: 6px;
  --radius-md: 8px;
  --radius-lg: 10px;
  --radius-xl: 12px;
  --radius-2xl: 16px;
  --radius-3xl: 20px;
  --radius-full: 50%;
  
  /* === TAMAÑOS DE FUENTE === */
  --text-xs: 0.75rem;    /* 12px */
  --text-sm: 0.875rem;   /* 14px */
  --text-base: 1rem;     /* 16px */
  --text-lg: 1.125rem;   /* 18px */
  --text-xl: 1.25rem;    /* 20px */
  --text-2xl: 1.5rem;    /* 24px */
  --text-3xl: 1.875rem;  /* 30px */
  --text-4xl: 2.25rem;   /* 36px */
  
  /* === ALTURAS Y ANCHOS COMUNES === */
  --height-input: 48px;
  --height-button: 48px;
  --height-header: 80px;
  --width-sidebar: 280px;
  --width-modal-sm: 400px;
  --width-modal-md: 600px;
  --width-modal-lg: 800px;
  
  /* === Z-INDEX === */
  --z-dropdown: 100;
  --z-modal: 1000;
  --z-toast: 1100;
  --z-tooltip: 1200;
}

/* ========================================
   RESET Y ESTILOS BASE
   ======================================== */

* {
  box-sizing: border-box;
  margin: 0;
  padding: 0;
}

html {
  font-size: 16px;
  scroll-behavior: smooth;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  background: var(--gradient-background);
  color: var(--text-primary);
  line-height: 1.6;
  min-height: 100vh;
  -webkit-font-smoothing: antialiased;
  -moz-osx-font-smoothing: grayscale;
}

/* ========================================
   CLASES UTILITARIAS GLOBALES
   ======================================== */

/* === BOTONES === */
.btn-base {
  padding: var(--space-md) var(--space-2xl);
  border: none;
  border-radius: var(--radius-xl);
  font-size: var(--text-base);
  font-weight: 600;
  cursor: pointer;
  transition: var(--transition-all);
  display: inline-flex;
  align-items: center;
  justify-content: center;
  gap: var(--space-sm);
  text-transform: uppercase;
  letter-spacing: 0.5px;
  text-decoration: none;
  white-space: nowrap;
}

.btn-primary {
  background: var(--gradient-primary);
  color: white;
  box-shadow: var(--shadow-success);
}

.btn-primary:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(143, 173, 90, 0.5);
}

.btn-secondary {
  background: var(--gradient-secondary);
  color: white;
  box-shadow: var(--shadow-error);
}

.btn-secondary:hover {
  transform: translateY(-2px);
  box-shadow: 0 8px 20px rgba(165, 42, 61, 0.5);
}

.btn-cancel {
  background: linear-gradient(135deg, var(--cafe-oscuro), var(--cafe-muy-oscuro));
  color: white;
  box-shadow: var(--shadow-coffee);
}

.btn-cancel:hover {
  background: linear-gradient(135deg, var(--cafe-muy-oscuro), var(--negro-cafe));
  transform: translateY(-2px);
}

.btn:active {
  transform: translateY(0);
}

/* === INPUTS === */
.input-base {
  padding: var(--space-md) var(--space-lg);
  border: 2px solid var(--cafe-claro);
  border-radius: var(--radius-lg);
  font-size: var(--text-base);
  background: white;
  transition: var(--transition-all);
  color: var(--text-primary);
  width: 100%;
  box-sizing: border-box;
}

.input-base:focus {
  outline: none;
  border-color: var(--cafe-oscuro);
  box-shadow: 0 0 12px rgba(139, 90, 60, 0.3);
  transform: scale(1.02);
}

.input-base:hover {
  border-color: var(--cafe-medio);
}

.input-base::placeholder {
  color: var(--cafe-medio);
  font-style: italic;
}

.input-readonly {
  background: var(--beige-claro);
  color: var(--cafe-oscuro);
  font-weight: 600;
}

/* === CARDS === */
.card-base {
  background: var(--gradient-card);
  border-radius: var(--radius-3xl);
  padding: var(--space-2xl);
  box-shadow: var(--shadow-md);
  border: 2px solid transparent;
  transition: var(--transition-all);
}

.card-base:hover {
  border-color: var(--cafe-medio);
  transform: translateY(-2px);
  box-shadow: var(--shadow-lg);
}

/* === MODALES === */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  width: 100%;
  height: 100%;
  background: rgba(44, 24, 16, 0.8);
  backdrop-filter: blur(8px);
  display: flex;
  justify-content: center;
  align-items: center;
  z-index: var(--z-modal);
}

.modal-content {
  background: var(--gradient-background);
  padding: var(--space-3xl);
  border-radius: var(--radius-3xl);
  max-height: 90vh;
  overflow-y: auto;
  width: 95%;
  max-width: var(--width-modal-lg);
  box-shadow: var(--shadow-2xl);
  color: var(--text-primary);
  position: relative;
}

/* === MENSAJES DE ESTADO === */
.message-base {
  padding: var(--space-lg) var(--space-xl);
  border-radius: var(--radius-xl);
  margin-bottom: var(--space-xl);
  display: flex;
  align-items: center;
  gap: var(--space-lg);
  font-weight: 500;
}

.message-success {
  background: var(--gradient-primary);
  color: white;
  box-shadow: var(--shadow-success);
}

.message-error {
  background: var(--gradient-secondary);
  color: white;
  box-shadow: var(--shadow-error);
}

/* === GRIDS === */
.form-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(280px, 1fr));
  gap: var(--space-xl);
}

.cards-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(320px, 1fr));
  gap: var(--space-2xl);
}

/* === INPUT GROUPS === */
.input-group {
  display: flex;
  flex-direction: column;
  gap: var(--space-sm);
}

.input-group label {
  color: var(--text-primary);
  font-weight: 600;
  font-size: var(--text-sm);
}

/* === SECCIONES === */
.section-card {
  background: var(--bg-card);
  border-radius: var(--radius-2xl);
  padding: var(--space-2xl);
  margin-bottom: var(--space-2xl);
  box-shadow: var(--shadow-md);
  border: 2px solid transparent;
  transition: var(--transition-all);
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

/* === DECORACIONES DE CAFÉ === */
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

/* === ANIMACIONES === */
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

.fade-enter-active, .fade-leave-active {
  transition: opacity 0.5s ease;
}

.fade-enter-from, .fade-leave-to {
  opacity: 0;
}

/* === SCROLL PERSONALIZADO === */
*::-webkit-scrollbar {
  width: 8px;
  height: 8px;
}

*::-webkit-scrollbar-track {
  background: var(--beige-claro);
  border-radius: var(--radius-sm);
}

*::-webkit-scrollbar-thumb {
  background: linear-gradient(var(--cafe-medio), var(--cafe-oscuro));
  border-radius: var(--radius-sm);
}

*::-webkit-scrollbar-thumb:hover {
  background: linear-gradient(var(--cafe-oscuro), var(--cafe-muy-oscuro));
}

/* === RESPONSIVE === */
@media (max-width: 768px) {
  :root {
    --space-3xl: 20px;
    --space-2xl: 16px;
    --text-3xl: 1.5rem;
    --text-2xl: 1.25rem;
  }
  
  .form-grid {
    grid-template-columns: 1fr;
  }
  
  .cards-grid {
    grid-template-columns: 1fr;
  }
  
  .modal-content {
    padding: var(--space-xl);
    margin: var(--space-lg);
    max-width: 95%;
  }
}

@media (max-width: 480px) {
  .btn-base {
    padding: var(--space-lg);
    font-size: var(--text-sm);
  }
  
  .section-card {
    padding: var(--space-lg);
  }
}

/* === CLASES DE UTILIDAD === */
.text-center { text-align: center; }
.text-left { text-align: left; }
.text-right { text-align: right; }

.mb-0 { margin-bottom: 0; }
.mb-sm { margin-bottom: var(--space-sm); }
.mb-md { margin-bottom: var(--space-md); }
.mb-lg { margin-bottom: var(--space-lg); }
.mb-xl { margin-bottom: var(--space-xl); }

.mt-0 { margin-top: 0; }
.mt-sm { margin-top: var(--space-sm); }
.mt-md { margin-top: var(--space-md); }
.mt-lg { margin-top: var(--space-lg); }
.mt-xl { margin-top: var(--space-xl); }

.flex { display: flex; }
.flex-column { flex-direction: column; }
.flex-center { justify-content: center; align-items: center; }
.flex-between { justify-content: space-between; }
.flex-end { justify-content: flex-end; }

.w-full { width: 100%; }
.h-full { height: 100%; }
</style>