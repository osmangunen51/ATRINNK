using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IDealarBrandService : ICachingSupported
    {
        DealerBrand GetDealerBrandByDealerBrandId(int dealerBrandId);
        IList<DealerBrand> GetDealarBrandsByMainPartyId(int mainPartyId);
        void InsertDealerBrand(DealerBrand dealerBrand);
        void UpdateDealerBrand(DealerBrand dealerBrand);

        void DeleteDealerBrand(DealerBrand dealerBrand);
    }
}
