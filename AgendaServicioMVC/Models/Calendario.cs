using System;

namespace AgendaServicioMVC.Models
{
    public class Calendario
    {
        public string Id { get; set; }
        public string Description { get; set; }
        public string Location { get; set; }
        public string Subject { get; set; }
        public string Calendar { get; set; }
        public string Estatus { get { return "2"; } }
        public string Date { get; set; }
        public DateTime Start { get; set; }
        public DateTime End { get; set; }
        public bool ReadOnly { get { return true; } }
        public bool Draggable { get { return false; } }
        public bool Resizable { get { return false; } }
    }
}