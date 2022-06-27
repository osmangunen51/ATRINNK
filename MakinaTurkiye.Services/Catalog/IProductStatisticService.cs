using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductStatisticService
    {
        List<ProductStatistic> GetProductStatistics(int page, int size);
        int InsertProductStatistic(ProductStatistic productStatistics);
        void UpdateProductStatistic(ProductStatistic productStatistics);
        ProductStatistic GetProductStatisticByStatisticId(int statisticId);
        List<ProductStatistic> GetProductStatisticsByProductId(int productId);
        List<ProductStatistic> GetProductStatisticsByMemberMainPartyId(int memberMainPartyId);
        ProductStatistic GetProductStatisticByProductIdAndIpAdressAndDate(int productId, string ipAdress, DateTime date, int hour);
        List<ProductStatistic> GetProductStatisticsByMemberMainPartyIdAndDate(int memberMainPartyId, DateTime beginDate, DateTime endDate, bool forOneDay = true);

    }
}
