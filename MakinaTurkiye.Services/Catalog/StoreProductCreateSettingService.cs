using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
   public class StoreProductCreateSettingService: IStoreProductCreateSettingService
    {
        IRepository<StoreProductCreateSetting> _storeProductCreateSettingRepository;

        IRepository<StoreProductCreatePropertie> _storeProduductCreatePropertie;

        public StoreProductCreateSettingService(IRepository<StoreProductCreateSetting> storeProductCreateSettingRepository,
                 IRepository<StoreProductCreatePropertie> storeProduductCreatePropertie)
        {
            this._storeProductCreateSettingRepository = storeProductCreateSettingRepository;
            this._storeProduductCreatePropertie = storeProduductCreatePropertie;

        }

        public void DeleteStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting)
        {
            if (storeProductCreateSetting == null)
                throw new ArgumentNullException("storeProductCreateSetting");
            _storeProductCreateSettingRepository.Delete(storeProductCreateSetting);
        }

        public StoreProductCreatePropertie GetStoreProductCreatePropertieById(int id)
        {
            if (id == 0)
                throw new ArgumentNullException("id");
            var query = _storeProduductCreatePropertie.Table;
            return query.FirstOrDefault(x => x.StoreProductCreatePropertieId == id);
        }

        public List<StoreProductCreatePropertie> GetStoreProductCreateProperties()
        {
            var query = _storeProduductCreatePropertie.Table;
            return query.ToList();
        }

        public List<StoreProductCreateSetting> GetStoreProductCreateSettingsByStoreMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartyId");
            var query = _storeProductCreateSettingRepository.Table;
            return query.Where(x => x.StoreMainPartyId == storeMainPartyId).ToList();
        }

        public void InsertStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting)
        {
            if (storeProductCreateSetting == null)
                throw new ArgumentNullException("storeProductCreateSetting");
            _storeProductCreateSettingRepository.Insert(storeProductCreateSetting);
        }

        public void UpdateStoreProductCreateSetting(StoreProductCreateSetting storeProductCreateSetting)
        {
            if (storeProductCreateSetting == null)
                throw new ArgumentNullException("storeProductCreateSetting");
            _storeProductCreateSettingRepository.Update(storeProductCreateSetting);

        }
    }
}
