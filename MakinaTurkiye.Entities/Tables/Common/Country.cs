namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Country: BaseEntity
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public bool? Active { get; set; }
        public string CultureCode { get; set; }
        public byte? CountryOrder { get; set; }

    }
}
