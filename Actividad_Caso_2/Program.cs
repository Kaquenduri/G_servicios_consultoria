using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using Actividad_Caso_2.Data;
using Actividad_Caso_2.Repositories.Interfaces;
using Actividad_Caso_2.Repositories.Implementations;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Services.Implementations;
using Npgsql;
using Actividad_Caso_2.Infrastructure;

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
builder.Services.AddExceptionHandler<ServiceExceptionHandler>();

builder.Services.AddDbContext<GestionProyectosContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection")));

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
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "api v1");
        c.RoutePrefix = string.Empty;
    });
}

app.UseExceptionHandler();
app.UseHttpsRedirection();
app.UseRouting();
app.MapControllers();
app.Run();
