namespace MakinaTurkiye.Entities.Tables.Common
{
    public class City : BaseEntity
    {
        public City()
        {

        }

        public int CityId { get; set; }
        public int CountryId { get; set; }
        public string Plate { get; set; }
        public string CityName_Big { get; set; }
        public string CityName { get; set; }
        public string CityName_Small { get; set; }
        public string AreaCode { get; set; }
        public byte? CityOrder { get; set; }
    }
}
