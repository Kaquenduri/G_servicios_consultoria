using Actividad_Caso_2.Data;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Repositories.Implementations;

public class UnitOfWork : IUnitOfWork
{
    private readonly GestionProyectosContext _context;

    public IClienteRepository Clientes { get; }
    public IEmpleadoRepository Empleados { get; }
    public IProyectoRepository Proyectos { get; }
    public IProyectoEmpleadoRepository ProyectoEmpleados { get; }
    public ITareaRepository Tareas { get; }
    public IGastoProyectoRepository GastosProyecto { get; }
    public IInteraccionClienteRepository InteraccionesCliente { get; }
    public IHitoRepository Hitos { get; }
    public IInformeProgresoRepository InformesProgreso { get; }

    public UnitOfWork(
        GestionProyectosContext context,
        IClienteRepository clientes,
        IEmpleadoRepository empleados,
        IProyectoRepository proyectos,
        IProyectoEmpleadoRepository proyectoEmpleados,
        ITareaRepository tareas,
        IGastoProyectoRepository gastosProyecto,
        IInteraccionClienteRepository interaccionesCliente,
        IHitoRepository hitos,
        IInformeProgresoRepository informesProgreso)
    {
        _context = context;

        Clientes = clientes;
        Empleados = empleados;
        Proyectos = proyectos;
        ProyectoEmpleados = proyectoEmpleados;
        Tareas = tareas;
        GastosProyecto = gastosProyecto;
        InteraccionesCliente = interaccionesCliente;
        Hitos = hitos;
        InformesProgreso = informesProgreso;
    }

    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}