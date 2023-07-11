using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Documento
{
    public int? DocClave { get; set; }

    public string? DocDescripcion { get; set; }

    public string? DocEstado { get; set; }
}
