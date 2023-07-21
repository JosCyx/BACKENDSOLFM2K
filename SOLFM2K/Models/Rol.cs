using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Rol
{
    public int RoEmpresa { get; set; }

    public int RoCodigo { get; set; }

    public string? RoNombre { get; set; }

    public string? RoEstado { get; set; }

    public int? RoAplicacion { get; set; }

    //public virtual Aplicacione? RoAplicacionNavigation { get; set; }
}
