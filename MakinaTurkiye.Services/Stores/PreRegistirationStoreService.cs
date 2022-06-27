using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class PreRegistirationStoreService : IPreRegistirationStoreService
    {
        IRepository<PreRegistrationStore> _preRegistrationStoreRepository;

        public PreRegistirationStoreService(IRepository<PreRegistrationStore> repository)
        {
            this._preRegistrationStoreRepository = repository;
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


        public IList<PreRegistrationStore> GetPreRegistrationStoreSearchByPhone(params string[] Phones)
        {
            Phones = Phones.Where(x => !string.IsNullOrEmpty(x)).ToArray();
            var query = _preRegistrationStoreRepository.Table;
            query = query.Where(x => Phones.Contains(x.PhoneNumber) || Phones.Contains(x.PhoneNumber2) || Phones.Contains(x.PhoneNumber3) || Phones.Contains(x.ContactPhoneNumber));
            return query.ToList();
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
