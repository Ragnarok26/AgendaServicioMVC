using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicio.Entities.Common
{
    public class Actividad
    {
        [Display(Name = "ID Actividad")]
		public int ActivityId { get; set; }

        [Display(Name = "Notas")]
		public string ActivityNotes { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [Display(Name = "Fecha de creación")]
        [DataType(DataType.Date)]
        public DateTime? ActivityCreateDate { get; set; }

        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy HH:mm}")]
        [DataType(DataType.Date)]
        [Display(Name = "Fecha Inicio")]
        public DateTime? ActivityBeginTime { get; set; }

        [Display(Name = "Actividad")]
		public string ActivityDetails { get; set; }

        [Display(Name = "Id Tipo de actividad / Tipo de servicio")]
		public short? ActivityTypeId { get; set; }

        [Display(Name = "Tipo de actividad")]
		public string ActivityType { get; set; }

        [Display(Name = "Fecha de finalización")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? ActivityEndTime { get; set; }

        [Display(Name = "Id de Ingeniero asignado a la actividad")]
		public int? EngineerId { get; set; }

        [Display(Name = "Id Estatus de la actividad")]
		public int? ActivityStatusId { get; set; }

        [Display(Name = "Estatus de la actividad")]
		public string ActivityStatus { get; set; }

        [Display(Name = "Actividad Cerrada")]
		public string ActivityClosed { get; set; }

        [Display(Name = "Confirmado Por")]
        public string ConfirmedBy { get; set; }

        public DateTime ActivityRecontact { get; set; }

        public Ingeniero Ingeniero { get; set; }

        [Display(Name = "Causa de Retraso")]
        public string CausaRetraso { get; set; }

        [Display(Name = "Espera")]
        public int TiempoEspera { get; set; }
        /*private int _tiempoEspera;
        public int TiempoEspera
        {
            get
            {
                DateTime? actual = DateTime.Now;
                TimeSpan? tiempo = null;
                try
                {
                    //calcula el timpo de espera
                    _tiempoEspera = 0;
                    if (actual.Value >= ActivityBeginTime)
                    {
                        _tiempoEspera = 1;
                        tiempo = actual.Value - ActivityBeginTime;
                    }
                    else
                    {
                        _tiempoEspera = -1;
                        tiempo = ActivityBeginTime - actual.Value;
                    }
                    _tiempoEspera *= tiempo.HasValue ? ((int)tiempo.Value.TotalDays) : 0;
                    return _tiempoEspera;
                }
                finally
                {
                    actual = null;
                    tiempo = null;
                }
            }
        }*/
    }
}
