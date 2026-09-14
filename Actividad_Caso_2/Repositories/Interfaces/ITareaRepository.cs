using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface ITareaRepository : IGenericRepository<Tarea>
{
    Task<List<Tarea>> GetByProyectoAsync(long proyectoId);

    Task<List<Tarea>> GetByEmpleadoAsync(long empleadoId);
}