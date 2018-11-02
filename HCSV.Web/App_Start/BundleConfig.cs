using System.Web;
using System.Web.Optimization;

namespace HCSV.Web
{
    public class BundleConfig
    {
        // For more information on bundling, visit http://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Content/TcsTemplate/mysite/javascript/jquery.tools.min.js",
                        "~/Content/TcsTemplate/mysite/javascript/tooltip.js",
                        "~/Content/TcsTemplate/mysite/javascript/superfish.js",
                        "~/Content/TcsTemplate/mysite/javascript/jquery.ui.core.js",
                        "~/Content/TcsTemplate/mysite/javascript/jquery.ui.widget.js",
                        "~/Content/TcsTemplate/mysite/javascript/jquery.ui.accordion.js",
                        "~/Content/TcsTemplate/mysite/javascript/jScrollPane.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            // Use the development version of Modernizr to develop with and learn from. Then, when you're
            // ready for production, use the build tool at http://modernizr.com to pick only the tests you need.
            bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
                        "~/Scripts/modernizr-*"));

            bundles.Add(new ScriptBundle("~/bundles/bootstrap").Include(
                      "~/Scripts/bootstrap.js",
                      "~/Scripts/respond.js"));

            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/bootstrap.css",
                      "~/Content/TcsTemplate/mysite/css/prettyPhoto7d5c.css",
                      "~/Content/TcsTemplate/themes/orange/css/scroll4427.css",
                      "~/Content/TcsTemplate/themes/orange/css/mainae55.css",
                      "~/Content/TcsTemplate/themes/orange/css/superfishb322.css",
                      "~/Content/TcsTemplate/themes/orange/css/tabs8f80.css",
                      "~/Content/TcsTemplate/themes/orange/css/jquery.ui.accordion9932.css",
                      "~/Content/TcsTemplate/themes/orange/css/jScrollPane5bc9.css",
                      "~/Content/TcsTemplate/themes/orange/css/awb_main.css",
                      "~/Content/TcsTemplate/awb/report_tcs/common/css/base.css",
                      "~/Content/TcsNewTemplate/css/default.css"));
        }
    }
}
