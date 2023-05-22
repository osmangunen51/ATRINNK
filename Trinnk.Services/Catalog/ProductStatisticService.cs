using Trinnk.Data;
using Trinnk.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;

namespace Trinnk.Services.Catalog
{
    public class ProductStatisticService : IProductStatisticService
    {
        private readonly ProductStatisticRepository _productStatisticRepository;


        public ProductStatisticService(ProductStatisticRepository productStatisticRepository)
        {
            this._productStatisticRepository = productStatisticRepository;
        }

        public List<ProductStatistic> GetProductStatistics(int page, int size)
        {
            var query = _productStatisticRepository.FindAll().OrderByDescending(x => x.idpos).Skip((page * size) - size).Take(size);
            return query.ToList();
        }

        public ProductStatistic GetProductStatisticByProductIdAndIpAdressAndDate(int productId, string ipAdress, DateTime date, int hour)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");


            var query = _productStatisticRepository.FindAll().Where(x => x.IpAdress == ipAdress && x.ProductId == productId && x.Hour == hour && x.RecordDate.Date == date.Date);

            return query.FirstOrDefault();
        }

        public ProductStatistic GetProductStatisticByStatisticId(int statisticId)
        {
            var query = _productStatisticRepository.FindByID(statisticId);
            return query;

        }


        public List<ProductStatistic> GetProductStatisticsByMemberMainPartyId(int memberMainPartyId)
        {
            var query = _productStatisticRepository.GetProductStatisticByMemberMainPartyID(memberMainPartyId);
            return query;
        }


        public List<ProductStatistic> GetProductStatisticsByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");

            var query = _productStatisticRepository.GetProductStatisticsByProductId(productId);
            return query.ToList();
        }

        public List<ProductStatistic> GetProductStatisticsByMemberMainPartyIdAndDate(int memberMainPartyId, DateTime beginDate, DateTime endDate, bool forOneDay)
        {
            /*
            var pMainPartyId = _dataProvider.GetParameter();
            pMainPartyId.ParameterName = "MemberMainPartyId";
            pMainPartyId.Value = memberMainPartyId;
            pMainPartyId.DbType = DbType.Int32;

            var pBeginDate = _dataProvider.GetParameter();
            pBeginDate.ParameterName = "BeginDate";
            pBeginDate.Value = beginDate.Date;
            pBeginDate.DbType = DbType.Date;

            var pEndDate = _dataProvider.GetParameter();
            pEndDate.ParameterName = "EndDate";
            pEndDate.Value = endDate.Date;
            pEndDate.DbType = DbType.Date;


            var pForOneDay = _dataProvider.GetParameter();
            pForOneDay.ParameterName = "ForOneDay";
            pForOneDay.DbType = DbType.Boolean;
            pForOneDay.Value = forOneDay;

            var productStatistics = _dbContext.SqlQuery<ProductStatistic>("SP_GetProductStatisticsByMemberMainPartyIdAndDates @MemberMainPartyId, @BeginDate, @EndDate, @ForOneDay", pMainPartyId, pBeginDate, pEndDate, pForOneDay).ToList();
            return productStatistics;
   */

            return _productStatisticRepository.GetProductStatisticsForReport(memberMainPartyId, beginDate, endDate, forOneDay);

        }

        public int InsertProductStatistic(ProductStatistic productStatistic)
        {
            if (productStatistic == null)
                throw new ArgumentNullException("productStatistic");

            return _productStatisticRepository.Add(productStatistic);

        }

        public void UpdateProductStatistic(ProductStatistic productStatistic)
        {
            if (productStatistic == null)
                throw new ArgumentNullException("productStatistic");

            _productStatisticRepository.Update(productStatistic);
        }
    }
}
