# Documentación del Backend - CoffeeBeanFlow

## Índice
1. [Descripción General](#descripción-general)
2. [Arquitectura del Proyecto](#arquitectura-del-proyecto)
3. [Modelos de Base de Datos](#modelos-de-base-de-datos)
4. [Controladores y Endpoints de la API](#controladores-y-endpoints-de-la-api)
5. [Configuración y Dependencias](#configuración-y-dependencias)
6. [Relaciones entre Entidades](#relaciones-entre-entidades)

---

## Descripción General

**CoffeeBeanFlowDB** es el backend del sistema de gestión de café, desarrollado en **.NET Core** con **Entity Framework Core** y base de datos **PostgreSQL**. El sistema gestiona todo el proceso de producción del café, desde el acopio hasta la catación final.

**Tecnologías Principales:**
- ASP.NET Core Web API
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger/OpenAPI para documentación
- CORS configurado para Vue.js

**Base URL de la API:** `http://localhost:5176/api`

---

## Arquitectura del Proyecto

### Estructura de Carpetas

```
CoffeBeanFlowDB/
├── Controllers/          # Controladores de la API REST
├── Models/              # Modelos de entidad (Item) y contextos (DbContext)
├── Migrations/          # Migraciones de Entity Framework
├── Program.cs           # Configuración principal de la aplicación
└── appsettings.json     # Configuración de conexión a BD
```

### Patrón de Arquitectura

- **Patrón:** API RESTful con Entity Framework Core
- **Base de Datos:** PostgreSQL con contextos separados por entidad
- **Operaciones CRUD:** Completas para cada entidad (GET, POST, PUT, DELETE)

---

## Modelos de Base de Datos

A continuación se detallan todos los modelos del sistema con sus atributos y tipos de datos.

### 1. Area_AcopioItem

**Tabla:** `Area_Acopio`
**Descripción:** Gestiona el registro de entrada del café en el área de acopio.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `Nlote` | `string` | Número de lote (PK) | Obligatorio, único |
| `Rtotal` | `decimal` | Rendimiento total | - |
| `Robjetivo` | `decimal` | Rendimiento objetivo | - |
| `Rsobreobjetivo` | `decimal` | Rendimiento sobre objetivo | - |
| `Vendido` | `bool` | Estado de venta | - |
| `Disponible` | `decimal` | Cantidad disponible | - |
| `Enproceso` | `string` | Estado en proceso | - |
| `Altura` | `decimal` | Altura de cultivo (msnm) | - |
| `Zona` | `string` | Zona geográfica | - |
| `Nrecibo` | `int` | Número de recibo | - |
| `Nproductor` | `string` | Nombre del productor | - |
| `Nfinca` | `string` | Nombre de la finca | - |
| `Despulpado` | `string` | Tipo de despulpado (5 tipos) | - |
| `Psegundas` | `decimal` | Porcentaje de segundas | - |
| `PDmecanicos` | `decimal` | Porcentaje de daños mecánicos | - |
| `PPulpaPergamino` | `decimal` | Porcentaje pulpa en pergamino | - |
| `PPergaminoPulpa` | `decimal` | Porcentaje pergamino en pulpa | - |
| `DFruta` | `decimal` | Densidad de fruta | - |
| `Dpergamino_humedo` | `decimal` | Densidad pergamino húmedo | - |
| `ID_Secado` | `int` | ID del proceso de secado (FK) | Obligatorio |

**Llaves:**
- **PK:** `Nlote`
- **FK:** `ID_Secado` → `SecadoItem`

---

### 2. BodegaItem

**Tabla:** `Bodega`
**Descripción:** Gestión del almacenamiento del café en bodega.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Bodega` | `int` | Identificador único (PK) | Auto-incremental |
| `D_Bellota` | `decimal` | Densidad de bellota | - |
| `D_Pergamino` | `decimal` | Densidad de pergamino | - |
| `Hinicial` | `decimal` | Humedad inicial | - |
| `Hfinal` | `decimal` | Humedad final | - |
| `W_pergamino` | `decimal` | Peso de pergamino | - |
| `W_bellota` | `decimal` | Peso de bellota | - |
| `FinicioReposo` | `DateTime` | Fecha inicio de reposo | - |
| `CantidadSacos` | `int` | Cantidad de sacos | - |
| `PMTexterna` | `decimal` | Promedio mensual temperatura externa | - |
| `PMTinterna` | `decimal` | Promedio mensual temperatura interna | - |
| `PMH_relativa` | `decimal` | Promedio mensual humedad relativa | - |
| `Nlote` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `ID_Bodega`
- **FK:** `Nlote` → `Area_AcopioItem`

---

### 3. CatacionItem

**Tabla:** `Catacion`
**Descripción:** Registro de catación y evaluación sensorial del café.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_catacion` | `int` | Identificador único (PK) | Auto-incremental |
| `FFreposo` | `DateTime` | Fecha final de reposo | - |
| `Nlote` | `string` | Número de lote | - |
| `Defectuoso` | `bool` | Indica si es defectuoso | - |
| `Limpio` | `bool` | Indica si está limpio | - |
| `Overde` | `string` | Olor verde | - |
| `CCcverde` | `string` | Clasificación color verde | - |
| `Quaker` | `int` | Cantidad de quaker | - |
| `Rtostado` | `decimal` | Rendimiento/resultado tostado | - |
| `Dtueste` | `decimal` | Densidad en tueste | - |
| `CCcalidad` | `int` | Clasificación de calidad | - |
| `Pfinales` | `decimal` | Puntos finales | - |
| `TAgtron` | `int` | Medición Agtron | - |

**Defectos Categoría 1 (Primarios):**
| `C1negro` | `decimal` | Defecto: granos negros | - |
| `C1ME` | `decimal` | Defecto: materia extraña | - |
| `C1insectos` | `decimal` | Defecto: insectos | - |
| `C1cerezaseca` | `decimal` | Defecto: cereza seca | - |
| `C1hongos` | `decimal` | Defecto: hongos | - |
| `C1agrio` | `decimal` | Defecto: agrio | - |

**Defectos Categoría 2 (Secundarios):**
| `C2pergamino` | `int` | Defecto: pergamino | - |
| `C2inmaduro` | `int` | Defecto: inmaduro | - |
| `C2negroP` | `decimal` | Defecto: negro parcial | - |
| `C2agrioP` | `decimal` | Defecto: agrio parcial/pleno | - |
| `C2cascara_pulpa` | `decimal` | Defecto: cáscara/pulpa | - |
| `C2insectos` | `decimal` | Defecto: insectos | - |
| `C2averanado` | `decimal` | Defecto: averanado | - |
| `C2partido_cortado_mordido` | `decimal` | Defecto: partido/cortado/mordido | - |
| `C2concha` | `decimal` | Defecto: concha | - |
| `C2flotador` | `decimal` | Defecto: flotador | - |

**Medidas de Zarandas (en mm):**
| `Trece` | `decimal` | Zaranda 13 | - |
| `Catorce` | `decimal` | Zaranda 14 | - |
| `Quince` | `decimal` | Zaranda 15 | - |
| `Dieciseis` | `decimal` | Zaranda 16 | - |
| `Diecisiete` | `decimal` | Zaranda 17 | - |
| `Dieciocho` | `decimal` | Zaranda 18 | - |
| `Diecinueve` | `decimal` | Zaranda 19 | - |
| `Veinte` | `decimal` | Zaranda 20 | - |
| `TresSobreDieciseis` | `decimal` | Zaranda 3/16 | - |
| `Residuo` | `string` | Residuos | - |

**Llaves:**
- **PK:** `ID_catacion`

---

### 4. Enviar_muestrasItem

**Tabla:** `Enviar_muestras`
**Descripción:** Relación entre trilla y catación (envío de muestras).

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Trilla` | `int` | ID de trilla (PK) | - |
| `ID_Catacion` | `int` | ID de catación (FK) | - |

**Llaves:**
- **PK:** `ID_Trilla`
- **FK:** `ID_Catacion` → `CatacionItem`

---

### 5. Formulario_CaracterizacionItem

**Tabla:** `Formulario_Caracterizacion`
**Descripción:** Caracterización física del café.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `Tiempo` | `DateTime` | Tiempo de caracterización (PK) | - |
| `Cinmaduras` | `int` | Cerezas inmaduras | - |
| `Csobremaduras` | `int` | Cerezas sobremaduras | - |
| `Csecas` | `int` | Cerezas secas | - |
| `Cobjetivo` | `int` | Cerezas objetivo | - |
| `Cverdes` | `int` | Cerezas verdes | - |
| `PCdebajo` | `decimal` | Porcentaje cerezas debajo | - |
| `Proceso` | `string` | Tipo de proceso (lavado, miel, etc.) | - |
| `DRmaduras` | `decimal` | Determinación rango óptimo maduras | - |
| `Mtabla` | `decimal` | Muestreo tabla | - |
| `PCverdes` | `decimal` | Porcentaje cerezas verdes | - |
| `PCsecas` | `decimal` | Porcentaje cerezas secas | - |
| `PCencima` | `decimal` | Porcentaje cerezas encima | - |
| `Emaduracion` | `decimal` | Escala de maduración | - |
| `Broca` | `decimal` | Nivel de broca | - |
| `Densidad` | `decimal` | Densidad | - |
| `Vanos` | `decimal` | Granos vanos | - |
| `PCobjetivo` | `decimal` | Porcentaje cerezas objetivo | - |
| `Secos` | `decimal` | Granos secos | - |
| `Nlote_AreaAcopio` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `Tiempo`
- **FK:** `Nlote_AreaAcopio` → `Area_AcopioItem`

---

### 6. Gbx_inmadurasItem

**Tabla:** `Gbx_inmaduras`
**Descripción:** Grados Brix de cerezas inmaduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_inmaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_inmaduras` | `int` | ID de inmaduras (FK) | - |

**Llaves:**
- **PK:** `ID_Gbx_inmaduras`
- **FK:** `ID_inmaduras` → `RCinmadurasItem`

---

### 7. Gbx_madurasItem

**Tabla:** `Gbx_maduras`
**Descripción:** Grados Brix de cerezas maduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_maduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_maduras` | `int` | ID de maduras (FK) | - |

**Llaves:**
- **PK:** `ID_Gbx_maduras`
- **FK:** `ID_maduras` → `RCmadurasItem`

---

### 8. Gbx_sobremadurasItem

**Tabla:** `Gbx_sobremaduras`
**Descripción:** Grados Brix de cerezas sobremaduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_sobremaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_sobremaduras` | `int` | ID de sobremaduras (FK) | - |

**Llaves:**
- **PK:** `ID_Gbx_sobremaduras`
- **FK:** `ID_sobremaduras` → `RCsobremadurasItem`

---

### 9. Guardar_CafeItem

**Tabla:** `Guardar_Cafe`
**Descripción:** Relación entre secado y bodega.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Secado` | `int` | ID de secado (PK) | - |
| `ID_Bodega` | `int` | ID de bodega (FK) | - |

**Llaves:**
- **PK:** `ID_Secado`
- **FK:** `ID_Bodega` → `BodegaItem`

---

### 10. HumedadItem

**Tabla:** `Humedad`
**Descripción:** Registro de humedad durante el secado.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Humedad` | `int` | Identificador único (PK) | Auto-incremental |
| `PHumedad` | `decimal` | Porcentaje de humedad | - |
| `Temperatura` | `int` | Temperatura | - |
| `ID_Secado` | `int` | ID de secado (FK) | - |

**Llaves:**
- **PK:** `ID_Humedad`
- **FK:** `ID_Secado` → `SecadoItem`

---

### 11. NcamaItem

**Tabla:** `Ncama`
**Descripción:** Número de cama de secado.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Ncama` | `int` | Identificador único (PK) | Auto-incremental |
| `ID_Secado` | `int` | ID de secado (FK) | - |

**Llaves:**
- **PK:** `ID_Ncama`
- **FK:** `ID_Secado` → `SecadoItem`

---

### 12. PesoVerdeItem

**Tabla:** `PesoVerde`
**Descripción:** Registro de pesos durante el proceso de trilla.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_PesoVerde` | `int` | Identificador único (PK) | Auto-incremental |
| `Winferiores` | `decimal` | Peso de inferiores | - |
| `Wfinal` | `decimal` | Peso final | - |
| `WFinferior` | `decimal` | Peso final inferior | - |
| `ID_PesoTrilla` | `int` | ID de trilla (FK) | - |

**Llaves:**
- **PK:** `ID_PesoVerde`
- **FK:** `ID_PesoTrilla` → `TrillaItem`

---

### 13. RCinmadurasItem

**Tabla:** `RCinmaduras`
**Descripción:** Registro de caracterización de cerezas inmaduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_inmaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix | - |
| `Tiempo` | `DateTime` | Tiempo (FK) | - |

**Llaves:**
- **PK:** `ID_inmaduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem`

---

### 14. RCmadurasItem

**Tabla:** `RCmaduras`
**Descripción:** Registro de caracterización de cerezas maduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_maduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix | - |
| `Tiempo` | `DateTime` | Tiempo (FK) | - |

**Llaves:**
- **PK:** `ID_maduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem`

---

### 15. RCsobremadurasItem

**Tabla:** `RCsobremaduras`
**Descripción:** Registro de caracterización de cerezas sobremaduras.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_sobremaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix | - |
| `Tiempo` | `DateTime` | Tiempo (FK) | - |

**Llaves:**
- **PK:** `ID_sobremaduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem`

---

### 16. Registro_FormularioItem

**Tabla:** `Registro_Formulario`
**Descripción:** Registro principal del formulario de caracterización.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Formulario` | `int` | Identificador único (PK) | Auto-incremental |
| `ID_sobremaduras` | `int` | ID sobremaduras (FK) | - |
| `ID_maduras` | `int` | ID maduras (FK) | - |
| `ID_inmaduras` | `int` | ID inmaduras (FK) | - |

**Llaves:**
- **PK:** `ID_Formulario`
- **FK:** `ID_sobremaduras`, `ID_maduras`, `ID_inmaduras`

---

### 17. RondasItem

**Tabla:** `Rondas`
**Descripción:** Rondas de catación.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Rondas` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor_calidad` | `decimal` | Valor de calidad | - |
| `ID_catacion` | `int` | ID de catación (FK) | - |

**Llaves:**
- **PK:** `ID_Rondas`
- **FK:** `ID_catacion` → `CatacionItem`

---

### 18. SecadoItem

**Tabla:** `Secado`
**Descripción:** Proceso de secado del café.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Secado` | `int` | Identificador único (PK) | Auto-incremental |
| `Finicio` | `DateTime` | Fecha de inicio | - |
| `Dsecado` | `decimal` | D secado | - |
| `Psolar` | `decimal` | Porcentaje solar | - |
| `Pmecanico` | `decimal` | Porcentaje mecánico | - |
| `Ffinal` | `DateTime` | Fecha final | - |
| `Nlote` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `ID_Secado`
- **FK:** `Nlote` → `Area_AcopioItem`

---

### 19. SuministraItem

**Tabla:** `Suministra`
**Descripción:** Relación entre bodega y trilla.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Bodega` | `int` | ID de bodega (PK) | - |
| `ID_Trilla` | `int` | ID de trilla (FK) | - |

**Llaves:**
- **PK:** `ID_Bodega`
- **FK:** `ID_Trilla` → `TrillaItem`

---

### 20. TemperaturaSecadoItem

**Tabla:** `TemperaturaSecado`
**Descripción:** Registro de temperaturas durante el secado.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Temperatura` | `int` | Identificador único (PK) | Auto-incremental |
| `Lectura` | `int` | Lectura de temperatura | - |
| `ID_Secado` | `int` | ID de secado (FK) | - |

**Llaves:**
- **PK:** `ID_Temperatura`
- **FK:** `ID_Secado` → `SecadoItem`

---

### 21. TermoHigrometriaItem

**Tabla:** `TermoHigrometria`
**Descripción:** Registro de termohigrometría.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Termo` | `int` | Identificador único (PK) | Auto-incremental |
| `Hrelativa` | `decimal` | Humedad relativa | - |
| `Tinterna` | `int` | Temperatura interna | - |
| `Texterna` | `int` | Temperatura externa | - |
| `ID_Secado` | `int` | ID de secado (FK) | - |

**Llaves:**
- **PK:** `ID_Termo`
- **FK:** `ID_Secado` → `SecadoItem`

---

### 22. TrillaItem

**Tabla:** `Trilla`
**Descripción:** Proceso de trillado del café.

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Trilla` | `int` | Identificador único (PK) | Auto-incremental |
| `Psegundas` | `decimal` | Porcentaje segundas | - |
| `Pmenudos` | `decimal` | Porcentaje menudos | - |
| `Pinferiores` | `decimal` | Porcentaje inferiores | - |
| `Pmadres` | `decimal` | Porcentaje madres | - |
| `Pprimera` | `decimal` | Porcentaje primera | - |
| `Pcaracolillo` | `decimal` | Porcentaje caracolillo | - |
| `Pescogeduras` | `decimal` | Peso escogeduras | - |
| `Pbarreduras` | `decimal` | Porcentaje barreduras | - |
| `Pcataduras` | `decimal` | Porcentaje cataduras | - |
| `POinferiores` | `decimal` | Porcentaje otras inferiores | - |
| `RTeoricoSeleccion` | `decimal` | Rendimiento teórico selección | - |
| `RTeoricoPelado` | `decimal` | Rendimiento teórico pelado | - |
| `RfinalPelado` | `decimal` | Rendimiento final pelado | - |
| `RfinalSeleccion` | `decimal` | Rendimiento final selección | - |
| `WVerdeFinal` | `decimal` | Peso verde final | - |
| `WVerdeTeorico` | `decimal` | Peso verde teórico | - |
| `FFinalReposo` | `DateTime` | Fecha final de reposo | - |
| `HFinal` | `decimal` | Humedad final | - |
| `HInicial` | `decimal` | Humedad inicial | - |
| `Nlote` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `ID_Trilla`
- **FK:** `Nlote` → `Area_AcopioItem`

---

## Controladores y Endpoints de la API

Todos los controladores siguen el patrón REST estándar. A continuación se detalla la estructura general:

### Patrón General de Endpoints

Para cada entidad, la API expone los siguientes endpoints:

| Método HTTP | Ruta | Descripción | Parámetros |
|-------------|------|-------------|------------|
| GET | `/api/{Controller}` | Obtener todos los registros | - |
| GET | `/api/{Controller}/{id}` | Obtener un registro por ID | `id` (ruta) |
| POST | `/api/{Controller}` | Crear un nuevo registro | JSON Body |
| PUT | `/api/{Controller}/{id}` | Actualizar un registro existente | `id` (ruta), JSON Body |
| DELETE | `/api/{Controller}/{id}` | Eliminar un registro | `id` (ruta) |

### Lista de Controladores

1. **Area_AcopioController** → `/api/Area_Acopio`
2. **BodegaController** → `/api/Bodega`
3. **CatacionApiController** → `/api/CatacionApi`
4. **Enviar_muestrasController** → `/api/Enviar_muestras`
5. **Formulario_CaracterizacionController** → `/api/Formulario_Caracterizacion`
6. **Gbx_inmadurasController** → `/api/Gbx_inmaduras`
7. **Gbx_madurasController** → `/api/Gbx_maduras`
8. **Gbx_sobremadurasController** → `/api/Gbx_sobremaduras`
9. **Guardar_CafeController** → `/api/Guardar_Cafe`
10. **HumedadController** → `/api/Humedad`
11. **NcamaController** → `/api/Ncama`
12. **PesoVerdeController** → `/api/PesoVerde`
13. **RCinmadurasController** → `/api/RCinmaduras`
14. **RCmadurasController** → `/api/RCmaduras`
15. **RCsobremadurasController** → `/api/RCsobremaduras`
16. **Registro_FormularioController** → `/api/Registro_Formulario`
17. **RondasController** → `/api/Rondas`
18. **SecadoApiController** → `/api/SecadoApi`
19. **SuministraController** → `/api/Suministra`
20. **TemperaturaSecadoController** → `/api/TemperaturaSecado`
21. **TermoHigrometriaController** → `/api/TermoHigrometria`
22. **TrillaController** → `/api/Trilla`

### Ejemplos de Endpoints Específicos

#### Area_AcopioController

**Base URL:** `/api/Area_Acopio`

```http
GET    /api/Area_Acopio              # Obtener todos los registros de acopio
GET    /api/Area_Acopio/{Nlote}      # Obtener registro por número de lote
POST   /api/Area_Acopio              # Crear nuevo registro
PUT    /api/Area_Acopio/{Nlote}      # Actualizar registro existente
DELETE /api/Area_Acopio/{Nlote}      # Eliminar registro
```

**Ejemplo de POST:**
```json
{
  "nlote": "LOTE-2025-001",
  "nrecibo": 12345,
  "nproductor": "José Pérez",
  "nfinca": "La Esperanza",
  "zona": "Zona Norte",
  "altura": 1500,
  "robjetivo": 85.5,
  "rsobreobjetivo": 90.0,
  "rtotal": 88.2,
  "despulpado": "lavado",
  "vendido": false,
  "disponible": 1000.5,
  "enproceso": "No",
  "psegundas": 5.2,
  "pdmecanicos": 2.1,
  "ppulpaPergamino": 1.5,
  "ppergaminoPulpa": 1.2,
  "dfruta": 1.05,
  "dpergamino_humedo": 0.85,
  "id_Secado": 1
}
```

#### SecadoApiController

**Base URL:** `/api/SecadoApi`

```http
GET    /api/SecadoApi           # Obtener todos los procesos de secado
GET    /api/SecadoApi/{id}      # Obtener proceso por ID
POST   /api/SecadoApi           # Crear nuevo proceso
PUT    /api/SecadoApi/{id}      # Actualizar proceso
DELETE /api/SecadoApi/{id}      # Eliminar proceso
```

**Ejemplo de POST:**
```json
{
  "nlote": "LOTE-2025-001",
  "finicio": "2025-01-15T08:00:00",
  "ffinal": "2025-01-22T18:00:00",
  "dsecado": 7.5,
  "psolar": 60.0,
  "pmecanico": 40.0
}
```

#### CatacionApiController

**Base URL:** `/api/CatacionApi`

```http
GET    /api/CatacionApi           # Obtener todas las cataciones
GET    /api/CatacionApi/{id}      # Obtener catación por ID
POST   /api/CatacionApi           # Crear nueva catación
PUT    /api/CatacionApi/{id}      # Actualizar catación
DELETE /api/CatacionApi/{id}      # Eliminar catación
```

---

## Configuración y Dependencias

### Program.cs - Configuración Principal

**Características principales:**

1. **Configuración de CORS:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8080",
                "http://localhost:8081",
                "http://localhost:3000",
                "http://localhost:5173",
                "http://127.0.0.1:8080",
                "http://127.0.0.1:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});
```

2. **Configuración de JSON:**
```csharp
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });
```

3. **Registro de DbContexts:**
Todos los contextos están registrados con PostgreSQL (Npgsql):
```csharp
builder.Services.AddDbContext<Area_AcopioContext>(options =>
    options.UseNpgsql(connectionString));
builder.Services.AddDbContext<BodegaContext>(options =>
    options.UseNpgsql(connectionString));
// ... y así para todos los contextos
```

4. **Swagger/OpenAPI:**
```csharp
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
```

5. **Endpoint de Health Check:**
```http
GET /api/health
```
Respuesta:
```json
{
  "status": "OK",
  "timestamp": "2025-01-15T10:30:00Z",
  "message": "API is running"
}
```

### Dependencias NuGet

- **Microsoft.EntityFrameworkCore**
- **Npgsql.EntityFrameworkCore.PostgreSQL**
- **Swashbuckle.AspNetCore** (Swagger)
- **Microsoft.AspNetCore.Mvc** (ASP.NET Core MVC)

---

## Relaciones entre Entidades

### Diagrama de Flujo del Proceso

```
Area_Acopio (Nlote)
    ↓
Formulario_Caracterizacion (Nlote_AreaAcopio)
    ↓
RCinmaduras/RCmaduras/RCsobremaduras (Tiempo)
    ↓
Gbx_inmaduras/Gbx_maduras/Gbx_sobremaduras
    ↓
Secado (ID_Secado) ← vinculado a Area_Acopio
    ↓
Humedad, TermoHigrometria, TemperaturaSecado, Ncama (ID_Secado)
    ↓
Guardar_Cafe (ID_Secado → ID_Bodega)
    ↓
Bodega (ID_Bodega)
    ↓
Suministra (ID_Bodega → ID_Trilla)
    ↓
Trilla (ID_Trilla)
    ↓
PesoVerde (ID_PesoTrilla)
    ↓
Enviar_muestras (ID_Trilla → ID_Catacion)
    ↓
Catacion (ID_catacion)
    ↓
Rondas (ID_catacion)
```

### Relaciones Principales

| Entidad Padre | Entidad Hija | Tipo de Relación | Clave Foránea |
|---------------|--------------|------------------|---------------|
| `Area_Acopio` | `Formulario_Caracterizacion` | 1:N | `Nlote_AreaAcopio` |
| `Area_Acopio` | `Secado` | 1:1 | `Nlote` |
| `Area_Acopio` | `Trilla` | 1:N | `Nlote` |
| `Secado` | `Humedad` | 1:N | `ID_Secado` |
| `Secado` | `TermoHigrometria` | 1:N | `ID_Secado` |
| `Secado` | `TemperaturaSecado` | 1:N | `ID_Secado` |
| `Secado` | `Ncama` | 1:N | `ID_Secado` |
| `Secado` | `Guardar_Cafe` | 1:1 | `ID_Secado` |
| `Bodega` | `Guardar_Cafe` | 1:1 | `ID_Bodega` |
| `Bodega` | `Suministra` | 1:1 | `ID_Bodega` |
| `Trilla` | `Suministra` | 1:1 | `ID_Trilla` |
| `Trilla` | `PesoVerde` | 1:N | `ID_PesoTrilla` |
| `Trilla` | `Enviar_muestras` | 1:1 | `ID_Trilla` |
| `Catacion` | `Enviar_muestras` | 1:1 | `ID_Catacion` |
| `Catacion` | `Rondas` | 1:N | `ID_catacion` |
| `Formulario_Caracterizacion` | `RCinmaduras` | 1:N | `Tiempo` |
| `Formulario_Caracterizacion` | `RCmaduras` | 1:N | `Tiempo` |
| `Formulario_Caracterizacion` | `RCsobremaduras` | 1:N | `Tiempo` |
| `RCinmaduras` | `Gbx_inmaduras` | 1:N | `ID_inmaduras` |
| `RCmaduras` | `Gbx_maduras` | 1:N | `ID_maduras` |
| `RCsobremaduras` | `Gbx_sobremaduras` | 1:N | `ID_sobremaduras` |

---

## Notas de Implementación

### Convenciones de Nombres
- **Modelos:** Terminan en `Item` (ej: `Area_AcopioItem`)
- **Contextos:** Terminan en `Context` (ej: `Area_AcopioContext`)
- **Controladores:** Terminan en `Controller` o `ApiController`

### Estrategia de Base de Datos
- Cada entidad tiene su propio `DbContext`
- Se utiliza PostgreSQL como motor de base de datos
- Las migraciones se encuentran organizadas por entidad en carpetas separadas

### Validaciones
- Las validaciones se realizan a nivel de controlador con `ModelState.IsValid`
- Se manejan excepciones de concurrencia con `DbUpdateConcurrencyException`
- Se controlan conflictos con `DbUpdateException`

### Manejo de Errores
- **200 OK:** Operación exitosa
- **201 Created:** Recurso creado
- **204 No Content:** Actualización/eliminación exitosa
- **400 Bad Request:** Validación fallida
- **404 Not Found:** Recurso no encontrado
- **409 Conflict:** Conflicto de duplicados

---

**Documentación generada para:** CoffeeBeanFlow Backend API
**Versión:** 1.0
**Fecha:** 2025-12-13
**Framework:** .NET Core con Entity Framework Core
**Base de Datos:** PostgreSQL