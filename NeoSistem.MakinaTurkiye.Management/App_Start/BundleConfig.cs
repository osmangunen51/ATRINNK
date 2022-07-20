using System.Web.Optimization;


namespace NeoSistem.MakinaTurkiye.Management.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
                var BunddleSystem = new ScriptBundle("~/js-system").Include(
                     // "~/Content/Scripts/jquery-1.8.3.js",
                     "~/Content/Scripts/jquery.unobtrusive-ajax.min.js",
                     "~/Content/Scripts/MicrosoftAjax.js",
                     "~/Content/Scripts/MicrosoftMvcAjax.js",
                     "~/Content/Scripts/MicrosoftMvcValidation.js",
                     "~/Content/Scripts/NeoSistem.js",
                     "~/Content/Scripts/jquery.js",
                     "~/Content/Scripts/jquery-ui.js",
                     "~/Content/Scripts/jquery-ui.datepicker-tr.js",
                     "~/Content/Scripts/jquery.cookie.js",
                     "~/Content/Scripts/JQuery-qtip.js",
                     "~/Content/Scripts/JQuery.cookie.js",
                     "~/Content/Scripts/Ribbon.js",
                     "~/Content/Scripts/jquery-ui-1.8.8.custom.min.js",
                    "~/Content/Scripts/jquery.treeview.js",
                     "~/Content/Scripts/jquery.treeview.async.js",
                     "~/Content/Scripts/jquery.treeview.edit.js",
                     "~/Content/Scripts/jquery.contextmenu.js",
                     "~/Content/Scripts/jquery.validate.js",
                     "~/Content/Scripts/jquery.metadata.js",
                     "~/Content/Scripts/SuperBox/jquery.superbox.js",
                     "~/Content/Scripts/CKEditor/ckeditor.js",
                     "~/Content/Scripts/CKFinder/ckfinder.js"
                );

            //BunddleSystem.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(BunddleSystem);

            var BunddleVendor = new ScriptBundle("~/js-vendor").Include(
                   "~/Content/vendor/bootstrap-sweetalert/sweetalert.js",
                   "~/Content/vendor/fontawesome/js/all.js",
                   "~/Content/vendor/clipboard.js",
                   "~/Content/Scripts/system.js"
                );

            // BunddleVendor.Orderer = new NonOrderingBundleOrderer();

                bundles.Add(BunddleVendor);
                var cssbundle = new StyleBundle("~/css").Include
                (
                    "~/Content/Site.css",
                    "~/Content/Ribbon.css",
                    "~/Content/qtip.css",
                    "~/Content/jquery.treeview.css",
                    "~/Content/jquery.contextmenu.css",
                    "~/ContentScripts/SuperBox/jquery.superbox.css",
                    "~/Content/smoothness/jquery-ui.css",
                    "~/Content/vendor/bootstrap-sweetalert/sweetalert.css",
                    "~/Content/vendor/fontawesome/css/all.css",
                    "~/Content/screen.css"
                );

            // cssbundle.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(cssbundle);
            bundles.UseCdn = true;
            #if DEBUG
                BundleTable.EnableOptimizations = false;
            #else
                BundleTable.EnableOptimizations = false;
            #endif
        }
    }
}