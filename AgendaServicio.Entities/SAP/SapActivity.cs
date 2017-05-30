using System;

namespace AgendaServicio.Entities.SAP
{
    public class SapActivity
    {
        public int ActivityId { get; set; }
        public int CallId { get; set; }
        public int? IdIngeniero { get; set; }
        public DateTime FechaInicio { get; set; }
        public DateTime FechaFin { get; set; }
        public string Detalle { get; set; }
    }
}
