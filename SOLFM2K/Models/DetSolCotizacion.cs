using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class DetSolCotizacion
{

    public int SolCotID { get; set; }

    public int SolCotTipoSol { get; set; }

    public int SolCotNoSol { get; set; }

    public int SolCotIdDetalle { get; set; }

    public string SolCotDescripcion { get; set; } = null!;

    public string SolCotUnidad { get; set; } = null!;

    public double SolCotCantidadTotal { get; set; }

    public int SolCotPresupuesto { get; set; }

    //public string? AudEvento { get; set; } = null!;

    //public DateTime? AudFecha { get; set; }

    //public string? AudUsuario { get; set; } = null!;

    //public string? AudObservacion { get; set; } = null!;

    //public int? AudVeces { get; set; }

    //public virtual CabSolCotizacion SolCotIdCabeceraNavigation { get; set; } = null!;

    //public virtual Item SolCotItemNavigation { get; set; } = null!;
}
