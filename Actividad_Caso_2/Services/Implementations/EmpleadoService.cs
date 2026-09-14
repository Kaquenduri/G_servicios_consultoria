using Actividad_Caso_2.Models;
using Actividad_Caso_2.Services.Interfaces;
using Actividad_Caso_2.Repositories.Interfaces;

namespace Actividad_Caso_2.Services.Implementations;

public class EmpleadoService : IEmpleadoService
{
    private readonly IUnitOfWork _unitOfWork;

    public EmpleadoService(IUnitOfWork unitOfWork)
    {
        _unitOfWork = unitOfWork;
    }

    public async Task<List<Empleado>> GetAllAsync()
    {
        return await _unitOfWork.Empleados.GetAllAsync();
    }

    public async Task<Empleado?> GetByIdAsync(long id)
    {
        return await _unitOfWork.Empleados.GetByIdAsync(id);
    }

    public async Task<Empleado> CreateAsync(
        Empleado empleado)
    {
        Validar(empleado);

        if (await _unitOfWork.Empleados
                .ExisteDocumentoAsync(
                    empleado.DocumentoIdentidad))
        {
            throw new InvalidOperationException(
                "Ya existe un empleado con ese documento.");
        }

        if (await _unitOfWork.Empleados
                .ExisteCorreoAsync(
                    empleado.CorreoCorporativo))
        {
            throw new InvalidOperationException(
                "Ya existe un empleado con ese correo.");
        }

        await _unitOfWork.Empleados.AddAsync(empleado);

        await _unitOfWork.CompleteAsync();

        return empleado;
    }

    public async Task<bool> UpdateAsync(
        long id,
        Empleado empleado)
    {
        var actual =
            await _unitOfWork.Empleados.GetByIdAsync(id);

        if (actual == null)
            return false;

        Validar(empleado);

        if (await _unitOfWork.Empleados
                .ExisteDocumentoAsync(
                    empleado.DocumentoIdentidad, id))
        {
            throw new InvalidOperationException(
                "El documento ya pertenece a otro empleado.");
        }

        if (await _unitOfWork.Empleados
                .ExisteCorreoAsync(
                    empleado.CorreoCorporativo, id))
        {
            throw new InvalidOperationException(
                "El correo ya pertenece a otro empleado.");
        }

        actual.Nombres = empleado.Nombres;
        actual.Apellidos = empleado.Apellidos;
        actual.DocumentoIdentidad =
            empleado.DocumentoIdentidad;
        actual.CorreoCorporativo =
            empleado.CorreoCorporativo;
        actual.Telefono = empleado.Telefono;
        actual.Cargo = empleado.Cargo;
        actual.FechaIngreso = empleado.FechaIngreso;
        actual.Activo = empleado.Activo;

        _unitOfWork.Empleados.Update(actual);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    public async Task<bool> DeleteAsync(long id)
    {
        var empleado =
            await _unitOfWork.Empleados.GetByIdAsync(id);

        if (empleado == null)
            return false;

        _unitOfWork.Empleados.Remove(empleado);

        await _unitOfWork.CompleteAsync();

        return true;
    }

    private static void Validar(Empleado empleado)
    {
        if (string.IsNullOrWhiteSpace(empleado.Nombres))
            throw new ArgumentException(
                "Los nombres son obligatorios.");

        if (string.IsNullOrWhiteSpace(empleado.Apellidos))
            throw new ArgumentException(
                "Los apellidos son obligatorios.");

        if (string.IsNullOrWhiteSpace(
                empleado.DocumentoIdentidad))
        {
            throw new ArgumentException(
                "El documento es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(
                empleado.CorreoCorporativo))
        {
            throw new ArgumentException(
                "El correo corporativo es obligatorio.");
        }

        if (string.IsNullOrWhiteSpace(empleado.Cargo))
            throw new ArgumentException(
                "El cargo es obligatorio.");
    }
}
