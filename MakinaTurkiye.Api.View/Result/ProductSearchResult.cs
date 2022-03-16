using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View.Result
{

    public class Coordinate
    {
        public string latitude { get; set; } = "0";
        public string longitude { get; set; } = "0";
    }



    public class ProductSearchResult
    {
        public int ProductId { get; set; } = 0;
        public int StoreMainPartyId { get; set; } = 0;
        public string CurrencyCodeName { get; set; } = "";
        public decimal ProductPrice { get; set; } = 0;
        public string ProductName { get; set; } = "";
        public string BrandName { get; set; } = "";
        public string ModelName { get; set; } = "";
        public string MainPicture { get; set; } = "";
        public string StoreName { get; set; } = "";
        public string CityName { get; set; } = "";
        public string CountryName { get; set; } = "";
        public string LocalityName { get; set; } = "";

        public string ProductType { get; set; } = "";
        public string ProductStatus { get; set; } ="";
        public string CategoryName { get; set; } = "";

        public string ProductContactUrl { get; set; } = "";
        public string Mensei { get; set; } = "";
        public string DeliveryStatus { get; set; } = "";
        public string MapCode { get; set; } = "";
        public string Location { get; set; } = "";
        public string ShortDescription { get; set; } = "";
        public string SalesDetails { get; set; } = "";
        public List<string> PictureList { get; set; } =new List<string>();
        public int MainPartyId { get; set; } = 0;
        public byte ProductPriceType { get; set; }
        public decimal ProductPriceLast { get; set; } = 0;
        public decimal ProductPriceBegin { get; set; } = 0;
        public string ProductDescription { get; set; } = "";

        public string Storelogo { get; set; } = "";
        public string StorePhone { get; set; } = "";
        public string StoreGsm { get; set; } = "";
        public string StoreBussinesPhone { get; set; } = "";
        public DateTime DatePublished { get; set; }

    }
}

