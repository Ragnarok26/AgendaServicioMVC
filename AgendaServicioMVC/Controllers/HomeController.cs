using AgendaServicioMVC.Models;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace AgendaServicioMVC.Controllers
{
    [Authorize]
    public class HomeController : Controller
    {
        public static string Pais = ConfigurationManager.AppSettings["HaasCnc.App.Country"];

        [AllowAnonymous]
        public ActionResult Index()
        {
            HttpCookie myCookie = Request.Cookies["UserSettings"];
            JavaScriptSerializer js = null;
            AgendaServicio.Entities.Common.Usuario usuario = null;
            Models.Login login = new Models.Login();
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
            if (usuario != null)
            {
                if (usuario.StartedSesion)
                {
                    return RedirectToAction("Index", "Listado");
                }
                else if (usuario.RememberMe)
                {
                    login.UserName = usuario.UserName;
                    login.Password = usuario.Password;
                }
                else
                {
                    login.UserName = string.Empty;
                    login.Password = string.Empty;
                }
                login.RememberMe = usuario.RememberMe;
                login.StartedSesion = false;
            }
            else
            {
                login.UserName = string.Empty;
                login.Password = string.Empty;
                login.RememberMe = false;
                login.StartedSesion = false;
            }
            return View(login);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult Login(Models.Login model, string returnUrl)
        {
            if (ModelState.IsValid)
            {
                AgendaServicio.Entities.Tools.SqlCollectionResult result = AgendaServicio.Business.Common.Usuario.GetUsuarioPorCredencial(ConfigurationManager.ConnectionStrings[Pais + ".HaasCnc.AgendaServicio"].ConnectionString, new AgendaServicio.Entities.Common.Usuario() { UserName = model.UserName, Password = model.Password });
                if (!result.HasError)
                {
                    if (((List<AgendaServicio.Entities.Common.Usuario>)result.Collection) != null)
                    {
                        Response.Cookies.Remove("UserSettings");
                        HttpCookie myCookie = new HttpCookie("UserSettings");
                        JavaScriptSerializer js = new JavaScriptSerializer();
                        AgendaServicio.Entities.Common.Usuario usuario = ((List<AgendaServicio.Entities.Common.Usuario>)result.Collection).FirstOrDefault();
                        try
                        {
                            usuario.StartedSesion = true;
                            usuario.RememberMe = model.RememberMe;
                            myCookie["UserData"] = js.Serialize(usuario);
                            myCookie.Expires = DateTime.Now.AddYears(1000);
                            ViewBag.Roles = usuario.Roles;
                        }
                        catch
                        {
                            myCookie = null;
                            usuario.StartedSesion = false;
                            ViewBag.Roles = null;
                        }
                        if (myCookie != null)
                        {
                            Response.Cookies.Add(myCookie);
                        }
                        return RedirectToAction("Index", "Listado");
                    }
                }
                ModelState.AddModelError("", "Usuario o contraseña incorrecta.");
            }
            return View("Index", model);
        }

        [HttpPost]
        [AllowAnonymous]
        [ValidateAntiForgeryToken]
        public ActionResult LogOut()
        {
            HttpCookie myCookie = Request.Cookies["UserSettings"];
            JavaScriptSerializer js = null;
            AgendaServicio.Entities.Common.Usuario usuario = null;
            if (myCookie != null)
            {
                Request.Cookies.Remove("UserSettings");
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
            if (usuario != null)
            {
                usuario.StartedSesion = false;
                js = new JavaScriptSerializer();
                myCookie["UserData"] = js.Serialize(usuario);
                myCookie.Expires = DateTime.Now.AddYears(1000);
                Response.Cookies.Add(myCookie);
            }
            return RedirectToAction("Index", "Home");
        }
    }
}