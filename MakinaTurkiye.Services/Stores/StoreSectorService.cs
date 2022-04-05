using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreSectorService : IStoreSectorService
    {
        private readonly IRepository<StoreSector> _storeSectorRepository;


        public StoreSectorService(IRepository<StoreSector> storeSectorRepository)
        {
            _storeSectorRepository = storeSectorRepository;
        }

        public void DeleteStoreSector(StoreSector storeSector)
        {
            if (storeSector == null)
                throw new ArgumentNullException("storeSector");

            _storeSectorRepository.Delete(storeSector);
        }

        public StoreSector GetStoreSectorByStoreMainPartyIdAndCategoryId(int mainPartyId, int categoryId)
        {
            if (mainPartyId <= 0)
                throw new ArgumentNullException("mainPartyId");

            if (categoryId <= 0)
                throw new ArgumentNullException("categoryId");

            var query = _storeSectorRepository.Table;
            return query.FirstOrDefault(x => x.CategoryId == categoryId && x.StoreMainPartyId == mainPartyId);
        }

        public StoreSector GetStoreSectorByStoreSectorId(int storeSectorId)
        {
            if (storeSectorId == 0)
                throw new ArgumentNullException("storeSectorId");
            return _storeSectorRepository.Table.FirstOrDefault(x => x.StoreSectorId == storeSectorId);
        }

        public List<StoreSector> GetStoreSectorsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _storeSectorRepository.Table;
            query = query.Where(x => x.StoreMainPartyId == mainPartyId);
            return query.ToList();
        }

        public void InsertStoreSector(StoreSector storeSector)
        {
            if (storeSector == null)
                throw new ArgumentNullException("storeSector");

            _storeSectorRepository.Insert(storeSector);
        }
    }
}
