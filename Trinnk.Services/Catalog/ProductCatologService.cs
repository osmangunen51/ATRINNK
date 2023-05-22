using Trinnk.Caching;
using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Trinnk.Services.Catalog
{
    public class ProductCatologService : BaseService, IProductCatologService
    {

        #region Constants 

        private const string PRODUCTCATALOGS_BY_PRODUCTCATALOG_ID_KEY = "productcatalog.byid-{0}";
        private const string PRODUCTCATALOGS_BY_PRODUCT_ID_KEY = "setting.byproductid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<ProductCatolog> _productCatologRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public ProductCatologService(IRepository<ProductCatolog> productCatologRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._productCatologRepository = productCatologRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public ProductCatolog GetProductCatologByProductCatologId(int productCatologId)
        {
            if (productCatologId == 0)
                throw new ArgumentNullException("productCatologId");

            string key = string.Format(PRODUCTCATALOGS_BY_PRODUCTCATALOG_ID_KEY, productCatologId);
            return _cacheManager.Get(key, () =>
            {
                var query = _productCatologRepository.Table;
                return query.FirstOrDefault(x => x.ProductCatologId == productCatologId);
            });
        }

        public IList<ProductCatolog> GetProductCatologsByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");

            string key = string.Format(PRODUCTCATALOGS_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () =>
            {
                var query = _productCatologRepository.Table;
                query = query.Where(x => x.ProductId == productId);

                var productCatalogs = query.ToList();
                return productCatalogs;
            });
        }


        public void DeleteProductCatolog(ProductCatolog productCatolog)
        {
            if (productCatolog == null)
                throw new ArgumentNullException("productCatolog");

            _productCatologRepository.Delete(productCatolog);
        }

        public void InsertProductCatolog(ProductCatolog productCatolog)
        {
            if (productCatolog == null)
                throw new ArgumentNullException("productCatolog");

            _productCatologRepository.Insert(productCatolog);
        }

        public void UpdateProductCatolog(ProductCatolog productCatolog)
        {
            if (productCatolog == null)
                throw new ArgumentNullException("productCatolog");

            _productCatologRepository.Update(productCatolog);
        }

        #endregion

    }
}
