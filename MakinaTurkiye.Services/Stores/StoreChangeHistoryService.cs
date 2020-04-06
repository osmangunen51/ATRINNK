using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreChangeHistoryService : IStoreChangeHistoryService
    {
        #region Fields

        private readonly IRepository<StoreChangeHistory> _storeChangeHistoryRepository;

        #endregion

        #region Ctor

        public StoreChangeHistoryService(IRepository<StoreChangeHistory> storeChangeHistoryRepository)
        {
            this._storeChangeHistoryRepository = storeChangeHistoryRepository;
        }

        #endregion

        #region Methods

        public IList<StoreChangeHistory> GetAllStoreChangeHistory()
        {
            return _storeChangeHistoryRepository.Table.ToList();
        }

        public StoreChangeHistory GetStoreChangeHistoryByStoreChangeHistoryId(int storeChangeHistoryId)
        {
            if (storeChangeHistoryId == 0)
                throw new ArgumentNullException("storeChangeHistoryId");
            var query = _storeChangeHistoryRepository.Table;
            return query.FirstOrDefault(x => x.StoreChangeHistoryId == storeChangeHistoryId);

        }

        public void AddStoreChangeHistory(StoreChangeHistory storeChangeHistory)
        {
            _storeChangeHistoryRepository.Insert(storeChangeHistory);
        }

        public void DeleteStoreChangeHistory(StoreChangeHistory storeChangeHistory)
        {
            _storeChangeHistoryRepository.Delete(storeChangeHistory);
        }

        public void AddStoreChangeHistoryForStore(Store store)
        {
            StoreChangeHistory storeChangeHistory = new StoreChangeHistory();
            storeChangeHistory.MainPartyId = store.MainPartyId;
            storeChangeHistory.StoreName = store.StoreName;
            storeChangeHistory.StoreActiveType = store.StoreActiveType;
            storeChangeHistory.StoreNo = store.StoreNo;
            storeChangeHistory.StoreCapital = store.StoreCapital;
            storeChangeHistory.StoreAbout = store.StoreAbout;
            storeChangeHistory.MersisNo = store.MersisNo;
            storeChangeHistory.StoreLogo = store.StoreLogo;
            storeChangeHistory.OldStoreLogo = store.OldStoreLogo;
            storeChangeHistory.OrderCode = store.OrderCode;
            storeChangeHistory.PacketId = store.PacketId;
            storeChangeHistory.PhilosophyText = store.PhilosophyText;
            storeChangeHistory.GeneralText = store.GeneralText;
            storeChangeHistory.HistoryText = store.HistoryText;
            storeChangeHistory.FounderText = store.FounderText;
            storeChangeHistory.StoreEMail = store.StoreEMail;
            storeChangeHistory.StoreEmployeesCount = store.StoreEmployeesCount;
            storeChangeHistory.StoreEstablishmentDate = store.StoreEstablishmentDate;
            storeChangeHistory.StoreEndorsement = store.StoreEndorsement;
            storeChangeHistory.StorePAcketEndDate = store.StorePacketEndDate;
            storeChangeHistory.StorePicture = store.StorePicture;
            storeChangeHistory.StoreRecordDate = store.StoreRecordDate;
            storeChangeHistory.StoreType = store.StoreType;
            storeChangeHistory.StoreUniqueShortName = store.StoreUniqueShortName;
            storeChangeHistory.StoreWeb = store.StoreWeb;
            storeChangeHistory.TaxNumber = store.TaxNumber;
            storeChangeHistory.TaxOffice = store.TaxOffice;
            storeChangeHistory.MersisNo = store.MersisNo;
            storeChangeHistory.TradeRegistrNo = store.TradeRegistrNo;
            storeChangeHistory.UpdatedDated = DateTime.Now;

            _storeChangeHistoryRepository.Insert(storeChangeHistory);
        }

        #endregion
    }
}
