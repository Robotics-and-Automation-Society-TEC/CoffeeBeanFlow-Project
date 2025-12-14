# 🐧 Guía de Instalación y Configuración - CoffeeBeanFlow en Ubuntu Linux

> **📌 Documento para:** Configuración completa del proyecto CoffeeBeanFlow en Ubuntu Linux  
> **✅ Versión de Ubuntu:** 20.04 LTS o superior  
> **📅 Última actualización:** Diciembre 14, 2025

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

- **Sistema Operativo:** Ubuntu 20.04 LTS o superior
- **Memoria RAM:** Mínimo 4GB (recomendado 8GB)
- **Espacio en Disco:** Mínimo 10GB libres
- **Conexión a Internet:** Requerida para descargar dependencias
- **Permisos:** Acceso sudo

---

## 🧹 Limpieza de Instalaciones Previas

Si ya tienes instalaciones previas de .NET, Node.js, PostgreSQL o Angular, es recomendable limpiarlas primero para evitar conflictos.

### 1. Limpiar .NET SDK

```bash
# Verificar versiones instaladas
dotnet --list-sdks

# Remover .NET si existe (opcional)
sudo apt-get remove --purge dotnet-sdk-*
sudo apt-get remove --purge aspnetcore-runtime-*
sudo apt-get remove --purge dotnet-runtime-*

# Limpiar repositorios
sudo rm /etc/apt/sources.list.d/microsoft-prod.list
sudo apt-get update
```

### 2. Limpiar Node.js y npm

```bash
# Verificar versiones instaladas
node --version
npm --version

# Remover Node.js si existe (opcional)
sudo apt-get remove --purge nodejs
sudo apt-get remove --purge npm

# Limpiar caché de npm
sudo rm -rf /usr/local/lib/node_modules
sudo rm -rf ~/.npm
```

### 3. Limpiar PostgreSQL (CUIDADO: Esto borrará todas las bases de datos)

```bash
# Verificar si PostgreSQL está instalado
psql --version

# Si deseas remover PostgreSQL completamente (CUIDADO: Se perderán datos)
sudo systemctl stop postgresql
sudo apt-get remove --purge postgresql-*
sudo rm -rf /var/lib/postgresql
sudo rm -rf /etc/postgresql
sudo userdel -r postgres
```

### 4. Limpiar Angular CLI

```bash
# Remover Angular CLI global
npm uninstall -g @angular/cli
```

### 5. Limpiar caché general

```bash
# Limpiar paquetes huérfanos
sudo apt-get autoremove
sudo apt-get autoclean
sudo apt-get clean
```

---

## 🔧 Instalación de Dependencias

### 1. Actualizar el Sistema

```bash
# Actualizar lista de paquetes
sudo apt-get update

# Actualizar paquetes instalados
sudo apt-get upgrade -y
```

### 2. Instalar Herramientas Básicas

```bash
# Instalar herramientas necesarias
sudo apt-get install -y wget curl apt-transport-https software-properties-common git
```

### 3. Instalar .NET 9.0 SDK

```bash
# Agregar repositorio de Microsoft
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb -O packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
rm packages-microsoft-prod.deb

# Actualizar lista de paquetes
sudo apt-get update

# Instalar .NET 9.0 SDK
sudo apt-get install -y dotnet-sdk-9.0

# Verificar instalación
dotnet --version
# Debe mostrar: 9.0.x
```

### 4. Instalar Node.js 20.x y npm

```bash
# Agregar repositorio de NodeSource para Node.js 20.x
curl -fsSL https://deb.nodesource.com/setup_20.x | sudo -E bash -

# Instalar Node.js y npm
sudo apt-get install -y nodejs

# Verificar instalación
node --version   # Debe mostrar: v20.x.x
npm --version    # Debe mostrar: 10.x.x
```

### 5. Instalar Angular CLI 21

```bash
# Instalar Angular CLI globalmente
sudo npm install -g @angular/cli@21

# Verificar instalación
ng version
# Debe mostrar: Angular CLI: 21.0.2
```

### 6. Instalar PostgreSQL 16

```bash
# Agregar repositorio de PostgreSQL
sudo sh -c 'echo "deb http://apt.postgresql.org/pub/repos/apt $(lsb_release -cs)-pgdg main" > /etc/apt/sources.list.d/pgdg.list'

# Importar clave del repositorio
wget --quiet -O - https://www.postgresql.org/media/keys/ACCC4CF8.asc | sudo apt-key add -

# Actualizar lista de paquetes
sudo apt-get update

# Instalar PostgreSQL 16
sudo apt-get install -y postgresql-16 postgresql-contrib-16

# Verificar instalación
psql --version
# Debe mostrar: psql (PostgreSQL) 16.x
```

### 7. Instalar Entity Framework Core Tools

```bash
# Instalar herramienta global de Entity Framework
dotnet tool install --global dotnet-ef

# Agregar al PATH (agregar a ~/.bashrc o ~/.profile)
echo 'export PATH="$PATH:$HOME/.dotnet/tools"' >> ~/.bashrc
source ~/.bashrc

# Verificar instalación
dotnet ef --version
```

---

## 🗄️ Configuración de PostgreSQL

### 1. Iniciar Servicio PostgreSQL

```bash
# Iniciar PostgreSQL
sudo systemctl start postgresql

# Habilitar inicio automático
sudo systemctl enable postgresql

# Verificar estado
sudo systemctl status postgresql
# Debe mostrar: active (running)
```

### 2. Configurar Usuario y Contraseña

```bash
# Cambiar a usuario postgres
sudo -i -u postgres

# Acceder a PostgreSQL
psql

# Dentro de psql, cambiar contraseña del usuario postgres
ALTER USER postgres PASSWORD 'postgres123';

# Salir de psql
\q

# Volver a tu usuario
exit
```

### 3. Crear Base de Datos

```bash
# Como usuario postgres
sudo -u postgres psql

# Dentro de psql, crear la base de datos
CREATE DATABASE coffeebeanflow_db;

# Verificar que se creó
\l

# Salir
\q
```

### 4. Configurar Acceso Local

```bash
# Editar archivo de configuración pg_hba.conf
sudo nano /etc/postgresql/16/main/pg_hba.conf

# Buscar la línea que dice:
# local   all             postgres                                peer

# Cambiarla a:
# local   all             postgres                                md5

# También verificar que exista esta línea para IPv4:
# host    all             all             127.0.0.1/32            md5

# Guardar (Ctrl+O) y salir (Ctrl+X)

# Reiniciar PostgreSQL para aplicar cambios
sudo systemctl restart postgresql
```

### 5. Probar Conexión

```bash
# Probar conexión con contraseña
psql -U postgres -h localhost -d coffeebeanflow_db

# Si pide contraseña, ingresar: postgres123
# Si conecta correctamente, salir con: \q
```

---

## 📥 Obtener el Proyecto

### Opción 1: Copiar desde Memoria USB / Carpeta Compartida

```bash
# Crear carpeta de proyectos
mkdir -p ~/proyectos
cd ~/proyectos

# Copiar el proyecto (ajustar ruta según donde esté)
cp -r /ruta/origen/CoffeeBeanFlow-Project-Cambio ~/proyectos/

# Entrar al proyecto
cd CoffeeBeanFlow-Project-Cambio
```

### Opción 2: Clonar desde Git (si está en repositorio)

```bash
# Crear carpeta de proyectos
mkdir -p ~/proyectos
cd ~/proyectos

# Clonar repositorio
git clone [URL_DEL_REPOSITORIO] CoffeeBeanFlow-Project-Cambio

# Entrar al proyecto
cd CoffeeBeanFlow-Project-Cambio
```

### Opción 3: Descargar como ZIP

```bash
# Si descargaste un ZIP, descomprimirlo
cd ~/Descargas
unzip CoffeeBeanFlow-Project-Cambio.zip -d ~/proyectos/

# Entrar al proyecto
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio
```

---

## ⚙️ Configuración del Backend (.NET)

### 1. Verificar Estructura del Proyecto

```bash
# Desde la raíz del proyecto
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio

# Ver estructura
ls -la
# Debes ver carpetas: Backend/, Frontend/, y archivos .md
```

### 2. Configurar Cadena de Conexión

```bash
# Entrar a la carpeta Backend
cd Backend

# Editar archivo de configuración
nano appsettings.json
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
    "DefaultConnection": "Host=localhost;Port=5432;Database=coffeebeanflow_db;Username=postgres;Password=postgres123"
  },
  "Cors": {
    "AllowedOrigins": [
      "http://localhost:4200"
    ]
  }
}
```

**⚠️ IMPORTANTE:** Cambia `postgres123` por la contraseña que configuraste para PostgreSQL.

Guardar con `Ctrl+O` y salir con `Ctrl+X`.

### 3. Restaurar Dependencias

```bash
# Asegúrate de estar en la carpeta Backend
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Backend

# Restaurar paquetes NuGet
dotnet restore

# Esto puede tomar unos minutos la primera vez
```

### 4. Verificar y Aplicar Migraciones

```bash
# Listar migraciones disponibles
dotnet ef migrations list

# Si hay migraciones, aplicarlas
dotnet ef database update

# Esto creará todas las tablas en la base de datos
```

**Si NO hay migraciones, crearlas:**

```bash
# Crear migración inicial
dotnet ef migrations add InitialCreate

# Aplicar a la base de datos
dotnet ef database update
```

### 5. Compilar el Proyecto

```bash
# Compilar para verificar que no hay errores
dotnet build

# Debe mostrar: Build succeeded. 0 Warning(s). 0 Error(s).
```

---

## 🎨 Configuración del Frontend (Angular)

### 1. Instalar Dependencias de Node

```bash
# Entrar a la carpeta Frontend
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend

# Limpiar caché de npm (opcional pero recomendado)
npm cache clean --force

# Instalar dependencias
npm install

# Esto puede tomar varios minutos
```

**Si encuentras errores de permisos:**

```bash
# Cambiar permisos de la carpeta node_modules
sudo chown -R $USER:$USER ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend

# Reintentar instalación
npm install
```

### 2. Verificar Configuración de API

```bash
# Verificar que el archivo de servicio apunte al puerto correcto
nano src/app/core/services/api.service.ts
```

Busca la línea que dice `baseUrl` y verifica que sea:

```typescript
private baseUrl = 'http://localhost:5253/api';
```

**Nota:** El puerto `5253` puede variar. Lo verificaremos cuando ejecutemos el backend.

### 3. Compilar el Proyecto

```bash
# Compilar para verificar que no hay errores
ng build

# O para compilación en modo desarrollo:
ng build --configuration development
```

---

## 🚀 Ejecutar el Proyecto

### 1. Ejecutar Backend (Terminal 1)

```bash
# Abrir una nueva terminal
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Backend

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

```bash
# En otra terminal, editar el servicio API
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend
nano src/app/core/services/api.service.ts

# Cambiar la línea:
private baseUrl = 'http://localhost:PUERTO_CORRECTO/api';

# Guardar y salir
```

### 3. Ejecutar Frontend (Terminal 2)

```bash
# Abrir una segunda terminal (Ctrl+Shift+T)
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend

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

```bash
# En una nueva terminal, probar el health check
curl http://localhost:5253/api/health

# Debe responder:
# {"status":"OK","timestamp":"2025-12-14T...","message":"CoffeeBeanFlow API is running"}
```

### 2. Verificar Base de Datos

```bash
# Conectar a PostgreSQL
psql -U postgres -h localhost -d coffeebeanflow_db

# Listar tablas
\dt

# Debes ver tablas como:
# - area_acopio
# - secado
# - bodega
# - catacion
# - etc.

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
2. Llena el formulario con datos de prueba
3. Guarda el lote
4. Ve a catación y selecciona el lote creado
5. Completa los datos de catación
6. Guarda

**Verificar en base de datos:**

```bash
psql -U postgres -h localhost -d coffeebeanflow_db

# Ver lotes
SELECT * FROM area_acopio;

# Ver cataciones
SELECT * FROM catacion;

\q
```

---

## 🔧 Troubleshooting

### Problema 1: "Cannot connect to PostgreSQL"

**Síntomas:**
- Error: `Connection refused` o `could not connect to server`

**Soluciones:**

```bash
# Verificar que PostgreSQL está corriendo
sudo systemctl status postgresql

# Si no está activo, iniciarlo
sudo systemctl start postgresql

# Verificar que escucha en el puerto correcto
sudo netstat -plunt | grep postgres
# Debe mostrar: tcp ... 0.0.0.0:5432 ... postgres

# Verificar autenticación
sudo nano /etc/postgresql/16/main/pg_hba.conf
# Asegúrate de tener: host all all 127.0.0.1/32 md5
sudo systemctl restart postgresql
```

### Problema 2: ".NET SDK not found"

**Síntomas:**
- Error: `dotnet: command not found`

**Soluciones:**

```bash
# Verificar instalación
which dotnet

# Si no está instalado, reinstalar
wget https://packages.microsoft.com/config/ubuntu/$(lsb_release -rs)/packages-microsoft-prod.deb
sudo dpkg -i packages-microsoft-prod.deb
sudo apt-get update
sudo apt-get install -y dotnet-sdk-9.0

# Agregar al PATH si es necesario
echo 'export PATH="$PATH:/usr/share/dotnet"' >> ~/.bashrc
source ~/.bashrc
```

### Problema 3: "Port 5253 already in use"

**Síntomas:**
- Error: `Address already in use`

**Soluciones:**

```bash
# Encontrar proceso usando el puerto
sudo lsof -i :5253

# Matar el proceso (reemplazar PID con el número que muestra)
sudo kill -9 PID

# O cambiar el puerto en el backend
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Backend
nano Properties/launchSettings.json
# Cambiar "applicationUrl" a otro puerto
```

### Problema 4: "Angular compilation errors"

**Síntomas:**
- Errores de TypeScript o módulos no encontrados

**Soluciones:**

```bash
# Limpiar node_modules y reinstalar
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend
rm -rf node_modules
rm package-lock.json
npm cache clean --force
npm install

# Si persiste, verificar versión de Node
node --version  # Debe ser v20.x.x

# Reinstalar Angular CLI
sudo npm uninstall -g @angular/cli
sudo npm install -g @angular/cli@21
```

### Problema 5: "CORS policy error"

**Síntomas:**
- Error en consola del navegador: `Access to XMLHttpRequest blocked by CORS policy`

**Soluciones:**

```bash
# Verificar configuración CORS en backend
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Backend
nano appsettings.json

# Asegúrate de tener:
"Cors": {
  "AllowedOrigins": [
    "http://localhost:4200"
  ]
}

# Reiniciar backend
# Ctrl+C en la terminal del backend y volver a ejecutar: dotnet run
```

### Problema 6: "Cannot find module '@angular/...' "

**Síntomas:**
- Error: módulos de Angular no encontrados

**Soluciones:**

```bash
cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Frontend

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

### Problema 7: "Permission denied" al ejecutar npm

**Síntomas:**
- Error: `EACCES: permission denied`

**Soluciones:**

```bash
# Cambiar propietario de carpetas npm
sudo chown -R $USER:$USER ~/.npm
sudo chown -R $USER:$USER ~/proyectos/CoffeeBeanFlow-Project-Cambio

# No usar sudo para instalar paquetes globales
# En su lugar, configurar npm para usar carpeta local
mkdir ~/.npm-global
npm config set prefix '~/.npm-global'
echo 'export PATH=~/.npm-global/bin:$PATH' >> ~/.bashrc
source ~/.bashrc
```

### Problema 8: "Database connection timeout"

**Síntomas:**
- Error: `timeout expired` al conectar a PostgreSQL

**Soluciones:**

```bash
# Verificar firewall
sudo ufw status

# Si está activo, permitir PostgreSQL
sudo ufw allow 5432/tcp

# Verificar configuración de red en PostgreSQL
sudo nano /etc/postgresql/16/main/postgresql.conf
# Buscar: listen_addresses = 'localhost'
# Descomentar si está comentado

# Reiniciar
sudo systemctl restart postgresql
```

---

## 📝 Comandos Útiles de Referencia

### PostgreSQL

```bash
# Iniciar servicio
sudo systemctl start postgresql

# Detener servicio
sudo systemctl stop postgresql

# Reiniciar servicio
sudo systemctl restart postgresql

# Ver estado
sudo systemctl status postgresql

# Conectar a base de datos
psql -U postgres -h localhost -d coffeebeanflow_db

# Hacer backup
pg_dump -U postgres coffeebeanflow_db > backup.sql

# Restaurar backup
psql -U postgres -d coffeebeanflow_db < backup.sql
```

### .NET

```bash
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

```bash
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

### Git (si usas control de versiones)

```bash
# Clonar repositorio
git clone URL

# Ver estado
git status

# Ver cambios
git diff

# Agregar archivos
git add .

# Hacer commit
git commit -m "Mensaje"

# Subir cambios
git push

# Bajar cambios
git pull

# Ver ramas
git branch

# Cambiar rama
git checkout nombre-rama
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

---

## 📚 Recursos Adicionales

### Documentación Oficial

- **.NET:** https://docs.microsoft.com/dotnet/
- **Angular:** https://angular.io/docs
- **PostgreSQL:** https://www.postgresql.org/docs/
- **Entity Framework Core:** https://docs.microsoft.com/ef/core/

### Tutoriales Útiles

- **PostgreSQL en Ubuntu:** https://www.postgresql.org/download/linux/ubuntu/
- **.NET en Linux:** https://docs.microsoft.com/dotnet/core/install/linux
- **Angular en Linux:** https://angular.io/guide/setup-local

---

## ✅ Checklist Final

Marca cada ítem cuando lo completes:

### Instalación
- [ ] Ubuntu actualizado (`sudo apt-get update && sudo apt-get upgrade`)
- [ ] .NET 9.0 SDK instalado (`dotnet --version`)
- [ ] Node.js 20.x instalado (`node --version`)
- [ ] npm instalado (`npm --version`)
- [ ] Angular CLI 21 instalado (`ng version`)
- [ ] PostgreSQL 16 instalado (`psql --version`)
- [ ] Entity Framework Tools instalado (`dotnet ef --version`)

### Configuración
- [ ] PostgreSQL iniciado (`sudo systemctl status postgresql`)
- [ ] Base de datos `coffeebeanflow_db` creada
- [ ] Usuario postgres configurado con contraseña
- [ ] Proyecto copiado/clonado
- [ ] `appsettings.json` configurado con cadena de conexión correcta
- [ ] Dependencias del backend restauradas (`dotnet restore`)
- [ ] Dependencias del frontend instaladas (`npm install`)

### Ejecución
- [ ] Migraciones aplicadas (`dotnet ef database update`)
- [ ] Backend compilado sin errores (`dotnet build`)
- [ ] Frontend compilado sin errores (`ng build`)
- [ ] Backend ejecutándose (`dotnet run`)
- [ ] Frontend ejecutándose (`ng serve`)
- [ ] Swagger accesible en navegador
- [ ] Frontend accesible en navegador
- [ ] Health check responde correctamente
- [ ] Formularios cargan sin errores

### Pruebas
- [ ] Puede crear un lote en área de acopio
- [ ] Puede ver lotes creados
- [ ] Puede editar un lote
- [ ] Puede crear una catación
- [ ] Datos se guardan en PostgreSQL

---

## 🆘 Contacto y Soporte

Si encuentras problemas que no están cubiertos en este documento:

1. **Revisar logs del backend:**
   ```bash
   cd ~/proyectos/CoffeeBeanFlow-Project-Cambio/Backend
   dotnet run --verbosity detailed
   ```

2. **Revisar logs del frontend:**
   - Abrir DevTools en el navegador (F12)
   - Ver pestaña "Console" para errores

3. **Revisar logs de PostgreSQL:**
   ```bash
   sudo tail -f /var/log/postgresql/postgresql-16-main.log
   ```

4. **Documentación del proyecto:**
   - Ver archivo `README.md` en la raíz del proyecto
   - Ver `BACKEND_DOCUMENTATION_COMPLETE.md` para detalles del backend
   - Ver `FRONTEND_DOCUMENTATION_COMPLETE.md` para detalles del frontend

---

## 🎉 ¡Listo!

Si completaste todos los pasos y el checklist, tu entorno de desarrollo está configurado correctamente.

**Próximos pasos:**
1. Familiarízate con la estructura del proyecto
2. Lee la documentación en `PLAN_FASES_IMPLEMENTACION.md`
3. Revisa los modelos de datos en `Modelo_Conceptual_Base_Datos_Completo.md`
4. Comienza a desarrollar nuevas funcionalidades

**¡Éxito con el proyecto CoffeeBeanFlow! ☕🚀**

---

**Nota:** Este documento fue creado para facilitar la configuración en Ubuntu Linux. Para Windows, consulta la documentación específica de Windows en el proyecto.
