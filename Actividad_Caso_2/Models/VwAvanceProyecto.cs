using System;
using System.Collections.Generic;

namespace Actividad_Caso_2.Models;

public partial class VwAvanceProyecto
{
    public long? ProyectoId { get; set; }

    public string? Codigo { get; set; }

    public string? Nombre { get; set; }

    public long? TotalTareas { get; set; }

    public long? TareasCompletadas { get; set; }

    public decimal? PromedioAvanceTareas { get; set; }
}
