using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Transaccione
{
    public int TrEmpresa { get; set; }

    public int TrAplicacion { get; set; }

    public int TrFuncion { get; set; }

    public int TrCodigo { get; set; }

    public string? TrNombre { get; set; }

    public string? TrEstado { get; set; }
}
