using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicio.Entities.Common
{
    public class Mantenimiento
    {
        [DisplayName("Id")]
        [Display(Name = "Id")]
        public int Id { get; set; }

        [DisplayName("File-M")]
        [Display(Name = "File-M")]
        public string FileM { get; set; }

        [DisplayName("Fecha de Compra")]
        [Display(Name = "Fecha de Compra")]
        public DateTime FechaCompra { get; set; }

        [DisplayName("Código de Cliente")]
        [Display(Name = "Id de Cliente")]
        public string IdCliente { get; set; }

        [DisplayName("Nombre de Cliente")]
        [Display(Name = "Nombre de Cliente")]
        public string Cliente { get; set; }

        [DisplayName("Cantidad")]
        [Display(Name = "Cantidad")]
        public decimal Cantidad { get; set; }

        [DisplayName("Modelo")]
        [Display(Name = "Modelo")]
        public string Modelo { get; set; }

        [DisplayName("Estado")]
        [Display(Name = "Estado")]
        public string Estado { get; set; }

        [DisplayName("Ciudad")]
        [Display(Name = "Ciudad")]
        public string Ciudad { get; set; }

        [DisplayName("No. Serie")]
        [Display(Name = "No. Serie")]
        public string NoSerie { get; set; }

        [DisplayName("No. Orden de Trabajo")]
        [Display(Name = "No. Orden de Trabajo")]
        public int? NumOrden { get; set; }

        [DisplayName("Fecha de Puesta en Marcha")]
        [Display(Name = "Fecha de Puesta en Marcha")]
        public DateTime? FechaPuestaMarcha { get; set; }

        [DisplayName("Fecha Sugerida")]
        [Display(Name = "Fecha Sugerida")]
        public DateTime? FechaSugerida { get; set; }

        [DisplayName("Tiempo Transcurrido")]
        [Display(Name = "Tiempo Transcurrido")]
        public int? TiempoTranscurrido { get; set; }
    }
}
