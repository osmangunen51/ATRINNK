using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class StoreStatisticService:IStoreStatisticService
    {
        private readonly IRepository<StoreStatistic> _storeProductStatisticRepository;
        private readonly IDataProvider _dataProvider;
        private readonly IDbContext _dbContext;

        public StoreStatisticService(IRepository<StoreStatistic> storeProductStatisticRepository,
            IDbContext dbContext,
            IDataProvider dataProvider)
        {
            this._storeProductStatisticRepository = storeProductStatisticRepository;
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
        }

        public void DeleteStoreStatistic(StoreStatistic storeProductStatistic)
        {
            if (storeProductStatistic == null)
                throw new ArgumentNullException("storeStatistic");

            _storeProductStatisticRepository.Delete(storeProductStatistic);
        }

        public StoreStatistic GetStoreStatisticByIpAndStoreIdAndRecordDate(int storeId, string ipAdress, DateTime recordDate)
        {
            if (storeId == 0)
                throw new ArgumentNullException("storeId");

            var query = _storeProductStatisticRepository.Table;


            var q = from s in query where DbFunctions.TruncateTime(s.RecordDate) == recordDate.Date && s.StoreId == storeId && s.UserIp == ipAdress select s;
            return q.FirstOrDefault();

        }

        public List<StoreStatistic> GetStoreStatisticsByStoreIdAndDates(int storeId, DateTime startDate, DateTime LastDate)
        {


            if (storeId == 0)
                throw new ArgumentNullException("storeId");

            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "StoreId";
            pMainPartyId.Value = storeId;
            pMainPartyId.DbType = DbType.Int32;

            var pBeginDate = _dataProvider.GetParameter();
            pBeginDate.ParameterName = "BeginDate";
            pBeginDate.Value = startDate.Date;
            pBeginDate.DbType = DbType.Date;

            var pEndDate = _dataProvider.GetParameter();
            pEndDate.ParameterName = "EndDate";
            pEndDate.Value = LastDate.Date;
            pEndDate.DbType = DbType.Date;
            
            var storeStatistics = _dbContext.SqlQuery<StoreStatistic>("SP_GetStoreStatisticsByStoreIdAndDates @StoreId, @BeginDate, @EndDate", pMainPartyId, pBeginDate, pEndDate).ToList();
            return storeStatistics;
        }
        
        public void InsertStoreStatistic(StoreStatistic storeProductStatistic)
        {
            if (storeProductStatistic == null)
                throw new ArgumentNullException("storeStatistic");

             _storeProductStatisticRepository.Insert(storeProductStatistic);
            
        }

        public void UpdateStoreStatistic(StoreStatistic storeProductStatistic)
        {
            if (storeProductStatistic == null)
                throw new ArgumentNullException("storeStatistic");

            _storeProductStatisticRepository.Update(storeProductStatistic);
        }
    }
}
