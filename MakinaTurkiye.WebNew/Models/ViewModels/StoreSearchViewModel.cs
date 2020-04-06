using System.Collections.Generic;
using MakinaTurkiye.Entities.Tables.Catalog;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
  public class StoreSearchViewModel
  {
    public CategoryModel Category { get; set; }
    public IList<Category> Categories { get; set; }
    public StoreModel StoreModel { get { return new StoreModel(); } }
    public IList<Category> CategoryParentCategoryItems { get; set; }
  }
}