using System;

namespace Trinnk.Entities.StoredProcedures.Catalog
{
    public class MostViewProductResult
    {
        public int ProductId { get; set; }
        public string CurrencyCodeName { get; set; }
        public decimal? ProductPrice { get; set; }
        public string ProductName { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string MainPicture { get; set; }
        public string StoreName { get; set; }
        public byte? ProductPriceType { get; set; }
        public decimal? ProductPriceLast { get; set; }
        public decimal? ProductPriceBegin { get; set; }

        public DateTime ProductRecodrtDate { get; set; } = default;
        public int ViewCount { get; set; } = default;

    }
}
