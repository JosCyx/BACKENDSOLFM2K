namespace SOLFM2K.Models
{
    public class OrdenComprasTemplate
    {
        public CabSolOrdenCompra? Cabecera { get; set; }
        public List<DetSolCotizacion>? Detalles { get; set; }
        public List<ItemSector>? Items { get; set; }
    }
}
