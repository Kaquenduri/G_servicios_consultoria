using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class InformeProgreso
{
    public long InformeProgresoId { get; set; }

    public long ProyectoId { get; set; }

    public long ElaboradoPorEmpleadoId { get; set; }

    public DateOnly PeriodoDesde { get; set; }

    public DateOnly PeriodoHasta { get; set; }

    public DateTime FechaInforme { get; set; }

    public short PorcentajeAvance { get; set; }

    public string Resumen { get; set; } = null!;

    public string? Riesgos { get; set; }

    public string? ProximosPasos { get; set; }

    public virtual Empleado ElaboradoPorEmpleado { get; set; } = null!;

    public virtual Proyecto Proyecto { get; set; } = null!;
}
