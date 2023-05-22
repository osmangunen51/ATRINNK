namespace NeoSistem.Trinnk.Web.Models
{

    public class CategorizationModel
    {

        public int SectorId { get; set; }

        public int ProductGroupId { get; set; }

        public int CategoryId { get; set; }

        public int? ModelId { get; set; }

        public int? BrandId { get; set; }

        public int? SeriesId { get; set; }

        public string OtherBrand { get; set; }

        public string OtherModel { get; set; }

        public int CategoryType { get; set; }

        public string CategoryTreeName { get; set; }
        public string CategoryPath { get; set; }
        public string CategoryPathUrl { get; set; }
    }
}