﻿using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IPreRegistirationStoreService
    {
        void InsertPreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void DeletePreRegistrationStore(PreRegistrationStore preRegistirationStore);
        void UpdatePreRegistrationStore(PreRegistrationStore preRegistirationStore);

        IPagedList<PreRegistrationStore> GetPreRegistirationStores(int page, int pageSize,string storeName, string email);
       
        PreRegistrationStore GetPreRegistirationStoreByPreRegistrationStoreId(int preRegistraionStoreId);

        IList<PreRegistrationStore> GetPreRegistrationStoreSearchByName(string storeName);
    }
}
