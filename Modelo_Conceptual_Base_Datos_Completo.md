# MODELO CONCEPTUAL DE BASE DE DATOS - ENTIDADES, ATRIBUTOS, RELACIONES Y FOREIGN KEYS

## ÍNDICE
1. [Entidades Fuertes](#entidades-fuertes)
2. [Entidades Débiles](#entidades-débiles)
3. [Tablas de Relación](#tablas-de-relación)
4. [Resumen del Modelo](#resumen-del-modelo)
5. [Notación Utilizada](#notación-utilizada)

---

## ENTIDADES FUERTES

### ENTIDAD 1: Área_Acopio

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **Nlote** (atributo clave - subrayado)

**Foreign Keys:**
- No tiene FK (es entidad principal en el flujo del proceso)

**Atributos simples:**
1. Altura
2. Zona
3. Nrecibo
4. Nproductor
5. Nfinca
6. Robjetivo
7. Rtotal
8. Vendido
9. Disponible
10. Enproceso

**Atributos compuestos:**

1. **Despulpado** (contiene):
   - Semilavado
   - Natural
   - Anaerobico
   - Otro
   - Miel
   - Lavado

2. **Pruebas_Fisicas_BH** (contiene):
   - PF_Pulpa_Pergamino
   - PF_DMecanicos
   - PF_Segundas
   - PF_Pergamino_Pulpa
   - PDensidad_Fruta
   - PDensidad_Pergamino_Humedo

---

### ENTIDAD 2: Secado

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **ID_Secado**

**Foreign Keys:**
- **Nlote** → Referencia a Área_Acopio

**Atributos simples:**
1. Finicio
2. Dsecado
3. Ffinal

**Atributos multivaluados:**
- **Ncama** (óvalo doble - puede tener múltiples valores)
- **Tsecado** (óvalo doble - puede tener múltiples valores)

**Relaciones:**
- Recibe café desde Área_Acopio (relación: Envía - 1:N)
- Guarda café en Bodega (relación: Guarda_en - N:N)
- Mide Humedad (relación identificadora - 1:N)
- Mide TermoHigrometria (relación identificadora - 1:N)

---

### ENTIDAD 3: Trilla

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **ID_Trilla**

**Foreign Keys:**
- **Nlote** → Referencia a Área_Acopio

**Atributos simples:**
1. Hinicial
2. Hfinal
3. RFinalPelado
4. RFinalSeleccion
5. WverdeFinal
6. RteoricoPelado
7. WverdeTeorico
8. RTeoricoSeleccion
9. FfinalReposo
10. Psegundas
11. Pcataduras
12. Pbarreduras
13. Pescogeduras
14. Pcaracolillo
15. Pprimera
16. Pmadres
17. Pmenudos
18. Pinferiores

**Relaciones:**
- Exporta PesoVerde (relación identificadora - 1:1)
- Envía muestras a Catación (relación: Envía_muestras_A - N:N)
- Suministra a Bodega (relación: Suministra)

---

### ENTIDAD 4: Bodega

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **ID_Bodega**

**Foreign Keys:**
- **Nlote** → Referencia a Área_Acopio

**Atributos simples:**
1. W_bellota
2. W_pergamino
3. Hfinal
4. Hinicial
5. D_Pergamino
6. D_Bellota
7. FinicioReposo
8. CantidadSacos
9. PMH_relativa
10. PMTinterna
11. PMTexterna

**Relaciones:**
- Recibe café desde Secado (relación: Guarda_en - N:N)
- Recibe producto de Trilla (relación: Suministra)

---

### ENTIDAD 5: Formulario_Caracterizacion

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **Tiempo** (atributo compuesto)

**Atributo compuesto (clave):**
- **Tiempo** (contiene):
  - Fecha
  - Hora

**Foreign Keys:**
- **Nlote_AreaAcopio** → Referencia a Área_Acopio

**Atributos simples:**
1. DRmaduras
2. PCdebajo
3. Proceso
4. LAsignado
5. Cverdes
6. Cobjetivo
7. Cinmaduras
8. Csobremaduras
9. Csecas
10. Mtabla
11. PCverdes
12. PCsecas
13. PCencima
14. Emaduracion
15. Broca
16. Densidad
17. Vanos
18. Secos
19. PCobjetivo

**Relaciones:**
- Pertenece a Área_Acopio (relación: Tiene - 1:1)
- Registra RCsobremaduras (relación identificadora - 1:1)
- Registra RCinmaduras (relación identificadora - 1:1)
- Registra RCmaduras (relación identificadora - 1:1)

---

### ENTIDAD 6: Catación

**Tipo:** Entidad fuerte (rectángulo simple)

**Clave primaria:**
- **ID_catacion**

**Foreign Keys:**
- No tiene FK directa (recibe información a través de la relación Enviar_muestras)

**Atributos simples:**
1. Nlote
2. Limpio
3. Defectuoso
4. FFreposo
5. Overde
6. Quaker
7. CCverde
8. Rtostado
9. Dfueste
10. CCcalidad
11. C1agrio
12. C1hongos
13. C1cerezaseca
14. C1negro
15. C1insectos
16. C1negroP
17. C1agrioP
18. C1ME
19. C2flotador
20. C2averanado
21. C2pergamino
22. C2inmaduro
23. C2concha
24. C2insectos

**Atributo multivaluado:**
- **Ronda** (óvalo doble)

**Atributo derivado:**
- **Pfinales** (óvalo con línea punteada)

**Atributos compuestos:**

1. **C2CP** (contiene):
   - C2cascara
   - C2pulpa

2. **C2PCM** (contiene):
   - C2partido
   - C2cortado
   - C2mordido

3. **Zaranda** (contiene):
   - TresSobreDieciseis
   - Veinte
   - Diecinueve
   - Dieciocho
   - Diecisiete
   - Dieciseis
   - Quince
   - Catorce
   - Trece

4. **TonAgton** (contiene):
   - 25
   - 35
   - 45
   - 55
   - 65
   - 75
   - 85
   - 95

**Relaciones:**
- Recibe muestras desde Trilla (relación: Envía_muestras_A - N:N)

---

## ENTIDADES DÉBILES

### ENTIDAD DÉBIL 1: Humedad

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Secado

**Clave parcial:**
- **ID_Humedad**

**Foreign Keys:**
- **ID_Secado** → Referencia a Secado (relación identificadora)

**Atributos:**
1. PHumedad
2. Temperatura

**Relación identificadora:** 
- **Mide** (1:N desde Secado hacia Humedad)

---

### ENTIDAD DÉBIL 2: TermoHigrometria

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Secado

**Clave parcial:**
- **ID_Termo**

**Foreign Keys:**
- **ID_Secado** → Referencia a Secado (relación identificadora)

**Atributos:**
1. Hrelativa
2. Tinterna
3. Texterna

**Relación identificadora:**
- **Mide** (1:N desde Secado hacia TermoHigrometria)

---

### ENTIDAD DÉBIL 3: TemperaturaSecado

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Secado

**Clave parcial:**
- **ID_Temperatura**

**Foreign Keys:**
- **ID_Secado** → Referencia a Secado

**Atributos:**
1. Lectura

---

### ENTIDAD DÉBIL 4: Ncama

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Secado

**Clave parcial:**
- **ID_Ncama**

**Foreign Keys:**
- **ID_Secado** → Referencia a Secado

---

### ENTIDAD DÉBIL 5: PesoVerde

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Trilla

**Clave parcial:**
- **ID_PesoVerde**

**Foreign Keys:**
- **ID_PesoTrilla** → Referencia a Trilla (relación identificadora)

**Atributos:**
1. Winferiores
2. Wfinal
3. WFinalInferiores

**Relación identificadora:**
- **Exportar** (1:1 desde Trilla hacia PesoVerde)

---

### ENTIDAD DÉBIL 6: RCsobremaduras

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Formulario_Caracterizacion

**Clave parcial:**
- **ID_sobremaduras**

**Foreign Keys:**
- **Tiempo** → Referencia a Formulario_Caracterizacion (relación identificadora)

**Atributo derivado:**
- **Gbx** (óvalo con línea punteada)

**Atributo multivaluado:**
- **Promedio** (óvalo doble)

**Atributos simples:**
- Observaciones

**Relación identificadora:**
- **Registra** (1:1 desde Formulario_Caracterizacion hacia RCsobremaduras)

---

### ENTIDAD DÉBIL 7: RCinmaduras

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Formulario_Caracterizacion

**Clave parcial:**
- **ID_inmaduras**

**Foreign Keys:**
- **Tiempo** → Referencia a Formulario_Caracterizacion (relación identificadora)

**Atributo derivado:**
- **Gbx** (óvalo con línea punteada)

**Atributo multivaluado:**
- **Promedio** (óvalo doble)

**Atributos simples:**
- Observaciones

**Relación identificadora:**
- **Registra** (1:1 desde Formulario_Caracterizacion hacia RCinmaduras)

---

### ENTIDAD DÉBIL 8: RCmaduras

**Tipo:** Entidad débil (rectángulo doble)  
**Dependencia:** Formulario_Caracterizacion

**Clave parcial:**
- **ID_maduras**

**Foreign Keys:**
- **Tiempo** → Referencia a Formulario_Caracterizacion (relación identificadora)

**Atributo derivado:**
- **Gbx** (óvalo con línea punteada)

**Atributo multivaluado:**
- **Promedio** (óvalo doble)

**Atributos simples:**
- Observaciones

**Relación identificadora:**
- **Registra** (1:1 desde Formulario_Caracterizacion hacia RCmaduras)

---

### ENTIDAD 9: Registro_Formulario

**Tipo:** Tabla de registro (vincula Formulario con RCs)

**Clave primaria:**
- **ID_Formulario**

**Foreign Keys:**
- **ID_sobremaduras** → Referencia a RCsobremaduras
- **ID_maduras** → Referencia a RCmaduras
- **ID_inmaduras** → Referencia a RCinmaduras

---

### ENTIDAD 10: Gbx_sobremaduras

**Tipo:** Entidad relacionada

**Clave primaria:**
- **ID_Gbx_sobremaduras**

**Foreign Keys:**
- **ID_sobremaduras** → Referencia a RCsobremaduras

**Atributos:**
1. Valor

---

### ENTIDAD 11: Gbx_maduras

**Tipo:** Entidad relacionada

**Clave primaria:**
- **ID_Gbx_maduras**

**Foreign Keys:**
- **ID_maduras** → Referencia a RCmaduras

**Atributos:**
1. Valor

---

### ENTIDAD 12: Gbx_inmaduras

**Tipo:** Entidad relacionada

**Clave primaria:**
- **ID_Gbx_inmaduras**

**Foreign Keys:**
- **ID_inmaduras** → Referencia a RCinmaduras

**Atributos:**
1. Valor

---

### ENTIDAD 13: Rondas

**Tipo:** Entidad relacionada con Catación

**Clave primaria:**
- **ID_Rondas**

**Foreign Keys:**
- **ID_catacion** → Referencia a Catación

**Atributos:**
1. Valor_calidad

---

## TABLAS DE RELACIÓN

### TABLA DE RELACIÓN 1: Guardar_Cafe

**Tipo:** Relación N:N entre Secado y Bodega

**Claves primarias compuestas:**
- **ID_Secado** (FK)
- **ID_Bodega** (FK)

**Foreign Keys:**
- **ID_Secado** → Referencia a Secado
- **ID_Bodega** → Referencia a Bodega

**Atributos adicionales:**
- CantidadSacos

**Descripción:** Múltiples procesos de secado pueden guardar café en múltiples bodegas

---

### TABLA DE RELACIÓN 2: Enviar_muestras

**Tipo:** Relación N:N entre Trilla y Catación

**Claves primarias compuestas:**
- **ID_Trilla** (FK)
- **ID_Catacion** (FK)

**Foreign Keys:**
- **ID_Trilla** → Referencia a Trilla
- **ID_Catacion** → Referencia a Catación

**Atributos adicionales:**
- FfinalReposo

**Descripción:** Múltiples procesos de trilla envían muestras a múltiples cataciones

---

### TABLA DE RELACIÓN 3: Suministra

**Tipo:** Relación entre Trilla y Bodega

**Claves primarias compuestas:**
- **ID_Bodega** (FK)
- **ID_Trilla** (FK)

**Foreign Keys:**
- **ID_Bodega** → Referencia a Bodega
- **ID_Trilla** → Referencia a Trilla

**Descripción:** Conecta el proceso de trilla con el almacenamiento en bodega

---

## RELACIONES ENTRE ENTIDADES

### RELACIÓN 1: Envía
- **Entidades participantes:** Área_Acopio → Secado
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Descripción:** Un área de acopio envía café a múltiples procesos de secado
- **Participación:** 
  - **Área_Acopio:** Total (══) - Todo café en acopio debe enviarse a secado
  - **Secado:** Total (══) - Todo proceso de secado proviene de un área de acopio
- **Implementación FK:** Secado.Nlote → Área_Acopio.Nlote

---

### RELACIÓN 2: Tiene
- **Entidades participantes:** Área_Acopio → Formulario_Caracterizacion
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Descripción:** Un área de acopio puede tener un formulario de caracterización
- **Participación:**
  - **Área_Acopio:** Parcial (——) - No toda área de acopio tiene formulario
  - **Formulario_Caracterizacion:** Total (══) - Todo formulario pertenece a un área de acopio
- **Implementación FK:** Formulario_Caracterizacion.Nlote_AreaAcopio → Área_Acopio.Nlote

---

### RELACIÓN 3: Guarda_en
- **Entidades participantes:** Secado ↔ Bodega
- **Cardinalidad:** N:N (Muchos a Muchos)
- **Descripción:** Múltiples procesos de secado pueden guardar café en múltiples bodegas
- **Atributo de la relación:** CantidadSacos
- **Participación:**
  - **Secado:** Parcial (——) - No todo café secado se guarda inmediatamente
  - **Bodega:** Parcial (——) - Una bodega puede estar vacía
- **Implementación:** Tabla intermedia Guardar_Cafe con FK a ambas entidades

---

### RELACIÓN 4: Envía_muestras_A
- **Entidades participantes:** Trilla ↔ Catación
- **Cardinalidad:** N:N (Muchos a Muchos)
- **Descripción:** Múltiples procesos de trilla envían muestras a múltiples cataciones
- **Atributo de la relación:** FfinalReposo
- **Participación:**
  - **Trilla:** Parcial (——) - No todas las trillas envían muestras a catación
  - **Catación:** Total (══) - Toda catación debe recibir muestras de al menos una trilla
- **Implementación:** Tabla intermedia Enviar_muestras con FK a ambas entidades

---

### RELACIÓN 5: Mide (Secado → Humedad)
- **Entidades participantes:** Secado → Humedad
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Un proceso de secado mide múltiples registros de humedad
- **Participación:**
  - **Secado:** Parcial (——) - No todo proceso de secado registra humedad
  - **Humedad:** Total (══) - Todo registro de humedad pertenece a un proceso de secado
- **Implementación FK:** Humedad.ID_Secado → Secado.ID_Secado

---

### RELACIÓN 6: Mide (Secado → TermoHigrometria)
- **Entidades participantes:** Secado → TermoHigrometria
- **Cardinalidad:** 1:N (Uno a Muchos)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Un proceso de secado mide múltiples registros termohigrométricos
- **Participación:**
  - **Secado:** Parcial (——) - No todo proceso de secado registra termohigrometría
  - **TermoHigrometria:** Total (══) - Todo registro termohigrométrico pertenece a un proceso de secado
- **Implementación FK:** TermoHigrometria.ID_Secado → Secado.ID_Secado

---

### RELACIÓN 7: Exportar (Trilla → PesoVerde)
- **Entidades participantes:** Trilla → PesoVerde
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada trilla tiene un registro único de peso verde para exportación
- **Participación:**
  - **Trilla:** Parcial (——) - No toda trilla se exporta
  - **PesoVerde:** Total (══) - Todo registro de peso verde pertenece a una trilla
- **Implementación FK:** PesoVerde.ID_PesoTrilla → Trilla.ID_Trilla

---

### RELACIÓN 8: Registra (Formulario_Caracterizacion → RCsobremaduras)
- **Entidades participantes:** Formulario_Caracterizacion → RCsobremaduras
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada formulario puede registrar un conjunto de datos de sobremaduras
- **Participación:**
  - **Formulario_Caracterizacion:** Parcial (——) - No todo formulario registra sobremaduras
  - **RCsobremaduras:** Parcial (——) - Puede no haber datos de sobremaduras
- **Implementación FK:** RCsobremaduras.Tiempo → Formulario_Caracterizacion.Tiempo

---

### RELACIÓN 9: Registra (Formulario_Caracterizacion → RCinmaduras)
- **Entidades participantes:** Formulario_Caracterizacion → RCinmaduras
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada formulario puede registrar un conjunto de datos de inmaduras
- **Participación:**
  - **Formulario_Caracterizacion:** Parcial (——) - No todo formulario registra inmaduras
  - **RCinmaduras:** Parcial (——) - Puede no haber datos de inmaduras
- **Implementación FK:** RCinmaduras.Tiempo → Formulario_Caracterizacion.Tiempo

---

### RELACIÓN 10: Registra (Formulario_Caracterizacion → RCmaduras)
- **Entidades participantes:** Formulario_Caracterizacion → RCmaduras
- **Cardinalidad:** 1:1 (Uno a Uno)
- **Tipo:** Relación identificadora (rombo doble)
- **Descripción:** Cada formulario puede registrar un conjunto de datos de maduras
- **Participación:**
  - **Formulario_Caracterizacion:** Parcial (——) - No todo formulario registra maduras
  - **RCmaduras:** Parcial (——) - Puede no haber datos de maduras
- **Implementación FK:** RCmaduras.Tiempo → Formulario_Caracterizacion.Tiempo

---

## RESUMEN DEL MODELO

### Estadísticas Generales

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

**Relaciones entre Entidades Fuertes:** 4
- Envía (1:N) - Total/Total
- Tiene (1:1) - Parcial/Total
- Guarda_en (N:N) - Parcial/Parcial
- Envía_muestras_A (N:N) - Parcial/Total

**Relaciones Identificadoras:** 6
- Mide (Secado → Humedad) - 1:N - Parcial/Total
- Mide (Secado → TermoHigrometria) - 1:N - Parcial/Total
- Exportar (Trilla → PesoVerde) - 1:1 - Parcial/Total
- Registra (Formulario → RCsobremaduras) - 1:1 - Parcial/Parcial
- Registra (Formulario → RCinmaduras) - 1:1 - Parcial/Parcial
- Registra (Formulario → RCmaduras) - 1:1 - Parcial/Parcial

**Total de Relaciones:** 10

**Atributos de Relaciones:**
- Guarda_en: CantidadSacos
- Envía_muestras_A: FfinalReposo

---

## RESUMEN DE FOREIGN KEYS POR ENTIDAD

| Entidad | Foreign Keys | Referencia a |
|---------|--------------|--------------|
| **Área_Acopio** | Ninguna | - |
| **Secado** | Nlote | Área_Acopio |
| **Humedad** | ID_Secado | Secado |
| **TermoHigrometria** | ID_Secado | Secado |
| **TemperaturaSecado** | ID_Secado | Secado |
| **Ncama** | ID_Secado | Secado |
| **Trilla** | Nlote | Área_Acopio |
| **PesoVerde** | ID_PesoTrilla | Trilla |
| **Bodega** | Nlote | Área_Acopio |
| **Formulario_Caracterizacion** | Nlote_AreaAcopio | Área_Acopio |
| **Registro_Formulario** | ID_sobremaduras, ID_maduras, ID_inmaduras | RCsobremaduras, RCmaduras, RCinmaduras |
| **RCsobremaduras** | Tiempo | Formulario_Caracterizacion |
| **RCmaduras** | Tiempo | Formulario_Caracterizacion |
| **RCinmaduras** | Tiempo | Formulario_Caracterizacion |
| **Gbx_sobremaduras** | ID_sobremaduras | RCsobremaduras |
| **Gbx_maduras** | ID_maduras | RCmaduras |
| **Gbx_inmaduras** | ID_inmaduras | RCinmaduras |
| **Catación** | Ninguna | - |
| **Rondas** | ID_catacion | Catación |
| **Guardar_Cafe** | ID_Secado, ID_Bodega | Secado, Bodega |
| **Enviar_muestras** | ID_Trilla, ID_Catacion | Trilla, Catación |
| **Suministra** | ID_Bodega, ID_Trilla | Bodega, Trilla |

---

## NOTACIÓN UTILIZADA

### Entidades:
- **Rectángulo simple:** Entidad fuerte
- **Rectángulo doble:** Entidad débil

### Atributos:
- **Óvalo simple:** Atributo simple
- **Óvalo doble:** Atributo multivaluado
- **Óvalo con línea punteada:** Atributo derivado
- **Atributo subrayado:** Clave primaria (PK)
- **Atributo subrayado con línea punteada:** Clave parcial

### Relaciones:
- **Rombo simple:** Relación normal
- **Rombo doble:** Relación identificadora
- **Línea simple (——):** Participación parcial
- **Línea doble (══):** Participación total

### Foreign Keys:
- **FK:** Indica que el atributo es una clave foránea
- **→:** Indica la dirección de la referencia (hacia qué tabla apunta)

### Cardinalidad:
- **1:1** - Uno a Uno
- **1:N** - Uno a Muchos
- **N:N** - Muchos a Muchos

---

## FLUJO DEL PROCESO DE CAFÉ

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

## INTEGRIDAD REFERENCIAL

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

**Documento generado:** Diciembre 2024  
**Versión:** 2.0 - Incluye Foreign Keys completas  
**Propósito:** Documentación técnica para implementación de base de datos del proceso de café
