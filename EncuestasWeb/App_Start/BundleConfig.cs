using System.Web;
using System.Web.Optimization;

namespace EncuestasWeb
{
    public class BundleConfig
    {
        // For more information on bundling, visit https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at https://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/site.css"));
            bundles.Add(new StyleBundle("~/Content/PluginsCSS").Include(

                  //FUENTE FONTAWESOME
                  "~/Content/Plugins/fontawesome-free-5.15.2/css/all.min.css",

                  //SWEET ALERT
                  "~/Content/Plugins/sweetalert2/css/sweetalert.css",


                   //DATATABLE
                   "~/content/plugins/datatables/css/jquery.datatables.min.css",
                   "~/Content/Plugins/datatables/css/responsive.dataTables.min.css"
                  ));

            bundles.Add(new StyleBundle("~/Content/PluginsJS").Include(

                    //FUENTE FONTAWESOME,
                    "~/Content/Plugins/fontawesome-free-5.15.2/js/all.min.js",


                   //SWEET ALERT
                   "~/Content/Plugins/sweetalert2/js/sweetalert.js",

                   //DATATABLE JS
                   "~/Content/Plugins/datatables/js/jquery.dataTables.min.js",
                   "~/Content/Plugins/datatables/js/dataTables.responsive.min.js",

                   //LOADING OVERLAY
                   "~/Content/Plugins/jquery-loading-overlay/loadingoverlay.min.js",

                   //ECHARTS
                   "~/Content/Plugins/Echarts/js/echarts.js",

                   "~/Scripts/jquery.validate.min.js"
                    ));
        }
    }
}
