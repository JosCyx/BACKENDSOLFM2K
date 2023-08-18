namespace SOLFM2K.Models
{
    public class CotizacionTemplate
    {
        public CabSolCotizacion? Cabecera { get; set; }
        public List<DetSolCotizacion>? Detalles { get; set; }
        public List<ItemSector>? Items { get; set; }
    }
}
