using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.UnitOfWork;

public interface IUnitOfWork : IDisposable
{
    IClienteRepository Clientes { get; }

    IEmpleadoRepository Empleados { get; }

    IProyectoRepository Proyectos { get; }

    IProyectoEmpleadoRepository ProyectoEmpleados { get; }

    ITareaRepository Tareas { get; }

    IGastoProyectoRepository GastosProyecto { get; }

    IInteraccionClienteRepository InteraccionesCliente { get; }

    IHitoRepository Hitos { get; }

    IInformeProgresoRepository InformesProgreso { get; }

    Task<int> CompleteAsync();
}