using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreStatistic: BaseEntity
    {
        
        public int StoreId { get; set; }
        public string UserIp { get; set; }
        public string UserCity { get; set; }
        public string UserCountry { get; set; }
        public byte ViewCount { get; set; }
        public byte SingularViewCount { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
