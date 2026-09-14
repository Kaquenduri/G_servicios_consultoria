using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Repositories.Interfaces;

public interface IProyectoRepository : IGenericRepository<Proyecto>
{
    Task<bool> ExisteCodigoAsync(
        string codigo,
        long? excluirProyectoId = null);

    Task<List<Proyecto>> GetByEstadoAsync(string estado);
}