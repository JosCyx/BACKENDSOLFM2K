namespace SOLFM2K.Models
{
    public class PagoTemplate
    {
        public CabSolPago cabecera { get; set; }
        public List<DetSolPago> detalles { get; set; }
    }
}
