# PLAN DE IMPLEMENTACIÓN POR FASES - COFFEEBEANFLOW

> **📌 NOTA IMPORTANTE:** Este documento ha sido actualizado para reflejar completamente el **Modelo_Conceptual_Base_Datos_Completo.md**. Todas las entidades, atributos, relaciones y foreign keys están alineadas con el modelo conceptual oficial.
>
> **✅ Última actualización:** Diciembre 13, 2025
> **✅ Base de referencia:** Modelo_Conceptual_Base_Datos_Completo.md

---

## 🔄 CORRECCIONES APLICADAS

Las siguientes correcciones se han realizado en base al Modelo Conceptual:

### ✅ Fase 1 - Área_Acopio (ACTUALIZADA)
**Cambios aplicados:**
- ✅ Agregado atributo compuesto **Despulpado** con 6 sub-atributos:
  - Semilavado, Natural, Anaerobico, Otro, Miel, Lavado
- ✅ Agregado atributo compuesto **Pruebas_Fisicas_BH** con 6 sub-atributos:
  - PF_Pulpa_Pergamino, PF_DMecanicos, PF_Segundas, PF_Pergamino_Pulpa, PDensidad_Fruta, PDensidad_Pergamino_Humedo
- ✅ Total de atributos: 22 (10 simples + 12 compuestos)

### 📋 Fases Pendientes (Según Modelo Conceptual)

Las siguientes fases deben implementar estas entidades con sus atributos correctos:

**Fase 3 - Secado:**
- ID_Secado (PK), Nlote (FK), Finicio, Dsecado, Ffinal
- Atributos multivaluados: Ncama, Tsecado
- Entidades débiles: Humedad, TermoHigrometria, TemperaturaSecado, Ncama

**Fase 5 - Bodega:**
- ID_Bodega (PK), Nlote (FK)
- 11 atributos: W_bellota, W_pergamino, Hfinal, Hinicial, D_Pergamino, D_Bellota, FinicioReposo, CantidadSacos, PMH_relativa, PMTinterna, PMTexterna

**Fase 8 - Trilla:**
- ID_Trilla (PK), Nlote (FK)
- 18 atributos incluyendo Hinicial, Hfinal, RFinalPelado, RFinalSeleccion, WverdeFinal, etc.
- Entidad débil: PesoVerde (ID_PesoVerde, Winferiores, Wfinal, WFinalInferiores)

**Fase 10 - Formulario_Caracterizacion:**
- Tiempo (PK compuesto: Fecha + Hora), Nlote_AreaAcopio (FK)
- 19 atributos incluyendo DRmaduras, PCdebajo, Proceso, LAsignado, etc.
- Entidades débiles: RCsobremaduras, RCinmaduras, RCmaduras (cada una con Gbx derivado, Promedio multivaluado, Observaciones)

**Fase 12 - Catación:**
- ID_catacion (PK)
- 24 atributos simples
- Atributo multivaluado: Ronda
- Atributo derivado: Pfinales
- Atributos compuestos: C2CP (C2cascara, C2pulpa), C2PCM (C2partido, C2cortado, C2mordido), Zaranda (9 valores), TonAgton (8 valores)
- Entidad relacionada: Rondas (ID_Rondas, ID_catacion FK, Valor_calidad)

**Fase 14 - Relaciones N:N:**
- Guardar_Cafe (ID_Secado FK, ID_Bodega FK, CantidadSacos)
- Enviar_muestras (ID_Trilla FK, ID_Catacion FK, FfinalReposo)
- Suministra (ID_Bodega FK, ID_Trilla FK)

---

## 📋 ÍNDICE DE FASES

- [Fase 0: Configuración Inicial del Entorno](#fase-0-configuración-inicial-del-entorno)
- [Fase 1: Backend Base - Infraestructura y Área de Acopio Completa](#fase-1-backend-base---infraestructura-y-área-de-acopio-completa)
- [Fase 2: Frontend Base - Proyecto Angular y Diseño](#fase-2-frontend-base---proyecto-angular-y-diseño)
- [Fase 3: Backend - Secado y Entidades Débiles](#fase-3-backend---secado-y-entidades-débiles)
- [Fase 4: Frontend - Formulario de Acopio](#fase-4-frontend---formulario-de-acopio)
- [Fase 5: Backend - Bodega y Relación Guardar_Cafe](#fase-5-backend---bodega-y-relación-guardar_cafe)
- [Fase 6: Frontend - Formulario de Secado con Entidades Débiles](#fase-6-frontend---formulario-de-secado-con-entidades-débiles)
- [Fase 7: Frontend - Formulario de Bodega](#fase-7-frontend---formulario-de-bodega)
- [Fase 8: Backend - Trilla y PesoVerde](#fase-8-backend---trilla-y-pesoverde)
- [Fase 9: Frontend - Formulario de Trilla](#fase-9-frontend---formulario-de-trilla)
- [Fase 10: Backend - Caracterización y RC*](#fase-10-backend---caracterización-y-rc)
- [Fase 11: Frontend - Formulario de Caracterización](#fase-11-frontend---formulario-de-caracterización)
- [Fase 12: Backend - Catación y Rondas](#fase-12-backend---catación-y-rondas)
- [Fase 13: Frontend - Formulario de Catación](#fase-13-frontend---formulario-de-catación)
- [Fase 14: Backend - Relaciones N:N Completas](#fase-14-backend---relaciones-nn-completas)
- [Fase 15: Frontend - Historial General](#fase-15-frontend---historial-general)
- [Fase 16: Frontend - Trazabilidad Completa](#fase-16-frontend---trazabilidad-completa)
- [Fase 17: Testing y Optimización](#fase-17-testing-y-optimización)
- [Fase 18: Deployment y Documentación Final](#fase-18-deployment-y-documentación-final)

---

## ESTRATEGIA GENERAL

### Enfoque de Implementación:
1. **Backend First:** Primero implementamos la API y base de datos
2. **Pruebas con Postman/Swagger:** Verificamos que el backend funcione
3. **Frontend después:** Implementamos la interfaz que consume la API
4. **Pruebas de Integración:** Verificamos que frontend y backend trabajen juntos

### Tecnologías por Fase:
- **Backend:** .NET 9.0, Entity Framework Core, PostgreSQL
- **Frontend:** Angular 21.0.2, TypeScript, SCSS
- **Testing:** Postman (backend), Jasmine/Karma (frontend)

---

## FASE 0: Configuración Inicial del Entorno

### 🎯 Objetivo
Preparar el entorno de desarrollo con todas las herramientas necesarias.

### 📦 Requisitos Previos
- Windows 11
- Conexión a Internet
- Permisos de administrador

### 🛠️ Tareas

#### 1. Instalación de .NET 9.0 SDK
```bash
# Descargar desde: https://dotnet.microsoft.com/download/dotnet/9.0
# Verificar instalación:
dotnet --version
# Debe mostrar: 9.0.x
```

#### 2. Instalación de PostgreSQL
```bash
# Descargar desde: https://www.postgresql.org/download/windows/
# Versión recomendada: 16.x
# Durante instalación:
# - Puerto: 5432
# - Password: Anotar para usar después
# - Stack Builder: Opcional

# Verificar instalación:
psql --version
```

#### 3. Instalación de Node.js y npm
```bash
# Descargar desde: https://nodejs.org/
# Versión LTS recomendada: 20.x

# Verificar instalación:
node --version  # v20.x.x
npm --version   # 10.x.x
```

#### 4. Instalación de Angular CLI
```bash
npm install -g @angular/cli@21

# Verificar instalación:
ng version
# Debe mostrar: Angular CLI: 21.0.2
```

#### 5. Instalación de Visual Studio Code
```bash
# Descargar desde: https://code.visualstudio.com/

# Extensiones recomendadas:
# - C# Dev Kit (Microsoft)
# - Angular Language Service
# - PostgreSQL (Chris Kolkman)
# - REST Client o Thunder Client
# - GitLens
```

#### 6. Instalación de Postman (opcional pero recomendado)
```bash
# Descargar desde: https://www.postman.com/downloads/
```

#### 7. Instalación de Git
```bash
# Descargar desde: https://git-scm.com/download/win
# Verificar:
git --version
```

### ✅ Verificación de la Fase 0
Ejecutar en terminal:
```bash
dotnet --version
psql --version
node --version
npm --version
ng version
git --version
```

**Criterios de Éxito:**
- ✅ Todos los comandos ejecutan sin errores
- ✅ Las versiones coinciden con las especificadas
- ✅ PostgreSQL acepta conexiones en localhost:5432

---

## FASE 1: Backend Base - Infraestructura y Área de Acopio Completa

### 🎯 Objetivo
Crear el proyecto backend base con configuración de PostgreSQL y la entidad Área_Acopio **completa** con todos sus atributos según el Modelo Conceptual.

### 📋 Entregables
1. Proyecto ASP.NET Core Web API
2. Conexión a PostgreSQL funcionando
3. Entity Framework Core configurado
4. Swagger funcionando
5. Health check endpoint
6. **Entidad Área_Acopio completa con:**
   - 10 atributos simples (Altura, Zona, Nrecibo, etc.)
   - Atributo compuesto **Despulpado** (6 sub-atributos: Semilavado, Natural, Anaerobico, Otro, Miel, Lavado)
   - Atributo compuesto **Pruebas_Fisicas_BH** (6 sub-atributos: PF_Pulpa_Pergamino, PF_DMecanicos, etc.)

### 🛠️ Tareas Detalladas

#### 1.1. Crear Base de Datos en PostgreSQL
```sql
-- Abrir pgAdmin o psql

-- Crear base de datos
CREATE DATABASE coffeebeanflow_db;

-- Conectarse a la base de datos
\c coffeebeanflow_db

-- Crear usuario (opcional)
CREATE USER coffeeapp WITH PASSWORD 'Coffee2025!';
GRANT ALL PRIVILEGES ON DATABASE coffeebeanflow_db TO coffeeapp;
```

#### 1.2. Crear Proyecto .NET
```bash
# Crear carpeta del proyecto
mkdir CoffeeBeanFlowAPI
cd CoffeeBeanFlowAPI

# Crear solución
dotnet new sln -n CoffeeBeanFlow

# Crear proyecto Web API
dotnet new webapi -n CoffeeBeanFlowAPI -f net9.0

# Agregar proyecto a la solución
dotnet sln add CoffeeBeanFlowAPI/CoffeeBeanFlowAPI.csproj

# Abrir en VS Code
code .
```

#### 1.3. Instalar Paquetes NuGet
```bash
cd CoffeeBeanFlowAPI

# Entity Framework Core
dotnet add package Microsoft.EntityFrameworkCore --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Design --version 9.0.0
dotnet add package Microsoft.EntityFrameworkCore.Tools --version 9.0.0

# PostgreSQL
dotnet add package Npgsql.EntityFrameworkCore.PostgreSQL --version 9.0.0

# Swagger (ya incluido en .NET 9)
# JSON handling
dotnet add package System.Text.Json --version 9.0.0
```

#### 1.4. Estructura de Carpetas
```
CoffeeBeanFlowAPI/
├── Controllers/
├── Models/
│   ├── Entities/
│   └── DTOs/
├── Data/
│   └── Contexts/
├── Services/
├── Migrations/
├── Program.cs
└── appsettings.json
```

Crear las carpetas:
```bash
mkdir Controllers
mkdir Models
mkdir Models/Entities
mkdir Models/DTOs
mkdir Data
mkdir Data/Contexts
mkdir Services
```

#### 1.5. Configurar appsettings.json
```json
{
  "Logging": {
    "LogLevel": {
      "Default": "Information",
      "Microsoft.AspNetCore": "Warning"
    }
  },
  "AllowedHosts": "*",
  "ConnectionStrings": {
    "DefaultConnection": "Host=localhost;Port=5432;Database=coffeebeanflow_db;Username=postgres;Password=TU_PASSWORD_AQUI"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200",
      "http://localhost:4201"
    ]
  }
}
```

#### 1.6. Crear Modelo Base (Area_Acopio completo según Modelo Conceptual)
**Archivo:** `Models/Entities/AreaAcopioEntity.cs`

```csharp
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CoffeeBeanFlowAPI.Models.Entities
{
    [Table("area_acopio")]
    public class AreaAcopioEntity
    {
        // Clave primaria
        [Key]
        [Column("nlote")]
        [MaxLength(50)]
        public string Nlote { get; set; } = string.Empty;

        // Atributos simples
        [Column("altura")]
        public decimal Altura { get; set; }

        [Column("zona")]
        [MaxLength(100)]
        public string Zona { get; set; } = string.Empty;

        [Column("nrecibo")]
        public int Nrecibo { get; set; }

        [Column("nproductor")]
        [MaxLength(100)]
        public string Nproductor { get; set; } = string.Empty;

        [Column("nfinca")]
        [MaxLength(100)]
        public string Nfinca { get; set; } = string.Empty;

        [Column("robjetivo")]
        public decimal Robjetivo { get; set; }

        [Column("rtotal")]
        public decimal? Rtotal { get; set; }

        [Column("vendido")]
        public bool Vendido { get; set; }

        [Column("disponible")]
        public decimal? Disponible { get; set; }

        [Column("enproceso")]
        [MaxLength(50)]
        public string Enproceso { get; set; } = "No";

        // Atributo compuesto: Despulpado (6 sub-atributos)
        [Column("semilavado")]
        public bool? Semilavado { get; set; }

        [Column("natural")]
        public bool? Natural { get; set; }

        [Column("anaerobico")]
        public bool? Anaerobico { get; set; }

        [Column("otro")]
        [MaxLength(100)]
        public string? Otro { get; set; }

        [Column("miel")]
        public bool? Miel { get; set; }

        [Column("lavado")]
        public bool? Lavado { get; set; }

        // Atributo compuesto: Pruebas_Fisicas_BH (6 sub-atributos)
        [Column("pf_pulpa_pergamino")]
        public decimal? PF_Pulpa_Pergamino { get; set; }

        [Column("pf_dmecanicos")]
        public decimal? PF_DMecanicos { get; set; }

        [Column("pf_segundas")]
        public decimal? PF_Segundas { get; set; }

        [Column("pf_pergamino_pulpa")]
        public decimal? PF_Pergamino_Pulpa { get; set; }

        [Column("pdensidad_fruta")]
        public decimal? PDensidad_Fruta { get; set; }

        [Column("pdensidad_pergamino_humedo")]
        public decimal? PDensidad_Pergamino_Humedo { get; set; }
    }
}
```

#### 1.7. Crear DbContext
**Archivo:** `Data/Contexts/CoffeeDbContext.cs`

```csharp
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Models.Entities;

namespace CoffeeBeanFlowAPI.Data.Contexts
{
    public class CoffeeDbContext : DbContext
    {
        public CoffeeDbContext(DbContextOptions<CoffeeDbContext> options)
            : base(options)
        {
        }

        public DbSet<AreaAcopioEntity> AreaAcopio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configuraciones adicionales si es necesario
            modelBuilder.Entity<AreaAcopioEntity>(entity =>
            {
                entity.HasKey(e => e.Nlote);
                entity.Property(e => e.Nlote).IsRequired();
                entity.Property(e => e.Nproductor).IsRequired();
                entity.Property(e => e.Nfinca).IsRequired();
            });
        }
    }
}
```

#### 1.8. Configurar Program.cs
```csharp
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.PropertyNamingPolicy = null;
        options.JsonSerializerOptions.ReferenceHandler =
            System.Text.Json.Serialization.ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition =
            System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull;
    });

// Configure DbContext with PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<CoffeeDbContext>(options =>
    options.UseNpgsql(connectionString));

// Configure CORS
var allowedOrigins = builder.Configuration.GetSection("Cors:AllowedOrigins").Get<string[]>();
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAngularApp", policy =>
    {
        policy.WithOrigins(allowedOrigins ?? new[] { "http://localhost:4200" })
              .AllowAnyHeader()
              .AllowAnyMethod()
              .AllowCredentials();
    });
});

// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("AllowAngularApp");

app.UseAuthorization();

app.MapControllers();

// Health check endpoint
app.MapGet("/api/health", () => Results.Ok(new
{
    status = "OK",
    timestamp = DateTime.UtcNow,
    message = "CoffeeBeanFlow API is running"
}));

app.Run();
```

#### 1.9. Crear Migración Inicial
```bash
# Desde la carpeta CoffeeBeanFlowAPI
dotnet ef migrations add InitialCreate

# Aplicar migración a la base de datos
dotnet ef database update
```

#### 1.10. Crear Controlador de Prueba
**Archivo:** `Controllers/AreaAcopioController.cs`

```csharp
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using CoffeeBeanFlowAPI.Data.Contexts;
using CoffeeBeanFlowAPI.Models.Entities;

namespace CoffeeBeanFlowAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AreaAcopioController : ControllerBase
    {
        private readonly CoffeeDbContext _context;

        public AreaAcopioController(CoffeeDbContext context)
        {
            _context = context;
        }

        // GET: api/AreaAcopio
        [HttpGet]
        public async Task<ActionResult<IEnumerable<AreaAcopioEntity>>> GetAreaAcopio()
        {
            return await _context.AreaAcopio.ToListAsync();
        }

        // GET: api/AreaAcopio/LOTE-001
        [HttpGet("{nlote}")]
        public async Task<ActionResult<AreaAcopioEntity>> GetAreaAcopio(string nlote)
        {
            var areaAcopio = await _context.AreaAcopio.FindAsync(nlote);

            if (areaAcopio == null)
            {
                return NotFound();
            }

            return areaAcopio;
        }

        // POST: api/AreaAcopio
        [HttpPost]
        public async Task<ActionResult<AreaAcopioEntity>> PostAreaAcopio(AreaAcopioEntity areaAcopio)
        {
            try
            {
                _context.AreaAcopio.Add(areaAcopio);
                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetAreaAcopio), new { nlote = areaAcopio.Nlote }, areaAcopio);
            }
            catch (DbUpdateException)
            {
                if (AreaAcopioExists(areaAcopio.Nlote))
                {
                    return Conflict(new { message = "El lote ya existe" });
                }
                throw;
            }
        }

        // PUT: api/AreaAcopio/LOTE-001
        [HttpPut("{nlote}")]
        public async Task<IActionResult> PutAreaAcopio(string nlote, AreaAcopioEntity areaAcopio)
        {
            if (nlote != areaAcopio.Nlote)
            {
                return BadRequest();
            }

            _context.Entry(areaAcopio).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!AreaAcopioExists(nlote))
                {
                    return NotFound();
                }
                throw;
            }

            return NoContent();
        }

        // DELETE: api/AreaAcopio/LOTE-001
        [HttpDelete("{nlote}")]
        public async Task<IActionResult> DeleteAreaAcopio(string nlote)
        {
            var areaAcopio = await _context.AreaAcopio.FindAsync(nlote);
            if (areaAcopio == null)
            {
                return NotFound();
            }

            _context.AreaAcopio.Remove(areaAcopio);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool AreaAcopioExists(string nlote)
        {
            return _context.AreaAcopio.Any(e => e.Nlote == nlote);
        }
    }
}
```

#### 1.11. Ejecutar y Probar
```bash
# Ejecutar el proyecto
dotnet run

# La API estará disponible en:
# https://localhost:7xxx
# http://localhost:5xxx

# Abrir Swagger en el navegador:
# https://localhost:7xxx/swagger
```

### ✅ Verificación de la Fase 1

#### Pruebas con Swagger:

1. **Health Check:**
   - GET `/api/health`
   - Respuesta esperada: `{ "status": "OK", ... }`

2. **Crear un registro:**
   - POST `/api/AreaAcopio`
   - Body:
   ```json
   {
     "nlote": "LOTE-TEST-001",
     "altura": 1500,
     "zona": "Zona Norte",
     "nrecibo": 12345,
     "nproductor": "José Pérez",
     "nfinca": "La Esperanza",
     "robjetivo": 85.5,
     "rtotal": 88.2,
     "vendido": false,
     "disponible": 1000,
     "enproceso": "No",
     "semilavado": false,
     "natural": true,
     "anaerobico": false,
     "otro": null,
     "miel": false,
     "lavado": false,
     "pF_Pulpa_Pergamino": 15.5,
     "pF_DMecanicos": 2.3,
     "pF_Segundas": 1.8,
     "pF_Pergamino_Pulpa": 12.4,
     "pDensidad_Fruta": 1.05,
     "pDensidad_Pergamino_Humedo": 0.85
   }
   ```
   - Respuesta esperada: `201 Created`

3. **Obtener todos los registros:**
   - GET `/api/AreaAcopio`
   - Respuesta esperada: Array con el registro creado

4. **Obtener un registro específico:**
   - GET `/api/AreaAcopio/LOTE-TEST-001`
   - Respuesta esperada: El objeto creado

5. **Actualizar un registro:**
   - PUT `/api/AreaAcopio/LOTE-TEST-001`
   - Cambiar algún campo
   - Respuesta esperada: `204 No Content`

6. **Eliminar un registro:**
   - DELETE `/api/AreaAcopio/LOTE-TEST-001`
   - Respuesta esperada: `204 No Content`

**Criterios de Éxito:**
- ✅ El proyecto compila sin errores
- ✅ La base de datos se crea correctamente
- ✅ Swagger muestra la documentación de la API
- ✅ Health check responde correctamente
- ✅ CRUD completo de Area_Acopio funciona
- ✅ Los datos se guardan en PostgreSQL (verificar con pgAdmin)

---

## FASE 2: Frontend Base - Proyecto Angular y Diseño

### 🎯 Objetivo
Crear el proyecto Angular base con el sistema de diseño y componentes compartidos.

### 📋 Entregables
1. Proyecto Angular 21 funcionando
2. Sistema de diseño (variables SCSS, colores, mixins)
3. Componentes compartidos (MenuLateral, LoadingSpinner)
4. Routing básico
5. ApiService configurado

### 🛠️ Tareas Detalladas

#### 2.1. Crear Proyecto Angular
```bash
# Crear carpeta del proyecto (fuera de la carpeta del backend)
cd ..
ng new CoffeeBeanFlowApp

# Configuración durante la creación:
# ? Would you like to add Angular routing? Yes
# ? Which stylesheet format would you like to use? SCSS

cd CoffeeBeanFlowApp
```

#### 2.2. Estructura de Carpetas
```bash
# Crear estructura
cd src/app
mkdir core
mkdir core/services
mkdir core/interceptors
mkdir shared
mkdir shared/components
mkdir features
mkdir models
mkdir -p styles
```

Estructura completa:
```
src/
├── app/
│   ├── core/
│   │   ├── services/
│   │   └── interceptors/
│   ├── shared/
│   │   └── components/
│   ├── features/
│   ├── models/
│   ├── app.component.ts
│   ├── app.component.html
│   ├── app.component.scss
│   ├── app.config.ts
│   └── app.routes.ts
├── styles/
│   ├── _variables.scss
│   ├── _mixins.scss
│   ├── _reset.scss
│   └── styles.scss
├── assets/
└── index.html
```

#### 2.3. Configurar Variables SCSS
**Archivo:** `src/styles/_variables.scss`

```scss
// ===================================
// PALETA DE COLORES CAFÉ
// ===================================

// Rojos vino
$burgundy: #A52A3D;
$burgundy-dark: #8B2332;

// Verdes café
$verde-claro: #8FAD5A;
$verde-oscuro: #4A5D2E;
$verde-muy-oscuro: #2e3d1a;

// Tonos café
$cafe-claro: #E5C29F;
$cafe-medio: #C8956F;
$cafe-oscuro: #8B5A3C;
$cafe-muy-oscuro: #4A2D1A;
$negro-cafe: #2C1810;

// Neutros
$beige-claro: #F4F0E6;
$blanco-crema: #FEFAF5;

// ===================================
// COLORES FUNCIONALES
// ===================================

$color-success: $verde-claro;
$color-success-dark: $verde-oscuro;
$color-error: $burgundy;
$color-error-dark: $burgundy-dark;
$color-warning: #f39c12;
$color-info: $cafe-medio;

// ===================================
// ESPACIADO
// ===================================

$space-xs: 4px;
$space-sm: 8px;
$space-md: 12px;
$space-lg: 16px;
$space-xl: 20px;
$space-2xl: 24px;
$space-3xl: 32px;
$space-4xl: 40px;

// ===================================
// SOMBRAS
// ===================================

$shadow-sm: 0 4px 8px rgba(74, 45, 26, 0.15);
$shadow-md: 0 8px 16px rgba(74, 45, 26, 0.2);
$shadow-lg: 0 12px 24px rgba(74, 45, 26, 0.25);

// ===================================
// BORDER RADIUS
// ===================================

$radius-sm: 6px;
$radius-md: 8px;
$radius-lg: 10px;
$radius-xl: 12px;
$radius-2xl: 16px;

// ===================================
// TRANSICIONES
// ===================================

$transition-fast: 0.15s ease;
$transition-normal: 0.3s ease;
$transition-slow: 0.4s ease;
```

#### 2.4. Configurar Mixins SCSS
**Archivo:** `src/styles/_mixins.scss`

```scss
@import 'variables';

// ===================================
// BOTONES
// ===================================

@mixin btn-base {
  padding: $space-md $space-lg;
  border: none;
  border-radius: $radius-xl;
  cursor: pointer;
  transition: all $transition-normal;
  font-weight: 500;
  font-size: 1rem;

  &:disabled {
    opacity: 0.6;
    cursor: not-allowed;
  }
}

@mixin btn-primary {
  @include btn-base;
  background: linear-gradient(135deg, $verde-claro, $verde-oscuro);
  color: white;
  box-shadow: $shadow-sm;

  &:hover:not(:disabled) {
    transform: translateY(-2px);
    box-shadow: $shadow-lg;
  }
}

// ===================================
// INPUTS
// ===================================

@mixin input-base {
  padding: $space-md;
  border: 2px solid $cafe-claro;
  border-radius: $radius-md;
  font-size: 1rem;
  transition: all $transition-normal;
  background: $blanco-crema;

  &:focus {
    outline: none;
    border-color: $verde-claro;
    box-shadow: 0 0 0 3px rgba(143, 173, 90, 0.2);
  }

  &.error {
    border-color: $color-error;
  }
}

// ===================================
// CARDS
// ===================================

@mixin card-base {
  background: $blanco-crema;
  border-radius: $radius-2xl;
  padding: $space-2xl;
  box-shadow: $shadow-md;
  transition: all $transition-normal;
}
```

#### 2.5. Configurar Reset CSS
**Archivo:** `src/styles/_reset.scss`

```scss
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: 'Segoe UI', Tahoma, Geneva, Verdana, sans-serif;
  line-height: 1.6;
  color: #2C1810;
  background: linear-gradient(145deg, #FEFAF5, #F4F0E6);
  min-height: 100vh;
}

button {
  font-family: inherit;
}

input, textarea, select {
  font-family: inherit;
}
```

#### 2.6. Configurar Estilos Globales
**Archivo:** `src/styles/styles.scss`

```scss
@import 'variables';
@import 'mixins';
@import 'reset';

// Clases utilitarias globales
.btn-primary {
  @include btn-primary;
}

.input-base {
  @include input-base;
}

.card-base {
  @include card-base;
}

// Loading spinner global
.loading-spinner {
  border: 4px solid $cafe-claro;
  border-top: 4px solid $verde-claro;
  border-radius: 50%;
  width: 40px;
  height: 40px;
  animation: spin 1s linear infinite;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}
```

#### 2.7. Configurar angular.json para SCSS
Asegurarse de que `angular.json` tenga:

```json
{
  "projects": {
    "CoffeeBeanFlowApp": {
      "architect": {
        "build": {
          "options": {
            "styles": [
              "src/styles/styles.scss"
            ],
            "stylePreprocessorOptions": {
              "includePaths": [
                "src/styles"
              ]
            }
          }
        }
      }
    }
  }
}
```

#### 2.8. Crear ApiService
**Archivo:** `src/app/core/services/api.service.ts`

```typescript
import { Injectable } from '@angular/core';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError, retry } from 'rxjs/operators';

@Injectable({
  providedIn: 'root'
})
export class ApiService {
  private baseUrl = 'http://localhost:5176/api';  // Ajustar según tu puerto

  constructor(private http: HttpClient) { }

  crear<T>(endpoint: string, data: T): Observable<T> {
    return this.http.post<T>(`${this.baseUrl}/${endpoint}`, data)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  obtenerTodos<T>(endpoint: string): Observable<T[]> {
    return this.http.get<T[]>(`${this.baseUrl}/${endpoint}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  obtenerPorId<T>(endpoint: string, id: string | number): Observable<T> {
    return this.http.get<T>(`${this.baseUrl}/${endpoint}/${id}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  actualizar<T>(endpoint: string, id: string | number, data: T): Observable<T> {
    return this.http.put<T>(`${this.baseUrl}/${endpoint}/${id}`, data)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  eliminar(endpoint: string, id: string | number): Observable<any> {
    return this.http.delete(`${this.baseUrl}/${endpoint}/${id}`)
      .pipe(
        retry(1),
        catchError(this.handleError)
      );
  }

  private handleError(error: HttpErrorResponse) {
    let errorMessage = 'Ocurrió un error desconocido';

    if (error.error instanceof ErrorEvent) {
      errorMessage = `Error: ${error.error.message}`;
    } else {
      errorMessage = `Código: ${error.status}\nMensaje: ${error.message}`;

      if (error.status === 0) {
        errorMessage = 'No se pudo conectar con el servidor.';
      } else if (error.status === 400) {
        errorMessage = 'Datos inválidos.';
      } else if (error.status === 404) {
        errorMessage = 'Recurso no encontrado.';
      } else if (error.status === 409) {
        errorMessage = 'El registro ya existe.';
      }
    }

    return throwError(() => new Error(errorMessage));
  }
}
```

#### 2.9. Crear Componente LoadingSpinner
```bash
ng generate component shared/components/loading-spinner
```

**Archivo:** `src/app/shared/components/loading-spinner/loading-spinner.component.ts`

```typescript
import { Component } from '@angular/core';

@Component({
  selector: 'app-loading-spinner',
  standalone: true,
  template: `
    <div class="spinner-container">
      <div class="loading-spinner"></div>
      <p class="loading-text">Cargando...</p>
    </div>
  `,
  styles: [`
    .spinner-container {
      display: flex;
      flex-direction: column;
      align-items: center;
      justify-content: center;
      padding: 2rem;
    }

    .loading-text {
      margin-top: 1rem;
      color: #8B5A3C;
      font-weight: 500;
    }
  `]
})
export class LoadingSpinnerComponent { }
```

#### 2.10. Crear Componente Home Básico
```bash
ng generate component features/home
```

**Archivo:** `src/app/features/home/home.component.ts`

```typescript
import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-home',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './home.component.html',
  styleUrls: ['./home.component.scss']
})
export class HomeComponent {
  title = 'CoffeeBeanFlow';
}
```

**Archivo:** `src/app/features/home/home.component.html`

```html
<div class="home-container">
  <header class="home-header">
    <div class="logo-container">
      <span class="logo-icon">☕</span>
      <h1 class="logo-title">{{ title }}</h1>
    </div>
    <p class="subtitle">Sistema de Gestión del Proceso de Café</p>
  </header>

  <div class="welcome-card">
    <h2>Bienvenido</h2>
    <p>El sistema está listo para comenzar.</p>
    <p class="status">Estado: <span class="status-ok">✓ Operativo</span></p>
  </div>
</div>
```

**Archivo:** `src/app/features/home/home.component.scss`

```scss
@import '../../../styles/variables';
@import '../../../styles/mixins';

.home-container {
  max-width: 1200px;
  margin: 0 auto;
  padding: $space-4xl;
}

.home-header {
  text-align: center;
  margin-bottom: $space-4xl;

  .logo-container {
    display: flex;
    align-items: center;
    justify-content: center;
    gap: $space-lg;
    margin-bottom: $space-lg;

    .logo-icon {
      font-size: 4rem;
    }

    .logo-title {
      font-size: 3rem;
      color: $cafe-muy-oscuro;
      font-weight: 700;
    }
  }

  .subtitle {
    color: $cafe-oscuro;
    font-size: 1.25rem;
  }
}

.welcome-card {
  @include card-base;
  text-align: center;
  max-width: 600px;
  margin: 0 auto;

  h2 {
    color: $verde-oscuro;
    margin-bottom: $space-lg;
  }

  .status {
    margin-top: $space-lg;
    font-weight: 500;

    .status-ok {
      color: $color-success-dark;
    }
  }
}
```

#### 2.11. Configurar Routing
**Archivo:** `src/app/app.routes.ts`

```typescript
import { Routes } from '@angular/router';
import { HomeComponent } from './features/home/home.component';

export const routes: Routes = [
  {
    path: '',
    component: HomeComponent
  },
  {
    path: '**',
    redirectTo: ''
  }
];
```

#### 2.12. Configurar App Component
**Archivo:** `src/app/app.component.ts`

```typescript
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet],
  template: '<router-outlet></router-outlet>',
  styles: []
})
export class AppComponent {
  title = 'CoffeeBeanFlowApp';
}
```

#### 2.13. Configurar HttpClient
**Archivo:** `src/app/app.config.ts`

```typescript
import { ApplicationConfig, provideZoneChangeDetection } from '@angular/core';
import { provideRouter } from '@angular/router';
import { provideHttpClient, withInterceptors } from '@angular/common/http';

import { routes } from './app.routes';

export const appConfig: ApplicationConfig = {
  providers: [
    provideZoneChangeDetection({ eventCoalescing: true }),
    provideRouter(routes),
    provideHttpClient()
  ]
};
```

#### 2.14. Ejecutar y Probar
```bash
# Ejecutar el proyecto
ng serve

# La aplicación estará disponible en:
# http://localhost:4200
```

### ✅ Verificación de la Fase 2

**Criterios de Éxito:**
- ✅ El proyecto Angular compila sin errores
- ✅ La aplicación carga en `http://localhost:4200`
- ✅ Se muestra la página de bienvenida con el diseño correcto
- ✅ Los colores del tema café se aplican correctamente
- ✅ ApiService está disponible y configurado
- ✅ No hay errores en la consola del navegador

**Comprobaciones Visuales:**
- ✅ Logo de café (☕) visible
- ✅ Título "CoffeeBeanFlow" con el color café oscuro
- ✅ Tarjeta de bienvenida con sombra y bordes redondeados
- ✅ Estado "Operativo" en color verde

---

## NOTAS IMPORTANTES

### 🔥 Antes de Continuar a la Siguiente Fase

1. **Verifica que ambos proyectos funcionen:**
   - Backend: `dotnet run` en CoffeeBeanFlowAPI
   - Frontend: `ng serve` en CoffeeBeanFlowApp

2. **Prueba la conectividad:**
   - Backend responde en Swagger
   - Frontend carga sin errores

3. **Guarda tu progreso en Git:**
   ```bash
   # Backend
   cd CoffeeBeanFlowAPI
   git init
   git add .
   git commit -m "Fase 1 completada: Backend base"

   # Frontend
   cd ../CoffeeBeanFlowApp
   git init
   git add .
   git commit -m "Fase 2 completada: Frontend base"
   ```

### 📝 Siguientes Pasos

Una vez que hayas completado y verificado las **Fase 0, 1 y 2**, estarás listo para continuar con:

- **Fase 3:** Backend - Secado y Entidades Débiles (Humedad, TermoHigrometria, TemperaturaSecado, Ncama)
- **Fase 4:** Frontend - Formulario de Acopio (formulario reactivo completo con todos los 22 atributos)

---

## RESUMEN DE FASES RESTANTES

### 📌 Orden de Implementación Actualizado (Según Modelo Conceptual)

**BACKEND (Entidades y API):**
1. ✅ **Fase 1:** Área_Acopio completa (22 atributos) - **COMPLETADA**
2. **Fase 3:** Secado + Entidades Débiles (Humedad, TermoHigrometria, TemperaturaSecado, Ncama)
3. **Fase 5:** Bodega (11 atributos)
4. **Fase 8:** Trilla + PesoVerde (entidad débil)
5. **Fase 10:** Formulario_Caracterizacion + RC* (RCsobremaduras, RCinmaduras, RCmaduras)
6. **Fase 12:** Catación + Rondas
7. **Fase 14:** Relaciones N:N (Guardar_Cafe, Enviar_muestras, Suministra)

**FRONTEND (Formularios):**
1. ✅ **Fase 2:** Base Angular + Sistema de diseño - **COMPLETADA**
2. **Fase 4:** Formulario Área de Acopio (con atributos compuestos Despulpado y Pruebas_Fisicas_BH)
3. **Fase 6:** Formulario Secado (con registros de Humedad y TermoHigrometria)
4. **Fase 7:** Formulario Bodega
5. **Fase 9:** Formulario Trilla (con PesoVerde)
6. **Fase 11:** Formulario Caracterización (con RC*)
7. **Fase 13:** Formulario Catación (con atributos compuestos y Rondas)
8. **Fase 15:** Historial General
9. **Fase 16:** Trazabilidad Completa

**FINALIZACIÓN:**
10. **Fase 17:** Testing y Optimización
11. **Fase 18:** Deployment y Documentación Final

### 🔑 Patrón de Implementación por Fase:

1. **Backend:** Implementar entidad completa con **TODOS** los atributos según Modelo Conceptual
2. **Frontend:** Implementar formulario con validaciones
3. **Pruebas:** Verificar CRUD completo
4. **Integración:** Probar frontend + backend juntos

Cada fase se puede solicitar individualmente diciendo:
- "Genera la fase 3"
- "Genera la fase 4"
- etc.

---

**¿Estás listo para comenzar?** 🚀

Cuando hayas completado las fases 0, 1 y 2, y hayas verificado que todo funciona correctamente, simplemente dime:

**"Genera la fase 3"** o **"Siguiente fase"**

Y continuaremos con la implementación de **Secado y sus Entidades Débiles** en el backend.
