using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class NivGerencium
{
    public int GerNivId { get; set; }

    public string GerNivNombre { get; set; } = null!;

    public string GerNivCorreo { get; set; } = null!;

    public int? GerNivArea { get; set; }
}
