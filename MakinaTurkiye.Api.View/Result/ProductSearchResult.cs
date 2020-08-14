using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View.Result
{
   public class ProductSearchResult
    {
        public int ProductId { get; set; } = 0;

        public string CurrencyCodeName { get; set; } = "";
        public decimal ProductPrice { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public string BrandName { get; set; } = "";
        public string ModelName { get; set; } = "";
        public string MainPicture { get; set; } = "";
        public string StoreName { get; set; } = "";
        public int MainPartyId { get; set; } = 0;
        public byte ProductPriceType { get; set; }
        public decimal ProductPriceLast { get; set; } = 0;
        public decimal ProductPriceBegin { get; set; } = 0;
    }
}
