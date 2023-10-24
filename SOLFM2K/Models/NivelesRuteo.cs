using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class NivelesRuteo
{
    public int CodRuteo { get; set; }

    public int Nivel { get; set; }

    public string DescRuteo { get; set; } = null!;

    public string EstadoRuteo { get; set; } = null!;

    public string ProcesoRuteo { get; set; } = null!;
}
