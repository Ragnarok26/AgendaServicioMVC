using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicio.Entities.Common
{
    public class Ingeniero
    {
        [Display(Name = "Id de Ingeniero asignado a la actividad")]
        public int Id { get; set; }

        [Display(Name = "Ingeniero")]
        public string Name { get; set; }
        public int? PositionId { get; set; }
        public string Position { get; set; }
        public bool? Active { get; set; }
        public Sucursal Sucursal { get; set; }
        public bool CallCenter { get; set; }
        public string Vacaciones { get; set; }
        public string VacacionesLetra { get; set; }
        public bool Servicio { get; set; }
        public DateTime InicioVacaciones { get; set; }
        public DateTime FinVacaciones { get; set; }
        public DateTime DateCallCenter { get; set; }
        public string email { get; set; }
    }
}
