using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Funcione
{
    public int FuEmpresa { get; set; }

    public int FuAplicacion { get; set; }

    public int FuCodigo { get; set; }

    public string? FuNombre { get; set; }

    public int? FuTransaccion { get; set; }

    public string? FuEstado { get; set; }
}
