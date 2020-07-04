using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public interface ICategoryPlaceChoiceService : ICachingSupported
    {
        IList<CategoryPlaceChoice> GetCategoryPlaceChoices();

        IList<CategoryPlaceChoice> GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct(int categoryPlaceType, bool isProductCategory);

        CategoryPlaceChoice GetCategoryPlaceChoiceByCategoryPlaceChoiceId(int categoryPlaceChoiceId);

        IList<CategoryPlaceChoice> GetCategoryPlaceChoicesByCategoryId(int categoryId);


        void InsertCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice);

        void DeleteCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice);

        void UpdateCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice);
    }
}
