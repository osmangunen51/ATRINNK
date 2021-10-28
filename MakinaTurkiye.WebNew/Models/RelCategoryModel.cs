namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using global::MakinaTurkiye.Core.Infrastructure;
    using global::MakinaTurkiye.Entities.Tables.Catalog;
    using global::MakinaTurkiye.Services.Catalog;
    using System.Collections.Generic;
    using System.Linq;

    public class RelCategoryModel
    {
        public IEnumerable<Category> SectorItems { get; set; }

        public string ActivationCode { get; set; }

        public IEnumerable<Category> ParentItems(int CategoryId)
        {
            ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
            var categoryIds = categoryService.GetCategoryIdsByCategoryParentId(CategoryId);
            if (!categoryIds.Any())
                return new List<Category>();

            var categories = categoryService.GetCategoriesByCategoryParentIds(categoryIds.ToList());
            return categories;
        }
    }

}