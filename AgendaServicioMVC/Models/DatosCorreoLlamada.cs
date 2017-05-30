using System;

namespace AgendaServicioMVC.Models
{
    public class DatosCorreoLlamada
    {
        public DateTime DateAct { get; set; }
        public string Date { get; set; }
        public int? CallId { get; set; }
        public int? Estatus { get; set; }
        public string EmailTo { get; set; }
        public string EmailCC { get; set; }
        public string EmailBCC { get; set; }
        public string EmailEng { get; set; }
        public string UserName { get; set; }
    }
}