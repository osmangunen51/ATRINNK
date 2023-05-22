using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class StoreActivityCategoryService : IStoreActivityCategoryService
    {
        private readonly IRepository<StoreActivityCategory> _storeActivityCategoryRepository;

        public StoreActivityCategoryService(IRepository<StoreActivityCategory> storeActivityCategoryRepository)
        {
            this._storeActivityCategoryRepository = storeActivityCategoryRepository;
        }

        public void DeleteStoreActivityCategory(StoreActivityCategory storeActivtyCategory)
        {
            if (storeActivtyCategory == null)
                throw new ArgumentNullException("storeActivityCategory");

            _storeActivityCategoryRepository.Delete(storeActivtyCategory);
        }

        public List<StoreActivityCategory> GetStoreActivityCategoriesByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _storeActivityCategoryRepository.Table;
            query = query.Include(x => x.Category);
            query = query.Where(x => x.MainPartyId == mainPartyId);
            return query.ToList();
        }

        public StoreActivityCategory GetStoreActivityCategoryByStoreActivityCategoryId(int storeActivityCategoryId)
        {
            if (storeActivityCategoryId == 0)
                throw new ArgumentNullException("storeActivityCategoryId");

            var query = _storeActivityCategoryRepository.Table;
            query = query.Include(x => x.Category);
            return query.FirstOrDefault(x => x.StoreActivityCategoryId == storeActivityCategoryId);

        }

        public void InsertStoreActivityCategory(StoreActivityCategory storeActivtyCategory)
        {
            if (storeActivtyCategory == null)
                throw new ArgumentNullException("storeActivityCategory");

            _storeActivityCategoryRepository.Insert(storeActivtyCategory);

        }

        public void UpdateStoreActivityCategory(StoreActivityCategory storeActivtyCategory)
        {
            if (storeActivtyCategory == null)
                throw new ArgumentNullException("storeActivityCategory");

            _storeActivityCategoryRepository.Update(storeActivtyCategory);
        }
    }
}
