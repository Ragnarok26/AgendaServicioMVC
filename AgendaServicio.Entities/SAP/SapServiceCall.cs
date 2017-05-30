using System;

namespace AgendaServicio.Entities.SAP
{
    public class SapServiceCall
    {
        public int CallId { get; set; }
        public int? IdTipoServicio { get; set; }
        public int? IdEstatusServicio { get; set; }
        public string Sucursal { get; set; }
        public string Descripcion { get; set; }
    }
}
