using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Api.View
{

    public class AdvertItemPicture
    {
        public int PictureId { get; set; } = 0;
        public string Value { get; set; }
        public bool IsMain { get; set; } = false;
    }
    public class Advert
    {
        public int CategoryId { get; set; } = 0;
        public int ProductGroupId { get; set; } = 0;
        public string ProductGroup { get; set; } = "";

        public int BrandId { get; set; } = 0;
        public string Brand { get; set; } = "";
        public string BrandType { get; set; } = "";

        public int ModelId { get; set; } = 0;
        public string Model { get; set; } = "";

        public int ProductId { get; set; }=0;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public string ProductType { get; set; } = "";
        public string SalesType { get; set; }
        public int Order { get; set; } = 0;
        public string ProductTypeText { get; set; } = "";
        public bool Active { get; set; } = false;
        public byte ActiveType { get; set; } = 0;
        public int? Mensei { get; set; } = 0;
        public string Price { get; set; } = "";
        public string SalesTypeText { get; set; }
        public string TypeText { get; set; } = "";
        public string Picture { get; set; } = "";
        public string Statu { get; set; } = "";
        public string StatuText { get; set; } ="";
        public string ModelName { get; set; } = "";
        public string ModelYear { get; set; } = "";
        public string SeriesName { get; set; } = "";
        public byte CurrencyId { get; set; }
        public string CurrencyText { get; set; }
        public bool Doping { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public long ViewCount { get; set; }
        public int CityId { get; set; } = 0;
        public string City { get; set; }
        public int LocalityId { get; set; } = 0;
        public string Locality { get; set; }
        public string BriefDetail { get; set; }
        public string BriefDetailText { get; set; }
        public int CountryId { get; set; } = 0;
        public string Country { get; set; }
        public int TownId { get; set; } = 0;
        public string Town { get; set; }
        public bool Fob { get; set; } =false;
        public bool Kdv { get; set; } = false;
        public string UnitType { get; set; } = "";
        public DateTime ProductAdvertBeginDate { get; set; }
        public DateTime ProductAdvertEndDate { get; set; }
        public List<AdvertItemPicture> Pictures = new List<AdvertItemPicture>();
        public short ProductPublicationDate { get; set; }
        public decimal ProductPriceLast { get; set; } = 0;
        public decimal ProductPriceBegin { get; set; } = 0;
        public ProductPriceType ProductPriceType { get; set; } = ProductPriceType.Price;
        public ProductPublicationDateType ProductPublicationDateType { get; set; } = ProductPublicationDateType.Gun;
        public byte DiscountType { get; set; } = 0;
        public decimal DiscountAmount { get; set; } = 0;
        public decimal TotalPrice { get; set; } = 0;
        public string ProductSalesType { get; set; } = "";
        public string WarrantyPeriod { get; set; } = "";
        public int OrderStatus { get; set; } = 0;
        public byte? MinumumAmount { get; set; } = 0;
    }
}
