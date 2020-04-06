namespace NeoSistem.MakinaTurkiye.Web.Models
{
  using System.Collections.Generic;
  using EnterpriseEntity.Extensions.Data;
  using System;
  using System.Linq;
  using System.Xml.Linq;
  using global::MakinaTurkiye.Entities.Tables.Catalog;
    using global::MakinaTurkiye.Services.Catalog;
    using global::MakinaTurkiye.Core.Infrastructure;

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