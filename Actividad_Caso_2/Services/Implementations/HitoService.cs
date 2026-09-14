using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Services.Implementations;

public class HitoService : IHitoService
{
    private readonly IUnitOfWork _unitOfWork;

    private static readonly string[] Estados =
    {
        "PENDIENTE",
        "EN_PROGRESO",
        "ALCANZADO",
        "RETRASADO",
        "CANCELADO"
    };

    public HitoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Hito>> GetByProyectoAsync(
        long proyectoId)
    {
        return await _unitOfWork.Hitos
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<Hito?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Hitos.GetByIdAsync(id);
    }

    public async Task<Hito> CreateAsync(Hito hito)
    {
        await ValidarAsync(hito);

        await _unitOfWork.Hitos.AddAsync(hito);

        await _unitOfWork.CompleteAsync();

        return hito;
    }

    public async Task<bool> UpdateAsync(
        long id,
        Hito hito)
    {
        var actual =
            await _unitOfWork.Hitos.GetByIdAsync(id);

        if (actual == null)
            return false;

        await ValidarAsync(hito);

        actual.ProyectoId = hito.ProyectoId;
        actual.Nombre = hito.Nombre;
        actual.Descripcion = hito.Descripcion;
        actual.FechaPlanificada =
            hito.FechaPlanificada;
        actual.FechaAlcanzada = hito.FechaAlcanzada;
        actual.Estado = hito.Estado;

        _unitOfWork.Hitos.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var hito =
            await _unitOfWork.Hitos.GetByIdAsync(id);

        if (hito == null)
            return false;

        _unitOfWork.Hitos.Remove(hito);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private async Task ValidarAsync(Hito hito)
    {
        if (string.IsNullOrWhiteSpace(hito.Nombre))
            throw new ArgumentException(
                "El nombre del hito es obligatorio.");

        if (!Estados.Contains(hito.Estado))
            throw new ArgumentException(
                "El estado del hito no es válido.");

        if (await _unitOfWork.Proyectos
                .GetByIdAsync(hito.ProyectoId) == null)
        {
            throw new KeyNotFoundException(
                "El proyecto no existe.");
        }
    }
}
