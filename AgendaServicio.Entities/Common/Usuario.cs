using System;
using System.Collections.Generic;

namespace AgendaServicio.Entities.Common
{
    public class Usuario
    {
        public long Id { get; set; }
        public string Name { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int IdCompany { get; set; }
        public long IdRol { get; set; }
        public bool StartedSesion { get; set; }
        public bool RememberMe { get; set; }
        public List<Rol> Roles { get; set; }
        public Compania Compania { get; set; }
    }
}
