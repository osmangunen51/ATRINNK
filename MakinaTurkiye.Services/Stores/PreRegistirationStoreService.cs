using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class PreRegistirationStoreService : IPreRegistirationStoreService
    {
        IRepository<PreRegistrationStore> _preRegistrationStoreRepository;
        IRepository<Store> _StoreRepository;
        IRepository<Phone> _PhoneRepository;

        public PreRegistirationStoreService(IRepository<PreRegistrationStore> repository, IRepository<Phone> PhoneRepository,IRepository<Store> StoreRepository)
        {
            this._preRegistrationStoreRepository = repository;
            this._StoreRepository = StoreRepository;
            this._PhoneRepository = PhoneRepository;
        }

        public void DeletePreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Delete(preRegistirationStore);
        }

        public PreRegistrationStore GetPreRegistirationStoreByPreRegistrationStoreId(int preRegistraionStoreId)
        {
            if (preRegistraionStoreId == 0)
                throw new ArgumentNullException("preRegistraionId");
            var query = _preRegistrationStoreRepository.Table;
            return query.FirstOrDefault(x => x.PreRegistrationStoreId == preRegistraionStoreId);
        }

        public IPagedList<PreRegistrationStore> GetPreRegistirationStores(int page, int pageSize, string storeName, string email)
        {
            int totalRecord = 0;
            var query = _preRegistrationStoreRepository.Table;
            totalRecord = query.Count();
            if (!string.IsNullOrEmpty(storeName))
            {
                query = query.Where(x => x.StoreName.ToLower().Contains(storeName.ToLower()));
            }
            if (!string.IsNullOrEmpty(email))
            {
                query = query.Where(x => x.Email.ToLower().Contains(email.ToLower()));
            }
            query = query.OrderByDescending(x => x.PreRegistrationStoreId).Skip(page * pageSize - pageSize).Take(pageSize);
            var preRegistrationStores = query.ToList();
            return new PagedList<PreRegistrationStore>(preRegistrationStores, page, pageSize, totalRecord);

        }


        public IList<Store> GetPreRegistrationStoreSearchByPhone(params string[] Phones)
        {
            Phones = Phones.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            for (int Don = 0; Don < Phones.Length; Don++)
            {
                Phones[Don] = Phones[Don].Replace(" ", "");
            }
            List<int> vs = new List<int>();
            var query = _PhoneRepository.Table;
            foreach (string phone in Phones)
            {
                var tmpquery = query.Where(x => Phones.Contains(phone));
                if (tmpquery.Count() > 0)
                { 
                    vs.AddRange(tmpquery.Select(x=>(int)x.MainPartyId).ToList());
                }
            }
            var querystore = _StoreRepository.Table;
            querystore = querystore.Where(x => vs.Contains(x.MainPartyId));
            return querystore.ToList();
        }

        public IList<PreRegistrationStore> GetPreRegistrationStoreSearchByName(string storeName)
        {
            var query = _preRegistrationStoreRepository.Table;
            query = query.Where(x => x.StoreName.ToLower().Contains(storeName.ToLower()));
            return query.ToList();
        }

        public void InsertPreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Insert(preRegistirationStore);
        }

        public void UpdatePreRegistrationStore(PreRegistrationStore preRegistirationStore)
        {
            if (preRegistirationStore == null)
                throw new ArgumentNullException("preRegistirationStore");
            _preRegistrationStoreRepository.Update(preRegistirationStore);
        }
    }
}
