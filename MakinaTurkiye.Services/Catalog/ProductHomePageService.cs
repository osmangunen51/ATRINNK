using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class ProductHomePageService : IProductHomePageService
    {
        #region Constants

        private const string PRODUCTHOMEPAGES_BY_CATEGORY_ID_KEY = "makinaturkiye.producthomepage.bycategoryId-{0}";


        #endregion


        IRepository<ProductHomePage> _productHomePageRepository;
        ICacheManager _cacheManager;

        public ProductHomePageService(IRepository<ProductHomePage> productHomePageRepository,
                        ICacheManager cacheManager)
        {
            this._productHomePageRepository = productHomePageRepository;
            this._cacheManager = cacheManager;
        }

        public ProductHomePage GetProductHomePageByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");
            var query = _productHomePageRepository.Table;
            return query.FirstOrDefault(x => x.ProductId == productId);
        }

        public IList<ProductHomePage> GetProductHomePages()
        {
            var query = _productHomePageRepository.Table;

            return query.ToList();
        }

        public IList<ProductHomePage> GetProductHomePagesByCategoryId(int categoryId, bool showHidden = false)
        {
            if (categoryId <= 0)
                throw new ArgumentNullException("categoryId");

            string key = string.Format(PRODUCTHOMEPAGES_BY_CATEGORY_ID_KEY, categoryId);
            return _cacheManager.Get(key, () =>
            {
                var query = _productHomePageRepository.Table;

                if (!showHidden)
                    query = query.Where(php => php.Active == true);

                query = query.Where(php => php.CategoryId == categoryId);

                var productHomePages = query.ToList();
                return productHomePages;
            });
        }

        public void DeleteProductHomePage(ProductHomePage productHomePage)
        {
            if (productHomePage == null)
                throw new ArgumentNullException("productHomePage");
            _productHomePageRepository.Delete(productHomePage);

        }

        public void InsertProductHomePage(ProductHomePage productHomePage)
        {
            if (productHomePage == null)
                throw new ArgumentNullException("productHomePage");
            _productHomePageRepository.Insert(productHomePage);
        }

        public void UpdateProductHomePage(ProductHomePage productHomePage)
        {
            if (productHomePage == null)
                throw new ArgumentNullException("productHomePage");
            _productHomePageRepository.Update(productHomePage);
        }
    }
}
