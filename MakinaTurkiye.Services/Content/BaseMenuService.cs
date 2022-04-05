using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Content;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Content
{
    public class BaseMenuService : IBaseMenuService
    {

        #region Constants

        private const string BASEMENUS_ALL_KEY = "makinaturkiye.basemenu.byall-{0}-{1}";
        private const string BASEMENUCATEGORIES_BY_BASE_MENU_ID_KEY = "makinaturkiye.basemenucategory.bybasemenuId-{0}";
        private const string BASEMENUS_BY_BASEMENU_ID_KEY = "makinaturkiye.basemenu.byid-{0}";
        private const string BASEMENUS_BY_ALL_KEY = "makinaturkiye.basemenu.all-{0}";

        #endregion


        #region Fields

        private readonly IRepository<BaseMenu> _baseMenuRepository;
        private readonly IRepository<BaseMenuCategory> _baseMenuCategoryRepository;
        private readonly IRepository<BaseMenuImage> _baseMenuImageRepository;
        private readonly ICacheManager _cacheManager;

        #endregion 

        #region Ctor

        public BaseMenuService(IRepository<BaseMenu> baseMenuRepository,
            IRepository<BaseMenuCategory> baseMenuCategoryRepository,
            IRepository<BaseMenuImage> baseMenuImageRepository, ICacheManager cacheManager)
        {
            this._baseMenuRepository = baseMenuRepository;
            this._baseMenuCategoryRepository = baseMenuCategoryRepository;
            this._baseMenuImageRepository = baseMenuImageRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public void DeleteBaseMenu(BaseMenu baseMenu)
        {
            if (baseMenu == null)
                throw new ArgumentNullException("baseMenu");
            _baseMenuRepository.Delete(baseMenu);
        }

        public void DeleteBaseMenuCategory(BaseMenuCategory baseMenuCategory)
        {
            if (baseMenuCategory == null)
                throw new ArgumentNullException("baseMenuCategory");
            _baseMenuCategoryRepository.Delete(baseMenuCategory);
        }

        public void DeleteBaseMenuImage(BaseMenuImage baseMenuImage)
        {
            if (baseMenuImage == null)
                throw new ArgumentNullException("baseMenuImage");

            _baseMenuImageRepository.Delete(baseMenuImage);
        }

        public IList<BaseMenu> GetAllBaseMenu(bool showHidden = false)
        {
            string key = string.Format(BASEMENUS_BY_ALL_KEY, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = _baseMenuRepository.Table.OrderBy(x => x.Order);

                var baseMenus = query.ToList();
                return baseMenus;
            });
        }

        public IList<BaseMenu> GetAllBaseMenus(int skip, int take)
        {
            string key = string.Format(BASEMENUS_ALL_KEY, skip, take);

            return _cacheManager.Get(key, () =>
            {
                var query = _baseMenuRepository.Table;
                query = query.OrderBy(x => x.HomePageOrder).ThenBy(x => x.BaseMenuId).Skip(skip).Take(take);
                return query.ToList();
            });
        }

        public BaseMenu GetBaseMenuByBaseMenuId(int baseMenuId)
        {
            if (baseMenuId == 0)
                throw new ArgumentNullException("baseMenuId");

            string key = string.Format(BASEMENUS_BY_BASEMENU_ID_KEY, baseMenuId);
            return _cacheManager.Get(key, () =>
            {
                var query = _baseMenuRepository.Table;
                return query.FirstOrDefault(b => b.BaseMenuId == baseMenuId);
            });
        }

        public IList<BaseMenuCategory> GetBaseMenuCategoriesByBaseMenuId(int baseMenuId)
        {
            if (baseMenuId <= 0)
                throw new ArgumentNullException("baseMenuId");

            string key = string.Format(BASEMENUCATEGORIES_BY_BASE_MENU_ID_KEY, baseMenuId);

            return _cacheManager.Get(key, () =>
            {
                var query = _baseMenuCategoryRepository.Table;

                query = query.Where(bm => bm.BaseMenuId == baseMenuId);

                query = query.Include(bm => bm.Category);

                query = query.OrderBy(bm => bm.Category.BaseMenuOrder).ThenBy(x => x.Category.CategoryName);

                var baseMenuCategories = query.ToList();
                return baseMenuCategories;
            });
        }

        public BaseMenuCategory GetBaseMenuCategoryByBaseMenuCategoryId(int baseMenuCategoryId)
        {
            if (baseMenuCategoryId == 0)
                throw new ArgumentNullException("baseMenuCategoryId");

            var query = _baseMenuCategoryRepository.Table;
            return query.FirstOrDefault(b => b.BaseMenuCategoryId == baseMenuCategoryId);
        }

        public BaseMenuImage GetBaseMenuImageByBaseMenuImageId(int baseMenuImageId)
        {
            if (baseMenuImageId == 0)
                throw new ArgumentNullException("baseMenuImageId");
            var query = _baseMenuImageRepository.Table;
            return query.FirstOrDefault(x => x.BaseMenuImageId == baseMenuImageId);
        }

        public void InsertBaseMenu(BaseMenu baseMenu)
        {
            if (baseMenu == null)
                throw new ArgumentNullException("baseMenu");
            _baseMenuRepository.Insert(baseMenu);
        }

        public void InsertBaseMenuCategory(BaseMenuCategory baseMenuCategory)
        {
            if (baseMenuCategory == null)
                throw new ArgumentNullException("baseMenuCategory");
            _baseMenuCategoryRepository.Insert(baseMenuCategory);
        }

        public void InsertBaseMenuImage(BaseMenuImage baseMenuImage)
        {
            if (baseMenuImage == null)
                throw new ArgumentNullException("baseMenuImage");

            _baseMenuImageRepository.Insert(baseMenuImage);
        }

        public void UpdateBaseMenu(BaseMenu baseMenu)
        {
            if (baseMenu == null)
                throw new ArgumentNullException("baseMenu");
            _baseMenuRepository.Update(baseMenu);
        }

        public void UpdateBaseMenuCategory(BaseMenuCategory baseMenuCategory)
        {
            if (baseMenuCategory == null)
                throw new ArgumentNullException("baseMenuCategory");
            _baseMenuCategoryRepository.Update(baseMenuCategory);
        }

        public void UpdateBaseMenuImage(BaseMenuImage baseMenuImage)
        {
            if (baseMenuImage == null)
                throw new ArgumentNullException("baseMenuImage");
            _baseMenuImageRepository.Update(baseMenuImage);
        }

        #endregion
    }
}
