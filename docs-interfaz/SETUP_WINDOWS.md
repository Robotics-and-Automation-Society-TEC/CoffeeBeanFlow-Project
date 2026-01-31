# 🪟 Guía de Instalación y Configuración - CoffeeBeanFlow en Windows

> **📌 Documento para:** Configuración completa del proyecto CoffeeBeanFlow en Windows  
> **✅ Versión de Windows:** Windows 10/11 (64-bit)  
> **📅 Última actualización:** Diciembre 16, 2025

---

## 📋 Tabla de Contenidos

1. [Requisitos Previos](#requisitos-previos)
2. [Limpieza de Instalaciones Previas](#limpieza-de-instalaciones-previas)
3. [Instalación de Dependencias](#instalación-de-dependencias)
4. [Configuración de PostgreSQL](#configuración-de-postgresql)
5. [Obtener el Proyecto](#obtener-el-proyecto)
6. [Configuración del Backend (.NET)](#configuración-del-backend-net)
7. [Configuración del Frontend (Angular)](#configuración-del-frontend-angular)
8. [Ejecutar el Proyecto](#ejecutar-el-proyecto)
9. [Verificación y Pruebas](#verificación-y-pruebas)
10. [Troubleshooting](#troubleshooting)

---

## 📦 Requisitos Previos

- **Sistema Operativo:** Windows 10/11 (64-bit)
- **Memoria RAM:** Mínimo 4GB (recomendado 8GB)
- **Espacio en Disco:** Mínimo 10GB libres
- **Conexión a Internet:** Requerida para descargar dependencias
- **Permisos:** Cuenta de administrador

---

## 🧹 Limpieza de Instalaciones Previas

Si ya tienes instalaciones previas de .NET, Node.js, PostgreSQL o Angular, es recomendable limpiarlas primero para evitar conflictos.

### 1. Limpiar .NET SDK

**Usando PowerShell como Administrador:**

```powershell
# Verificar versiones instaladas
dotnet --list-sdks

# Para desinstalar completamente:
# 1. Ve a Panel de Control > Programas y características
# 2. Busca "Microsoft .NET SDK" y desinstala todas las versiones antiguas
# 3. O descarga la herramienta oficial de limpieza:
# https://github.com/dotnet/cli-lab/releases (dotnet-core-uninstall)
```

### 2. Limpiar Node.js y npm

```powershell
# Verificar versiones instaladas
node --version
npm --version

# Para desinstalar:
# 1. Ve a Panel de Control > Programas y características
# 2. Desinstala "Node.js"
# 3. Elimina carpetas residuales:
Remove-Item -Recurse -Force "$env:APPDATA\npm"
Remove-Item -Recurse -Force "$env:APPDATA\npm-cache"
Remove-Item -Recurse -Force "$env:ProgramFiles\nodejs"
```

### 3. Limpiar PostgreSQL (CUIDADO: Esto borrará todas las bases de datos)

```powershell
# Para desinstalar PostgreSQL:
# 1. Ve a Panel de Control > Programas y características
# 2. Desinstala "PostgreSQL"
# 3. Elimina carpetas residuales:
Remove-Item -Recurse -Force "C:\Program Files\PostgreSQL"
Remove-Item -Recurse -Force "$env:APPDATA\postgresql"
```

### 4. Limpiar Angular CLI

```powershell
# Remover Angular CLI global
npm uninstall -g @angular/cli
```

### 5. Limpiar Caché de npm

```powershell
# Limpiar caché
npm cache clean --force
```

---

## 🔧 Instalación de Dependencias

### 1. Instalar .NET 9.0 SDK

**Opción A: Instalador Oficial (Recomendado)**

1. Descarga el instalador desde: https://dotnet.microsoft.com/download/dotnet/9.0
2. Selecciona **".NET 9.0 SDK"** para Windows x64
3. Ejecuta el instalador descargado (`dotnet-sdk-9.0.xxx-win-x64.exe`)
4. Sigue el asistente de instalación (mantén las opciones por defecto)
5. Reinicia la terminal o PowerShell

**Verificar instalación:**

```powershell
# Abrir PowerShell
dotnet --version
# Debe mostrar: 9.0.xxx
```

**Opción B: Usando winget (Windows Package Manager)**

```powershell
# Desde PowerShell como Administrador
winget install Microsoft.DotNet.SDK.9
```

### 2. Instalar Node.js 20.x y npm

**Opción A: Instalador Oficial (Recomendado)**

1. Descarga el instalador desde: https://nodejs.org/
2. Selecciona la versión **"20.x LTS"**
3. Descarga el instalador Windows x64 (`.msi`)
4. Ejecuta el instalador
5. **IMPORTANTE:** Durante la instalación, marca la casilla:
   - ✅ **"Automatically install the necessary tools"**
   - ✅ **"Add to PATH"**
6. Completa la instalación
7. Reinicia la terminal o PowerShell

**Verificar instalación:**

```powershell
node --version   # Debe mostrar: v20.x.x
npm --version    # Debe mostrar: 10.x.x
```

**Opción B: Usando winget**

```powershell
winget install OpenJS.NodeJS.LTS
```

### 3. Instalar Angular CLI 21

```powershell
# Desde PowerShell (puede requerir Administrador)
npm install -g @angular/cli@21

# Verificar instalación
ng version
# Debe mostrar: Angular CLI: 21.0.x
```

**Si encuentras error de permisos de ejecución:**

```powershell
# Ejecutar como Administrador
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser
```

### 4. Instalar PostgreSQL 16

**Instalación:**

1. Descarga el instalador desde: https://www.postgresql.org/download/windows/
2. Selecciona **PostgreSQL 16** para Windows x64
3. Ejecuta el instalador (`.exe`)
4. Durante la instalación:
   - **Password:** Configura una contraseña para el usuario `postgres` (anótala, la necesitarás) (La contraseña puede ser 1234)
   - **Port:** Deja el puerto por defecto `5432`
   - **Locale:** Puedes dejarlo por defecto o seleccionar español
   - **Components:** Asegúrate de instalar:
     - ✅ PostgreSQL Server
     - ✅ pgAdmin 4 (herramienta gráfica)
     - ✅ Command Line Tools
5. Completa la instalación

**Verificar instalación:**

```powershell
# Agregar PostgreSQL al PATH (si no se agregó automáticamente)
# Busca la ruta de instalación, por ejemplo:
$env:Path += ";C:\Program Files\PostgreSQL\16\bin"

# Verificar versión
psql --version
# Debe mostrar: psql (PostgreSQL) 16.x
```

**Nota:** Si `psql` no se reconoce, necesitas agregarlo al PATH permanentemente:
1. Busca "Variables de entorno" en el menú inicio
2. Click en "Variables de entorno"
3. En "Variables del sistema", selecciona "Path" y click "Editar"
4. Agrega: `C:\Program Files\PostgreSQL\16\bin`
5. Click "Aceptar" y reinicia PowerShell

### 5. Instalar Entity Framework Core Tools

```powershell
# Instalar herramienta global de Entity Framework
dotnet tool install --global dotnet-ef

# Verificar instalación
dotnet ef --version
# Debe mostrar: Entity Framework Core .NET Command-line Tools 8.x.x
```

### 6. Instalar Git (Opcional pero Recomendado)

Si planeas usar control de versiones:

1. Descarga desde: https://git-scm.com/download/win
2. Ejecuta el instalador
3. Durante instalación, mantén opciones por defecto
4. Completa la instalación

**Verificar:**

```powershell
git --version
```

---

## 🗄️ Configuración de PostgreSQL

### 1. Verificar Servicio PostgreSQL

```powershell
# Verificar que el servicio está corriendo
Get-Service postgresql*

# Si no está corriendo, iniciarlo
Start-Service postgresql-x64-16
# (El nombre puede variar: postgresql-x64-16 o postgresql-16)

# Configurar inicio automático
Set-Service postgresql-x64-16 -StartupType Automatic
```

**Usando Servicios de Windows (GUI):**

1. Presiona `Win + R`, escribe `services.msc` y presiona Enter
2. Busca "postgresql-x64-16"
3. Click derecho → Propiedades
4. Tipo de inicio: **Automático**
5. Estado del servicio: **Iniciado**
6. Click "Aplicar" y "Aceptar"

### 2. Crear Base de Datos

**Opción A: Usando pgAdmin 4 (GUI - Más fácil)**

1. Abre **pgAdmin 4** desde el menú inicio
2. En el panel izquierdo, expande "Servers"
3. Click derecho en "PostgreSQL 16" → Connect
4. Ingresa la contraseña que configuraste durante la instalación
5. Click derecho en "Databases" → Create → Database
6. Nombre: `coffeebeanflow_db`
7. Click "Save"

**Opción B: Usando línea de comandos**

```powershell
# Conectar a PostgreSQL (te pedirá la contraseña)
psql -U postgres

# Dentro de psql, crear la base de datos
CREATE DATABASE coffeebeanflow_db;

# Verificar que se creó
\l

# Salir
\q
```

### 3. Configurar Acceso

Si tienes problemas de conexión, edita el archivo de configuración:

```powershell
# Ubicación típica del archivo pg_hba.conf:
# C:\Program Files\PostgreSQL\16\data\pg_hba.conf

# Abrir con notepad (como Administrador)
notepad "C:\Program Files\PostgreSQL\16\data\pg_hba.conf"

# Buscar líneas que digan:
# host    all             all             127.0.0.1/32            scram-sha-256

# Cambiar a:
# host    all             all             127.0.0.1/32            md5

# Guardar y cerrar

# Reiniciar servicio PostgreSQL
Restart-Service postgresql-x64-16
```

### 4. Probar Conexión

```powershell
# Probar conexión con contraseña
psql -U postgres -h localhost -d coffeebeanflow_db

# Si pide contraseña, ingresar la que configuraste
# Si conecta correctamente, salir con: \q
```

---

## 📥 Obtener el Proyecto

### Opción 1: Copiar desde USB / Carpeta

```powershell
# Crear carpeta de proyectos
New-Item -Path "$env:USERPROFILE\proyectos" -ItemType Directory -Force
cd "$env:USERPROFILE\proyectos"

# Copiar el proyecto (ajustar ruta según donde esté)
Copy-Item -Path "D:\CoffeeBeanFlow-Project-Cambio" -Destination "$env:USERPROFILE\proyectos\" -Recurse

# Entrar al proyecto
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio"
```

### Opción 2: Clonar desde Git

```powershell
# Crear carpeta de proyectos
New-Item -Path "$env:USERPROFILE\proyectos" -ItemType Directory -Force
cd "$env:USERPROFILE\proyectos"

# Clonar repositorio
git clone [URL_DEL_REPOSITORIO] CoffeeBeanFlow-Project-Cambio

# Entrar al proyecto
cd CoffeeBeanFlow-Project-Cambio
```

### Opción 3: Descargar como ZIP

1. Descarga el archivo ZIP del proyecto
2. Click derecho en el ZIP → **Extraer todo...**
3. Extrae en: `C:\Users\TuUsuario\proyectos\`
4. Abre PowerShell y navega:

```powershell
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio"
```

---

## ⚙️ Configuración del Backend (.NET)

### 1. Verificar Estructura del Proyecto

```powershell
# Desde la raíz del proyecto
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio"

# Ver estructura
Get-ChildItem
# Debes ver carpetas: Backend/, Frontend/, y archivos .md
```

### 2. Configurar Cadena de Conexión

```powershell
# Entrar a la carpeta Backend
cd Backend

# Abrir archivo de configuración con notepad
notepad appsettings.json
```

Asegúrate de que el archivo `appsettings.json` tenga esta configuración:

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
    "DefaultConnection": "Host=localhost;Port=5432;Database=coffeebeanflow_db;Username=postgres;Password=TU_CONTRASEÑA_AQUI"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200"
    ]
  }
}
```

**⚠️ IMPORTANTE:** Cambia `TU_CONTRASEÑA_AQUI` por la contraseña que configuraste para PostgreSQL.

Guarda el archivo (`Ctrl+S`) y cierra notepad.

### 3. Restaurar Dependencias

```powershell
# Asegúrate de estar en la carpeta Backend
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Backend"

# Restaurar paquetes NuGet
dotnet restore

# Esto puede tomar unos minutos la primera vez
```

### 4. Verificar y Aplicar Migraciones

```powershell
# Listar migraciones disponibles
dotnet ef migrations list

# Si hay migraciones, aplicarlas
dotnet ef database update

# Esto creará todas las tablas en la base de datos
```

**Si NO hay migraciones, crearlas:**

```powershell
# Crear migración inicial
dotnet ef migrations add InitialCreate

# Aplicar a la base de datos
dotnet ef database update
```

**Si hay error con dotnet-ef:**

```powershell
# Reinstalar la herramienta
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef

# Reiniciar PowerShell y reintentar
```

### 5. Compilar el Proyecto

```powershell
# Compilar para verificar que no hay errores
dotnet build

# Debe mostrar: Build succeeded. 0 Warning(s). 0 Error(s).
```

---

## 🎨 Configuración del Frontend (Angular)

### 1. Instalar Dependencias de Node

```powershell
# Entrar a la carpeta Frontend
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Frontend"

# Limpiar caché de npm (opcional pero recomendado)
npm cache clean --force

# Instalar dependencias
npm install

# Esto puede tomar varios minutos
```

**Si encuentras errores de permisos:**

```powershell
# Ejecutar PowerShell como Administrador y reintentar
npm install

# O configurar npm para no usar enlaces simbólicos (en Windows a veces causa problemas)
npm config set legacy-peer-deps true
npm install
```

### 2. Verificar Configuración de API

La configuración de la API ya debería estar correcta, pero verifica:

```powershell
# Abrir el archivo de servicio (puedes usar notepad o VS Code)
notepad src\app\core\services\api.service.ts

# O si tienes VS Code instalado:
code src\app\core\services\api.service.ts
```

Busca la línea que dice `baseUrl` y verifica que sea:

```typescript
private baseUrl = 'http://localhost:5253/api';
```

**Nota:** El puerto `5253` puede variar. Lo verificaremos cuando ejecutemos el backend.

### 3. Compilar el Proyecto

```powershell
# Compilar para verificar que no hay errores
ng build

# O para compilación en modo desarrollo:
ng build --configuration development
```

---

## 🚀 Ejecutar el Proyecto

### 1. Ejecutar Backend (PowerShell 1)

```powershell
# Abrir PowerShell
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Backend"

# Ejecutar el backend
dotnet run

# Debes ver algo como:
# info: Microsoft.Hosting.Lifetime[14]
#       Now listening on: http://localhost:5253
# info: Microsoft.Hosting.Lifetime[0]
#       Application started. Press Ctrl+C to shut down.
```

**📝 NOTA IMPORTANTE:** Anota el puerto en el que corre (en este ejemplo es `5253`). Si es diferente, deberás actualizar el frontend.

**Para verificar que funciona:**
- Abre un navegador y ve a: `http://localhost:5253/swagger`
- Debes ver la documentación de la API (Swagger UI)

### 2. Actualizar Puerto en Frontend (si es necesario)

Si el backend NO está en el puerto `5253`:

```powershell
# En otra PowerShell, editar el servicio API
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Frontend"
notepad src\app\core\services\api.service.ts

# Cambiar la línea:
private baseUrl = 'http://localhost:PUERTO_CORRECTO/api';

# Guardar y cerrar
```

### 3. Ejecutar Frontend (PowerShell 2)

```powershell
# Abrir una segunda PowerShell
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Frontend"

# Ejecutar el servidor de desarrollo
ng serve

# Debes ver:
# ** Angular Live Development Server is listening on localhost:4200, open your browser on http://localhost:4200/ **
# ✔ Compiled successfully.
```

**Para acceder:**
- Abre un navegador y ve a: `http://localhost:4200`
- Debes ver la aplicación CoffeeBeanFlow

---

## ✅ Verificación y Pruebas

### 1. Verificar Backend

```powershell
# En una nueva PowerShell, probar con curl (si está instalado)
curl http://localhost:5253/api/health

# O abre el navegador en:
# http://localhost:5253/swagger
```

### 2. Verificar Base de Datos

**Opción A: Usando pgAdmin 4 (GUI)**

1. Abre pgAdmin 4
2. Navega: Servers → PostgreSQL 16 → Databases → coffeebeanflow_db → Schemas → public → Tables
3. Debes ver tablas como:
   - area_acopio
   - secado
   - bodega
   - catacion
   - trilla
   - caracterizacion
   - etc.

**Opción B: Usando línea de comandos**

```powershell
# Conectar a PostgreSQL
psql -U postgres -h localhost -d coffeebeanflow_db

# Listar tablas
\dt

# Salir
\q
```

### 3. Verificar Frontend

1. **Abrir navegador:** `http://localhost:4200`
2. **Ir a Catación:** Click en el menú o navegar a `http://localhost:4200/catacion/nuevo`
3. **Verificar que carga:** Debe mostrar el formulario sin quedarse en "Cargando..."
4. **Probar crear registro:** Llenar formulario y guardar

### 4. Probar Flujo Completo

**Crear un lote de prueba:**

1. Ve a: `http://localhost:4200/acopio/nuevo`
2. Llena el formulario con datos de prueba:
   - Número de Lote: `L2025-001`
   - Nombre Productor: `Juan Pérez`
   - Nombre Finca: `El Cafetal`
   - Zona: `Norte`
   - Fecha de Acopio: Fecha actual
   - Cantidad: `1000`
3. Guarda el lote
4. Ve a catación y selecciona el lote creado
5. Completa los datos de catación
6. Guarda

**Verificar en base de datos:**

Abre pgAdmin 4 o usa psql para ver los datos guardados.

---

## 🔧 Troubleshooting

### Problema 1: "Cannot connect to PostgreSQL"

**Síntomas:**
- Error: `Connection refused` o `could not connect to server`

**Soluciones:**

```powershell
# Verificar que PostgreSQL está corriendo
Get-Service postgresql*

# Si no está activo, iniciarlo
Start-Service postgresql-x64-16

# Verificar firewall de Windows
# 1. Busca "Firewall de Windows" en el menú inicio
# 2. Click en "Configuración avanzada"
# 3. Reglas de entrada → Nueva regla
# 4. Puerto → TCP → 5432 → Permitir conexión

# Verificar que PostgreSQL escucha en el puerto
netstat -an | findstr "5432"
# Debe mostrar: TCP 0.0.0.0:5432 LISTENING
```

### Problema 2: ".NET SDK not found"

**Síntomas:**
- Error: `'dotnet' is not recognized as an internal or external command`

**Soluciones:**

```powershell
# Verificar instalación
where.exe dotnet

# Si no se encuentra, reinstalar .NET SDK
# Descargar desde: https://dotnet.microsoft.com/download/dotnet/9.0

# Agregar al PATH manualmente:
# 1. Busca "Variables de entorno" en el menú inicio
# 2. Variables del sistema → Path → Editar
# 3. Agregar: C:\Program Files\dotnet
# 4. Reiniciar PowerShell
```

### Problema 3: "Port 5253 already in use"

**Síntomas:**
- Error: `Address already in use` o `Failed to bind to address`

**Soluciones:**

```powershell
# Encontrar proceso usando el puerto
netstat -ano | findstr "5253"
# Anotará el PID (última columna)

# Matar el proceso (reemplazar 1234 con el PID real)
taskkill /PID 1234 /F

# O cambiar el puerto en el backend
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Backend"
notepad Properties\launchSettings.json
# Cambiar "applicationUrl" a otro puerto
```

### Problema 4: "Angular compilation errors"

**Síntomas:**
- Errores de TypeScript o módulos no encontrados

**Soluciones:**

```powershell
# Limpiar node_modules y reinstalar
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Frontend"
Remove-Item -Recurse -Force node_modules
Remove-Item package-lock.json
npm cache clean --force
npm install

# Si persiste, verificar versión de Node
node --version  # Debe ser v20.x.x

# Reinstalar Angular CLI
npm uninstall -g @angular/cli
npm install -g @angular/cli@21
```

### Problema 5: "CORS policy error"

**Síntomas:**
- Error en consola del navegador: `Access to XMLHttpRequest blocked by CORS policy`

**Soluciones:**

```powershell
# Verificar configuración CORS en backend
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Backend"
notepad appsettings.json

# Asegúrate de tener:
"Cors": {
  "AllowedOrigins": [
    "http://localhost:4200"
  ]
}

# Reiniciar backend
# Ctrl+C en la PowerShell del backend y volver a ejecutar: dotnet run
```

### Problema 6: "Cannot find module '@angular/...' "

**Síntomas:**
- Error: módulos de Angular no encontrados

**Soluciones:**

```powershell
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Frontend"

# Instalar dependencias específicas
npm install @angular/animations@21
npm install @angular/common@21
npm install @angular/compiler@21
npm install @angular/core@21
npm install @angular/forms@21
npm install @angular/platform-browser@21
npm install @angular/platform-browser-dynamic@21
npm install @angular/router@21

# Reinstalar todo
npm install
```

### Problema 7: "Permission denied" o "Access denied"

**Síntomas:**
- Error: `EACCES: permission denied` o `Access is denied`

**Soluciones:**

```powershell
# Ejecutar PowerShell como Administrador
# Click derecho en PowerShell → "Ejecutar como administrador"

# Cambiar política de ejecución
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

# Deshabilitar antivirus temporalmente (puede interferir con npm install)
# Solo durante la instalación, luego reactivarlo
```

### Problema 8: "Database connection timeout"

**Síntomas:**
- Error: `timeout expired` al conectar a PostgreSQL

**Soluciones:**

```powershell
# Verificar archivo postgresql.conf
notepad "C:\Program Files\PostgreSQL\16\data\postgresql.conf"

# Buscar y verificar:
# listen_addresses = 'localhost' o '*'

# Verificar pg_hba.conf
notepad "C:\Program Files\PostgreSQL\16\data\pg_hba.conf"

# Debe tener:
# host all all 127.0.0.1/32 md5

# Reiniciar servicio
Restart-Service postgresql-x64-16
```

### Problema 9: "dotnet-ef not recognized"

**Síntomas:**
- Error: `'dotnet-ef' is not recognized`

**Soluciones:**

```powershell
# Reinstalar Entity Framework Tools
dotnet tool uninstall --global dotnet-ef
dotnet tool install --global dotnet-ef

# Verificar PATH de herramientas .NET
# Agregar a PATH si es necesario:
# %USERPROFILE%\.dotnet\tools

# Reiniciar PowerShell
```

### Problema 10: "ng not recognized"

**Síntomas:**
- Error: `'ng' is not recognized as an internal or external command`

**Soluciones:**

```powershell
# Verificar instalación global de Angular CLI
npm list -g @angular/cli

# Si no está instalado:
npm install -g @angular/cli@21

# Verificar PATH de npm global
npm config get prefix
# Debe estar en PATH: C:\Users\TuUsuario\AppData\Roaming\npm

# Agregar al PATH si es necesario
# Reiniciar PowerShell
```

---

## 📝 Comandos Útiles de Referencia

### PostgreSQL (Windows)

```powershell
# Ver servicios PostgreSQL
Get-Service postgresql*

# Iniciar servicio
Start-Service postgresql-x64-16

# Detener servicio
Stop-Service postgresql-x64-16

# Reiniciar servicio
Restart-Service postgresql-x64-16

# Conectar a base de datos
psql -U postgres -h localhost -d coffeebeanflow_db

# Hacer backup
pg_dump -U postgres coffeebeanflow_db > backup.sql

# Restaurar backup
psql -U postgres -d coffeebeanflow_db -f backup.sql
```

### .NET

```powershell
# Ver versión
dotnet --version

# Listar SDKs instalados
dotnet --list-sdks

# Restaurar dependencias
dotnet restore

# Compilar
dotnet build

# Ejecutar
dotnet run

# Limpiar archivos compilados
dotnet clean

# Ver migraciones
dotnet ef migrations list

# Crear migración
dotnet ef migrations add NombreMigracion

# Aplicar migraciones
dotnet ef database update

# Revertir última migración
dotnet ef database update PenultimaMigracion
```

### Angular / Node.js

```powershell
# Ver versión de Node
node --version

# Ver versión de npm
npm --version

# Ver versión de Angular CLI
ng version

# Instalar dependencias
npm install

# Limpiar caché
npm cache clean --force

# Ejecutar desarrollo
ng serve

# Compilar para producción
ng build --configuration production

# Ejecutar tests
ng test

# Generar componente
ng generate component nombre-componente
```

### PowerShell Útiles

```powershell
# Ver procesos escuchando en puertos
netstat -ano | findstr "LISTENING"

# Matar proceso por PID
taskkill /PID 1234 /F

# Ver servicios
Get-Service

# Reiniciar servicio
Restart-Service nombre-servicio

# Ver variables de entorno
Get-ChildItem Env:

# Agregar a PATH temporal
$env:Path += ";C:\ruta\a\agregar"

# Limpiar pantalla
cls
```

---

## 🎯 Resumen de Puertos y URLs

| Servicio | Puerto/URL | Descripción |
|----------|-----------|-------------|
| PostgreSQL | `5432` | Base de datos |
| Backend API | `http://localhost:5253` | API REST |
| Swagger UI | `http://localhost:5253/swagger` | Documentación API |
| Health Check | `http://localhost:5253/api/health` | Verificación de estado |
| Frontend | `http://localhost:4200` | Aplicación web |
| pgAdmin 4 | GUI Application | Administración PostgreSQL |

---

## 📚 Recursos Adicionales

### Documentación Oficial

- **.NET:** https://docs.microsoft.com/dotnet/
- **Angular:** https://angular.io/docs
- **PostgreSQL:** https://www.postgresql.org/docs/
- **Entity Framework Core:** https://docs.microsoft.com/ef/core/

### Herramientas Recomendadas

- **Visual Studio Code:** https://code.visualstudio.com/
- **Git for Windows:** https://git-scm.com/download/win
- **Windows Terminal:** https://aka.ms/terminal (mejor que PowerShell/CMD)
- **Postman:** https://www.postman.com/ (para probar APIs)

---

## 🔒 Scripts de Inicio Automatizado

### Opción 1: Archivo Batch para iniciar Backend

Crea un archivo `iniciar-backend.bat` en la carpeta Backend:

```batch
@echo off
echo ========================================
echo Iniciando Backend - CoffeeBeanFlow
echo ========================================
cd /d "%~dp0"
dotnet run
pause
```

### Opción 2: Archivo Batch para iniciar Frontend

Crea un archivo `iniciar-frontend.bat` en la carpeta Frontend:

```batch
@echo off
echo ========================================
echo Iniciando Frontend - CoffeeBeanFlow
echo ========================================
cd /d "%~dp0"
call npm install
call ng serve
pause
```

### Opción 3: Script PowerShell para iniciar todo

Crea un archivo `iniciar-proyecto.ps1` en la raíz del proyecto:

```powershell
# Iniciar Backend en nueva ventana
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\Backend'; dotnet run"

# Esperar 5 segundos
Start-Sleep -Seconds 5

# Iniciar Frontend en nueva ventana
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$PSScriptRoot\Frontend'; ng serve"

# Esperar 10 segundos
Start-Sleep -Seconds 10

# Abrir navegador
Start-Process "http://localhost:4200"

Write-Host "Proyecto iniciado correctamente!"
Write-Host "Backend: http://localhost:5253"
Write-Host "Frontend: http://localhost:4200"
```

Para ejecutar el script:

```powershell
# Dar permisos de ejecución (primera vez)
Set-ExecutionPolicy -ExecutionPolicy RemoteSigned -Scope CurrentUser

# Ejecutar
.\iniciar-proyecto.ps1
```

---

## ✅ Checklist Final

Marca cada ítem cuando lo completes:

### Instalación
- [ ] Windows 10/11 actualizado
- [ ] .NET 9.0 SDK instalado (`dotnet --version`)
- [ ] Node.js 20.x instalado (`node --version`)
- [ ] npm instalado (`npm --version`)
- [ ] Angular CLI 21 instalado (`ng version`)
- [ ] PostgreSQL 16 instalado (`psql --version`)
- [ ] Entity Framework Tools instalado (`dotnet ef --version`)
- [ ] Git instalado (opcional) (`git --version`)

### Configuración
- [ ] Servicio PostgreSQL corriendo (`Get-Service postgresql*`)
- [ ] Base de datos `coffeebeanflow_db` creada
- [ ] Usuario postgres configurado con contraseña
- [ ] Proyecto copiado/descargado
- [ ] `appsettings.json` configurado con cadena de conexión correcta
- [ ] Dependencias del backend restauradas (`dotnet restore`)
- [ ] Dependencias del frontend instaladas (`npm install`)

### Ejecución
- [ ] Migraciones aplicadas (`dotnet ef database update`)
- [ ] Backend compilado sin errores (`dotnet build`)
- [ ] Frontend compilado sin errores (`ng build`)
- [ ] Backend ejecutándose (`dotnet run`)
- [ ] Frontend ejecutándose (`ng serve`)
- [ ] Swagger accesible en navegador (`http://localhost:5253/swagger`)
- [ ] Frontend accesible en navegador (`http://localhost:4200`)
- [ ] Health check responde correctamente
- [ ] Formularios cargan sin errores

### Pruebas
- [ ] Puede crear un lote en área de acopio
- [ ] Puede ver lotes creados
- [ ] Puede editar un lote
- [ ] Puede crear una catación
- [ ] Datos se guardan en PostgreSQL
- [ ] Puede ver historial general
- [ ] Puede ver trazabilidad de lote

---

## 🆘 Contacto y Soporte

Si encuentras problemas que no están cubiertos en este documento:

### 1. Revisar logs del backend

```powershell
cd "$env:USERPROFILE\proyectos\CoffeeBeanFlow-Project-Cambio\Backend"
dotnet run --verbosity detailed
```

### 2. Revisar logs del frontend

- Abre DevTools en el navegador (`F12`)
- Ve a la pestaña "Console" para ver errores JavaScript
- Ve a la pestaña "Network" para ver llamadas HTTP fallidas

### 3. Revisar logs de PostgreSQL

```powershell
# Los logs están en:
# C:\Program Files\PostgreSQL\16\data\log\

# Ver el archivo más reciente
Get-Content "C:\Program Files\PostgreSQL\16\data\log\postgresql-*.log" -Tail 50
```

### 4. Documentación del proyecto

- Ver archivo `README.md` en la raíz del proyecto
- Ver `BACKEND_DOCUMENTATION_COMPLETE.md` para detalles del backend
- Ver `FRONTEND_DOCUMENTATION_COMPLETE.md` para detalles del frontend
- Ver `PLAN_FASES_IMPLEMENTACION.md` para entender la estructura

---

## 🎉 ¡Listo!

Si completaste todos los pasos y el checklist, tu entorno de desarrollo en Windows está configurado correctamente.

**Próximos pasos:**
1. Familiarízate con la estructura del proyecto
2. Lee la documentación en `PLAN_FASES_IMPLEMENTACION.md`
3. Revisa los modelos de datos en `Modelo_Conceptual_Base_Datos_Completo.md`
4. Comienza a desarrollar nuevas funcionalidades

**Atajos de teclado útiles:**
- `Ctrl + C` - Detener proceso en PowerShell
- `F12` - Abrir DevTools en navegador
- `Ctrl + Shift + R` - Recargar sin caché en navegador
- `Windows + R` - Ejecutar comando rápido

**¡Éxito con el proyecto CoffeeBeanFlow! ☕🚀**

---

## 🔍 Diferencias Clave Windows vs Linux

| Aspecto | Windows | Linux (Ubuntu) |
|---------|---------|----------------|
| Gestor de paquetes | winget, instaladores .exe/.msi | apt-get |
| Terminal | PowerShell, CMD | bash, zsh |
| Servicios | `Get-Service`, `services.msc` | `systemctl` |
| Variables PATH | GUI o `$env:Path` | `~/.bashrc`, `export PATH` |
| Editor texto | notepad, VS Code | nano, vim, VS Code |
| Rutas | `\` (C:\Users\...) | `/` (/home/...) |
| Permisos | Administrador | sudo |
| PostgreSQL config | `C:\Program Files\PostgreSQL\...` | `/etc/postgresql/...` |

---

**Nota:** Este documento fue creado para facilitar la configuración en Windows 10/11. Para Linux (Ubuntu), consulta el archivo `SETUP_UBUNTU_LINUX.md` en el proyecto.

**Última revisión:** Diciembre 16, 2025
