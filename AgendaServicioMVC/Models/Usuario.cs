using System;
using System.Web;
using System.Web.Script.Serialization;

namespace AgendaServicioMVC.Models
{
    public class Usuario
    {
        public static AgendaServicio.Entities.Common.Usuario Obtener()
        {
            HttpCookie myCookie = HttpContext.Current.Request.Cookies["UserSettings"];
            JavaScriptSerializer js = null;
            AgendaServicio.Entities.Common.Usuario usuario = null;
            if (myCookie != null)
            {
                if (!string.IsNullOrEmpty(myCookie["UserData"]))
                {
                    try
                    {
                        js = new JavaScriptSerializer();
                        usuario = (AgendaServicio.Entities.Common.Usuario)js.Deserialize(myCookie["UserData"], typeof(AgendaServicio.Entities.Common.Usuario));
                    }
                    catch
                    {
                        usuario = null;
                    }
                }
            }
            return usuario;
        }
    }
}