using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreChangeHistoryService : IStoreChangeHistoryService
    {
        #region Fields
        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly IRepository<StoreChangeHistory> _storeChangeHistoryRepository;

        #endregion

        #region Ctor

        public StoreChangeHistoryService(IRepository<StoreChangeHistory> storeChangeHistoryRepository,
            IDbContext dbContext, IDataProvider dataProvider)
        {
            this._storeChangeHistoryRepository = storeChangeHistoryRepository;
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
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

        public List<StoreChangeInfoResult> SP_StoreInfoChange(int pageSize, int pageIndex, out int totalRecord)
        {


            var pPage = _dataProvider.GetParameter();
            pPage.ParameterName = "PageSize";
            pPage.Value = pageSize;
            pPage.DbType = DbType.Int32;

            var pPageInde = _dataProvider.GetParameter();
            pPageInde.ParameterName = "PageIndex";
            pPageInde.Value = pageIndex;
            pPageInde.DbType = DbType.Int32;

            var pTotalRecords = _dataProvider.GetParameter();
            pTotalRecords.ParameterName = "TotalRecord";
            pTotalRecords.DbType = DbType.Int32;
            pTotalRecords.Direction = ParameterDirection.Output;


            var result = _dbContext.SqlQuery<StoreChangeInfoResult>("SP_StoreInfoChange @PageSize, @PageIndex, @TotalRecord OUTPUT", pPage, pPageInde, pTotalRecords).ToList();

            totalRecord = (pTotalRecords.Value != DBNull.Value) ? Convert.ToInt32(pTotalRecords.Value) : 0;
            return result;

        }

        #endregion
    }
}
