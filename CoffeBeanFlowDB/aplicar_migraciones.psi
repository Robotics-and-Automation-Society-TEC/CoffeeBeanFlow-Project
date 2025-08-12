# Lista de tus DbContext
$contexts = @(
    "Area_AcopioContext",
    "BodegaContext",
    "CatacionContext",
    "Enviar_muestrasContext",
    "Formulario_CaracterizacionContext",
    "Gbx_inmadurasContext",
    "Gbx_madurasContext",
    "Gbx_sobremadurasContext",
    "Guardar_CafeContext",
    "HumedadContext",
    "NcamaContext",
    "PesoVerdeContext",
    "RCinmadurasContext",
    "RCmadurasContext",
    "RCsobremadurasContext",
    "Registro_FormularioContext",
    "RondasContext",
    "SecadoContext",
    "SuministraContext",
    "TemperaturaSecadoContext",
    "TermoHigrometriaContext",
    "TrillaContext"
)

# Aplicar cada migración a la base de datos
foreach ($context in $contexts) {
    Write-Host "`n🚀 Aplicando migración para $context..."
    dotnet ef database update --context $context
}
