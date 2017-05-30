using AgendaServicio.Entities.Tools;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace AgendaServicioMVC.Controllers
{
    public class AdministracionController : Controller
    {
        public static string Pais = ConfigurationManager.AppSettings["HaasCnc.App.Country"];

        private ActionResult RetornarVista(string vista, object model = null)
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
                            if (model == null)
                            {
                                return View(vista);
                            }
                            else
                            {
                                return View(vista, model);
                            }
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

        public ActionResult IngenieroSucursal()
        {
            return RetornarVista("IngenieroSucursal");
        }

        public ActionResult Usuario()
        {
            return RetornarVista("Usuario");
        }

        public ActionResult Rol()
        {
            return RetornarVista("Rol");
        }

        public ActionResult Compania()
        {
            return RetornarVista("Compania");
        }

        public ActionResult PlantillaCorreo()
        {
            string html = AgendaServicioMVC.Models.PlantillaCorreoLlamada.GetHtml(null, Server.MapPath("~/PlantillasCorreoLlamada/" + Pais + "/Correo.html"));
            html = html.Replace("~/CorreoLlamada", Url.Content("~/Content/CorreoLlamada"));
            return RetornarVista("PlantillaCorreo", html);
        }

        public ActionResult Mantenimiento()
        {
            return RetornarVista("Mantenimiento");
        }

        public FileResult ExportarMantenimiento()
        {
            string NombreArchivo = string.Empty;
            string ConnStr = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            NombreArchivo = "Reporte de Mantenimientos en Maquina Nueva.xlsx";
            SqlCollectionResult result = AgendaServicio.Business.Common.Mantenimiento.GetMantenimientos(ConnStr);
            System.Net.Mime.ContentDisposition cd = new System.Net.Mime.ContentDisposition
            {
                FileName = NombreArchivo,
                Inline = false,
            };
            Response.AppendHeader("Content-Disposition", cd.ToString());
            return File(AgendaServicio.Business.Common.Mantenimiento.Exportar((List<AgendaServicio.Entities.Common.Mantenimiento>)result.Collection), "application/pdf");
        }

        [HttpPost]
        public PartialViewResult AgregarUsuarioModal()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult AgregarCompaniaModal()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult AgregarRolModal()
        {
            return PartialView();
        }

        [HttpPost]
        public PartialViewResult ListadoIngenieroSucursal(string name = null)
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
            if (name != null)
            {
                if (name.Replace(" ", "").Length == 0)
                {
                    name = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            List<SelectListItem> items = new List<SelectListItem>();
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            List<AgendaServicio.Entities.Common.Sucursal> sucursales = null;
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
            try
            {
                result = AgendaServicio.Business.Common.Ingeniero.GetIngenierosPorNombre(ConnectionString, name, RoleId);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Ingeniero>)result.Collection)
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ListadoUsuario(AgendaServicio.Entities.Common.Usuario usuario = null)
        {
            if (usuario != null)
            {
                if (usuario.Name != null)
                {
                    if (usuario.Name.Replace(" ", "").Length == 0)
                    {
                        usuario.Name = null;
                    }
                }
            }
            else
            {
                usuario = new AgendaServicio.Entities.Common.Usuario();
                usuario.Name = null;
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.Usuario.GetUsuariosPorNombre(ConnectionString, usuario);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Usuario>)result.Collection)
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ListadoCompania(string name = null)
        {
            if (name != null)
            {
                if (name.Replace(" ", "").Length == 0)
                {
                    name = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.Compania.GetCompaniasPorNombre(ConnectionString, name);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Compania>)result.Collection)
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ListadoRol(string name = null)
        {
            if (name != null)
            {
                if (name.Replace(" ", "").Length == 0)
                {
                    name = null;
                }
            }
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.Rol.GetRolesPorNombre(ConnectionString, name);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Rol>)result.Collection)
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ListadoMantenimiento()
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                result = AgendaServicio.Business.Common.Mantenimiento.GetMantenimientos(ConnectionString);
                return PartialView(
                    ((List<AgendaServicio.Entities.Common.Mantenimiento>)result.Collection)
                );
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ContenidoEditarUsuario(AgendaServicio.Entities.Common.Usuario usuario)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            List<SelectListItem> items = new List<SelectListItem>();
            List<AgendaServicio.Entities.Common.Compania> companias = null;
            List<AgendaServicio.Entities.Common.Rol> roles = null;
            try
            {
                result = AgendaServicio.Business.Common.Compania.GetCompanias(ConnectionString);
                companias = (List<AgendaServicio.Entities.Common.Compania>)result.Collection;
                for (int x = 0; x < companias.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = companias.ElementAt(x).Name,
                            Value = companias.ElementAt(x).Id.ToString()
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
                ViewBag.ListCompanias = new SelectList(items, "Value", "Text");
                items = null;
                companias = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                result = AgendaServicio.Business.Common.Rol.GetRoles(ConnectionString);
                roles = (List<AgendaServicio.Entities.Common.Rol>)result.Collection;
                for (int x = 0; x < roles.Count; x++)
                {
                    items.Add(
                        new SelectListItem()
                        {
                            Text = roles.ElementAt(x).Name,
                            Value = roles.ElementAt(x).Id.ToString()
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
                ViewBag.ListRoles = new SelectList(items, "Value", "Text");
                items = null;
                roles = null;
                result = null;
            }
            items = new List<SelectListItem>();
            try
            {
                if (usuario != null)
                {
                    if (usuario.Id > 0)
                    {
                        result = AgendaServicio.Business.Common.Usuario.GetUsuarioPorId(ConnectionString, usuario);
                        return PartialView(
                            ((List<AgendaServicio.Entities.Common.Usuario>)result.Collection).FirstOrDefault()
                        );
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                else
                {
                    return PartialView();
                }
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ContenidoEditarCompania(AgendaServicio.Entities.Common.Compania compania)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                if (compania != null)
                {
                    if (compania.Id > 0)
                    {
                        result = AgendaServicio.Business.Common.Compania.GetCompaniaPorId(ConnectionString, compania.Id);
                        return PartialView(
                            ((List<AgendaServicio.Entities.Common.Compania>)result.Collection).FirstOrDefault()
                        );
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                else
                {
                    return PartialView();
                }
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public PartialViewResult ContenidoEditarRol(AgendaServicio.Entities.Common.Rol rol)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlCollectionResult result = null;
            try
            {
                if (rol != null)
                {
                    if (rol.Id > 0)
                    {
                        result = AgendaServicio.Business.Common.Rol.GetRolPorId(ConnectionString, rol.Id);
                        return PartialView(
                            ((List<AgendaServicio.Entities.Common.Rol>)result.Collection).FirstOrDefault()
                        );
                    }
                    else
                    {
                        return PartialView();
                    }
                }
                else
                {
                    return PartialView();
                }
            }
            finally
            {
                result = null;
            }
        }

        [HttpPost]
        public JsonResult EditarIngenieroSucursal(AgendaServicio.Entities.Common.Ingeniero ingeniero)
        {
            return Json(AgendaServicio.Business.Common.Ingeniero.UpdateIngenieroSucursal(ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString, ingeniero.Id.ToString(), ingeniero.Sucursal.Id));
        }

        [HttpPost]
        public JsonResult EditarUsuario(AgendaServicio.Entities.Common.Usuario usuario)
        {
            if (usuario != null)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
                return Json(AgendaServicio.Business.Common.Usuario.UpdateUsuario(ConnectionString, usuario));
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        public JsonResult EditarCompania(AgendaServicio.Entities.Common.Compania compania)
        {
            if (compania != null)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
                return Json(AgendaServicio.Business.Common.Compania.UpdateCompania(ConnectionString, compania.Id, compania.Name));
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        public JsonResult EditarRol(AgendaServicio.Entities.Common.Rol rol)
        {
            if (rol != null)
            {
                string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
                return Json(AgendaServicio.Business.Common.Rol.UpdateRol(ConnectionString, rol.Id, rol.Name));
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        public JsonResult EliminarUsuario(List<AgendaServicio.Entities.Common.Usuario> usuarios)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlChangesResult result = null;
            if (usuarios != null)
            {
                int contador = 0;
                for (int x = 0; x < usuarios.Count; x++)
                {
                    result = AgendaServicio.Business.Common.Usuario.RemoveUsuario(ConnectionString, usuarios.ElementAt(x));
                    if (!result.HasError)
                    {
                        contador++;
                    }
                    result = null;
                }
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = contador == 0, Message = contador == 0 ? "No se ha realizado la operación." : "Se han procesado " + contador + " de " + usuarios.Count + " registros.", RowsAffected = contador });
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        public JsonResult EliminarCompania(List<AgendaServicio.Entities.Common.Compania> companias)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlChangesResult result = null;
            if (companias != null)
            {
                int contador = 0;
                for (int x = 0; x < companias.Count; x++)
                {
                    result = AgendaServicio.Business.Common.Compania.RemoveCompania(ConnectionString, companias.ElementAt(x).Id);
                    if (!result.HasError)
                    {
                        contador++;
                    }
                    result = null;
                }
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = contador == 0, Message = contador == 0 ? "No se ha realizado la operación." : "Se han procesado " + contador + " de " + companias.Count + " registros.", RowsAffected = contador });
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        public JsonResult EliminarRol(List<AgendaServicio.Entities.Common.Rol> roles)
        {
            string ConnectionString = ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString;
            AgendaServicio.Entities.Tools.SqlChangesResult result = null;
            if (roles != null)
            {
                int contador = 0;
                for (int x = 0; x < roles.Count; x++)
                {
                    result = AgendaServicio.Business.Common.Rol.RemoveRol(ConnectionString, roles.ElementAt(x).Id);
                    if (!result.HasError)
                    {
                        contador++;
                    }
                    result = null;
                }
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = contador == 0, Message = contador == 0 ? "No se ha realizado la operación." : "Se han procesado " + contador + " de " + roles.Count + " registros.", RowsAffected = contador });
            }
            else
            {
                return Json(new AgendaServicio.Entities.Tools.SqlChangesResult() { HasError = true, Message = "Los datos que se intentan procesar están nulos.", RowsAffected = 0 });
            }
        }

        [HttpPost]
        [ValidateInput(false)]
        public JsonResult GuardarCambiosPlantillaCorreo()
        {
            long logId = 0;
            string empresa = "HaasCnc";
            string username = string.Empty;
            AgendaServicio.Entities.Common.Usuario usuario = Models.Usuario.Obtener();
            if (usuario != null)
            {
                username = usuario.UserName;
            }
            if (!string.IsNullOrEmpty(username))
            {
                logId = LogTools.RegisterLog(0, username, empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "Se está modificando el formato de correo de " + Pais, DateTime.Now);
                string html = Request.Unvalidated.Form["content"];
                if (!string.IsNullOrEmpty(html))
                {
                    html = @"<html xmlns:v=""urn:schemas-microsoft-com:vml""
                         xmlns:o=""urn:schemas-microsoft-com:office:office""
                         xmlns:w=""urn:schemas-microsoft-com:office:word""
                         xmlns:m=""http://schemas.microsoft.com/office/2004/12/omml""
                         xmlns=""http://www.w3.org/TR/REC-html40"">
                         <head>
                             <meta http-equiv=Content-Type content=""text/html; charset=windows-1252"">
                             <meta name=ProgId content=Word.Document>
                             <meta name=Generator content=""Microsoft Word 15"">
                             <meta name=Originator content=""Microsoft Word 15"">
                         </head>
                         <body lang=ES-MX link=blue vlink=""#954F72"" style='tab-interval:35.4pt'>" +
                                 html +
                             @"</body>
                     </html>";
                    html = html.Replace(Url.Content("~/Content/CorreoLlamada"), "~/CorreoLlamada");
                    string path = Server.MapPath("~/PlantillasCorreoLlamada/" + Pais + "/Correo.html");
                    try
                    {
                        System.IO.File.Delete(path);
                        System.IO.File.WriteAllText(path, html);
                        logId = LogTools.RegisterLog(logId, username, empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "Se ha modificado el formato de correo de " + Pais, DateTime.Now);
                        return Json(new { HasError = false, Message = string.Empty });
                    }
                    catch (Exception ex)
                    {
                        logId = LogTools.RegisterLog(logId, username, empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No se ha modificado el formato de correo de " + Pais + ": " + ex.Message, DateTime.Now);
                        return Json(new { HasError = true, Message = ex.Message });
                    }
                    finally
                    {
                        html = null;
                        path = null;
                    }
                }
                else
                {
                    html = null;
                    logId = LogTools.RegisterLog(logId, username, empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No se ha modificado el formato de correo de " + Pais, DateTime.Now);
                    return Json(new { HasError = true, Message = "No se obtuvo el contenido HTML." });
                }
            }
            else
            {
                logId = LogTools.RegisterLog(logId, username, empresa, MethodBase.GetCurrentMethod().DeclaringType.Name, MethodBase.GetCurrentMethod().Name, "No se ha modificado el formato de correo de " + Pais, DateTime.Now);
                return Json(new { HasError = true, Message = "Usuario no definido." });
            }
        }
	}
}