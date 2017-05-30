using System;

namespace AgendaServicio.Entities.Common
{
    public class CorreoLlamada
    {
        public long Id { get; set; }
        public int IdLlamada { get; set; }
        public DateTime FechaEnvío { get; set; }
        public string Usuario { get; set; }
    }
}
