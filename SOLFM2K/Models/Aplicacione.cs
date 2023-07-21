using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Aplicacione
{
    public int ApEmpresa { get; set; }

    public int ApCodigo { get; set; }

    public string? ApNombre { get; set; }

    public string? ApNemonico { get; set; }

    public int? ApFuncion { get; set; }

    public string? ApEstado { get; set; }

    public string? ApVersion { get; set; }

    //public virtual ICollection<Rol> Rols { get; set; } = new List<Rol>();
}
