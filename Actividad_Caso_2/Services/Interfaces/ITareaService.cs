using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface ITareaService
{
    Task<List<Tarea>> GetAllAsync();

    Task<Tarea?> GetByIdAsync(long id);

    Task<List<Tarea>> GetByProyectoAsync(
        long proyectoId);

    Task<List<Tarea>> GetByEmpleadoAsync(
        long empleadoId);

    Task<Tarea> CreateAsync(Tarea tarea);

    Task<bool> UpdateAsync(long id, Tarea tarea);

    Task<bool> DeleteAsync(long id);
}