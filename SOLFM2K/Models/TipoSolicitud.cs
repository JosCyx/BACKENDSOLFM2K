namespace SOLFM2K.Models
{
    public class TipoSolicitud
    {
        public int TipoSolId { get; set; }

        public string TipoSolNombre { get; set; } = null!;

        public string TipoSolInicial { get; set; } = null!;

        public virtual ICollection<CabSolCotizacion> CabSolCotizacions { get; set; } = new List<CabSolCotizacion>();
        public virtual ICollection<CabSolOrdenCompra> CabSolOrdenCompras { get; set; } = new List<CabSolOrdenCompra>();
        public virtual ICollection<CabSolPago> CabSolPagos { get; set; } = new List<CabSolPago>();

    }
}
