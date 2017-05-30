using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgendaServicioMVC.Controllers
{
    public class VacacionController : Controller
    {
        public static string Pais = ConfigurationManager.AppSettings["HaasCnc.App.Country"];

        public ActionResult Index()
        {
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            if (usuario != null)
            {
                ViewBag.Usuario = usuario;
                if (usuario.Roles != null)
                {
                    if (usuario.StartedSesion)
                    {
                        if (!usuario.Roles.Any(v => v.Name.Equals("Solo Lectura")))
                        {
                            return View();
                        }
                        else
                        {
                            return RedirectToAction("Index", "Listado");
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        [HttpPost]
        public JsonResult DatosCalendario(DateTime? inicial = null, DateTime? final = null, DateTime? actual = null)
        {
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            long? RoleId = null;
            if (usuario != null)
            {
                if (usuario.Roles != null)
                {
                    if (usuario.Roles.Any(v => v.Name.Equals("Servicio")))
                    {
                        RoleId = usuario.Roles.Where(v => v.Name.Equals("Servicio")).FirstOrDefault().Id;
                    }
                    else if (usuario.Roles.Any(v => v.Name.Equals("Aplicaciones")))
                    {
                        RoleId = usuario.Roles.Where(v => v.Name.Equals("Aplicaciones")).FirstOrDefault().Id;
                    }
                }
            }
            actual = actual.HasValue ? actual : DateTime.Today;
            /*if (final.HasValue)
            {
                final = final.Value.AddDays(-1);
            }*/
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = AgendaServicio.Business.Common.Ingeniero.GetIngenierosNoDisponiblesPorRango(ConnectionString, actual.Value, inicial, final, RoleId);
            List<Models.Calendario> items = null;
            try
            {
                if (!result.HasError)
                {
                    items = ((List<AgendaServicio.Entities.Common.Ingeniero>)result.Collection).Select(
                        v => new Models.Calendario()
                        {
                            Calendar = "",
                            Date = actual.Value.ToString("dd/MM/yyyy"),
                            Description = v.Vacaciones,
                            End = DateTime.SpecifyKind(v.FinVacaciones, DateTimeKind.Utc),
                            Id = v.Id.ToString(),
                            Location = "",
                            Start = DateTime.SpecifyKind(v.InicioVacaciones, DateTimeKind.Utc),
                            Subject = v.Name + " (Sucursal " + v.Sucursal.Name.Replace("Zin Asignar", "Sin Asignar") + ") " + v.Vacaciones
                        }
                    ).ToList();
                }
                else
                {
                    items = new List<Models.Calendario>();
                }
                return Json(new { HasError = result.HasError, Message = result.Message, Source = items });
            }
            finally
            {
                result = null;
                ConnectionString = null;
                actual = null;
                inicial = null;
                final = null;
                items = null;
            }
        }
	}
}