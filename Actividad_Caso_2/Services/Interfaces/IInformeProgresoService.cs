using Actividad_Caso_2.Models;

namespace Actividad_Caso_2.Services.Interfaces;

public interface IInformeProgresoService
{
    Task<List<InformeProgreso>> GetByProyectoAsync(
        long proyectoId);

    Task<InformeProgreso?> GetByIdAsync(long id);

    Task<InformeProgreso> CreateAsync(
        InformeProgreso informe);

    Task<bool> UpdateAsync(
        long id,
        InformeProgreso informe);

    Task<bool> DeleteAsync(long id);
}