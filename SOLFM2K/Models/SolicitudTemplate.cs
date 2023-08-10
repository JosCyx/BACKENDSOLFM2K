namespace SOLFM2K.Models
{
    public class SolicitudTemplate
    {
        public int cab_tipo_solicitud { get; set; }
        public int cab_area_solicitante { get; set; }
        public string cab_numerico { get; set; }
        public int cab_ID { get; set; }
        public int cab_no_solicitud { get; set; }
        public int cab_solicitante { get; set; }
        public DateTime cab_fecha { get; set; }
        public string cab_asunto { get; set; }
        public string cab_procedimiento { get; set; }
        public string cab_observaciones { get; set; }
        public string cab_adj_cotizacion { get; set; }
        public int cab_num_cotizaciones { get; set; }
        public string cab_estado { get; set; }
        public int cab_estado_track { get; set; }
        public DateTime cab_plazo_entrega { get; set; }
        public DateTime cab_fechamax_entrega { get; set; }
        public int? cab_inspector { get; set; }
        public string cab_telef_inspector { get; set; }


        public int det_ID { get; set; }
        public int det_tipo_sol { get; set; }
        public int det_no_sol { get; set; }
        public int det_id_detalle { get; set; }
        public string det_descripcion { get; set; }
        public string det_unidad { get; set; }
        public int det_cantidad_total { get; set; }
        public string det_aud_evento { get; set; }
        public DateTime det_aud_fecha { get; set; }
        public string det_aud_usuario { get; set; }
        public string det_aud_observacion { get; set; }
        public int det_aud_veces { get; set; }


        public int itm_ID { get; set; }
        public int itm_tipo_solicitud { get; set; }
        public int itm_no_solicitud { get; set; }
        public int itm_id_detalle { get; set; }
        public int itm_id_item { get; set; }
        public int itm_cantidad { get; set; }
        public int itm_sector { get; set; }
        public string itm_aud_evento { get; set; }
        public DateTime itm_aud_fecha { get; set; }
        public string itm_aud_usuario { get; set; }
        public string itm_aud_observacion { get; set; }
        public int itm_aud_veces { get; set; }
    }
}
