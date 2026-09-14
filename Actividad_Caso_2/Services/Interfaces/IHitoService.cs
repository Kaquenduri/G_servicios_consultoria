using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IHitoService
{
    Task<List<Hito>> GetByProyectoAsync(
        long proyectoId);

    Task<Hito?> GetByIdAsync(long id);

    Task<Hito> CreateAsync(Hito hito);

    Task<bool> UpdateAsync(
        long id,
        Hito hito);

    Task<bool> DeleteAsync(long id);
}