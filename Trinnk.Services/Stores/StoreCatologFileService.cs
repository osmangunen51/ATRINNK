using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Stores
{
    public class StoreCatologFileService : BaseService, IStoreCatologFileService
    {

        #region Constants

        private const string STORECATALOGFILES_BY_STORE_MAIN_PARTY_ID_KEY = "storecatalogfile.bystoremainpartyid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<StoreCatologFile> _storeCatologFileRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public StoreCatologFileService(IRepository<StoreCatologFile> storeCatologFileRepository,
                                       ICacheManager cacheManager) : base(cacheManager)
        {
            this._storeCatologFileRepository = storeCatologFileRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods


        public void DeleteStoreCatologFile(StoreCatologFile storeCatologFile)
        {
            if (storeCatologFile == null)
                throw new ArgumentNullException("storeCatologFile");

            _storeCatologFileRepository.Delete(storeCatologFile);
        }

        public StoreCatologFile GetStoreCatologFileByCatologId(int categologFileId)
        {
            if (categologFileId == 0)
                throw new ArgumentNullException("catlogFileId");

            var query = _storeCatologFileRepository.Table;
            return query.FirstOrDefault(x => x.StoreCatologFileId == categologFileId);
        }

        public void InsertStoreCatologFile(StoreCatologFile storeCatologFile)
        {
            if (storeCatologFile == null)
                throw new ArgumentNullException("storeCatologFile");

            _storeCatologFileRepository.Insert(storeCatologFile);
        }

        public List<StoreCatologFile> StoreCatologFilesByStoreMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartId");

            string key = string.Format(STORECATALOGFILES_BY_STORE_MAIN_PARTY_ID_KEY, storeMainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = _storeCatologFileRepository.Table;
                query = query.Where(x => x.StoreMainPartyId == storeMainPartyId);

                var storeCatalogFiles = query.ToList();
                return storeCatalogFiles;
            });
        }

        public void UpdateStoreCatologFile(StoreCatologFile storeCatologFile)
        {
            if (storeCatologFile == null)
                throw new ArgumentNullException("storeCatologFile");
            _storeCatologFileRepository.Update(storeCatologFile);
        }

        #endregion
    }
}
