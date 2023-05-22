using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Api.View.Statistics
{
    public class Advert
    {
        public int MainPartyId { get; set; }
        public long ViewCount { get; set; }
        public string Series { get { return JsonConvert.SerializeObject(VSeries); }}
        public string Datas { get { return JsonConvert.SerializeObject(VDatas); } }
        public List<string> VSeries { get; set; }
        public List<int> VDatas { get; set; }
        public string LblSeries{ get; set; }
        public string LblDatas { get; set; }

    }

    public class AdvertDetailItem
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int ViewCount { get; set; }
    }

    public class ProductDetailItem
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int ViewCount { get; set; }
    }
    public class AdvertDetail
    {
        public int MainPartyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TotalViewCount { get; set; }
        public long TotalUserCount { get; set; }
        public long LastTotalViewCount { get; set; }
        public long LastSingularViewCount { get; set; }
        public List<AdvertDetailItem> DetailList { get; set; }=new List<AdvertDetailItem>();
        public List<ProductDetailItem> ProductDetailList { get; set; } = new List<ProductDetailItem>();
        public string Series { get { return JsonConvert.SerializeObject(VSeries); } }
        public string Datas { get { return JsonConvert.SerializeObject(VDatas); } }
        public List<string> VSeries { get; set; }
        public List<int> VDatas { get; set; }
    }
}
