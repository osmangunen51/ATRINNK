using System.Collections.Generic;
using System.Web.Optimization;

namespace NeoSistem.MakinaTurkiye.Management.App_Start
{
    public class NonOrderingBundleOrderer : IBundleOrderer
    {
        public IEnumerable<BundleFile> OrderFiles(BundleContext context, IEnumerable<BundleFile> files)
        {
            return files;
        }
    }
}