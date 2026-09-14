using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IProyectoService
{
    Task<List<Proyecto>> GetAllAsync();
    Task<Proyecto?> GetByIdAsync(long id);
    Task<List<Proyecto>> GetByEstadoAsync(string estado);

    Task<Proyecto> CreateAsync(Proyecto proyecto);

    Task<bool> UpdateAsync(long id, Proyecto proyecto);

    Task<bool> CerrarAsync(long id);

    Task<bool> DeleteAsync(long id);
}