@echo off
echo ========================================
echo Generando TODOS los controladores...
echo ========================================

echo [1/22] Generando Area_AcopioController...
dotnet aspnet-codegenerator controller -name Area_AcopioController -m Area_AcopioItem -dc Area_AcopioContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Area_AcopioController generado exitosamente) else (echo ✗ Error generando Area_AcopioController)

echo [2/22] Generando BodegaController...
dotnet aspnet-codegenerator controller -name BodegaController -m BodegaItem -dc BodegaContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ BodegaController generado exitosamente) else (echo ✗ Error generando BodegaController)

echo [3/22] Generando CatacionController...
dotnet aspnet-codegenerator controller -name CatacionController -m CatacionItem -dc CatacionContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ CatacionController generado exitosamente) else (echo ✗ Error generando CatacionController)

echo [4/22] Generando Enviar_muestrasController...
dotnet aspnet-codegenerator controller -name Enviar_muestrasController -m Enviar_muestrasItem -dc Enviar_muestrasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Enviar_muestrasController generado exitosamente) else (echo ✗ Error generando Enviar_muestrasController)

echo [5/22] Generando Formulario_CaracterizacionController...
dotnet aspnet-codegenerator controller -name Formulario_CaracterizacionController -m Formulario_CaracterizacionItem -dc Formulario_CaracterizacionContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Formulario_CaracterizacionController generado exitosamente) else (echo ✗ Error generando Formulario_CaracterizacionController)

echo [6/22] Generando Gbx_inmadurasController...
dotnet aspnet-codegenerator controller -name Gbx_inmadurasController -m Gbx_inmadurasItem -dc Gbx_inmadurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Gbx_inmadurasController generado exitosamente) else (echo ✗ Error generando Gbx_inmadurasController)

echo [7/22] Generando Gbx_madurasController...
dotnet aspnet-codegenerator controller -name Gbx_madurasController -m Gbx_madurasItem -dc Gbx_madurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Gbx_madurasController generado exitosamente) else (echo ✗ Error generando Gbx_madurasController)

echo [8/22] Generando Gbx_sobremadurasController...
dotnet aspnet-codegenerator controller -name Gbx_sobremadurasController -m Gbx_sobremadurasItem -dc Gbx_sobremadurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Gbx_sobremadurasController generado exitosamente) else (echo ✗ Error generando Gbx_sobremadurasController)

echo [9/22] Generando Guardar_CafeController...
dotnet aspnet-codegenerator controller -name Guardar_CafeController -m Guardar_CafeItem -dc Guardar_CafeContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Guardar_CafeController generado exitosamente) else (echo ✗ Error generando Guardar_CafeController)

echo [10/22] Generando HumedadController...
dotnet aspnet-codegenerator controller -name HumedadController -m HumedadItem -dc HumedadContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ HumedadController generado exitosamente) else (echo ✗ Error generando HumedadController)

echo [11/22] Generando NcamaController...
dotnet aspnet-codegenerator controller -name NcamaController -m NcamaItem -dc NcamaContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ NcamaController generado exitosamente) else (echo ✗ Error generando NcamaController)

echo [12/22] Generando PesoVerdeController...
dotnet aspnet-codegenerator controller -name PesoVerdeController -m PesoVerdeItem -dc PesoVerdeContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ PesoVerdeController generado exitosamente) else (echo ✗ Error generando PesoVerdeController)

echo [13/22] Generando RCinmadurasController...
dotnet aspnet-codegenerator controller -name RCinmadurasController -m RCinmadurasItem -dc RCinmadurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ RCinmadurasController generado exitosamente) else (echo ✗ Error generando RCinmadurasController)

echo [14/22] Generando RCmadurasController...
dotnet aspnet-codegenerator controller -name RCmadurasController -m RCmadurasItem -dc RCmadurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ RCmadurasController generado exitosamente) else (echo ✗ Error generando RCmadurasController)

echo [15/22] Generando RCsobremadurasController...
dotnet aspnet-codegenerator controller -name RCsobremadurasController -m RCsobremadurasItem -dc RCsobremadurasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ RCsobremadurasController generado exitosamente) else (echo ✗ Error generando RCsobremadurasController)

echo [16/22] Generando Registro_FormularioController...
dotnet aspnet-codegenerator controller -name Registro_FormularioController -m Registro_FormularioItem -dc Registro_FormularioContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ Registro_FormularioController generado exitosamente) else (echo ✗ Error generando Registro_FormularioController)

echo [17/22] Generando RondasController...
dotnet aspnet-codegenerator controller -name RondasController -m RondasItem -dc RondasContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ RondasController generado exitosamente) else (echo ✗ Error generando RondasController)

echo [18/22] Generando SecadoController...
dotnet aspnet-codegenerator controller -name SecadoController -m SecadoItem -dc SecadoContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ SecadoController generado exitosamente) else (echo ✗ Error generando SecadoController)

echo [19/22] Generando SuministraController...
dotnet aspnet-codegenerator controller -name SuministraController -m SuministraItem -dc SuministraContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ SuministraController generado exitosamente) else (echo ✗ Error generando SuministraController)

echo [20/22] Generando TemperaturaSecadoController...
dotnet aspnet-codegenerator controller -name TemperaturaSecadoController -m TemperaturaSecadoItem -dc TemperaturaSecadoContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ TemperaturaSecadoController generado exitosamente) else (echo ✗ Error generando TemperaturaSecadoController)

echo [21/22] Generando TermoHigrometriaController...
dotnet aspnet-codegenerator controller -name TermoHigrometriaController -m TermoHigrometriaItem -dc TermoHigrometriaContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ TermoHigrometriaController generado exitosamente) else (echo ✗ Error generando TermoHigrometriaController)

echo [22/22] Generando TrillaController...
dotnet aspnet-codegenerator controller -name TrillaController -m TrillaItem -dc TrillaContext --relativeFolderPath Controllers --useDefaultLayout --referenceScriptLibraries
if %errorlevel% equ 0 (echo ✓ TrillaController generado exitosamente) else (echo ✗ Error generando TrillaController)

echo ========================================
echo PROCESO COMPLETADO!
echo Se han generado 22 controladores con sus vistas correspondientes.
echo ========================================
pause