using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IHitoRepository : IGenericRepository<Hito>
{
    Task<List<Hito>> GetByProyectoAsync(long proyectoId);
}