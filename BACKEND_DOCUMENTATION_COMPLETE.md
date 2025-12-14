# Documentación Completa del Backend - CoffeeBeanFlow

## Índice
1. [Descripción General](#descripción-general)
2. [Arquitectura del Proyecto](#arquitectura-del-proyecto)
3. [Modelo Conceptual y Relacional](#modelo-conceptual-y-relacional)
4. [Modelos de Base de Datos](#modelos-de-base-de-datos)
5. [Controladores y Endpoints de la API](#controladores-y-endpoints-de-la-api)
6. [Configuración y Dependencias](#configuración-y-dependencias)
7. [Relaciones entre Entidades](#relaciones-entre-entidades)
8. [Integridad Referencial](#integridad-referencial)
9. [Correcciones Críticas Requeridas](#correcciones-críticas-requeridas)

---

## Descripción General

**CoffeeBeanFlowDB** es el backend del sistema de gestión de café, desarrollado en **.NET 9.0** con **Entity Framework Core** y base de datos **PostgreSQL**. El sistema gestiona todo el proceso de producción del café, desde el acopio hasta la catación final.

**Tecnologías Objetivo:**
- ASP.NET Core Web API (.NET 9.0)
- Entity Framework Core
- PostgreSQL (Npgsql)
- Swagger/OpenAPI para documentación
- CORS configurado para Angular

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

## Modelo Conceptual y Relacional

### Resumen del Modelo

**Entidades Fuertes:** 6
- Área_Acopio
- Secado
- Trilla
- Bodega
- Formulario_Caracterizacion
- Catación

**Entidades Débiles:** 8
- Humedad (depende de Secado)
- TermoHigrometria (depende de Secado)
- TemperaturaSecado (depende de Secado)
- Ncama (depende de Secado)
- PesoVerde (depende de Trilla)
- RCsobremaduras (depende de Formulario_Caracterizacion)
- RCinmaduras (depende de Formulario_Caracterizacion)
- RCmaduras (depende de Formulario_Caracterizacion)

**Entidades Adicionales:** 5
- Registro_Formulario
- Gbx_sobremaduras
- Gbx_maduras
- Gbx_inmaduras
- Rondas

**Total de Entidades:** 19

**Tablas de Relación N:N:** 3
- Guardar_Cafe (Secado ↔ Bodega)
- Enviar_muestras (Trilla ↔ Catación)
- Suministra (Trilla → Bodega)

---

## Modelos de Base de Datos

A continuación se detallan todos los modelos del sistema con sus atributos, tipos de datos y claves foráneas según el **Modelo Conceptual Completo**.

### 1. Area_AcopioItem

**Tabla:** `Area_Acopio`
**Descripción:** Gestiona el registro de entrada del café en el área de acopio.
**Tipo:** Entidad fuerte

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `Nlote` | `string` | Número de lote (PK) | Obligatorio, único |
| `Altura` | `decimal` | Altura de cultivo (msnm) | - |
| `Zona` | `string` | Zona geográfica | - |
| `Nrecibo` | `int` | Número de recibo | - |
| `Nproductor` | `string` | Nombre del productor | - |
| `Nfinca` | `string` | Nombre de la finca | - |
| `Robjetivo` | `decimal` | Rendimiento objetivo | - |
| `Rtotal` | `decimal` | Rendimiento total | - |
| `Vendido` | `bool` | Estado de venta | - |
| `Disponible` | `decimal` | Cantidad disponible | - |
| `Enproceso` | `string` | Estado en proceso | - |

**Atributos Compuestos - Despulpado:**
| `Semilavado` | `bool` | Tipo de despulpado: Semilavado | - |
| `Natural` | `bool` | Tipo de despulpado: Natural | - |
| `Anaerobico` | `bool` | Tipo de despulpado: Anaerobico | - |
| `Otro` | `bool` | Tipo de despulpado: Otro | - |
| `Miel` | `bool` | Tipo de despulpado: Miel | - |
| `Lavado` | `bool` | Tipo de despulpado: Lavado | - |

**Atributos Compuestos - Pruebas_Fisicas_BH:**
| `PF_Pulpa_Pergamino` | `decimal` | Prueba Física: Pulpa en Pergamino | - |
| `PF_DMecanicos` | `decimal` | Prueba Física: Daños Mecánicos | - |
| `PF_Segundas` | `decimal` | Prueba Física: Segundas | - |
| `PF_Pergamino_Pulpa` | `decimal` | Prueba Física: Pergamino en Pulpa | - |
| `PDensidad_Fruta` | `decimal` | Densidad de Fruta (g/cm³) | - |
| `PDensidad_Pergamino_Humedo` | `decimal` | Densidad Pergamino Húmedo (g/cm³) | - |

**Llaves:**
- **PK:** `Nlote`
- **FK:** ❌ NO tiene (es entidad principal en el flujo)

**⚠️ CORRECCIÓN CRÍTICA:**
- **ELIMINAR:** `ID_Secado` como FK (esto invierte incorrectamente la relación)
- La relación correcta es: Secado tiene FK hacia Area_Acopio, NO al revés

---

### 2. SecadoItem

**Tabla:** `Secado`
**Descripción:** Proceso de secado del café.
**Tipo:** Entidad fuerte

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Secado` | `int` | Identificador único (PK) | Auto-incremental |
| `Finicio` | `DateTime` | Fecha de inicio | - |
| `Dsecado` | `decimal` | Días de secado | - |
| `Ffinal` | `DateTime` | Fecha final | - |
| `Nlote` | `string` | Número de lote (FK) | Obligatorio |

**Atributos Multivaluados (implementados como entidades débiles):**
- `Ncama` → Entidad débil NcamaItem
- `Tsecado` → Entidad débil TemperaturaSecadoItem

**Llaves:**
- **PK:** `ID_Secado`
- **FK:** `Nlote` → `Area_AcopioItem.Nlote`

**Relaciones:**
- Recibe café desde Área_Acopio (relación: Envía - 1:N)
- Guarda café en Bodega (relación: Guarda_en - N:N)
- Mide Humedad (relación identificadora - 1:N)
- Mide TermoHigrometria (relación identificadora - 1:N)

---

### 3. TrillaItem

**Tabla:** `Trilla`
**Descripción:** Proceso de trillado del café.
**Tipo:** Entidad fuerte

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Trilla` | `int` | Identificador único (PK) | Auto-incremental |
| `Hinicial` | `decimal` | Humedad inicial | - |
| `Hfinal` | `decimal` | Humedad final | - |
| `RFinalPelado` | `decimal` | Rendimiento final pelado | - |
| `RFinalSeleccion` | `decimal` | Rendimiento final selección | - |
| `WverdeFinal` | `decimal` | Peso verde final | - |
| `RteoricoPelado` | `decimal` | Rendimiento teórico pelado | - |
| `WverdeTeorico` | `decimal` | Peso verde teórico | - |
| `RTeoricoSeleccion` | `decimal` | Rendimiento teórico selección | - |
| `FfinalReposo` | `DateTime` | Fecha final de reposo | - |
| `Psegundas` | `decimal` | Porcentaje segundas | - |
| `Pcataduras` | `decimal` | Porcentaje cataduras | - |
| `Pbarreduras` | `decimal` | Porcentaje barreduras | - |
| `Pescogeduras` | `decimal` | Peso escogeduras | - |
| `Pcaracolillo` | `decimal` | Porcentaje caracolillo | - |
| `Pprimera` | `decimal` | Porcentaje primera | - |
| `Pmadres` | `decimal` | Porcentaje madres | - |
| `Pmenudos` | `decimal` | Porcentaje menudos | - |
| `Pinferiores` | `decimal` | Porcentaje inferiores | - |
| `Nlote` | `string` | Número de lote (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Trilla`
- **FK:** `Nlote` → `Area_AcopioItem.Nlote`

**Relaciones:**
- Exporta PesoVerde (relación identificadora - 1:1)
- Envía muestras a Catación (relación: Envía_muestras_A - N:N)
- Suministra a Bodega (relación: Suministra)

---

### 4. BodegaItem

**Tabla:** `Bodega`
**Descripción:** Gestión del almacenamiento del café en bodega.
**Tipo:** Entidad fuerte

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Bodega` | `int` | Identificador único (PK) | Auto-incremental |
| `W_bellota` | `decimal` | Peso de bellota | - |
| `W_pergamino` | `decimal` | Peso de pergamino | - |
| `Hfinal` | `decimal` | Humedad final | - |
| `Hinicial` | `decimal` | Humedad inicial | - |
| `D_Pergamino` | `decimal` | Densidad de pergamino | - |
| `D_Bellota` | `decimal` | Densidad de bellota | - |
| `FinicioReposo` | `DateTime` | Fecha inicio de reposo | - |
| `CantidadSacos` | `int` | Cantidad de sacos | - |
| `PMH_relativa` | `decimal` | Promedio mensual humedad relativa | - |
| `PMTinterna` | `decimal` | Promedio mensual temperatura interna | - |
| `PMTexterna` | `decimal` | Promedio mensual temperatura externa | - |
| `Nlote` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `ID_Bodega`
- **FK:** `Nlote` → `Area_AcopioItem.Nlote`

**Relaciones:**
- Recibe café desde Secado (relación: Guarda_en - N:N)
- Recibe producto de Trilla (relación: Suministra)

---

### 5. Formulario_CaracterizacionItem

**Tabla:** `Formulario_Caracterizacion`
**Descripción:** Caracterización física del café.
**Tipo:** Entidad fuerte

**Atributo compuesto (clave):**
| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `Tiempo` | `DateTime` | Tiempo de caracterización (PK) | Compuesto: Fecha + Hora |

**Atributos simples:**
| `DRmaduras` | `decimal` | Determinación rango óptimo maduras | - |
| `PCdebajo` | `decimal` | Porcentaje cerezas debajo | - |
| `Proceso` | `string` | Tipo de proceso (lavado, miel, etc.) | - |
| `LAsignado` | `string` | Lote Asignado | - |
| `Cverdes` | `int` | Cerezas verdes | - |
| `Cobjetivo` | `int` | Cerezas objetivo | - |
| `Cinmaduras` | `int` | Cerezas inmaduras | - |
| `Csobremaduras` | `int` | Cerezas sobremaduras | - |
| `Csecas` | `int` | Cerezas secas | - |
| `Mtabla` | `decimal` | Muestreo tabla | - |
| `PCverdes` | `decimal` | Porcentaje cerezas verdes | - |
| `PCsecas` | `decimal` | Porcentaje cerezas secas | - |
| `PCencima` | `decimal` | Porcentaje cerezas encima | - |
| `Emaduracion` | `decimal` | Escala de maduración | - |
| `Broca` | `decimal` | Nivel de broca | - |
| `Densidad` | `decimal` | Densidad | - |
| `Vanos` | `decimal` | Granos vanos | - |
| `Secos` | `decimal` | Granos secos | - |
| `PCobjetivo` | `decimal` | Porcentaje cerezas objetivo | - |
| `Nlote_AreaAcopio` | `string` | Número de lote (FK) | - |

**Llaves:**
- **PK:** `Tiempo`
- **FK:** `Nlote_AreaAcopio` → `Area_AcopioItem.Nlote`

**Relaciones:**
- Pertenece a Área_Acopio (relación: Tiene - 1:1)
- Registra RCsobremaduras (relación identificadora - 1:1)
- Registra RCinmaduras (relación identificadora - 1:1)
- Registra RCmaduras (relación identificadora - 1:1)

---

### 6. CatacionItem

**Tabla:** `Catacion`
**Descripción:** Registro de catación y evaluación sensorial del café.
**Tipo:** Entidad fuerte

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_catacion` | `int` | Identificador único (PK) | Auto-incremental |
| `Nlote` | `string` | Número de lote | - |
| `Limpio` | `bool` | Indica si está limpio | - |
| `Defectuoso` | `bool` | Indica si es defectuoso | - |
| `FFreposo` | `DateTime` | Fecha final de reposo | - |
| `Overde` | `string` | Olor verde | - |
| `Quaker` | `int` | Cantidad de quaker | - |
| `CCverde` | `string` | Clasificación color verde | - |
| `Rtostado` | `decimal` | Rendimiento/resultado tostado | - |
| `Dfueste` | `decimal` | Densidad en tueste | - |
| `CCcalidad` | `int` | Clasificación de calidad | - |

**Defectos Categoría 1 (Primarios):**
| `C1agrio` | `decimal` | Defecto: agrio | - |
| `C1hongos` | `decimal` | Defecto: hongos | - |
| `C1cerezaseca` | `decimal` | Defecto: cereza seca | - |
| `C1negro` | `decimal` | Defecto: granos negros | - |
| `C1insectos` | `decimal` | Defecto: insectos | - |
| `C1negroP` | `decimal` | Defecto: negro parcial | - |
| `C1agrioP` | `decimal` | Defecto: agrio parcial | - |
| `C1ME` | `decimal` | Defecto: materia extraña | - |

**Defectos Categoría 2 (Secundarios):**
| `C2flotador` | `decimal` | Defecto: flotador | - |
| `C2averanado` | `decimal` | Defecto: averanado | - |
| `C2pergamino` | `int` | Defecto: pergamino | - |
| `C2inmaduro` | `int` | Defecto: inmaduro | - |
| `C2concha` | `decimal` | Defecto: concha | - |
| `C2insectos` | `decimal` | Defecto: insectos | - |

**⚠️ CORRECCIÓN REQUERIDA - Atributos Compuestos C2CP:**
Estos atributos están FUSIONADOS incorrectamente y deben separarse:

| Atributo Actual | Debe Separarse En |
|----------------|-------------------|
| `C2cascara_pulpa` | `C2cascara` (decimal) |
|  | `C2pulpa` (decimal) |

**⚠️ CORRECCIÓN REQUERIDA - Atributos Compuestos C2PCM:**
| Atributo Actual | Debe Separarse En |
|----------------|-------------------|
| `C2partido_cortado_mordido` | `C2partido` (decimal) |
|  | `C2cortado` (decimal) |
|  | `C2mordido` (decimal) |

**Medidas de Zarandas (atributo compuesto Zaranda):**
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

**Atributo compuesto TonAgton (Tonalidad Agtron) - 8 valores:**
| `TonAgton_25` | `decimal` | Tonalidad Agtron 25 | - |
| `TonAgton_35` | `decimal` | Tonalidad Agtron 35 | - |
| `TonAgton_45` | `decimal` | Tonalidad Agtron 45 | - |
| `TonAgton_55` | `decimal` | Tonalidad Agtron 55 | - |
| `TonAgton_65` | `decimal` | Tonalidad Agtron 65 | - |
| `TonAgton_75` | `decimal` | Tonalidad Agtron 75 | - |
| `TonAgton_85` | `decimal` | Tonalidad Agtron 85 | - |
| `TonAgton_95` | `decimal` | Tonalidad Agtron 95 | - |

**Atributo Derivado:**
| `Pfinales` | `decimal` | Puntos finales (calculado) | Derivado |

**Llaves:**
- **PK:** `ID_catacion`
- **FK:** No tiene FK directa (recibe información a través de Enviar_muestras)

**Relaciones:**
- Recibe muestras desde Trilla (relación: Envía_muestras_A - N:N)
- Tiene Rondas (relación: 1:N)

---

## ENTIDADES DÉBILES

### 7. HumedadItem

**Tabla:** `Humedad`
**Descripción:** Registro de humedad durante el secado.
**Tipo:** Entidad débil (depende de Secado)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Humedad` | `int` | Clave parcial (PK) | Auto-incremental |
| `PHumedad` | `decimal` | Porcentaje de humedad | - |
| `Temperatura` | `int` | Temperatura | - |
| `ID_Secado` | `int` | ID de secado (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Humedad`
- **FK:** `ID_Secado` → `SecadoItem.ID_Secado` (relación identificadora)

**Relación identificadora:** Mide (1:N desde Secado)

---

### 8. TermoHigrometriaItem

**Tabla:** `TermoHigrometria`
**Descripción:** Registro de termohigrometría.
**Tipo:** Entidad débil (depende de Secado)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Termo` | `int` | Clave parcial (PK) | Auto-incremental |
| `Hrelativa` | `decimal` | Humedad relativa | - |
| `Tinterna` | `int` | Temperatura interna | - |
| `Texterna` | `int` | Temperatura externa | - |
| `ID_Secado` | `int` | ID de secado (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Termo`
- **FK:** `ID_Secado` → `SecadoItem.ID_Secado` (relación identificadora)

**Relación identificadora:** Mide (1:N desde Secado)

---

### 9. TemperaturaSecadoItem

**Tabla:** `TemperaturaSecado`
**Descripción:** Registro de temperaturas durante el secado.
**Tipo:** Entidad débil (depende de Secado)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Temperatura` | `int` | Clave parcial (PK) | Auto-incremental |
| `Lectura` | `int` | Lectura de temperatura | - |
| `ID_Secado` | `int` | ID de secado (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Temperatura`
- **FK:** `ID_Secado` → `SecadoItem.ID_Secado`

---

### 10. NcamaItem

**Tabla:** `Ncama`
**Descripción:** Número de cama de secado.
**Tipo:** Entidad débil (depende de Secado)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Ncama` | `int` | Clave parcial (PK) | Auto-incremental |
| `Numero` | `int` | Número de cama | - |
| `ID_Secado` | `int` | ID de secado (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Ncama`
- **FK:** `ID_Secado` → `SecadoItem.ID_Secado`

---

### 11. PesoVerdeItem

**Tabla:** `PesoVerde`
**Descripción:** Registro de pesos durante el proceso de trilla.
**Tipo:** Entidad débil (depende de Trilla)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_PesoVerde` | `int` | Clave parcial (PK) | Auto-incremental |
| `Winferiores` | `decimal` | Peso de inferiores | - |
| `Wfinal` | `decimal` | Peso final | - |
| `WFinalInferiores` | `decimal` | Peso final inferior | - |
| `ID_PesoTrilla` | `int` | ID de trilla (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_PesoVerde`
- **FK:** `ID_PesoTrilla` → `TrillaItem.ID_Trilla` (relación identificadora)

**Relación identificadora:** Exportar (1:1 desde Trilla)

---

### 12. RCsobremadurasItem

**Tabla:** `RCsobremaduras`
**Descripción:** Registro de caracterización de cerezas sobremaduras.
**Tipo:** Entidad débil (depende de Formulario_Caracterizacion)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_sobremaduras` | `int` | Clave parcial (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio (multivaluado) | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix (derivado) | Atributo derivado |
| `Tiempo` | `DateTime` | Tiempo (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_sobremaduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem.Tiempo` (relación identificadora)

**Relación identificadora:** Registra (1:1 desde Formulario_Caracterizacion)

---

### 13. RCinmadurasItem

**Tabla:** `RCinmaduras`
**Descripción:** Registro de caracterización de cerezas inmaduras.
**Tipo:** Entidad débil (depende de Formulario_Caracterizacion)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_inmaduras` | `int` | Clave parcial (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio (multivaluado) | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix (derivado) | Atributo derivado |
| `Tiempo` | `DateTime` | Tiempo (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_inmaduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem.Tiempo` (relación identificadora)

**Relación identificadora:** Registra (1:1 desde Formulario_Caracterizacion)

---

### 14. RCmadurasItem

**Tabla:** `RCmaduras`
**Descripción:** Registro de caracterización de cerezas maduras.
**Tipo:** Entidad débil (depende de Formulario_Caracterizacion)

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_maduras` | `int` | Clave parcial (PK) | Auto-incremental |
| `Promedio` | `decimal` | Promedio (multivaluado) | - |
| `Observaciones` | `string` | Observaciones | - |
| `Gbx` | `decimal` | Grados Brix (derivado) | Atributo derivado |
| `Tiempo` | `DateTime` | Tiempo (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_maduras`
- **FK:** `Tiempo` → `Formulario_CaracterizacionItem.Tiempo` (relación identificadora)

**Relación identificadora:** Registra (1:1 desde Formulario_Caracterizacion)

---

## ENTIDADES ADICIONALES

### 15. Registro_FormularioItem

**Tabla:** `Registro_Formulario`
**Descripción:** Registro principal del formulario de caracterización.
**Tipo:** Tabla de registro (vincula Formulario con RCs)

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

### 16. Gbx_sobremadurasItem

**Tabla:** `Gbx_sobremaduras`
**Descripción:** Grados Brix de cerezas sobremaduras.
**Tipo:** Entidad relacionada

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_sobremaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_sobremaduras` | `int` | ID de sobremaduras (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Gbx_sobremaduras`
- **FK:** `ID_sobremaduras` → `RCsobremadurasItem.ID_sobremaduras`

---

### 17. Gbx_madurasItem

**Tabla:** `Gbx_maduras`
**Descripción:** Grados Brix de cerezas maduras.
**Tipo:** Entidad relacionada

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_maduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_maduras` | `int` | ID de maduras (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Gbx_maduras`
- **FK:** `ID_maduras` → `RCmadurasItem.ID_maduras`

---

### 18. Gbx_inmadurasItem

**Tabla:** `Gbx_inmaduras`
**Descripción:** Grados Brix de cerezas inmaduras.
**Tipo:** Entidad relacionada

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Gbx_inmaduras` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor` | `decimal` | Valor de grados Brix | - |
| `ID_inmaduras` | `int` | ID de inmaduras (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Gbx_inmaduras`
- **FK:** `ID_inmaduras` → `RCinmadurasItem.ID_inmaduras`

---

### 19. RondasItem

**Tabla:** `Rondas`
**Descripción:** Rondas de catación.
**Tipo:** Entidad relacionada con Catación

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Rondas` | `int` | Identificador único (PK) | Auto-incremental |
| `Valor_calidad` | `decimal` | Valor de calidad | - |
| `ID_catacion` | `int` | ID de catación (FK) | Obligatorio |

**Llaves:**
- **PK:** `ID_Rondas`
- **FK:** `ID_catacion` → `CatacionItem.ID_catacion`

---

## TABLAS DE RELACIÓN N:N

### 20. Guardar_CafeItem

**Tabla:** `Guardar_Cafe`
**Descripción:** Relación N:N entre Secado y Bodega.
**Tipo:** Tabla intermedia

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Secado` | `int` | ID de secado (PK, FK) | - |
| `ID_Bodega` | `int` | ID de bodega (PK, FK) | - |
| `CantidadSacos` | `int` | Cantidad de sacos (atributo de relación) | - |

**Llaves:**
- **PK Compuesta:** (`ID_Secado`, `ID_Bodega`)
- **FK:** `ID_Secado` → `SecadoItem.ID_Secado`
- **FK:** `ID_Bodega` → `BodegaItem.ID_Bodega`

**Descripción:** Múltiples procesos de secado pueden guardar café en múltiples bodegas.

---

### 21. Enviar_muestrasItem

**Tabla:** `Enviar_muestras`
**Descripción:** Relación N:N entre Trilla y Catación.
**Tipo:** Tabla intermedia

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Trilla` | `int` | ID de trilla (PK, FK) | - |
| `ID_Catacion` | `int` | ID de catación (PK, FK) | - |
| `FfinalReposo` | `DateTime` | Fecha final de reposo (atributo de relación) | - |

**Llaves:**
- **PK Compuesta:** (`ID_Trilla`, `ID_Catacion`)
- **FK:** `ID_Trilla` → `TrillaItem.ID_Trilla`
- **FK:** `ID_Catacion` → `CatacionItem.ID_catacion`

**Descripción:** Múltiples procesos de trilla envían muestras a múltiples cataciones.

---

### 22. SuministraItem

**Tabla:** `Suministra`
**Descripción:** Relación entre Trilla y Bodega.
**Tipo:** Tabla intermedia

| Atributo | Tipo de Dato | Descripción | Restricciones |
|----------|--------------|-------------|---------------|
| `ID_Bodega` | `int` | ID de bodega (PK, FK) | - |
| `ID_Trilla` | `int` | ID de trilla (PK, FK) | - |

**Llaves:**
- **PK Compuesta:** (`ID_Bodega`, `ID_Trilla`)
- **FK:** `ID_Bodega` → `BodegaItem.ID_Bodega`
- **FK:** `ID_Trilla` → `TrillaItem.ID_Trilla`

**Descripción:** Conecta el proceso de trilla con el almacenamiento en bodega.

---

## Controladores y Endpoints de la API

Todos los controladores siguen el patrón REST estándar.

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
2. **SecadoApiController** → `/api/SecadoApi`
3. **TrillaController** → `/api/Trilla`
4. **BodegaController** → `/api/Bodega`
5. **Formulario_CaracterizacionController** → `/api/Formulario_Caracterizacion`
6. **CatacionApiController** → `/api/CatacionApi`
7. **HumedadController** → `/api/Humedad`
8. **TermoHigrometriaController** → `/api/TermoHigrometria`
9. **TemperaturaSecadoController** → `/api/TemperaturaSecado`
10. **NcamaController** → `/api/Ncama`
11. **PesoVerdeController** → `/api/PesoVerde`
12. **RCsobremadurasController** → `/api/RCsobremaduras`
13. **RCinmadurasController** → `/api/RCinmaduras`
14. **RCmadurasController** → `/api/RCmaduras`
15. **Registro_FormularioController** → `/api/Registro_Formulario`
16. **Gbx_sobremadurasController** → `/api/Gbx_sobremaduras`
17. **Gbx_madurasController** → `/api/Gbx_maduras`
18. **Gbx_inmadurasController** → `/api/Gbx_inmaduras`
19. **RondasController** → `/api/Rondas`
20. **Guardar_CafeController** → `/api/Guardar_Cafe`
21. **Enviar_muestrasController** → `/api/Enviar_muestras`
22. **SuministraController** → `/api/Suministra`

---

## Configuración y Dependencias

### Program.cs - Configuración Principal

**Características principales:**

1. **Configuración de CORS para Angular:**
```csharp
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:4200",
                "http://localhost:4201",
                "http://localhost:3000",
                "http://127.0.0.1:4200"
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
builder.Services.AddDbContext<SecadoContext>(options =>
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
  "timestamp": "2025-12-13T10:30:00Z",
  "message": "API is running"
}
```

### Dependencias NuGet

- **Microsoft.EntityFrameworkCore** (9.0+)
- **Npgsql.EntityFrameworkCore.PostgreSQL** (9.0+)
- **Swashbuckle.AspNetCore** (Swagger)
- **Microsoft.AspNetCore.Mvc** (ASP.NET Core MVC)

---

## Relaciones entre Entidades

### RELACIONES ENTRE ENTIDADES FUERTES

#### RELACIÓN 1: Envía (Área_Acopio → Secado)
- **Entidades participantes:** Área_Acopio → Secado
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Descripción:** Un área de acopio envía café a múltiples procesos de secado
- **Participación:**
  - **Área_Acopio:** Total (══) - Todo café en acopio debe enviarse a secado
  - **Secado:** Total (══) - Todo proceso de secado proviene de un área de acopio
- **Implementación FK:** `Secado.Nlote` → `Área_Acopio.Nlote`

**⚠️ CORRECCIÓN CRÍTICA:** La FK debe estar en Secado, NO en Area_Acopio.

---

#### RELACIÓN 2: Tiene (Área_Acopio → Formulario_Caracterizacion)
- **Entidades participantes:** Área_Acopio → Formulario_Caracterizacion
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Descripción:** Un área de acopio puede tener un formulario de caracterización
- **Participación:**
  - **Área_Acopio:** Parcial (——) - No toda área de acopio tiene formulario
  - **Formulario_Caracterizacion:** Total (══) - Todo formulario pertenece a un área de acopio
- **Implementación FK:** `Formulario_Caracterizacion.Nlote_AreaAcopio` → `Área_Acopio.Nlote`

---

#### RELACIÓN 3: Guarda_en (Secado ↔ Bodega)
- **Entidades participantes:** Secado ↔ Bodega
- **Cardinalidad:** N:N (Muchos a Muchos)
- **Descripción:** Múltiples procesos de secado pueden guardar café en múltiples bodegas
- **Atributo de la relación:** CantidadSacos
- **Participación:**
  - **Secado:** Parcial (——) - No todo café secado se guarda inmediatamente
  - **Bodega:** Parcial (——) - Una bodega puede estar vacía
- **Implementación:** Tabla intermedia `Guardar_Cafe` con FK a ambas entidades

---

#### RELACIÓN 4: Envía_muestras_A (Trilla ↔ Catación)
- **Entidades participantes:** Trilla ↔ Catación
- **Cardinalidad:** N:N (Muchos a Muchos)
- **Descripción:** Múltiples procesos de trilla envían muestras a múltiples cataciones
- **Atributo de la relación:** FfinalReposo
- **Participación:**
  - **Trilla:** Parcial (——) - No todas las trillas envían muestras a catación
  - **Catación:** Total (══) - Toda catación debe recibir muestras de al menos una trilla
- **Implementación:** Tabla intermedia `Enviar_muestras` con FK a ambas entidades

---

### RELACIONES IDENTIFICADORAS

#### RELACIÓN 5: Mide (Secado → Humedad)
- **Entidades participantes:** Secado → Humedad
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Un proceso de secado mide múltiples registros de humedad
- **Participación:**
  - **Secado:** Parcial (——) - No todo proceso de secado registra humedad
  - **Humedad:** Total (══) - Todo registro de humedad pertenece a un proceso de secado
- **Implementación FK:** `Humedad.ID_Secado` → `Secado.ID_Secado`

---

#### RELACIÓN 6: Mide (Secado → TermoHigrometria)
- **Entidades participantes:** Secado → TermoHigrometria
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Un proceso de secado mide múltiples registros termohigrométricos
- **Participación:**
  - **Secado:** Parcial (——) - No todo proceso de secado registra termohigrometría
  - **TermoHigrometria:** Total (══) - Todo registro termohigrométrico pertenece a un proceso de secado
- **Implementación FK:** `TermoHigrometria.ID_Secado` → `Secado.ID_Secado`

---

#### RELACIÓN 7: Exportar (Trilla → PesoVerde)
- **Entidades participantes:** Trilla → PesoVerde
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada trilla tiene un registro único de peso verde para exportación
- **Participación:**
  - **Trilla:** Parcial (——) - No toda trilla se exporta
  - **PesoVerde:** Total (══) - Todo registro de peso verde pertenece a una trilla
- **Implementación FK:** `PesoVerde.ID_PesoTrilla` → `Trilla.ID_Trilla`

---

#### RELACIÓN 8-10: Registra (Formulario_Caracterizacion → RC*)
- **Entidades participantes:** Formulario_Caracterizacion → RCsobremaduras/RCinmaduras/RCmaduras
- **Cardinalidad:** 1:1 (Uno a Uno) para cada una
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada formulario puede registrar un conjunto de datos por tipo de cereza
- **Participación:** Parcial/Parcial para todos
- **Implementación FK:**
  - `RCsobremaduras.Tiempo` → `Formulario_Caracterizacion.Tiempo`
  - `RCinmaduras.Tiempo` → `Formulario_Caracterizacion.Tiempo`
  - `RCmaduras.Tiempo` → `Formulario_Caracterizacion.Tiempo`

---

## Integridad Referencial

### Reglas de Eliminación:

1. **Área_Acopio → Secado/Trilla/Bodega/Formulario:**
   - ON DELETE: RESTRICT (no se puede eliminar si tiene registros relacionados)
   - ON UPDATE: CASCADE (actualiza Nlote en todas las tablas relacionadas)

2. **Secado → Humedad/TermoHigrometria/TemperaturaSecado/Ncama:**
   - ON DELETE: CASCADE (elimina automáticamente los registros dependientes)
   - ON UPDATE: CASCADE

3. **Trilla → PesoVerde:**
   - ON DELETE: CASCADE
   - ON UPDATE: CASCADE

4. **Formulario_Caracterizacion → RC(sobremaduras/maduras/inmaduras):**
   - ON DELETE: CASCADE
   - ON UPDATE: CASCADE

5. **RC(sobremaduras/maduras/inmaduras) → Gbx:**
   - ON DELETE: CASCADE
   - ON UPDATE: CASCADE

6. **Catación → Rondas:**
   - ON DELETE: CASCADE
   - ON UPDATE: CASCADE

---

## Correcciones Críticas Requeridas

### 🔴 PRIORIDAD CRÍTICA

#### 1. Corregir Foreign Key Invertida (Area_Acopio ↔ Secado)

**PROBLEMA ACTUAL:**
```csharp
// ❌ INCORRECTO - En Area_AcopioItem
public int ID_Secado { get; set; }  // FK a Secado
```

**DEBE SER:**
```csharp
// ✅ CORRECTO - Eliminar de Area_AcopioItem
// No debe tener FK a Secado

// ✅ CORRECTO - En SecadoItem
public string Nlote { get; set; }  // FK a Area_Acopio
```

**IMPACTO:** Esta corrección cambia la cardinalidad de 1:1 a 1:N correctamente.

---

#### 2. Separar Atributos Compuestos en CatacionItem

**PROBLEMA ACTUAL:**
```csharp
// ❌ INCORRECTO
public decimal C2cascara_pulpa { get; set; }
public decimal C2partido_cortado_mordido { get; set; }
```

**DEBE SER:**
```csharp
// ✅ CORRECTO - C2CP (Cáscara y Pulpa)
public decimal C2cascara { get; set; }
public decimal C2pulpa { get; set; }

// ✅ CORRECTO - C2PCM (Partido, Cortado, Mordido)
public decimal C2partido { get; set; }
public decimal C2cortado { get; set; }
public decimal C2mordido { get; set; }
```

---

#### 3. Implementar TonAgton como Atributo Compuesto

**PROBLEMA ACTUAL:**
```csharp
// ❌ INCORRECTO
public int TAgtron { get; set; }
```

**DEBE SER:**
```csharp
// ✅ CORRECTO - 8 valores de Tonalidad Agtron
public decimal TonAgton_25 { get; set; }
public decimal TonAgton_35 { get; set; }
public decimal TonAgton_45 { get; set; }
public decimal TonAgton_55 { get; set; }
public decimal TonAgton_65 { get; set; }
public decimal TonAgton_75 { get; set; }
public decimal TonAgton_85 { get; set; }
public decimal TonAgton_95 { get; set; }
```

---

### 🟡 PRIORIDAD ALTA

#### 4. Añadir Atributos Faltantes

**En Area_AcopioItem:**
- Separar `Despulpado` en 6 campos booleanos:
  - `Semilavado`, `Natural`, `Anaerobico`, `Otro`, `Miel`, `Lavado`

**En Formulario_CaracterizacionItem:**
- Añadir: `LAsignado` (string)

**En CatacionItem:**
- Añadir: `Residuo` (string)

---

### 🟢 PRIORIDAD MEDIA

#### 5. Estandarizar Nomenclatura

Estandarizar capitalización en TrillaItem:
- `RfinalPelado` → `RFinalPelado`
- `RfinalSeleccion` → `RFinalSeleccion`
- `WVerdeFinal` → `WverdeFinal`
- `RTeoricoPelado` → `RteoricoPelado`
- `WVerdeTeorico` → `WverdeTeorico`
- `FFinalReposo` → `FfinalReposo`

---

## Flujo del Proceso de Café

```
Área_Acopio (Nlote)
    │
    ├─→ Secado (FK: Nlote)
    │      │
    │      ├─→ Humedad (FK: ID_Secado)
    │      ├─→ TermoHigrometria (FK: ID_Secado)
    │      ├─→ TemperaturaSecado (FK: ID_Secado)
    │      ├─→ Ncama (FK: ID_Secado)
    │      │
    │      └─→ Guardar_Cafe (FK: ID_Secado, ID_Bodega)
    │             └─→ Bodega (FK: Nlote)
    │
    ├─→ Trilla (FK: Nlote)
    │      │
    │      ├─→ PesoVerde (FK: ID_PesoTrilla)
    │      ├─→ Enviar_muestras (FK: ID_Trilla, ID_Catacion)
    │      │      └─→ Catación
    │      │             └─→ Rondas (FK: ID_catacion)
    │      │
    │      └─→ Suministra (FK: ID_Trilla, ID_Bodega)
    │             └─→ Bodega
    │
    └─→ Formulario_Caracterizacion (FK: Nlote_AreaAcopio)
           │
           ├─→ RCsobremaduras (FK: Tiempo)
           │      └─→ Gbx_sobremaduras (FK: ID_sobremaduras)
           │
           ├─→ RCmaduras (FK: Tiempo)
           │      └─→ Gbx_maduras (FK: ID_maduras)
           │
           └─→ RCinmaduras (FK: Tiempo)
                  └─→ Gbx_inmaduras (FK: ID_inmaduras)
```

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

**Documentación generada para:** CoffeeBeanFlow Backend API - Versión Completa y Consolidada
**Versión:** 2.0 - COMPLETA
**Fecha:** 2025-12-13
**Framework:** .NET 9.0 con Entity Framework Core
**Base de Datos:** PostgreSQL
**Estado:** ✅ Consolidada con Modelo Conceptual Completo
