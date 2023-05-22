using System;
using System.ComponentModel.DataAnnotations;

namespace Trinnk.Entities.Tables.Catalog
{
    public class ProductStatistic
    {
        [Key]
        public int idpos { get; set; }
        public int ProductId { get; set; }
        public int MemberMainPartyId { get; set; }
        public string IpAdress { get; set; }
        public string UserCity { get; set; }
        public string UserCountry { get; set; }
        public byte SingularViewCount { get; set; }
        public int Hour { get; set; }
        public int ViewCount { get; set; }
        public DateTime RecordDate { get; set; }
    }
}
