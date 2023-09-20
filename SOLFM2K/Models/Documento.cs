using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class Documento
{
    public int DocId { get; set; }

    public int DocTipoSolicitud { get; set; }

    public int DocNoSolicitud { get; set; }
    public string? DocNombre { get; set; }

    public string DocUrl { get; set; } = null!;

    public string? DocEstado { get; set; }

}
