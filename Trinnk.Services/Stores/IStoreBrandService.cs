using Trinnk.Entities.Tables.Stores;
using System.Collections.Generic;

namespace Trinnk.Services.Stores
{
    public interface IStoreBrandService : ICachingSupported
    {
        StoreBrand GetStoreBrandByStoreBrand(int storeBrandId);

        IList<StoreBrand> GetStoreBrandByMainPartyId(int mainPartyId);

        void InsertStoreBrand(StoreBrand storeBrand);

        void DeleteStoreBrand(StoreBrand storeBrand);
    }
}
