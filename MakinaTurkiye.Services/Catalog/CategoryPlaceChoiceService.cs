using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class CategoryPlaceChoiceService : BaseService, ICategoryPlaceChoiceService
    {

        #region Constants

        private const string CATEGORYPLACECHOICE_BY_CATEGORYPLACETYPE_ID_KEY = "makinaturkiye.Categoryplacechoice.bycategoryplacetypeId-{0}{1}";

        #endregion

        #region Fields

        private readonly IRepository<CategoryPlaceChoice> _categoryPlaceChoiceRepository;
        private readonly ICacheManager _cacheManager;

        #endregion


        #region Ctor

        public CategoryPlaceChoiceService(IRepository<CategoryPlaceChoice> categoryPlaceChoiceRepository,
                                          ICacheManager cacheManager) : base(cacheManager)
        {
            _categoryPlaceChoiceRepository = categoryPlaceChoiceRepository;
            _cacheManager = cacheManager;
        }

        #endregion


        #region Methods

        public IList<CategoryPlaceChoice> GetCategoryPlaceChoices()
        {
            var query = _categoryPlaceChoiceRepository.Table;

            return query.OrderBy(x => x.Order).ThenBy(x => x.Category.CategoryName).ToList();
        }

        public IList<CategoryPlaceChoice> GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct(int categoryPlaceType,
            bool isProductCategory = true)
        {
            if (categoryPlaceType == 0)
                throw new ArgumentNullException("categoryPlaceType");

            int isProductCategory1 = 0;
            if (isProductCategory == true) isProductCategory1 = 1;

            string key = string.Format(CATEGORYPLACECHOICE_BY_CATEGORYPLACETYPE_ID_KEY, categoryPlaceType, isProductCategory1);
            return _cacheManager.Get(key, () =>
            {
                var query = _categoryPlaceChoiceRepository.Table;
                query =
                    query.Where(x => x.CategoryPlaceType == categoryPlaceType && x.IsProductCategory == isProductCategory)
                        .OrderBy(x => x.Order)
                        .ThenBy(x => x.Category.CategoryName);
                return query.ToList();
            });

        }

        public CategoryPlaceChoice GetCategoryPlaceChoiceByCategoryPlaceChoiceId(int categoryPlaceChoiceId)
        {
            if (categoryPlaceChoiceId == 0)
                throw new ArgumentNullException("categoryPlaceType");

            var query = _categoryPlaceChoiceRepository.Table;
            return query.FirstOrDefault(x => x.CategoryPlaceChoiceId == categoryPlaceChoiceId);
        }

        public IList<CategoryPlaceChoice> GetCategoryPlaceChoicesByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException("categoryId");

            var query = _categoryPlaceChoiceRepository.Table;
            query = query.Where(x => x.CategoryId == categoryId).OrderBy(x => x.Order).ThenBy(x => x.Category.CategoryName);
            return query.ToList();
        }



        public void UpdateCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice)
        {
            if (categoryPlaceChoice == null)
                throw new ArgumentNullException("categoryPlaceChoice");

            _categoryPlaceChoiceRepository.Update(categoryPlaceChoice);
        }

        public void InsertCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice)
        {
            if (categoryPlaceChoice == null)
                throw new ArgumentNullException("categoryPlaceChoice");

            _categoryPlaceChoiceRepository.Insert(categoryPlaceChoice);
        }

        public void DeleteCategoryPlaceChoice(CategoryPlaceChoice categoryPlaceChoice)
        {
            if (categoryPlaceChoice == null)
                throw new ArgumentNullException("categoryPlaceChoice");

            _categoryPlaceChoiceRepository.Delete(categoryPlaceChoice);
        }

        #endregion

    }
}
