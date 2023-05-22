using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Catalog
{
    public class MTProductCategoryModel
    {
        public MTProductCategoryModel()
        {
            this.CategoryItemModels = new List<MTProductCategoryItemModel>();
            this.TopCategoryItemModels = new List<MTCategoryItemModel>();
            this.SubCategories = new List<MTProductSearchOneStepCategoryItemModel>();
            MtJsonLdModel = new MTJsonLdModel();
        }

        public int SelectedCategoryId { get; set; }
        public string SelectedCategoryName { get; set; }
        public string SelectedCategoryContentTitle { get; set; }
        public byte SelectedCategoryType { get; set; }

        public string SelectedCategoryDescription { get; set; }

        public IList<MTProductCategoryItemModel> CategoryItemModels { get; set; }
        public IList<MTCategoryItemModel> TopCategoryItemModels { get; set; }
        public IList<MTProductSearchOneStepCategoryItemModel> SubCategories { get; set; }
        public MTJsonLdModel MtJsonLdModel { get; set; }
        public string Navigation { get; set; }
    }
}