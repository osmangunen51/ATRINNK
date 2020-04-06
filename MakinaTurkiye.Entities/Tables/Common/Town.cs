namespace MakinaTurkiye.Entities.Tables.Common
{
    public class Town:BaseEntity
    {
        public int TownId { get; set; }
        public int? CityId { get; set; }
        public int? LocalityId { get; set; }
        public int? DistrictId { get; set; }
        public string TownName_Big { get; set; }
        public string TownName { get; set; }
        public string TownName_Small { get; set; }

        public virtual District District { get; set; }
    }
}
