using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class GastoProyecto
{
    public long GastoProyectoId { get; set; }

    public long ProyectoId { get; set; }

    public long RegistradoPorEmpleadoId { get; set; }

    public DateOnly FechaGasto { get; set; }

    public string Categoria { get; set; } = null!;

    public string Concepto { get; set; } = null!;

    public decimal Monto { get; set; }

    public string? NumeroComprobante { get; set; }

    public string? Observacion { get; set; }

    public DateTime FechaRegistro { get; set; }

    public virtual Proyecto Proyecto { get; set; } = null!;

    public virtual Empleado RegistradoPorEmpleado { get; set; } = null!;
}
