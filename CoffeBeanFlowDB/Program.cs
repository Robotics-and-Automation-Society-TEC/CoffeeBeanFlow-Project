using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;
using System.Text.Json.Serialization;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers()
    .AddJsonOptions(options =>
    {
        // Configurar JSON para manejar mejor las propiedades
        options.JsonSerializerOptions.PropertyNamingPolicy = null; // Mantener nombres originales
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
        options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
    });

// Agregar configuración de API Explorer y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// *** CONFIGURAR CORS PARA VUE.JS ***
builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowVueApp", policy =>
    {
        policy.WithOrigins(
                "http://localhost:8080",    // Vue CLI default
                "http://localhost:8081",    // Vue CLI default
                "http://localhost:3000",    // Vite default
                "http://localhost:5173",    // Vite alternate
                "http://127.0.0.1:8080",
                "http://127.0.0.1:3000"
            )
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials(); // Permitir cookies si las necesitas
    });
});

// Configurar Entity Framework con PostgreSQL
var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") 
    ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.");

// Registrar todos los contextos de la base de datos con Npgsql
builder.Services.AddDbContext<Area_AcopioContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<BodegaContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<CatacionContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Enviar_muestrasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Formulario_CaracterizacionContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Gbx_inmadurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Gbx_madurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Gbx_sobremadurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Guardar_CafeContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<HumedadContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<NcamaContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<PesoVerdeContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<RCinmadurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<RCmadurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<RCsobremadurasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<Registro_FormularioContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<RondasContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<SecadoContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<SuministraContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<TemperaturaSecadoContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<TermoHigrometriaContext>(options =>
    options.UseNpgsql(connectionString));

builder.Services.AddDbContext<TrillaContext>(options =>
    options.UseNpgsql(connectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    // Habilitar Swagger solo en desarrollo
    app.UseSwagger();
    app.UseSwaggerUI();
    
    // En desarrollo, mostrar errores detallados
    app.UseDeveloperExceptionPage();
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

// *** IMPORTANTE: USAR CORS ANTES DE AUTHORIZATION ***
app.UseCors("AllowVueApp");

app.UseAuthorization();

// Cambiar el enrutamiento para API
app.MapControllers();

// *** OPCIONAL: Endpoint de prueba para verificar conectividad ***
app.MapGet("/api/health", () => new { 
    status = "OK", 
    timestamp = DateTime.UtcNow,
    message = "API is running" 
});

app.Run();