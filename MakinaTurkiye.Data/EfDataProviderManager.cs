using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using System;

namespace MakinaTurkiye.Data
{
    public partial class EfDataProviderManager : BaseDataProviderManager
    {
        public EfDataProviderManager(): base()
        {
        }

        public override IDataProvider LoadDataProvider()
        {

            var providerName = "sqlserver";
            if (String.IsNullOrWhiteSpace(providerName))
                throw new MakinaTurkiyeException("Data Settings doesn't contain a providerName");

            switch (providerName.ToLowerInvariant())
            {
                case "sqlserver":
                    return new SqlServerDataProvider();
                //case "sqlce":
                //    return new SqlCeDataProvider();
                default:
                    throw new MakinaTurkiyeException(string.Format("Not supported dataprovider name: {0}", providerName));
            }
        }

    }
}
