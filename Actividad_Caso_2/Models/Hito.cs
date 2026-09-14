using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class Hito
{
    public long HitoId { get; set; }

    public long ProyectoId { get; set; }

    public string Nombre { get; set; } = null!;

    public string? Descripcion { get; set; }

    public DateOnly FechaPlanificada { get; set; }

    public DateOnly? FechaAlcanzada { get; set; }

    public string Estado { get; set; } = null!;

    public virtual Proyecto Proyecto { get; set; } = null!;
}
