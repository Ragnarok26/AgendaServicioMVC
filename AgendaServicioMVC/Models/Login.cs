using System;
using System.ComponentModel.DataAnnotations;

namespace AgendaServicioMVC.Models
{
    public class Login
    {
        [Required]
        [Display(Name = "User name")]
        public string UserName { get; set; }

        [Required]
        [DataType(DataType.Password)]
        [Display(Name = "Password")]
        public string Password { get; set; }

        [Display(Name = "Recordarme")]
        public bool RememberMe { get; set; }

        public bool StartedSesion { get; set; }
    }
}