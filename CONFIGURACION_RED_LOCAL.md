# 🌐 Configuración para Acceso desde Dispositivos en Red Local

Esta guía te permite acceder a **CoffeeBeanFlow** desde cualquier dispositivo (celular, tablet, otra computadora) conectado a la misma red WiFi.

---

## 📋 Requisitos Previos

- Backend y Frontend funcionando en `localhost`
- Todos los dispositivos conectados a la **misma red WiFi**
- Permisos de administrador (para configurar firewall)

---

## 🔧 Configuración Inicial (Una Sola Vez)

### **Paso 1: Obtener la IP de tu Computadora**

#### **Linux/Ubuntu:**
```bash
hostname -I | awk '{print $1}'
```

**O con más detalle:**
```bash
ip addr show | grep "inet " | grep -v 127.0.0.1
```

#### **Windows:**
```powershell
ipconfig | findstr IPv4
```

**Ejemplo de IP:** `192.168.0.47` (anota tu IP, la necesitarás)

---

### **Paso 2: Configurar CORS en el Backend**

Edita el archivo: `Backend/Program.cs`

**Busca la línea (aproximadamente línea 22-26):**
```csharp
var allowedOrigins = new[] { 
    "http://localhost:4200", 
    "http://localhost:4201" 
};
```

**Cámbiala por (reemplaza `192.168.0.47` con tu IP):**
```csharp
var allowedOrigins = new[] { 
    "http://localhost:4200",
    "http://192.168.0.47:4200",  // ← Tu IP aquí
    "http://localhost:4201" 
};
```

---

### **Paso 3: Configurar Backend para Escuchar en Todas las Interfaces**

Edita el archivo: `Backend/Properties/launchSettings.json`

**Busca (línea 8 aproximadamente):**
```json
"applicationUrl": "http://localhost:5253",
```

**Cámbialo por:**
```json
"applicationUrl": "http://0.0.0.0:5253",
```

---

### **Paso 4: Configurar la URL de la API en el Frontend**

Edita el archivo: `Frontend/src/app/core/services/api.service.ts`

**Busca la línea (aproximadamente línea 11):**
```typescript
private baseUrl = 'http://localhost:5253/api';
```

**Cámbiala por (reemplaza con tu IP):**
```typescript
private baseUrl = 'http://192.168.0.47:5253/api';
```

---

### **Paso 5: Abrir Puertos en el Firewall**

#### **Linux/Ubuntu:**
```bash
sudo ufw allow 5253/tcp
sudo ufw allow 4200/tcp
sudo ufw reload
sudo ufw status
```

#### **Windows (PowerShell como Administrador):**
```powershell
# Backend (puerto 5253)
netsh advfirewall firewall add rule name="CoffeeBeanFlow Backend" dir=in action=allow protocol=TCP localport=5253

# Frontend (puerto 4200)
netsh advfirewall firewall add rule name="CoffeeBeanFlow Frontend" dir=in action=allow protocol=TCP localport=4200

# Verificar
netsh advfirewall firewall show rule name="CoffeeBeanFlow Backend"
netsh advfirewall firewall show rule name="CoffeeBeanFlow Frontend"
```

---

## 🚀 Ejecutar la Aplicación

### **Terminal 1 - Backend:**

#### **Linux:**
```bash
cd ~/Documentos/Repo-Cafe/CoffeeBeanFlow-Project/Backend
dotnet run
```

#### **Windows:**
```powershell
cd C:\Ruta\A\Tu\Proyecto\Backend
dotnet run
```

**Debes ver:**
```
Now listening on: http://0.0.0.0:5253
```

---

### **Terminal 2 - Frontend:**

#### **Linux:**
```bash
cd ~/Documentos/Repo-Cafe/CoffeeBeanFlow-Project/Frontend
ng serve --host 0.0.0.0
```

#### **Windows:**
```powershell
cd C:\Ruta\A\Tu\Proyecto\Frontend
ng serve --host 0.0.0.0
```

**Debes ver:**
```
Application bundle generation complete.
** Angular Live Development Server is listening on 0.0.0.0:4200 **
```

---

## 📱 Acceder desde Otros Dispositivos

Una vez que ambos servidores estén corriendo:

### **Desde cualquier dispositivo en la misma WiFi:**

- **Frontend (Aplicación):** `http://192.168.0.47:4200`
- **Backend (API/Swagger):** `http://192.168.0.47:5253/swagger`
- **Health Check:** `http://192.168.0.47:5253/api/health`

*Reemplaza `192.168.0.47` con tu IP real.*

---

## 🔄 Si Cambias de Red o Computadora

### **1. Obtén la nueva IP:**

**Linux:**
```bash
hostname -I | awk '{print $1}'
```

**Windows:**
```powershell
ipconfig | findstr IPv4
```

### **2. Actualiza 2 archivos:**

**A. `Backend/Program.cs` (línea 22-26):**
```csharp
var allowedOrigins = new[] { 
    "http://localhost:4200",
    "http://NUEVA_IP:4200",  // ← Actualizar aquí
    "http://localhost:4201" 
};
```

**B. `Frontend/src/app/core/services/api.service.ts` (línea 11):**
```typescript
private baseUrl = 'http://NUEVA_IP:5253/api';  // ← Actualizar aquí
```

### **3. Reinicia backend y frontend**

---

## 🛠️ Solución de Problemas

### **Error: "Port already in use"**

El puerto está ocupado por un proceso anterior.

#### **Linux:**
```bash
# Detener procesos
pkill -f "dotnet run"
pkill -f "ng serve"

# Esperar
sleep 2

# Verificar que estén libres
ss -tuln | grep 5253
ss -tuln | grep 4200
```

#### **Windows:**
```powershell
# Ver qué está usando el puerto
netstat -ano | findstr :5253
netstat -ano | findstr :4200

# Matar el proceso (reemplaza PID con el número que aparece)
taskkill /PID <PID> /F
```

---

### **Error: "Http failure response for http://localhost:5253"**

El frontend está usando `localhost` en lugar de tu IP.

**Solución:** Verifica que `Frontend/src/app/core/services/api.service.ts` tenga tu IP:
```typescript
private baseUrl = 'http://TU_IP:5253/api';
```

Luego **reinicia** el frontend:
```bash
# Detener (Ctrl+C)
# Volver a ejecutar
ng serve --host 0.0.0.0
```

---

### **Error: "Invalid Host header" (Angular)**

#### **Solución 1:**
```bash
ng serve --host 0.0.0.0 --allowed-hosts all
```

#### **Solución 2 (permanente):**
Edita `Frontend/angular.json`, en `"serve"`:
```json
"serve": {
  "builder": "@angular-devkit/build-angular:dev-server",
  "options": {
    "host": "0.0.0.0",
    "allowedHosts": ["all"]
  }
}
```

---

### **No puedo acceder desde el celular**

**Checklist:**

1. ✅ **¿Están en la misma WiFi?**
   - Verifica en ajustes del celular y computadora

2. ✅ **¿El firewall está abierto?**
   ```bash
   # Linux
   sudo ufw status
   
   # Windows
   netsh advfirewall show allprofiles state
   ```

3. ✅ **¿Los servidores están corriendo en `0.0.0.0`?**
   - Backend debe mostrar: `Now listening on: http://0.0.0.0:5253`
   - Frontend debe mostrar: `listening on 0.0.0.0:4200`

4. ✅ **¿Puedes acceder desde la misma computadora?**
   ```bash
   # Probar desde la misma máquina
   curl http://TU_IP:5253/api/health
   ```

5. ✅ **¿La IP es correcta?**
   - Vuelve a verificar tu IP actual

---

## 📝 Script de Inicio Rápido

### **Linux (Bash):**

Crea el archivo: `iniciar-red-local.sh`

```bash
#!/bin/bash

# Obtener IP
IP=$(hostname -I | awk '{print $1}')

echo "========================================"
echo "🚀 CoffeeBeanFlow - Acceso Red Local"
echo "========================================"
echo "📍 IP: $IP"
echo ""

# Limpiar procesos anteriores
pkill -f "dotnet run" 2>/dev/null
pkill -f "ng serve" 2>/dev/null
sleep 2

# Verificar puertos libres
if ss -tuln | grep -q 5253; then
    echo "⚠️  Puerto 5253 ocupado, limpiando..."
    lsof -ti:5253 | xargs kill -9 2>/dev/null
    sleep 2
fi

if ss -tuln | grep -q 4200; then
    echo "⚠️  Puerto 4200 ocupado, limpiando..."
    lsof -ti:4200 | xargs kill -9 2>/dev/null
    sleep 2
fi

# Iniciar Backend
echo "🔧 Iniciando Backend..."
cd "$(dirname "$0")/Backend"
nohup dotnet run > /tmp/coffeebeanflow-backend.log 2>&1 &
BACKEND_PID=$!
sleep 5

# Verificar Backend
if ss -tuln | grep -q 5253; then
    echo "✅ Backend corriendo (PID: $BACKEND_PID)"
else
    echo "❌ Error al iniciar Backend"
    exit 1
fi

# Iniciar Frontend
echo "🎨 Iniciando Frontend..."
cd "$(dirname "$0")/Frontend"
nohup ng serve --host 0.0.0.0 > /tmp/coffeebeanflow-frontend.log 2>&1 &
FRONTEND_PID=$!
sleep 10

# Verificar Frontend
if ss -tuln | grep -q 4200; then
    echo "✅ Frontend corriendo (PID: $FRONTEND_PID)"
else
    echo "❌ Error al iniciar Frontend"
    exit 1
fi

echo ""
echo "========================================"
echo "✅ ¡Listo! Accede desde cualquier dispositivo:"
echo ""
echo "  📱 Aplicación: http://$IP:4200"
echo "  🔌 API:        http://$IP:5253/swagger"
echo "  💚 Health:     http://$IP:5253/api/health"
echo ""
echo "📋 Logs:"
echo "  Backend:  tail -f /tmp/coffeebeanflow-backend.log"
echo "  Frontend: tail -f /tmp/coffeebeanflow-frontend.log"
echo ""
echo "🛑 Para detener:"
echo "  pkill -f 'dotnet run'"
echo "  pkill -f 'ng serve'"
echo "========================================"
```

**Ejecutar:**
```bash
chmod +x iniciar-red-local.sh
./iniciar-red-local.sh
```

---

### **Windows (PowerShell):**

Crea el archivo: `iniciar-red-local.ps1`

```powershell
# Obtener IP local
$IP = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias "Wi-Fi*" -ErrorAction SilentlyContinue | Select-Object -First 1).IPAddress

if (-not $IP) {
    $IP = (Get-NetIPAddress -AddressFamily IPv4 -InterfaceAlias "Ethernet*" | Select-Object -First 1).IPAddress
}

Write-Host "========================================" -ForegroundColor Green
Write-Host "🚀 CoffeeBeanFlow - Acceso Red Local" -ForegroundColor Green
Write-Host "========================================" -ForegroundColor Green
Write-Host "📍 IP: $IP" -ForegroundColor Yellow
Write-Host ""

# Limpiar procesos anteriores
Write-Host "🧹 Limpiando procesos anteriores..." -ForegroundColor Cyan
Get-Process | Where-Object {$_.ProcessName -like "*dotnet*" -or $_.ProcessName -like "*node*"} | Where-Object {$_.CommandLine -like "*dotnet run*" -or $_.CommandLine -like "*ng serve*"} | Stop-Process -Force -ErrorAction SilentlyContinue
Start-Sleep -Seconds 2

# Iniciar Backend
Write-Host "🔧 Iniciando Backend..." -ForegroundColor Cyan
$BackendPath = Join-Path $PSScriptRoot "Backend"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$BackendPath'; dotnet run" -WindowStyle Normal

Start-Sleep -Seconds 7

# Iniciar Frontend
Write-Host "🎨 Iniciando Frontend..." -ForegroundColor Cyan
$FrontendPath = Join-Path $PSScriptRoot "Frontend"
Start-Process powershell -ArgumentList "-NoExit", "-Command", "cd '$FrontendPath'; ng serve --host 0.0.0.0" -WindowStyle Normal

Start-Sleep -Seconds 12

Write-Host ""
Write-Host "========================================" -ForegroundColor Green
Write-Host "✅ ¡Listo! Accede desde cualquier dispositivo:" -ForegroundColor Green
Write-Host ""
Write-Host "  📱 Aplicación: http://${IP}:4200" -ForegroundColor Cyan
Write-Host "  🔌 API:        http://${IP}:5253/swagger" -ForegroundColor Cyan
Write-Host "  💚 Health:     http://${IP}:5253/api/health" -ForegroundColor Cyan
Write-Host ""
Write-Host "========================================" -ForegroundColor Green
```

**Ejecutar:**
```powershell
.\iniciar-red-local.ps1
```

---

## 📊 Verificación de Estado

### **Comandos Útiles:**

#### **Linux:**
```bash
# Ver servicios corriendo
ps aux | grep -E "(dotnet run|ng serve)" | grep -v grep

# Ver puertos
ss -tuln | grep -E "(5253|4200)"

# Ver logs
tail -f /tmp/coffeebeanflow-backend.log
tail -f /tmp/coffeebeanflow-frontend.log

# Probar conectividad
curl http://$(hostname -I | awk '{print $1}'):5253/api/health
```

#### **Windows:**
```powershell
# Ver servicios corriendo
Get-Process | Where-Object {$_.ProcessName -like "*dotnet*" -or $_.ProcessName -like "*node*"}

# Ver puertos
netstat -ano | findstr ":5253 :4200"

# Probar conectividad
Invoke-WebRequest -Uri "http://${IP}:5253/api/health"
```

---

## 🔒 Notas de Seguridad

⚠️ **IMPORTANTE:** Esta configuración es **SOLO para desarrollo local**.

**NO uses en producción:**
- `--host 0.0.0.0` expone el servidor a toda la red
- `AllowCredentials()` con múltiples orígenes puede ser inseguro
- No hay autenticación ni cifrado (HTTP, no HTTPS)

**Para producción, considera:**
- Usar HTTPS con certificados SSL
- Configurar autenticación y autorización
- Implementar rate limiting
- Usar un proxy reverso (nginx, Apache)
- Configurar CORS de forma más restrictiva

---

## 📚 Recursos Adicionales

- **Documentación Backend:** `BACKEND_DOCUMENTATION_COMPLETE.md`
- **Documentación Frontend:** `FRONTEND_DOCUMENTATION_COMPLETE.md`
- **Setup Windows:** `SETUP_WINDOWS.md`
- **Setup Ubuntu/Linux:** `SETUP_UBUNTU_LINUX.md`

---

## 💡 Preguntas Frecuentes

### **¿Puedo acceder desde Internet (fuera de mi WiFi)?**

No directamente. Necesitarías:
1. Configurar **Port Forwarding** en tu router
2. Conocer tu **IP pública**
3. Configurar **DNS dinámico** (si tu IP pública cambia)
4. **Implementar HTTPS** y seguridad adecuada

**No recomendado** para desarrollo sin medidas de seguridad apropiadas.

### **¿Por qué usar `0.0.0.0` en lugar de mi IP?**

`0.0.0.0` le dice al servidor que escuche en **todas las interfaces de red** de tu computadora:
- `127.0.0.1` (localhost)
- Tu IP local (ej: `192.168.0.47`)
- Cualquier otra IP que tenga tu computadora

Esto permite que otros dispositivos se conecten.

### **¿La configuración persiste después de reiniciar?**

Los cambios en los archivos (`Program.cs`, `api.service.ts`, etc.) **SÍ persisten**.

Pero debes **volver a ejecutar** los comandos para iniciar backend y frontend cada vez que:
- Reinicias la computadora
- Cierras las terminales
- Los procesos se detienen

---

## 📞 Soporte

Si encuentras problemas:

1. Verifica los logs: `/tmp/coffeebeanflow-backend.log` y `/tmp/coffeebeanflow-frontend.log` (Linux)
2. Asegúrate de que la IP sea correcta y no haya cambiado
3. Verifica que el firewall permita las conexiones
4. Comprueba que ambos dispositivos estén en la misma red WiFi

---

**Última actualización:** 19 de diciembre de 2025  
**Versión:** 1.0
