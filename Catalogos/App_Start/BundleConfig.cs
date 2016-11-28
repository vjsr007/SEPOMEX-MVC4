using System.Web;
using System.Web.Optimization;

namespace Catalogos
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/Libs/jquery-{version}.js",
                        "~/Scripts/Libs/jquery.validate.js",
                        "~/Scripts/Libs/jquery.validate.unobtrusive.js",
                        "~/Scripts/Libs/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/Libs/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/utilerias").Include(
                "~/Scripts/Utils.js",
                "~/Scripts/App.js",
                "~/Scripts/AppData.js"));

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/Libs/angular.js",
                        "~/Scripts/Libs/underscore.js"));

            bundles.Add(new ScriptBundle("~/bundles/html5shiv").Include(
                        "~/Scripts/Libs/html5shiv.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/Libs/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenujs").Include(
                        "~/Scripts/Libs/metisMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/customtheme").Include(
                        "~/Scripts/Libs/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                        "~/Scripts/Libs/jquery.jqGrid.src.js",
                        "~/Scripts/Libs/i18n/grid.locale-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                        "~/Scripts/Libs/jquery.blockUI.js"));


            bundles.Add(new StyleBundle("~/Content/css").Include("~/Content/Site.css"));

            bundles.Add(new StyleBundle("~/Content/bootstrap").Include("~/Content/bootstrap.css"));
            bundles.Add(new StyleBundle("~/Content/metisMenu").Include("~/Content/metisMenu.css"));
            bundles.Add(new StyleBundle("~/Content/custom").Include("~/Content/sb-admin-2.css"));
            bundles.Add(new StyleBundle("~/Content/font-awesome").Include("~/Content/font-awesome.css"));

            bundles.Add(new StyleBundle("~/Content/theme").Include(
                        "~/Content/jquery.ui.1.10.3.ie.css",
                        "~/Content/jquery-ui-1.10.3.custom.css",
                        "~/Content/jquery-ui-1.10.3.theme.css"));

            bundles.Add(new StyleBundle("~/Content/jqgridCss").Include("~/Content/ui.jqgrid.css"));
        }
    }
}