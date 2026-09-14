using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Actividad_Caso_2.Data;
using Actividad_Caso_2.Repositories.Interfaces;
using Actividad_Caso_2.Repositories.Implementations;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Services.Implementations;
using Npgsql;

var builder = WebApplication.CreateBuilder(args);

// Load .env file variables
DotNetEnv.Env.Load();

builder.Services.AddOpenApi();
builder.Services
    .AddControllers()
    .AddJsonOptions(options =>
    {
        options.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles;
    });

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection")
    ?? throw new InvalidOperationException(
        "No se encontro la cadena de conexion 'DefaultConnection'.");

var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");

var npgsqlBuilder = new NpgsqlConnectionStringBuilder(connectionString);
if (!string.IsNullOrWhiteSpace(dbPassword))
{
    npgsqlBuilder.Password = dbPassword;
}

builder.Services.AddDbContext<GestionProyectosContext>(options =>
    options.UseNpgsql(npgsqlBuilder.ConnectionString));

// Register Repositories
builder.Services.AddScoped<IClienteRepository, ClienteRepository>();
builder.Services.AddScoped<IEmpleadoRepository, EmpleadoRepository>();
builder.Services.AddScoped<IProyectoRepository, ProyectoRepository>();
builder.Services.AddScoped<IProyectoEmpleadoRepository, ProyectoEmpleadoRepository>();
builder.Services.AddScoped<ITareaRepository, TareaRepository>();
builder.Services.AddScoped<IGastoProyectoRepository, GastoProyectoRepository>();
builder.Services.AddScoped<IInteraccionClienteRepository, InteraccionClienteRepository>();
builder.Services.AddScoped<IHitoRepository, HitoRepository>();
builder.Services.AddScoped<IInformeProgresoRepository, InformeProgresoRepository>();

// Register UnitOfWork
builder.Services.AddScoped<IUnitOfWork, UnitOfWork>();

// Register Services
builder.Services.AddScoped<IClienteService, ClienteService>();
builder.Services.AddScoped<IEmpleadoService, EmpleadoService>();
builder.Services.AddScoped<IProyectoService, ProyectoService>();
builder.Services.AddScoped<IProyectoEmpleadoService, ProyectoEmpleadoService>();
builder.Services.AddScoped<ITareaService, TareaService>();
builder.Services.AddScoped<IGastoProyectoService, GastoProyectoService>();
builder.Services.AddScoped<IInteraccionClienteService, InteraccionClienteService>();
builder.Services.AddScoped<IHitoService, HitoService>();
builder.Services.AddScoped<IInformeProgresoService, InformeProgresoService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Home/Error");
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
