using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class VwResumenPresupuestoProyecto
{
    public long? ProyectoId { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public decimal? PresupuestoEstimado { get; set; }

    public decimal? GastoReal { get; set; }

    public decimal? SaldoPresupuesto { get; set; }

    public decimal? PorcentajeConsumido { get; set; }
}
