using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IProyectoEmpleadoService
{
    Task<List<ProyectoEmpleado>> GetByProyectoAsync(
        long proyectoId);

    Task<ProyectoEmpleado> AsignarAsync(
        ProyectoEmpleado asignacion);

    Task<bool> DesasignarAsync(
        long proyectoId,
        long empleadoId);
}