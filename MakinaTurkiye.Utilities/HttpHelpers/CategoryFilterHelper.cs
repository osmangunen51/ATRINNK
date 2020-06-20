namespace MakinaTurkiye.Utilities.HttpHelpers
{
    public class CategoryFilterHelper
    {
        public int CountryId { get; set; }
        public string CountryName { get; set; }
        public int CityId { get; set; }
        public string CityName { get; set; }
        public int LocalityId { get; set; }
        public string LocalityName { get; set; }
        public string SearchText { get; set; }
        public int CustomFilterId { get; set; }
        public string SearchType { get; set; }
        public string ViewType { get; set; }
    }

}