using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicio.Entities.Common
{
    public class LlamadaServicio
    {
        [Display(Name = "Id de llamada")]
        public int CallId { get; set; }

        [Display(Name = "Número de llamada")]
        public int CallDocNum { get; set; }

        [Display(Name = "Estatus")]
        public string StatusCall { get; set; }

        public int Estatus { get; set; }

        [Display(Name = "Estado")]
        public string DescriptionEstatus { get; set; }

        public int CorreosEnviados { get; set; }

        [Display(Name = "Descripción de problema")]
        public string Subject { get; set; }

        [Display(Name = "Comentarios")]
        public string Description { get; set; }
        
        public string DescriptionTruncated
        {
            get { return !string.IsNullOrEmpty(Description) ? Description.Substring(0, Description.Length > 40 ? 40 : Description.Length) + "..." : ""; }
        }

        [Display(Name = "Prioridad")]
        public string Priority { get; set; }

        [Display(Name = "Id Cliente")]
        public string CustomerId { get; set; }

        [Display(Name = "Cliente")]
        public string CustomerName { get; set; }

        [Display(Name = "No. Serie")]
        public string SN { get; set; }

        [Display(Name = "FileM")]
        public string InternalSN { get; set; }

        [Display(Name = "Maquina")]
        public string ItemCode { get; set; }

        [Display(Name = "Nombre del equipo")]
        public string ItemName { get; set; }
        
        public short? CallTypeId { get; set; }
        
        public short? AsigneeId { get; set; }

        [Display(Name = "Fecha de creación")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CreateDate { get; set; }

        [Display(Name = "Fecha de cierre")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? CloseDate { get; set; }

        [Display(Name = "Última actualización")]
        [DisplayFormat(DataFormatString = "{0:dd-MM-yyyy}")]
        [DataType(DataType.Date)]
        public DateTime? UpdateDate { get; set; }

        [Display(Name = "Tipo de servicio")]
        public string CallType { get; set; }

        [Display(Name = "Asignado por")]
        public string Asignee { get; set; }

        [Display(Name = "Teléfono")]
        [DataType(DataType.PhoneNumber)]
        [RegularExpression(@"^\(?([0-9]{3})\)?[-. ]?([0-9]{3})[-. ]?([0-9]{4})$", ErrorMessage = "Not a valid Phone number")]
        public string CustomerPhone { get; set; }

        [Display(Name = "Dirección del cliente")]
        public string CustomerAddress { get; set; }

        [Display(Name = "Dirección de la máquina")]
        public string MachineAddress { get; set; }

        [Display(Name = "Contacto Principal")]
        public string ContactName { get; set; }

        [Display(Name = "Dirección de Contacto")]
        public string ContactAddr { get; set; }

        [Display(Name = "Teléfono")]
        public string ContactTel { get; set; }

        [Display(Name = "Teléfono adicional")]
        public string ContactTelAd { get; set; }

        [Display(Name = "Celular")]
        public string ContactCel { get; set; }

        [Display(Name = "Correo Electrónico")]
        public string ContactEmail { get; set; }

        [Display(Name = "Tiempo de Respuesta")]
        public int TiempoRespuesta { get; set; }

        [Display(Name = "Causa de Retraso")]
        public string CausaRetraso { get; set; }

        public DateTime? FechaPrimeraActividad { get; set; }

        public string SucursalServicio { get; set; }
        
        public List<Actividad> Actividades { get; set; }
        
        public Sucursal Sucursal { get; set; }
    }
}
