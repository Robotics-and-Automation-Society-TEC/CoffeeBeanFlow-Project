# Documentación del Frontend - CoffeeBeanFlow (Vue.js)

## Índice
1. [Descripción General](#descripción-general)
2. [Arquitectura del Proyecto](#arquitectura-del-proyecto)
3. [Sistema de Diseño y Estilos](#sistema-de-diseño-y-estilos)
4. [Componentes de la Aplicación](#componentes-de-la-aplicación)
5. [Routing y Navegación](#routing-y-navegación)
6. [Servicios y API](#servicios-y-api)
7. [Guía de Migración a Angular](#guía-de-migración-a-angular)

---

## Descripción General

**CoffeeApp** es la aplicación frontend del sistema CoffeeBeanFlow, desarrollada en **Vue.js 3** con **Vue Router 4** y **Axios** para consumir la API REST del backend. El sistema permite gestionar todo el proceso de producción del café desde una interfaz web moderna y responsiva.

**Tecnologías Principales:**
- Vue.js 3 (Composition API y Options API)
- Vue Router 4
- Axios para consumo de API
- CSS personalizado con variables globales
- Tailwind CSS 4.1.11

**URL de desarrollo:** `http://localhost:8080`

---

## Arquitectura del Proyecto

### Estructura de Carpetas

```
coffeeapp/
├── public/              # Archivos estáticos públicos
├── src/
│   ├── components/      # Componentes Vue
│   │   ├── HomeView.vue              # Vista principal/dashboard
│   │   ├── MenuLateral.vue           # Menú de navegación lateral
│   │   ├── FormularioAcopio.vue      # Formulario de acopio
│   │   ├── FCaracterizacion.vue      # Formulario de caracterización
│   │   ├── FSecado.vue               # Formulario de secado
│   │   ├── FBodega.vue               # Formulario de bodega
│   │   ├── FTrilla.vue               # Formulario de trilla
│   │   ├── FCatacion.vue             # Formulario de catación
│   │   ├── HistorialGeneral.vue      # Vista de historial
│   │   └── AiAsistante.vue           # Asistente IA flotante
│   ├── router/          # Configuración de rutas
│   │   └── index.js
│   ├── services/        # Servicios de API
│   │   └── apiService.js
│   ├── App.vue          # Componente raíz
│   └── main.js          # Punto de entrada
├── babel.config.js      # Configuración de Babel
├── package.json         # Dependencias y scripts
└── vue.config.js        # Configuración de Vue CLI
```

### Patrón de Arquitectura

- **Patrón de Componentes:** Single File Components (SFC) de Vue
- **Gestión de Estado:** Data local en cada componente (no usa Vuex/Pinia)
- **Comunicación con API:** Servicio centralizado con Axios
- **Routing:** Vue Router con navegación programática

---

## Sistema de Diseño y Estilos

### Paleta de Colores (Tema Café)

El sistema utiliza un tema de colores basado en tonos de café, definido mediante variables CSS:

#### Colores Principales

```css
/* Paleta de colores café */
--burgundy: #A52A3D;              /* Rojo vino */
--burgundy-dark: #8B2332;         /* Rojo vino oscuro */
--verde-claro: #8FAD5A;           /* Verde café claro */
--verde-oscuro: #4A5D2E;          /* Verde café oscuro */
--verde-muy-oscuro: #2e3d1a;      /* Verde café muy oscuro */
--cafe-claro: #E5C29F;            /* Café claro/beige */
--cafe-medio: #C8956F;            /* Café medio */
--cafe-oscuro: #8B5A3C;           /* Café oscuro */
--cafe-muy-oscuro: #4A2D1A;       /* Café muy oscuro */
--negro-cafe: #2C1810;            /* Negro café */
--beige-claro: #F4F0E6;           /* Beige claro */
--blanco-crema: #FEFAF5;          /* Blanco crema */
```

#### Colores Funcionales

```css
--color-success: var(--verde-claro);
--color-success-dark: var(--verde-oscuro);
--color-error: var(--burgundy);
--color-error-dark: var(--burgundy-dark);
--color-warning: #f39c12;
--color-info: var(--cafe-medio);
```

#### Colores de Texto

```css
--text-primary: var(--negro-cafe);
--text-secondary: var(--cafe-oscuro);
--text-muted: var(--cafe-medio);
--text-light: var(--cafe-claro);
--text-white: var(--blanco-crema);
```

### Gradientes

```css
--gradient-primary: linear-gradient(135deg, var(--verde-claro), var(--verde-oscuro));
--gradient-secondary: linear-gradient(135deg, var(--burgundy), var(--burgundy-dark));
--gradient-background: linear-gradient(145deg, var(--blanco-crema), var(--beige-claro));
--gradient-header: linear-gradient(135deg, var(--cafe-muy-oscuro), var(--negro-cafe));
--gradient-coffee: linear-gradient(90deg, var(--burgundy), var(--verde-claro), var(--cafe-oscuro));
```

### Sistema de Espaciado

```css
--space-xs: 4px;
--space-sm: 8px;
--space-md: 12px;
--space-lg: 16px;
--space-xl: 20px;
--space-2xl: 24px;
--space-3xl: 32px;
--space-4xl: 40px;
--space-5xl: 48px;
```

### Sombras

```css
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
```

### Border Radius

```css
--radius-xs: 4px;
--radius-sm: 6px;
--radius-md: 8px;
--radius-lg: 10px;
--radius-xl: 12px;
--radius-2xl: 16px;
--radius-3xl: 20px;
--radius-full: 50%;
```

### Tipografía

```css
--text-xs: 0.75rem;    /* 12px */
--text-sm: 0.875rem;   /* 14px */
--text-base: 1rem;     /* 16px */
--text-lg: 1.125rem;   /* 18px */
--text-xl: 1.25rem;    /* 20px */
--text-2xl: 1.5rem;    /* 24px */
--text-3xl: 1.875rem;  /* 30px */
--text-4xl: 2.25rem;   /* 36px */
```

### Transiciones

```css
--transition-fast: 0.15s ease;
--transition-normal: 0.3s ease;
--transition-slow: 0.4s ease;
--transition-very-slow: 0.6s ease;
```

### Clases Utilitarias Globales

#### Botones

- `.btn-base`: Estilo base para todos los botones
- `.btn-primary`: Botón principal (verde)
- `.btn-secondary`: Botón secundario (rojo vino)
- `.btn-cancel`: Botón de cancelar (café oscuro)

#### Inputs

- `.input-base`: Estilo base para inputs
- `.input-readonly`: Input de solo lectura

#### Cards

- `.card-base`: Tarjeta base con sombra
- `.section-card`: Tarjeta de sección con hover

#### Modales

- `.modal-overlay`: Fondo oscuro del modal
- `.modal-content`: Contenido del modal

---

## Componentes de la Aplicación

### 1. App.vue

**Descripción:** Componente raíz de la aplicación.

**Funcionalidad:**
- Define las variables CSS globales
- Establece el sistema de diseño base
- Proporciona estilos reset y base
- Renderiza el `<router-view>` para las vistas

**Estructura:**
```vue
<template>
  <router-view />
</template>

<style>
  /* Variables CSS globales */
  /* Estilos reset */
  /* Clases utilitarias */
</style>
```

**Responsabilidades:**
- Gestión del tema visual
- Definición de variables CSS
- Estilos globales de la aplicación

---

### 2. HomeView.vue

**Descripción:** Vista principal del sistema (Dashboard).

**Funcionalidad:**
- Muestra tarjetas de navegación para cada módulo
- Modal de selección de acción (Nuevo registro / Ver historial)
- Integración del menú lateral y asistente IA

**Datos del Componente:**
```javascript
data() {
  return {
    sections: [
      {
        title: "Área de Acopio",
        icon: "🏪",
        badge: "Recepción",
        description: "Registro y control de entrada de café cereza.",
        className: "acopio-card"
      },
      {
        title: "Caracterización",
        icon: "🔬",
        badge: "Análisis",
        description: "Análisis y características físicas del café.",
        className: "caracterizacion-card"
      },
      // ... más secciones
    ],
    modalVisible: false,
    selectedSection: null
  }
}
```

**Métodos Principales:**
- `handleClick(section)`: Abre el modal de acción
- `nuevoRegistro()`: Navega al formulario correspondiente
- `verRegistroViejo()`: Navega al historial
- `cerrarModal()`: Cierra el modal

**Rutas de Navegación:**
```javascript
const routes = {
  "Área de Acopio": "/formulario-nuevo",
  "Caracterización": "/caracterizacion",
  "Secado": "/secado",
  "Bodega": "/bodega",
  "Trilla": "/trilla",
  "Catación": "/catacion"
};
```

**Estructura Visual:**
- Header con logo y título
- Grid de tarjetas de proceso (6 módulos)
- Modal de selección de acción
- Menú lateral (componente)
- Asistente IA flotante (componente)

---

### 3. MenuLateral.vue

**Descripción:** Menú de navegación lateral deslizable desde la derecha.

**Funcionalidad:**
- Botón hamburguesa flotante
- Sidebar que se desliza desde la derecha
- Overlay de fondo oscuro
- Navegación a todas las secciones

**Datos del Componente:**
```javascript
data() {
  return {
    isOpen: false
  }
}
```

**Métodos:**
- `toggleMenu()`: Abre/cierra el menú
- `goTo(path)`: Navega a una ruta y cierra el menú

**Opciones del Menú:**
1. 🏠 Inicio → `/`
2. 🏪 Área de Acopio → `/formulario-nuevo`
3. 🔬 Caracterización → `/caracterizacion`
4. 🌡️ Secado → `/secado`
5. 📦 Bodega → `/bodega`
6. ⚙️ Trilla → `/trilla`
7. ☕ Catación → `/catacion`
8. 📊 Historial General → `/historial`

**Características de Diseño:**
- Posición fija en la esquina superior derecha
- Animación de deslizamiento
- Z-index alto para estar sobre otros elementos
- Overlay clickable para cerrar

---

### 4. FormularioAcopio.vue

**Descripción:** Formulario de registro de entrada de café en el área de acopio.

**Estructura del Formulario:**

#### Sección 1: Información General
- **Número de Lote** (string, requerido)
- **Número de Recibo** (number, requerido)
- **Productor** (string, requerido)
- **Finca** (string, requerido)
- **Zona** (string, requerido)
- **Altura** (number, requerido, msnm)

#### Sección 2: Rango de Maduración
- **Rendimiento Objetivo** (decimal, requerido)
- **Rendimiento Sobre Objetivo** (decimal)
- **Rendimiento Total** (decimal)
- **Tipo de Despulpado** (select: despulpado, miel, lavado, natural)
- **Porcentajes** (opcionales):
  - % Flote
  - % Vano
  - % Broca
  - % Verde
  - % Secos

#### Sección 3: Estado del Producto
- **Estado Actual** (select: disponible, vendido, en_proceso, requerido)
- **Cantidad Disponible** (decimal, si estado = disponible)

#### Sección 4: Pruebas Físicas
- % Segundas
- % Daños Mecánicos
- % Pulpa en Pergamino
- % Pergamino en Pulpa

#### Sección 5: Pruebas de Densidad
- Densidad de Fruta (g/cm³)
- Densidad Pergamino Húmedo (g/cm³)
- ID Secado (generado automáticamente)

**Métodos Principales:**
- `validarCampo(campo)`: Valida campos individuales
- `validarPorcentaje(campo)`: Valida que esté entre 0-100
- `validarFormularioCompleto()`: Valida todo el formulario
- `crearSecadoPlaceholder(nlote)`: Crea registro de secado automático
- `mapearDatosParaAPI(idSecado)`: Mapea datos del form a formato API
- `submitForm()`: Envía el formulario a la API
- `limpiarFormulario()`: Resetea todos los campos
- `cancelar()`: Vuelve a la vista principal

**Flujo de Guardado:**
1. Validar formulario completo
2. Crear registro de Secado placeholder
3. Obtener ID_Secado generado
4. Crear registro de Area_Acopio con ID_Secado
5. Mostrar mensaje de éxito
6. Limpiar formulario
7. Redirigir a HomeView

**Validaciones:**
- Campos obligatorios marcados con *
- Validación en tiempo real (blur)
- Validación de porcentajes (0-100)
- Mensajes de error específicos por campo

---

### 5. FCaracterizacion.vue

**Descripción:** Formulario de caracterización física del café.

**Campos del Formulario:**
- Tiempo (DateTime, PK)
- Cerezas: inmaduras, sobremaduras, secas, objetivo, verdes
- Porcentajes: debajo, verdes, secas, encima, objetivo
- Proceso (lavado, miel, etc.)
- Escala de maduración
- Broca, Densidad, Vanos, Secos
- Número de lote (FK)

**Endpoint API:** `POST /api/Formulario_Caracterizacion`

---

### 6. FSecado.vue

**Descripción:** Formulario de registro del proceso de secado.

**Estructura del Formulario:**

#### Sección 1: Información Básica
- **Número de Lote** (select o input manual)
  - Si la API está disponible, muestra dropdown con lotes
  - Si no, permite entrada manual
- **Fecha Inicio de Secado** (date, requerido)
- **Fecha Final** (date, requerido)
- **Días de Secado** (calculado automáticamente)

#### Sección 2: Porcentajes de Proceso
- **Porcentaje Mecánico** (0-100%, requerido)
- **Porcentaje Solar** (0-100%, requerido)
- **Validación:** La suma debe ser 100%
- Indicador visual del total

**Características Especiales:**
- Carga dinámica de lotes desde la API
- Cálculo automático de días entre fechas
- Validación de suma de porcentajes = 100%
- Manejo de errores de API (fallback a entrada manual)

**Métodos de Cálculo:**
```javascript
calcularDiasSecado() {
  if (this.form.finicio && this.form.ffinal) {
    const inicio = new Date(this.form.finicio);
    const final = new Date(this.form.ffinal);
    const diff = final - inicio;
    this.diasSecado = Math.ceil(diff / (1000 * 60 * 60 * 24));
  }
}
```

**Endpoint API:** `POST /api/SecadoApi`

---

### 7. FBodega.vue

**Descripción:** Formulario de registro de almacenamiento en bodega.

**Campos del Formulario:**
- Número de Lote (select/input)
- Fecha Inicio de Reposo
- Densidades: Bellota, Pergamino
- Humedad: Inicial, Final
- Pesos: Pergamino, Bellota
- Cantidad de Sacos
- Promedios Mensuales: Temperatura externa/interna, Humedad relativa

**Endpoint API:** `POST /api/Bodega`

---

### 8. FTrilla.vue

**Descripción:** Formulario de registro del proceso de trillado.

**Estructura del Formulario:**

#### Información Básica
- Número de Lote
- Fecha Final de Reposo

#### Porcentajes de Clasificación
- % Segundas
- % Menudos
- % Inferiores
- % Madres
- % Primera
- % Caracolillo
- % Barreduras
- % Cataduras
- % Otras Inferiores

#### Pesos y Rendimientos
- Peso Escogeduras
- Peso Verde Final/Teórico
- Rendimiento Teórico: Selección, Pelado
- Rendimiento Final: Selección, Pelado

#### Humedad
- Humedad Inicial
- Humedad Final

**Endpoint API:** `POST /api/Trilla`

---

### 9. FCatacion.vue

**Descripción:** Formulario de registro de catación y evaluación sensorial.

**Estructura del Formulario:**

#### Sección 1: Datos Generales
- Número de Lote
- Fecha Final de Reposo
- Cantidad Defectuosas
- Limpio (estado)

#### Sección 2: Características Sensoriales
- Olor en Verde (select: limpio, extraño)
- Quaker
- Clasificación Color en Verde (select múltiple)

#### Sección 3: Características del Tostado
- Rendimiento Tostado (%)
- Densidad de Tueste (g/ml)
- Clasificación de Calidad
- Puntos Finales
- Medición Agtron

#### Sección 4: Defectos Categoría 1
- Negro, Materia Extraña, Insectos, Cereza Seca, Hongos, Agrio

#### Sección 5: Defectos Categoría 2
- Pergamino, Inmaduro, Negro Parcial, Agrio Parcial
- Cáscara/Pulpa, Insectos, Averanado
- Partido/Cortado/Mordido, Concha, Flotador

#### Sección 6: Medidas de Zarandas
- Zarandas 13-20
- Zaranda 3/16
- Residuo

**Endpoint API:** `POST /api/CatacionApi`

---

### 10. HistorialGeneral.vue

**Descripción:** Vista de consulta de historial de registros.

**Funcionalidades:**

#### Selector de Tipo de Formulario
Grid de botones para seleccionar qué tipo de registro consultar:
1. Área de Acopio
2. Caracterización
3. Secado
4. Bodega
5. Trilla
6. Catación

**Configuración de Tipos:**
```javascript
tiposFormulario: [
  {
    id: 'acopio',
    nombre: 'Área de Acopio',
    icon: '🏪',
    descripcion: 'Registros de entrada',
    gradiente: 'linear-gradient(135deg, #8FAD5A, #4A5D2E)',
    color: '#8FAD5A'
  },
  // ... más tipos
]
```

#### Modos de Vista (solo para Acopio)
1. **Registros Individuales:** Solo registros de acopio
2. **Seguimiento Completo:** Todo el proceso del lote (trazabilidad)

#### Barra de Búsqueda
- Campo de búsqueda dinámico
- Placeholder según tipo seleccionado
- Botón de limpiar búsqueda

#### Filtros Rápidos
- Chips de filtro rápido
- Filtrado por estado, fecha, etc.

#### Estadísticas Rápidas
Grid de 3 tarjetas:
1. **Registros encontrados:** Total filtrado
2. **Estadística Principal:** Varía según tipo
3. **Registros hoy:** Conteo del día

#### Vista de Registros
- Grid de tarjetas de registro
- Información específica por tipo
- Estados visuales con colores
- Badges de identificación

**Métodos Principales:**
- `cambiarTipoFormulario(tipo)`: Cambia el tipo de registro
- `filtrarRegistros()`: Filtra por búsqueda
- `aplicarFiltroRapido(filtro)`: Aplica filtro predefinido
- `formatearFecha(fecha)`: Formatea fechas
- `volverAtras()`: Vuelve a HomeView

---

### 11. AiAsistante.vue

**Descripción:** Asistente de IA flotante (demo).

**Funcionalidad:**
- Botón flotante en esquina inferior derecha
- Ventana de chat emergente
- Entrada de mensajes
- Historial de conversación

**Estado del Componente:**
```javascript
data() {
  return {
    open: false,
    userText: "",
    messages: [
      { sender: "bot", text: "Hola 👋 ¿En qué puedo ayudarte hoy?" }
    ]
  }
}
```

**Métodos:**
- `sendMessage()`: Envía mensaje del usuario
- Respuesta automática (demo sin API real)

**Nota:** Actualmente es una demostración sin backend de IA real.

---

## Routing y Navegación

### Configuración de Rutas (router/index.js)

```javascript
import { createRouter, createWebHistory } from 'vue-router';

const routes = [
  {
    path: "/",
    name: "HomeView",
    component: HomeView
  },
  {
    path: "/formulario-nuevo",
    name: "FormularioNuevo",
    component: FormularioAcopio
  },
  {
    path: "/caracterizacion",
    name: "FormularioCaracterizacion",
    component: FormularioCaracterizacion
  },
  {
    path: '/secado',
    name: 'FormularioSecado',
    component: FSecado
  },
  {
    path: '/bodega',
    name: 'FormularioBodega',
    component: FBodega
  },
  {
    path: '/trilla',
    name: 'FormularioTrilla',
    component: FTrilla
  },
  {
    path: '/catacion',
    name: 'FormularioCatación',
    component: FCatacion
  },
  {
    path: '/historial',
    name: 'HistorialGeneral',
    component: HistorialGeneral
  },
  {
    path: '/asistente',
    name: 'AiAsistante',
    component: AiAsistante
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

export default router;
```

### Navegación Programática

Ejemplos de navegación en componentes:

```javascript
// Navegación simple
this.$router.push('/historial');

// Navegación con nombre
this.$router.push({ name: 'HomeView' });

// Navegación con parámetros de query
this.$router.push({
  name: 'HistorialGeneral',
  query: { seccion: 'Área de Acopio' }
});
```

---

## Servicios y API

### apiService.js

**Descripción:** Servicio centralizado para comunicación con la API REST.

**Configuración Base:**
```javascript
import axios from 'axios'

const API_BASE_URL = 'http://localhost:5176/api'

const apiClient = axios.create({
  baseURL: API_BASE_URL,
  headers: {
    'Content-Type': 'application/json',
  },
  timeout: 10000
})
```

**Interceptor de Errores:**
```javascript
apiClient.interceptors.response.use(
  (response) => response,
  (error) => {
    console.error('Error en API:', error)
    return Promise.reject(error)
  }
)
```

**Métodos Disponibles:**

#### 1. crear(endpoint, data)
```javascript
// Crear un nuevo registro
await apiService.crear('Area_Acopio', {
  nlote: 'LOTE-001',
  nrecibo: 123,
  // ... más campos
})
```

#### 2. obtenerTodos(endpoint)
```javascript
// Obtener todos los registros
const registros = await apiService.obtenerTodos('Area_Acopio')
```

#### 3. obtenerPorId(endpoint, id)
```javascript
// Obtener un registro específico
const registro = await apiService.obtenerPorId('Area_Acopio', 'LOTE-001')
```

#### 4. actualizar(endpoint, id, data)
```javascript
// Actualizar un registro
await apiService.actualizar('Area_Acopio', 'LOTE-001', {
  nrecibo: 456,
  // ... campos a actualizar
})
```

#### 5. eliminar(endpoint, id)
```javascript
// Eliminar un registro
await apiService.eliminar('Area_Acopio', 'LOTE-001')
```

**Manejo de Errores:**
```javascript
try {
  const response = await apiService.crear('Area_Acopio', data)
  console.log('Éxito:', response)
} catch (error) {
  if (error.response) {
    // Error de respuesta del servidor
    console.error('Error API:', error.response.data)
  } else if (error.request) {
    // No hubo respuesta
    console.error('Sin respuesta del servidor')
  } else {
    // Error en la configuración
    console.error('Error:', error.message)
  }
}
```

**Endpoints Utilizados:**

| Módulo | Endpoint | Métodos Usados |
|--------|----------|----------------|
| Área de Acopio | `/api/Area_Acopio` | crear, obtenerTodos, obtenerPorId |
| Secado | `/api/SecadoApi` | crear, obtenerTodos |
| Bodega | `/api/Bodega` | crear, obtenerTodos |
| Trilla | `/api/Trilla` | crear, obtenerTodos |
| Catación | `/api/CatacionApi` | crear, obtenerTodos |
| Caracterización | `/api/Formulario_Caracterizacion` | crear, obtenerTodos |

---

## Guía de Migración a Angular

### Equivalencias Vue → Angular

#### 1. Estructura de Componentes

**Vue (SFC):**
```vue
<template>
  <div>{{ mensaje }}</div>
</template>

<script>
export default {
  data() {
    return { mensaje: 'Hola' }
  }
}
</script>

<style scoped>
/* estilos */
</style>
```

**Angular (Component):**
```typescript
// component.ts
import { Component } from '@angular/core';

@Component({
  selector: 'app-componente',
  templateUrl: './component.html',
  styleUrls: ['./component.css']
})
export class ComponenteComponent {
  mensaje: string = 'Hola';
}
```

```html
<!-- component.html -->
<div>{{ mensaje }}</div>
```

#### 2. Directivas y Binding

| Vue | Angular | Descripción |
|-----|---------|-------------|
| `v-model` | `[(ngModel)]` | Two-way binding |
| `v-if` | `*ngIf` | Renderizado condicional |
| `v-for` | `*ngFor` | Bucle de elementos |
| `v-show` | `[hidden]` | Mostrar/ocultar con CSS |
| `@click` | `(click)` | Event binding |
| `:class` | `[class]` o `[ngClass]` | Class binding |
| `:style` | `[style]` o `[ngStyle]` | Style binding |

**Ejemplo - Formulario de Acopio:**

**Vue:**
```vue
<input
  type="text"
  v-model="form.lote"
  @blur="validarCampo('lote')"
  :class="{ 'input-error': errors.lote }"
/>
```

**Angular:**
```html
<input
  type="text"
  [(ngModel)]="form.lote"
  (blur)="validarCampo('lote')"
  [class.input-error]="errors.lote"
/>
```

#### 3. Ciclo de Vida

| Vue | Angular | Uso |
|-----|---------|-----|
| `beforeCreate` | - | - |
| `created` | `constructor()` | Inicialización |
| `beforeMount` | - | - |
| `mounted` | `ngOnInit()` | Después del render |
| `beforeUpdate` | - | - |
| `updated` | `ngAfterViewChecked()` | Después de actualización |
| `beforeUnmount` | `ngOnDestroy()` | Limpieza |

**Ejemplo:**

**Vue:**
```javascript
export default {
  mounted() {
    this.cargarDatos();
  }
}
```

**Angular:**
```typescript
ngOnInit() {
  this.cargarDatos();
}
```

#### 4. Métodos y Computed

**Vue:**
```javascript
export default {
  data() {
    return {
      form: { pmecanico: 60, psolar: 40 }
    }
  },
  computed: {
    totalPorcentajes() {
      return this.form.pmecanico + this.form.psolar;
    }
  },
  methods: {
    submitForm() {
      // ...
    }
  }
}
```

**Angular:**
```typescript
export class ComponenteComponent {
  form = {
    pmecanico: 60,
    psolar: 40
  };

  get totalPorcentajes(): number {
    return this.form.pmecanico + this.form.psolar;
  }

  submitForm() {
    // ...
  }
}
```

#### 5. Servicios (apiService.js → Angular Service)

**Vue (apiService.js):**
```javascript
const apiService = {
  async crear(endpoint, data) {
    const response = await apiClient.post(`/${endpoint}`, data)
    return response.data
  }
}
```

**Angular (api.service.ts):**
```typescript
import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5176/api';

  constructor(private http: HttpClient) { }

  crear(endpoint: string, data: any): Observable<any> {
    return this.http.post(`${this.baseUrl}/${endpoint}`, data);
  }

  obtenerTodos(endpoint: string): Observable<any[]> {
    return this.http.get<any[]>(`${this.baseUrl}/${endpoint}`);
  }

  obtenerPorId(endpoint: string, id: string | number): Observable<any> {
    return this.http.get<any>(`${this.baseUrl}/${endpoint}/${id}`);
  }

  actualizar(endpoint: string, id: string | number, data: any): Observable<any> {
    return this.http.put(`${this.baseUrl}/${endpoint}/${id}`, data);
  }

  eliminar(endpoint: string, id: string | number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${endpoint}/${id}`);
  }
}
```

**Uso en componente Angular:**
```typescript
import { ApiService } from './services/api.service';

export class FormularioAcopioComponent {
  constructor(private apiService: ApiService) { }

  submitForm() {
    this.apiService.crear('Area_Acopio', this.form)
      .subscribe({
        next: (response) => {
          console.log('Éxito:', response);
        },
        error: (error) => {
          console.error('Error:', error);
        }
      });
  }
}
```

#### 6. Routing

**Vue (router/index.js):**
```javascript
const routes = [
  { path: "/", name: "HomeView", component: HomeView },
  { path: "/formulario-nuevo", name: "FormularioNuevo", component: FormularioAcopio }
];
```

**Angular (app-routing.module.ts):**
```typescript
import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';

const routes: Routes = [
  { path: '', component: HomeViewComponent },
  { path: 'formulario-nuevo', component: FormularioAcopioComponent },
  { path: 'caracterizacion', component: FCaracterizacionComponent },
  { path: 'secado', component: FSecadoComponent },
  { path: 'bodega', component: FBodegaComponent },
  { path: 'trilla', component: FTrillaComponent },
  { path: 'catacion', component: FCatacionComponent },
  { path: 'historial', component: HistorialGeneralComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
```

**Navegación:**

**Vue:**
```javascript
this.$router.push('/historial');
this.$router.push({ name: 'HomeView' });
```

**Angular:**
```typescript
constructor(private router: Router) { }

navegarHistorial() {
  this.router.navigate(['/historial']);
}
```

#### 7. Validación de Formularios

**Angular (Reactive Forms):**
```typescript
import { FormBuilder, FormGroup, Validators } from '@angular/forms';

export class FormularioAcopioComponent {
  formularioAcopio: FormGroup;

  constructor(private fb: FormBuilder) {
    this.formularioAcopio = this.fb.group({
      lote: ['', [Validators.required, Validators.maxLength(50)]],
      recibo: ['', [Validators.required, Validators.min(1)]],
      productor: ['', Validators.required],
      finca: ['', Validators.required],
      zona: ['', Validators.required],
      altura: ['', [Validators.required, Validators.min(0)]],
      rangoObjetivo: ['', Validators.required]
    });
  }

  get formularioValido(): boolean {
    return this.formularioAcopio.valid;
  }
}
```

```html
<form [formGroup]="formularioAcopio" (ngSubmit)="submitForm()">
  <input formControlName="lote" type="text" />
  <span *ngIf="formularioAcopio.get('lote')?.errors?.['required']">
    Campo requerido
  </span>
</form>
```

---

### Estructura de Proyecto Angular Recomendada

```
coffee-angular-app/
├── src/
│   ├── app/
│   │   ├── core/                    # Servicios singleton, guards
│   │   │   ├── services/
│   │   │   │   └── api.service.ts
│   │   │   └── interceptors/
│   │   │       └── error.interceptor.ts
│   │   ├── shared/                  # Componentes compartidos
│   │   │   ├── components/
│   │   │   │   ├── menu-lateral/
│   │   │   │   └── ai-asistente/
│   │   │   └── directives/
│   │   ├── features/                # Módulos de características
│   │   │   ├── home/
│   │   │   │   ├── home.component.ts
│   │   │   │   ├── home.component.html
│   │   │   │   └── home.component.css
│   │   │   ├── acopio/
│   │   │   │   ├── formulario-acopio.component.ts
│   │   │   │   └── ...
│   │   │   ├── caracterizacion/
│   │   │   ├── secado/
│   │   │   ├── bodega/
│   │   │   ├── trilla/
│   │   │   ├── catacion/
│   │   │   └── historial/
│   │   ├── models/                  # Interfaces y modelos
│   │   │   ├── area-acopio.model.ts
│   │   │   ├── secado.model.ts
│   │   │   └── ...
│   │   ├── app-routing.module.ts
│   │   ├── app.component.ts
│   │   └── app.module.ts
│   ├── assets/
│   └── styles/
│       ├── _variables.scss          # Variables de diseño
│       ├── _mixins.scss
│       └── styles.scss              # Estilos globales
└── angular.json
```

---

### Modelos TypeScript (Interfaces)

**area-acopio.model.ts:**
```typescript
export interface AreaAcopio {
  nlote: string;
  nrecibo: number;
  nproductor: string;
  nfinca: string;
  zona: string;
  altura: number;
  robjetivo: number;
  rsobreobjetivo?: number;
  rtotal?: number;
  despulpado?: string;
  vendido: boolean;
  disponible?: number;
  enproceso: string;
  psegundas?: number;
  pdmecanicos?: number;
  ppulpaPergamino?: number;
  ppergaminoPulpa?: number;
  dfruta?: number;
  dpergamino_humedo?: number;
  id_Secado: number;
}
```

**secado.model.ts:**
```typescript
export interface Secado {
  id_Secado?: number;
  nlote: string;
  finicio: Date;
  ffinal: Date;
  dsecado?: number;
  psolar: number;
  pmecanico: number;
}
```

---

### Componentes Clave a Migrar

#### 1. FormularioAcopioComponent (Angular)

**Características:**
- Reactive Forms con validación
- Manejo de estado con RxJS
- Comunicación con ApiService
- Validación en tiempo real

**Ejemplo de estructura:**
```typescript
export class FormularioAcopioComponent implements OnInit {
  formularioAcopio: FormGroup;
  showSuccess = false;
  showError = false;
  errorMessage = '';

  constructor(
    private fb: FormBuilder,
    private apiService: ApiService,
    private router: Router
  ) {
    this.crearFormulario();
  }

  ngOnInit() {
    // Inicialización
  }

  crearFormulario() {
    this.formularioAcopio = this.fb.group({
      // ... campos del formulario
    });
  }

  submitForm() {
    if (this.formularioAcopio.valid) {
      this.apiService.crear('Area_Acopio', this.formularioAcopio.value)
        .subscribe({
          next: (response) => {
            this.showSuccess = true;
            setTimeout(() => this.router.navigate(['/']), 2000);
          },
          error: (error) => {
            this.errorMessage = error.message;
            this.showError = true;
          }
        });
    }
  }
}
```

#### 2. HomeViewComponent (Angular)

**Características:**
- Grid de tarjetas responsivo
- Modal con Angular Material Dialog (opcional)
- Navegación programática

**Ejemplo:**
```typescript
export class HomeViewComponent {
  sections = [
    {
      title: 'Área de Acopio',
      icon: '🏪',
      badge: 'Recepción',
      description: 'Registro y control de entrada de café cereza.',
      route: '/formulario-nuevo'
    },
    // ... más secciones
  ];

  constructor(
    private router: Router,
    private dialog: MatDialog  // Si usas Angular Material
  ) { }

  navegarFormulario(route: string) {
    this.router.navigate([route]);
  }
}
```

---

### Migración de Estilos

**Opción 1: SCSS con Variables**

Crear `_variables.scss`:
```scss
// Paleta de colores café
$burgundy: #A52A3D;
$verde-claro: #8FAD5A;
$verde-oscuro: #4A5D2E;
$cafe-claro: #E5C29F;
$cafe-medio: #C8956F;
$cafe-oscuro: #8B5A3C;

// Espaciado
$space-xs: 4px;
$space-sm: 8px;
$space-md: 12px;
$space-lg: 16px;

// Mixins
@mixin btn-base {
  padding: $space-md $space-lg;
  border: none;
  border-radius: 12px;
  cursor: pointer;
  transition: all 0.3s ease;
}
```

**Opción 2: CSS Custom Properties (igual que Vue)**

En `styles.css` (global):
```css
:root {
  --burgundy: #A52A3D;
  --verde-claro: #8FAD5A;
  /* ... todas las variables del App.vue */
}

.btn-base {
  /* ... estilos globales */
}
```

---

### Checklist de Migración

#### Fase 1: Configuración Inicial
- [ ] Crear proyecto Angular: `ng new coffee-angular-app`
- [ ] Instalar dependencias: HttpClient, FormsModule, ReactiveFormsModule
- [ ] Configurar Angular Material (opcional)
- [ ] Crear estructura de carpetas

#### Fase 2: Servicios
- [ ] Migrar apiService.js → api.service.ts
- [ ] Crear interceptor de errores
- [ ] Configurar CORS en environment

#### Fase 3: Modelos
- [ ] Crear interfaces TypeScript para cada entidad
- [ ] AreaAcopio, Secado, Bodega, Trilla, Catacion, etc.

#### Fase 4: Componentes Principales
- [ ] HomeViewComponent
- [ ] MenuLateralComponent
- [ ] AiAsistenteComponent

#### Fase 5: Formularios
- [ ] FormularioAcopioComponent (Reactive Forms)
- [ ] FCaracterizacionComponent
- [ ] FSecadoComponent
- [ ] FBodegaComponent
- [ ] FTrillaComponent
- [ ] FCatacionComponent

#### Fase 6: Otras Vistas
- [ ] HistorialGeneralComponent

#### Fase 7: Routing
- [ ] Configurar app-routing.module.ts
- [ ] Implementar navegación programática

#### Fase 8: Estilos
- [ ] Migrar variables CSS/SCSS
- [ ] Aplicar estilos globales
- [ ] Estilos específicos por componente

#### Fase 9: Testing
- [ ] Pruebas unitarias de componentes
- [ ] Pruebas de integración con API
- [ ] Pruebas E2E

#### Fase 10: Optimización
- [ ] Lazy loading de módulos
- [ ] Optimización de bundle
- [ ] PWA (opcional)

---

## Dependencias del Proyecto (package.json)

```json
{
  "name": "coffeeapp",
  "version": "0.1.0",
  "dependencies": {
    "axios": "^1.11.0",           // Cliente HTTP
    "core-js": "^3.8.3",          // Polyfills
    "vue": "^3.2.13",             // Vue.js 3
    "vue-router": "^4.5.1"        // Vue Router 4
  },
  "devDependencies": {
    "@babel/core": "^7.12.16",
    "@babel/eslint-parser": "^7.12.16",
    "@vue/cli-plugin-babel": "~5.0.0",
    "@vue/cli-plugin-eslint": "~5.0.0",
    "@vue/cli-service": "~5.0.0",
    "autoprefixer": "^10.4.21",
    "eslint": "^7.32.0",
    "eslint-plugin-vue": "^8.0.3",
    "postcss": "^8.5.6",
    "tailwindcss": "^4.1.11"      // Tailwind CSS
  }
}
```

**Scripts:**
- `npm run serve`: Servidor de desarrollo
- `npm run build`: Build de producción
- `npm run lint`: Linter

---

## Notas Finales

### Características Principales del Frontend

1. **Sistema de Diseño Coherente:**
   - Variables CSS globales
   - Paleta de colores temática de café
   - Componentes reutilizables

2. **Formularios Robustos:**
   - Validación en tiempo real
   - Mensajes de error específicos
   - Estados de carga

3. **Integración con API:**
   - Servicio centralizado
   - Manejo de errores
   - Timeout configurado

4. **UX/UI:**
   - Animaciones suaves
   - Feedback visual
   - Responsivo

5. **Navegación:**
   - Menú lateral deslizable
   - Breadcrumbs visuales
   - Navegación programática

### Consideraciones para Angular

1. **TypeScript:** Angular requiere TypeScript, lo que agrega type safety
2. **Reactive Forms:** Mejor para formularios complejos con validación
3. **RxJS:** Manejo de asincronía con Observables
4. **Dependency Injection:** Sistema de inyección de dependencias robusto
5. **Módulos:** Organización en módulos para lazy loading

---

**Documentación generada para:** CoffeeBeanFlow Frontend (Vue.js)
**Versión:** 1.0
**Fecha:** 2025-12-13
**Framework:** Vue.js 3 con Vue Router 4
**Próxima Migración:** Angular