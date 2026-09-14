using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.UnitOfWork;

namespace Actividad_Caso_2.Services.Implementations;

public class GastoProyectoService
    : IGastoProyectoService
{
    private readonly IUnitOfWork _unitOfWork;

    public GastoProyectoService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<GastoProyecto>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _unitOfWork.GastosProyecto
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<decimal> GetTotalProyectoAsync(
        long proyectoId)
    {
        return await _unitOfWork.GastosProyecto
            .GetTotalProyectoAsync(proyectoId);
    }

    public async Task<GastoProyecto> CreateAsync(
        GastoProyecto gasto)
    {
        if (gasto.Monto <= 0)
            throw new ArgumentException(
                "El monto debe ser mayor que cero.");

        if (string.IsNullOrWhiteSpace(gasto.Categoria))
            throw new ArgumentException(
                "La categoría es obligatoria.");

        if (string.IsNullOrWhiteSpace(gasto.Concepto))
            throw new ArgumentException(
                "El concepto es obligatorio.");

        if (await _unitOfWork.Proyectos
                .GetByIdAsync(gasto.ProyectoId) == null)
        {
            throw new KeyNotFoundException(
                "El proyecto no existe.");
        }

        if (await _unitOfWork.Empleados
                .GetByIdAsync(
                    gasto.RegistradoPorEmpleadoId) == null)
        {
            throw new KeyNotFoundException(
                "El empleado no existe.");
        }

        await _unitOfWork.GastosProyecto.AddAsync(gasto);

        await _unitOfWork.CompleteAsync();

        return gasto;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var gasto =
            await _unitOfWork.GastosProyecto
                .GetByIdAsync(id);

        if (gasto == null)
            return false;

        _unitOfWork.GastosProyecto.Remove(gasto);

        await _unitOfWork.CompleteAsync();

        return true;
    }
}