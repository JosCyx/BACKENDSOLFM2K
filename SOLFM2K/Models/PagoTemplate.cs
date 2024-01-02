namespace SOLFM2K.Models
{
    public class PagoTemplate
    {
        public CabSolPago cabecera { get; set; }
        public List<FacturaSolPago> facturas { get; set; }
        public List<DetalleFacturaPago> detalleFacturas { get; set; }
        public List<DetSolPago> detalles { get; set; }
    }
}
