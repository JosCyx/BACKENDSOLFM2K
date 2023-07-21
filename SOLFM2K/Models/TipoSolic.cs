using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class TipoSolic
{
    public int TipoSolId { get; set; }

    public string TipoSolNombre { get; set; } = null!;

    public string TipoSolInicial { get; set; } = null!;

    public string? TipoSolEstado { get; set; }

    //public virtual CabSolCotizacion? CabSolCotizacion { get; set; }

    //public virtual CabSolOrdenCompra? CabSolOrdenCompra { get; set; }

    //public virtual CabSolPago? CabSolPago { get; set; }
}
