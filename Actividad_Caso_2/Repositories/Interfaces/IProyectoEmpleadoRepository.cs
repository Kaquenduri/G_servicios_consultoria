using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IProyectoEmpleadoRepository
    : IGenericRepository<ProyectoEmpleado>
{
    Task<bool> ExisteAsignacionAsync(
        long proyectoId,
        long empleadoId);

    Task<ProyectoEmpleado?> GetAsync(
        long proyectoId,
        long empleadoId);

    Task<List<ProyectoEmpleado>> GetByProyectoAsync(
        long proyectoId);
}