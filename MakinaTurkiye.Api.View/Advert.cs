using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{

    public class AdvertItemPicture
    {
        public int PictureId { get; set; } = 0;
        public string Value { get; set; }
    }

    public class Advert
    {
        public int ProductId { get; set; }=0;
        public string Name { get; set; } = "";
        public string Description { get; set; } = "";
        public int Order { get; set; } = 0;
        public int CategoryId { get; set; } = 0;
        public string ProductType { get; set; } = "";
        public string ProductTypeText { get; set; } = "";
        public bool Active { get; set; } = false;
        public byte ActiveType { get; set; } = 0;
        public byte Mensei { get; set; } = 0;
        public string Price { get; set; } = "";
        public string SalesType { get; set; }
        public string SalesTypeText { get; set; }
        public string TypeText { get; set; } = "";
        public string Picture { get; set; } = "";
        public string Statu { get; set; } = "";
        public string StatuText { get; set; } ="";
        public string ModelName { get; set; } = "";
        public string ModelYear { get; set; } = "";
        public string SeriesName { get; set; } = "";
        public byte CurrencyId { get; set; }
        public bool Doping { get; set; }
        public string CategoryName { get; set; }
        public string BrandName { get; set; }
        public long ViewCount { get; set; }
        public int CityId { get; set; } = 0;
        public string City { get; set; }
        public int LocalityId { get; set; } = 0;
        public string Locality { get; set; }
        public string BriefDetail { get; set; }
        public int CountryId { get; set; } = 0;
        public string Country { get; set; }

        public int TownId { get; set; } = 0;
        public string Town { get; set; }
    }
}
