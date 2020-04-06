namespace MakinaTurkiye.Entities.Tables.Common
{
    public class District: BaseEntity
    {
        public int DistrictId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public string DistrictName_Big { get; set; }
        public string DistrictName { get; set; }
        public string DistrictName_Small { get; set; }
        public string ZipCode { get; set; }
    }
}
