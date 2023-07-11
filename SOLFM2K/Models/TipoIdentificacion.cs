using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class TipoIdentificacion
{
    public int TipoDocId { get; set; }

    public string TipoDocNombre { get; set; } = null!;

    public string TipoDocInicial { get; set; } = null!;

    public virtual ICollection<Empleado> Empleados { get; set; } = new List<Empleado>();
}
