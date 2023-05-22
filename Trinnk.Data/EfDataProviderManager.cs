using Trinnk.Core;
using Trinnk.Core.Data;
using System;

namespace Trinnk.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager() : base()
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = "sqlserver";
            if (String.IsNullOrWhiteSpace(providerName))
                throw new TrinnkException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                //case "sqlce":
                //    return new SqlCeDataProvider();
                default:
                    throw new TrinnkException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
