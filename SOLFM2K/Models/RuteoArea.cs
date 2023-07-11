using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class RuteoArea
{
    public int CodTipoSolicitud { get; set; }

    public int CodDept { get; set; }

    public int CodRuteo { get; set; }

    public string Estado { get; set; } = null!;
}
