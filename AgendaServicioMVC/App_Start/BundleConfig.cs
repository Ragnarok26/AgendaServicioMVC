using System.Web;
using System.Web.Optimization;

namespace AgendaServicioMVC
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, consulte http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate.min.js",
                        "~/Scripts/jquery.validate.unobtrusive.min.js"));

            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/respond.js",
                      "~/Scripts/moment.min.js",
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/bootstrap-datetimepicker.min.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/bootstrap-datetimepicker.min.css",
                      "~/Content/site.css",
                      "~/Content/reset.css",
                      "~/Content/style.css"));

            bundles.Add(new ScriptBundle("~/bundles/Modal").Include(
                        "~/Scripts/js/Modal.js"));

            bundles.Add(new ScriptBundle("~/bundles/Principal").Include(
                        "~/Scripts/js/Principal.js"));

            bundles.Add(new ScriptBundle("~/bundles/Login").Include(
                        "~/Scripts/js/Login.js"));

            bundles.Add(new ScriptBundle("~/bundles/Toast").Include(
                        "~/Scripts/jquery-toast-plugin.js"));

            bundles.Add(new StyleBundle("~/Content/Toast").Include(
                      "~/Content/jquery-toast-plugin.css"));
        }
    }
}
