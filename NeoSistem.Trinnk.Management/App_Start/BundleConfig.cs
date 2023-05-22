using System.Web.Optimization;


namespace NeoSistem.Trinnk.Management.App_Start
{
    public class BundleConfig
    {
        public static void RegisterBundles(BundleCollection bundles)
        {
                var BunddleSystem = new ScriptBundle("~/js-system").Include(
                     

                );

            BunddleSystem.Orderer = new NonOrderingBundleOrderer();
            bundles.Add(BunddleSystem);

            var BunddleVendor = new ScriptBundle("~/js-vendor").Include(
                  
                );

            // BunddleVendor.Orderer = new NonOrderingBundleOrderer();

                bundles.Add(BunddleVendor);
                var cssbundle = new StyleBundle("~/css").Include
                (
                    
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