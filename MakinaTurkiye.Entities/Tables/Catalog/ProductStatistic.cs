using System;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductStatistic: BaseEntity
    {
       
        public int ProductId { get; set; }
        public int MemberMainPartyId { get; set; }
        public string IpAdress { get; set; }
        public string UserCity { get; set; }
        public string UserCountry { get; set; }
        public byte SingularViewCount { get; set; }
        public byte Hour { get; set; }
        public byte ViewCount { get; set; }

        public DateTime RecordDate { get; set; }
    }
}
