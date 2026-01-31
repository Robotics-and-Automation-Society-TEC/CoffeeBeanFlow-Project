# PROMPT PARA ANÁLISIS Y CONSOLIDACIÓN DE DOCUMENTACIÓN

---

Hola Claude. Necesito tu ayuda para analizar y consolidar la documentación de mi proyecto CoffeeBeanFlow.

## Contexto del Proyecto

Estoy desarrollando una aplicación llamada **CoffeeBeanFlow** para gestionar funcionalidades relacionadas con el procesamiento de café. Actualmente trabajo en **Windows 11** y el stack tecnológico objetivo es:
- **Backend**: .NET 9.0
- **Frontend**: Angular CLI 21.0.2

## Archivos Disponibles

Tengo tres archivos de documentación:

1. **Modelo_Conceptual_Base_Datos_Completo.md** - Contiene el modelo conceptual y relacional COMPLETO del proyecto, incluyendo todas las entidades, atributos, relaciones y funcionalidades del sistema. Este es el documento de referencia principal.

2. **BACKEND_DOCUMENTATION.md** - Documentación actual del backend (incompleta)

3. **FRONTEND_DOCUMENTATION.md** - Documentación actual del frontend (incompleta)

## Tarea Solicitada

Necesito que realices un **análisis comparativo** en los siguientes pasos:

### Paso 1: Análisis de Brechas
Compara `BACKEND_DOCUMENTATION.md` y `FRONTEND_DOCUMENTATION.md` contra `Modelo_Conceptual_Base_Datos_Completo.md` e identifica:
- ¿Qué entidades faltan documentar?
- ¿Qué atributos no están contemplados?
- ¿Qué relaciones no están reflejadas?
- ¿Qué funcionalidades del modelo conceptual no aparecen en las documentaciones actuales?

### Paso 2: Reporte de Hallazgos
Genera un reporte detallado que indique:
- **Para Backend**: Listado específico de lo que falta añadir
- **Para Frontend**: Listado específico de lo que falta añadir

### Paso 3: Documentos Consolidados Finales
Crea dos nuevos documentos que integren toda la información:
- **BACKEND_DOCUMENTATION_COMPLETE.md** - Fusión de BACKEND_DOCUMENTATION.md + elementos faltantes de Modelo_Conceptual_Base_Datos_Completo.md
- **FRONTEND_DOCUMENTATION_COMPLETE.md** - Fusión de FRONTEND_DOCUMENTATION.md + elementos faltantes de Modelo_Conceptual_Base_Datos_Completo.md

## Convenciones de Nomenclatura

Las variables siguen este formato semántico (prefijos y sufijos):

**Prefijos numéricos/cuantitativos:**
- `N` = Número
- `W` = Peso (Weight)
- `P` = Porcentaje
- `R` = Rendimiento
- `D` = Densidad

**Prefijos de medición ambiental:**
- `T` = Temperatura
- `H` = Humedad

**Prefijos temporales:**
- `F` = Fecha
- `FF` = Fecha Final
- `PM` = Promedio Mensual (PMH = Promedio Mensual Humedad, PMT = Promedio Mensual Temperatura)

**Prefijos de contexto cafetalero:**
- `L` = Lote
- `C` = Cereza
- `RC` = Registro Cerezas
- `PC` = Porcentaje de Cerezas
- `CC` = Clasificación

**Prefijos de proceso/análisis:**
- `E` = Escala
- `DR` = Determinación de Rango óptimo de maduras
- `M` = Muestreo (Mtabla = Muestreo de tabla)

**Prefijos de cualidades organolépticas:**
- `O` = Olor
- `Ton` = Tonalidad

**Sufijos:**
- `I` = Inicial
- `F` = Final

## Instrucciones Importantes

- **NO generes código** en esta fase
- **NO crees documentos nuevos** hasta que yo te lo solicite explícitamente
- Enfócate primero en el **análisis y reporte de brechas**
- Proporciona los hallazgos de manera clara y estructurada antes de proceder a la consolidación

¿Estás listo para comenzar con el análisis?
