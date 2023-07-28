using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolCotizacion
{
    public int CabSolCotTipoSolicitud { get; set; }

    public int CabSolCotArea { get; set; }

    public int CabSolCotNoSolicitud { get; set; }

    public int CabSolCotSolicitante { get; set; }

    public DateTime CabSolCotFecha { get; set; }

    public string CabSolCotAsunto { get; set; } = null!;

    public string CabSolCotProcedimiento { get; set; } = null!;

    public string CabSolCotObervaciones { get; set; } = null!;

    public string CabSolCotAdjCot { get; set; } = null!;

    public int CabSolCotNumCotizacion { get; set; }

    public string CabSolCotEstado { get; set; } = null!;

    public int CabSolCotEstadoTracking { get; set; }

    //public virtual TipoSolic CabSolCotIdCabeceraNavigation { get; set; } = null!;

    //public virtual ICollection<SolCotizacion> SolCotizacions { get; set; } = new List<SolCotizacion>();
}
