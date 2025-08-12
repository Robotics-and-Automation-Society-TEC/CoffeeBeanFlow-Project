using Microsoft.EntityFrameworkCore;
using CoffeBeanFlowDB.Contexts;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Cambiar de AddControllersWithViews() a AddControllers() para API
builder.Services.AddControllers();

// Agregar configuración de API Explorer y Swagger
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

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
}

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

// Cambiar el enrutamiento para API
app.MapControllers();

app.Run();