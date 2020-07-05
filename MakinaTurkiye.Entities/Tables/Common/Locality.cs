using System;

namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Locality:BaseEntity
    {
        public int LocalityId { get; set; }
        public Nullable<int> CountryId { get; set; }
        public Nullable<int> CityId { get; set; }
        public string LocalityName_Big { get; set; }
        public string LocalityName { get; set; }
        public string LocalithName_Small { get; set; }
    }
}
