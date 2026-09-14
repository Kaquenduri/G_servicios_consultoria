using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IEmpleadoService
{
    Task<List<Empleado>> GetAllAsync();
    Task<Empleado?> GetByIdAsync(long id);
    Task<Empleado> CreateAsync(Empleado empleado);
    Task<bool> UpdateAsync(long id, Empleado empleado);
    Task<bool> DeleteAsync(long id);
}