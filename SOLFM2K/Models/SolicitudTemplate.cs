namespace SOLFM2K.Models
{
    public class SolicitudTemplate
    {
        public CabSolCotizacion? Cabecera { get; set; }
        public List<DetSolCotizacion>? Detalles { get; set; }
        public List<ItemSector>? Items { get; set; }
    }
}
