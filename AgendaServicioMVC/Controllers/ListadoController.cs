using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgendaServicioMVC.Controllers
{
    public class ListadoController : Controller
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
                        List<SelectListItem> items = new List<SelectListItem>();
                        Dictionary<string, int> datos = null;
                        try
                        {
                            datos = AgendaServicio.Business.Common.EstadoServicio.GetEstadoServicio();
                            /*for (int x = 0; x < datos.Count; x++)
                            {
                                items.Add(
                                    new SelectListItem()
                                    {
                                        Text = datos.ElementAt(x).Key,
                                        Value = datos.ElementAt(x).Value.ToString()
                                    }
                                );
                            }
                            ViewBag.ListEstatusServicio = new SelectList(items, "Value", "Text", 1);*/
                            ViewBag.ListEstatusServicio = datos.Where(v => v.Value != (int)AgendaServicio.Entities.Common.EstadoServicio.ACTIVIDADES_REPROGRAMADAS).ToDictionary(v => v.Key, v => v.Value);
                            return View();
                        }
                        finally
                        {
                            items = null;
                            datos = null;
                        }
                    }
                }
            }
            return RedirectToAction("Index", "Home");
        }

        public ViewResult CuerpoCorreo(string llamada)
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
            Models.DatosCorreoLlamada Llamada = null;
            JavaScriptSerializer js = new JavaScriptSerializer();
            ViewBag.PathCorreo = Server.MapPath("~/PlantillasCorreoLlamada/" + Pais + "/Correo.html");
            try
            {
                Llamada = js.Deserialize<Models.DatosCorreoLlamada>(llamada);
                Llamada.DateAct = DateTime.ParseExact(Llamada.Date, "yyyy-MM-dd", System.Globalization.CultureInfo.InvariantCulture);
            }
            catch
            {
                Llamada = null;
            }
            finally
            {
                llamada = null;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicio(ConnectionString, Llamada.Estatus.HasValue ? Llamada.Estatus.Value : 1, Llamada.DateAct, Llamada.CallId, RoleId);
                return View(
                    ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).OrderBy(v => v.SucursalServicio).First()
                );
            }
            catch
            {
                return View();
            }
            finally
            {
                result = null;
            }
        }

        [ChildActionOnly]
        public ActionResult GoHome()
        {
            return RedirectToAction("Index", "Home");
        }

        [ChildActionOnly]
        public PartialViewResult EditarModalCalendario()
        {
            return PartialView("EditarModal");
        }

        /*[HttpPost]
        public PartialViewResult Listado(string datePick = null, int? valueEstatus = null, string valueCallID = "")
        {
            ViewBag.Usuario = Models.Usuario.Obtener();
            ViewBag.EstatusServicio = valueEstatus.HasValue ? valueEstatus.Value : 1;
            ViewBag.ShowEmailIcon = true;
            ViewBag.ShowWaitTime = true;
            if (datePick == null)
            {
                datePick = string.Empty;
            }
            DateTime? DateAct = !string.IsNullOrEmpty(datePick) ? (DateTime?)DateTime.ParseExact(datePick, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Today;
            int? CallId = null;
            if (!string.IsNullOrEmpty(valueCallID))
            {
                try
                {
                    CallId = int.Parse(valueCallID);
                }
                catch
                {
                    CallId = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicio(ConnectionString, valueEstatus.HasValue ? valueEstatus.Value : 1, DateAct, CallId);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).OrderBy(v => v.SucursalServicio).ThenBy(v => v.Actividades.Max(w => w.ActivityBeginTime)).ThenByDescending(v => v.Actividades.Max(w => w.TiempoEspera)).ToList()
                );
            }
            finally
            {
                result = null;
            }
        }*/

        [HttpPost]
        public PartialViewResult ListadoReprogramadas()
        {
            ViewBag.EstatusServicio = new List<int>() { 3 };
            ViewBag.ShowEmailIcon = false;
            ViewBag.ShowWaitTime = false;
            ViewBag.ShowHeader = false;
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            ViewBag.Usuario = usuario;
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
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicioReprogramadas(ConnectionString, RoleId);
                return PartialView("Listado",
                    ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).OrderBy(v => v.SucursalServicio).ThenByDescending(v => v.Actividades.Max(w => w.TiempoEspera)).ToList()
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult Listado(string datePick = null, List<int> valuesEstatus = null, string valueCallID = "")
        {
            ViewBag.Usuario = Models.Usuario.Obtener();
            //ViewBag.EstatusServicio = valueEstatus.HasValue ? valueEstatus.Value : 1;
            if (valuesEstatus == null)
            {
                valuesEstatus = new List<int>() { 1 };
            }
            else if (valuesEstatus.Count == 0)
            {
                valuesEstatus.Add(1);
            }
            ViewBag.EstatusServicio = valuesEstatus;
            ViewBag.ShowEmailIcon = true;
            ViewBag.ShowWaitTime = true;
            ViewBag.ShowHeader = true;
            if (datePick == null)
            {
                datePick = string.Empty;
            }
            DateTime? DateAct = !string.IsNullOrEmpty(datePick) ? (DateTime?)DateTime.ParseExact(datePick, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Today;
            int? CallId = null;
            if (!string.IsNullOrEmpty(valueCallID))
            {
                try
                {
                    CallId = int.Parse(valueCallID);
                }
                catch
                {
                    CallId = null;
                }
            }
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
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            List<AgendaServicio.Entities.Common.LlamadaServicio> listado = new List<AgendaServicio.Entities.Common.LlamadaServicio>();
            int contador = 0;
            try
            {
                for (int x = 0; x < valuesEstatus.Count; x++)
                {
                    try
                    {
                        result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicio(ConnectionString, valuesEstatus[x], DateAct, CallId, RoleId);
                        if (!result.HasError)
                        {
                            listado.AddRange((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection);
                        }
                    }
                    catch
                    {
                        result = null;
                    }
                    finally
                    {
                        listado = listado.OrderBy(v => v.SucursalServicio).ThenBy(v => v.Actividades.Max(w => w.ActivityBeginTime)).ThenByDescending(v => v.Actividades.Max(w => w.TiempoEspera)).ToList();
                    }
                }
                result = null;
                try
                {
                    //contador = listado.Where(v => v.Actividades != null).SelectMany(v => v.Actividades).Count(v => v.ActivityCreateDate >= DateAct.Value);
                    //contador = listado.Where(v => v != null).Where(v => v.CreateDate.HasValue).Count(v => v.CreateDate >= DateAct.Value);
                    //contador = listado.Where(v => v != null).Where(v => v.Actividades != null).SelectMany(v => v.Actividades.OrderBy(w => w.ActivityId)).Count(v => v.ActivityCreateDate >= DateAct.Value);
                    //contador = listado.Where(v => v != null).Where(v => v.FechaPrimeraActividad.HasValue).Count(v => v.FechaPrimeraActividad.Value >= DateAct.Value && v.FechaPrimeraActividad.Value < DateAct.Value.AddDays(1));
                    result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicioPorFecha(ConnectionString, DateAct, RoleId);
                    if (!result.HasError)
                    {
                        contador = ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).Count;
                    }
                    else
                    {
                        contador = 0;
                    }
                }
                catch
                {
                    contador = 0;
                }
                finally
                {
                    ViewBag.ConteoListado = contador;
                    ViewBag.FechaListado = DateAct.Value;
                }
                return PartialView(listado);
            }
            finally
            {
                listado = null;
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult IngenierosCallCenter(DateTime? date)
        {
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            ViewBag.Usuario = usuario;
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
            if (!date.HasValue)
            {
                date = DateTime.Today;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.Ingeniero.GetIngenierosCallCenter(ConnectionString, date.Value, RoleId);
                return PartialView(
                    (List<AgendaServicio.Entities.Common.Ingeniero>)result.Collection
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult IngenierosDisponibilidad(DateTime? date, string valueCallID)
        {
            if (!date.HasValue)
            {
                date = DateTime.Today;
            }
            int? CallId = null;
            if (!string.IsNullOrEmpty(valueCallID))
            {
                try
                {
                    CallId = int.Parse(valueCallID);
                }
                catch
                {
                    CallId = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
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
            try
            {
                result = AgendaServicio.Business.Common.Ingeniero.GetDisponibilidadIngenieros(ConnectionString, date.Value, CallId, RoleId);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Ingeniero>)result.Collection).OrderBy(v => v.Sucursal.Name).ToList()
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult EditarModal()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult CorreoModal()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult EditarLlamada(string datePick = null, int? valueEstatus = null, string valueCallID = "")
        {
            if (datePick == null)
            {
                datePick = string.Empty;
            }
            DateTime? DateAct = !string.IsNullOrEmpty(datePick) ? (DateTime?)DateTime.ParseExact(datePick, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Today;
            int? CallId = null;
            if (!string.IsNullOrEmpty(valueCallID))
            {
                try
                {
                    CallId = int.Parse(valueCallID);
                }
                catch
                {
                    CallId = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            List<SelectListItem> items = new List<SelectListItem>();
            List<AgendaServicio.Entities.Common.Ingeniero> ingenieros = null;
            List<AgendaServicio.Entities.Common.Sucursal> sucursales = null;
            List<AgendaServicio.Entities.Common.LlamadaServicio> tipoServicio = null;
            Dictionary<string, int> estatus = null;
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
            try
            {
                result = AgendaServicio.Business.Common.Ingeniero.GetDisponibilidadIngenieros(ConnectionString, DateAct.Value, CallId, RoleId);
                ingenieros = (List<AgendaServicio.Entities.Common.Ingeniero>)result.Collection;
                for (int x = 0; x < ingenieros.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = ingenieros.ElementAt(x).Name + 
                            (
                                !string.IsNullOrEmpty(ingenieros.ElementAt(x).Vacaciones) ?
                                "(" + ingenieros.ElementAt(x).VacacionesLetra + ")" :
                                (
                                    ingenieros.ElementAt(x).Servicio ? 
                                    "(S)" :
                                    (
                                        ingenieros.ElementAt(x).CallCenter ?
                                        "(CC)" :
                                        ""
                                    )
                                )
                            ),
                            Value = ingenieros.ElementAt(x).Id.ToString()
                        }
                    );
                }
            }
            catch
            {
                items = new List<SelectListItem>();
            }
            finally
            {
                ViewBag.ListIngenieros = items;
                items = null;
                ingenieros = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                result = AgendaServicio.Business.Common.Sucursal.GetSucursales(ConnectionString);
                sucursales = (List<AgendaServicio.Entities.Common.Sucursal>)result.Collection;
                for (int x = 0; x < sucursales.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = sucursales.ElementAt(x).Name,
                            Value = sucursales.ElementAt(x).Id
                        }
                    );
                }
            }
            catch
            {
                items = new List<SelectListItem>();
            }
            finally
            {
                ViewBag.ListSucursales = new SelectList(items, "Value", "Text");
                items = null;
                sucursales = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetTiposLlamadasServicio(ConnectionString);
                tipoServicio = (List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection;
                for (int x = 0; x < tipoServicio.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = tipoServicio.ElementAt(x).CallType,
                            Value = tipoServicio.ElementAt(x).CallTypeId.ToString()
                        }
                    );
                }
            }
            catch
            {
                items = new List<SelectListItem>();
            }
            finally
            {
                ViewBag.ListTipoServicio = new SelectList(items, "Value", "Text", 1);
                items = null;
                tipoServicio = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                estatus = AgendaServicio.Business.Common.EstadoServicio.GetEstadoServicio();
                for (int x = 0; x < estatus.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = estatus.ElementAt(x).Key,
                            Value = estatus.ElementAt(x).Value.ToString()
                        }
                    );
                }
            }
            catch
            {
                items = new List<SelectListItem>();
            }
            finally
            {
                ViewBag.ListEstatusServicio = new SelectList(items, "Value", "Text");
                items = null;
                estatus = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicio(ConnectionString, valueEstatus.HasValue ? valueEstatus.Value : 1, DateAct, CallId, RoleId);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).OrderBy(v => v.SucursalServicio).First()
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult CorreoLlamada(string datePick = null, int? valueEstatus = null, string valueCallID = "", string EmailTo = "", string EmailEng = "")
        {
            if (datePick == null)
            {
                datePick = string.Empty;
            }
            DateTime? DateAct = !string.IsNullOrEmpty(datePick) ? (DateTime?)DateTime.ParseExact(datePick, "dd/MM/yyyy", System.Globalization.CultureInfo.InvariantCulture) : DateTime.Today;
            int? CallId = null;
            if (!string.IsNullOrEmpty(valueCallID))
            {
                try
                {
                    CallId = int.Parse(valueCallID);
                }
                catch
                {
                    CallId = null;
                }
            }
            return PartialView(
                new Models.DatosCorreoLlamada()
                {
                    DateAct = DateAct.Value,
                    Date = DateAct.Value.ToString("yyyy-MM-dd"),
                    CallId = CallId,
                    EmailTo = EmailTo,
                    EmailEng = EmailEng,
                    EmailCC = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.CC"],
                    Estatus = valueEstatus
                }
            );
        }

        [HttpPost]
        public JsonResult ProcesoEditarActividad(AgendaServicio.Entities.SAP.SapActivity Actividad)
        {
            if (Actividad != null)
            {
                return Json(AgendaServicio.Business.SAP.SapManagement.GuardarActividad(Actividad));
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult ProcesoEditarLlamada(AgendaServicio.Entities.SAP.SapServiceCall Llamada)
        {
            if (Llamada != null)
            {
                if (Llamada.IdEstatusServicio.HasValue)
                {
                    AgendaServicio.Entities.Tools.SqlChangesResult result = AgendaServicio.Business.Common.LlamadaServicio.AddLlamadaEstatus(ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString, Llamada.CallId, Llamada.IdEstatusServicio.Value);
                }
                return Json(AgendaServicio.Business.SAP.SapManagement.GuardarLlamadaServicio(Llamada));
            }
            else
            {
                return Json(null);
            }
        }

        [HttpPost]
        public JsonResult AgregarIngenieros(string json, DateTime date, int[] EngineersId)
        {
            return Json(AgendaServicio.Business.Common.Ingeniero.AddRangoIngenierosCallCenter(ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString, date, EngineersId));
        }

        [HttpPost]
        public JsonResult QuitarIngenieros(DateTime date, int EngineerId)
        {
            return Json(AgendaServicio.Business.Common.Ingeniero.RemoveIngenieroCallCenter(ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString, date, EngineerId));
        }

        [HttpPost]
        public JsonResult EnviarCorreo(Models.DatosCorreoLlamada datos)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            string Asunto = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.Subject"].ToString();
            string Smtp = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.SMTP"].ToString();
            string Usuario = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.User"].ToString();
            string Pass = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.Pass"].ToString();
            string De = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.From"].ToString();
            string Para = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.To"].ToString();
            string Copia = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.CC"].ToString();
            string CopiaOculta = ConfigurationManager.AppSettings[Pais + ".HaasCnc.Email.BCC"].ToString();
            Dictionary<string, byte[]> imagenes = new Dictionary<string, byte[]>();
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            AgendaServicio.Entities.Common.LlamadaServicio Llamada = null;
            string Mensaje = string.Empty;
            byte[] reader = null;
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            ViewBag.Usuario = usuario;
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
            try
            {
                result = AgendaServicio.Business.Common.LlamadaServicio.GetLlamadasServicio(ConnectionString, datos.Estatus.HasValue ? datos.Estatus.Value : 1, datos.DateAct, datos.CallId, RoleId);
                Llamada = ((List<AgendaServicio.Entities.Common.LlamadaServicio>)result.Collection).OrderBy(v => v.SucursalServicio).First();
                Mensaje = Models.PlantillaCorreoLlamada.GetHtml(Llamada, Server.MapPath("~/PlantillasCorreoLlamada/" + Pais + "/Correo.html"));
                Mensaje = Mensaje.Replace("~/CorreoLlamada/" + Pais + "/", "");
                string Path = Server.MapPath("~/Content/CorreoLlamada/" + Pais + "/");
                List<string> images = Models.PlantillaCorreoLlamada.GetImages(Path);
                images = images.Select(v => v.Replace(Path, "")).ToList();
                for (int x = 0; x < images.Count; x++)
                {
                    reader = System.IO.File.ReadAllBytes(Path + images.ElementAt(x));
                    Mensaje = Mensaje.Replace(images.ElementAt(x), "cid:" + images.ElementAt(x).Substring(0, images.ElementAt(x).IndexOf('.')));
                    imagenes.Add(images.ElementAt(x), reader);
                }
                Mensaje = Mensaje.Replace("javascript:window.open('", "");
                Mensaje = Mensaje.Replace("');", "");
                if (!string.IsNullOrEmpty(Para))
                {
                    datos.EmailTo = Para;
                }
                if (!string.IsNullOrEmpty(Copia))
                {
                    datos.EmailCC = Copia;
                }
                if (!string.IsNullOrEmpty(CopiaOculta))
                {
                    datos.EmailBCC = CopiaOculta;
                }
                if (string.IsNullOrEmpty(datos.EmailBCC))
                {
                    if (!string.IsNullOrEmpty(datos.EmailEng))
                    {
                        datos.EmailBCC = datos.EmailEng;
                    }
                }
                else if (!string.IsNullOrEmpty(datos.EmailEng))
                {
                    datos.EmailBCC += ";" + datos.EmailEng;
                }
                return Json(new { Sent = AgendaServicio.Business.Tools.Correo.Enviar(ConnectionString, datos.UserName, datos.CallId.Value, Asunto, datos.EmailTo, datos.EmailCC, datos.EmailBCC, Mensaje, Smtp, Usuario, Pass, De, imagenes) });
            }
            finally
            {
                De = null;
                Smtp = null;
                Pass = null;
                Asunto = null;
                result = null;
                Usuario = null;
                Llamada = null;
                Mensaje = null;
                imagenes = null;
                reader = null;
            }
        }
	}
}