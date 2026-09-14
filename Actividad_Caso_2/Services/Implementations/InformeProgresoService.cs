using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Services.Implementations;

public class InformeProgresoService
    : IInformeProgresoService
{
    private readonly IUnitOfWork _unitOfWork;

    public InformeProgresoService(
        IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<InformeProgreso>>
        GetByProyectoAsync(long proyectoId)
    {
        return await _unitOfWork.InformesProgreso
            .GetByProyectoAsync(proyectoId);
    }

    public async Task<InformeProgreso?> GetByIdAsync(
        long id)
    {
        return await _unitOfWork.InformesProgreso
            .GetByIdAsync(id);
    }

    public async Task<InformeProgreso> CreateAsync(
        InformeProgreso informe)
    {
        await ValidarAsync(informe);

        await _unitOfWork.InformesProgreso
            .AddAsync(informe);

        await _unitOfWork.CompleteAsync();

        return informe;
    }

    public async Task<bool> UpdateAsync(
        long id,
        InformeProgreso informe)
    {
        var actual =
            await _unitOfWork.InformesProgreso
                .GetByIdAsync(id);

        if (actual == null)
            return false;

        await ValidarAsync(informe);

        actual.ProyectoId = informe.ProyectoId;
        actual.ElaboradoPorEmpleadoId =
            informe.ElaboradoPorEmpleadoId;

        actual.PeriodoDesde = informe.PeriodoDesde;
        actual.PeriodoHasta = informe.PeriodoHasta;

        actual.PorcentajeAvance =
            informe.PorcentajeAvance;

        actual.Resumen = informe.Resumen;
        actual.Riesgos = informe.Riesgos;
        actual.ProximosPasos = informe.ProximosPasos;

        _unitOfWork.InformesProgreso.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var informe =
            await _unitOfWork.InformesProgreso
                .GetByIdAsync(id);

        if (informe == null)
            return false;

        _unitOfWork.InformesProgreso.Remove(informe);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private async Task ValidarAsync(
        InformeProgreso informe)
    {
        if (informe.PeriodoHasta <
            informe.PeriodoDesde)
        {
            throw new ArgumentException(
                "El periodo final no puede ser anterior al inicial.");
        }

        if (informe.PorcentajeAvance < 0 ||
            informe.PorcentajeAvance > 100)
        {
            throw new ArgumentException(
                "El porcentaje debe estar entre 0 y 100.");
        }

        if (string.IsNullOrWhiteSpace(informe.Resumen))
            throw new ArgumentException(
                "El resumen es obligatorio.");

        if (await _unitOfWork.Proyectos
                .GetByIdAsync(informe.ProyectoId) == null)
        {
            throw new KeyNotFoundException(
                "El proyecto no existe.");
        }

        if (await _unitOfWork.Empleados
                .GetByIdAsync(
                    informe.ElaboradoPorEmpleadoId) == null)
        {
            throw new KeyNotFoundException(
                "El empleado no existe.");
        }
    }
}
