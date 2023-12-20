namespace SOLFM2K.Models
{
    public partial class OCAXTemplate
    {
        public string DetOrden { get; set; }
        public string DetcodProducto { get; set; }
        public string DetdesProducto { get; set; }
        public Decimal Detcantidad { get; set; }
        public Decimal Detprecio { get; set; }
        public Decimal  DetTotal { get; set; }
        public int DetEstadoOC { get; set; }
    }
}
