using System.Web;
using System.Web.Optimization;

namespace Catalogos
{
    public class BundleConfig
    {
        // For more information on Bundling, visit http://go.microsoft.com/fwlink/?LinkId=254725
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js",
                        "~/Scripts/jquery.validate.js",
                        "~/Scripts/jquery.validate.unobtrusive.js",
                        "~/Scripts/jquery.unobtrusive-ajax.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryui").Include(
                        "~/Scripts/jquery-ui.js"));

            bundles.Add(new ScriptBundle("~/bundles/utilerias").Include(
                        "~/Js/App.js",
                        "~/Js/Utils.js"));

            bundles.Add(new ScriptBundle("~/bundles/html5shiv").Include(
                        "~/Scripts/html5shiv.js"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrapjs").Include(
                        "~/Scripts/bootstrap.js"));

            bundles.Add(new ScriptBundle("~/bundles/metisMenujs").Include(
                        "~/Scripts/metisMenu.js"));

            bundles.Add(new ScriptBundle("~/bundles/customtheme").Include(
                        "~/Scripts/sb-admin-2.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqgrid").Include(
                        "~/Scripts/jquery.jqGrid.src.js",
                        "~/Scripts/i18n/grid.locale-es.js"));

            bundles.Add(new ScriptBundle("~/bundles/blockui").Include(
                        "~/Scripts/jquery.blockUI.js"));

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

            bundles.Add(new ScriptBundle("~/bundles/angular").Include(
                        "~/Scripts/angular.js",
                        "~/Scripts/underscore.js"));
        }
    }
}