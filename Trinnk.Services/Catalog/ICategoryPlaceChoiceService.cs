using Trinnk.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace Trinnk.Services.Catalog
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
