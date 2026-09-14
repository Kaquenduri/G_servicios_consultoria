using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IInformeProgresoRepository
    : IGenericRepository<InformeProgreso>
{
    Task<List<InformeProgreso>> GetByProyectoAsync(
        long proyectoId);
}