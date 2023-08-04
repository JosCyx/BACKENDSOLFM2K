using System;
using System.Collections.Generic;

namespace SOLFM2K.Models;

public partial class DetSolCotizacion
{
    public int SolCotIdCabecera { get; set; }

    public int SolCotIdDetalle { get; set; }

    //public int SolCotItem { get; set; }

    public string SolCotDescripcion { get; set; } = null!;

    public string SolCotUnidad { get; set; } = null!;

    public int SolCotCantidadTotal { get; set; }

    public string AudEvento { get; set; } = null!;

    public DateTime AudFecha { get; set; }

    public string AudUsuario { get; set; } = null!;

    public string AudObservacion { get; set; } = null!;

    public int AudVeces { get; set; }

    //public virtual CabSolCotizacion SolCotIdCabeceraNavigation { get; set; } = null!;

    //public virtual Item SolCotItemNavigation { get; set; } = null!;
}
