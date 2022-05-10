using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View.Statistics
{
    public class Store
    {
        public int MainPartyId { get; set; }
        public long ViewCount { get; set; }
        public long SingularViewCount { get; set; }
        public string Series { get { return JsonConvert.SerializeObject(VSeries); }}
        public string Datas { get { return JsonConvert.SerializeObject(VDatas); } }
        public List<string> VSeries { get; set; }
        public List<int> VDatas { get; set; }
    }

    public class StoreDetailItem
    {
        public string Country { get; set; }
        public string City { get; set; }
        public int ViewCount { get; set; }
    }

    public class StoreDetail
    {
        public int MainPartyId { get; set; }
        public DateTime StartDate { get; set; }
        public DateTime EndDate { get; set; }
        public long TotalViewCount { get; set; }
        public long TotalUserCount { get; set; }
        public long LastTotalViewCount { get; set; }
        public long LastSingularViewCount { get; set; }
        public List<StoreDetailItem> DetailList { get; set; }=new List<StoreDetailItem>();
    }
}
