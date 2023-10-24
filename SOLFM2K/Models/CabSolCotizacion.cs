using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class CabSolCotizacion
{

    public int CabSolCotID { get; set; }

    public string? CabSolCotNumerico { get; set; }

    public int CabSolCotTipoSolicitud { get; set; }

    public int CabSolCotIdDept { get; set; }

    public int CabSolCotIdArea { get; set; }

    public int CabSolCotNoSolicitud { get; set; }

    public string CabSolCotSolicitante { get; set; }

    public DateTime CabSolCotFecha { get; set; }

    public string CabSolCotAsunto { get; set; } = null!;

    public string? CabSolCotProcedimiento { get; set; } = null!;

    public string? CabSolCotObervaciones { get; set; } = null!;

    public string? CabSolCotAdjCot { get; set; } = null!;

    public int? CabSolCotNumCotizacion { get; set; }

    public string CabSolCotEstado { get; set; } = null!;

    public int CabSolCotEstadoTracking { get; set; }


    public DateTime CabSolCotPlazoEntrega { get; set; }

    public DateTime CabSolCotFechaMaxentrega { get; set; }

    public int? CabSolCotInspector { get; set; } 

    public string? CabSolCotTelefInspector { get; set; } = null!;

    public string? CabSolCotAprobPresup { get; set; } = null!;

    public string? CabSolCotMtovioDev { get; set; } = null!;

    public int CabSolCotIdEmisor { get; set; }

    public int CabSolCotApprovedBy { get; set; }

    public int CabSolCotFinancieroBy { get; set; }


    //public virtual TipoSolic CabSolCotIdCabeceraNavigation { get; set; } = null!;

    //public virtual ICollection<SolCotizacion> SolCotizacions { get; set; } = new List<SolCotizacion>();
}
