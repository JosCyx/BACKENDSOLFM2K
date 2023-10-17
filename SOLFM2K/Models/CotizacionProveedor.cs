using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CotizacionProveedor
{
    public int CotProvId { get; set; }

    public int CotProvTipoSolicitud { get; set; }

    public int CotProvNoSolicitud { get; set; }

    public string CotProvRuc { get; set; } = null!;

    public string CotProvNombre { get; set; } = null!;

    public string CotProvTelefono { get; set; } = null!;

    public string CotProvCorreo { get; set; } = null!;

    public string? CotProvDireccion { get; set; }

    public int CotProvVerify { get; set; }
}
