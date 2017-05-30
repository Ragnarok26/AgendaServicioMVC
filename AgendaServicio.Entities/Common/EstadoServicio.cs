using System.ComponentModel;

namespace AgendaServicio.Entities.Common
{
    public enum EstadoServicio : int
    {
        [Description("Pendiente")]
        ACTIVIDADES_PENDIENTES = 1,
        [Description("Programada")]
        ACTIVIDADES_PROGRAMADAS = 2,
        [Description("Re-programada")]
        ACTIVIDADES_REPROGRAMADAS = 3,
        [Description("Terminada")]
        ACTIVIDADES_TERMINADAS = 4,
        [Description("Cancelada")]
        ACTIVIDADES_CANCELADAS = 5
    }
}
