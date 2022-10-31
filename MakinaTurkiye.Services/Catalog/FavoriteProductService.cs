using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Members;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class FavoriteProductService : IFavoriteProductService
    {

        #region Constants
        private const string FAVORITEPRODUCTS_BY_MAINPARTY_ID_AND_PRODUCT_ID_KEY = "maknaturkiye.favoriteproduct.bymainpartyId-productId-{0}-{1}";
        #endregion

        #region Fields

        private readonly IRepository<FavoriteProduct> _favoriteProductRepository;
        private readonly IRepository<Product> _productRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public FavoriteProductService(IRepository<FavoriteProduct> favoriteProductRepository,
           ICacheManager cacheManager)
        {
            _favoriteProductRepository = favoriteProductRepository;
            _cacheManager = cacheManager;

        }

        #endregion

        #region Methods

        public IPagedList<FavoriteProduct> GetAllFavoriteProduct(int pageSize, int page)
        {
            var favoriteProduct = _favoriteProductRepository.Table;
            int totalCount = favoriteProduct.ToList().Count;
            favoriteProduct = favoriteProduct.OrderByDescending(x => x.FavoriteProductId).Skip((pageSize * page) - pageSize).Take(pageSize);
            return new PagedList<FavoriteProduct>(favoriteProduct, page, pageSize, totalCount);

        }
        public FavoriteProduct GetFavoriteProductByFavoriteProductId(int favoriteProductId)
        {
            if (favoriteProductId == 0)
                return null;

            var query = _favoriteProductRepository.Table;
            return query.FirstOrDefault(fp => fp.FavoriteProductId == favoriteProductId);
        }

        public FavoriteProduct GetFavoriteProductByMainPartyIdWithProductId(int mainPartyId, int productId)
        {
            if (mainPartyId == 0)
                return null;

            if (productId == 0)
                return null;

            string key = string.Format(FAVORITEPRODUCTS_BY_MAINPARTY_ID_AND_PRODUCT_ID_KEY, mainPartyId, productId);
            return _cacheManager.Get(key, () =>
            {

                var query = _favoriteProductRepository.Table;

                return query.FirstOrDefault(fp => fp.MainPartyId == mainPartyId && fp.ProductId == productId);
            });
        }

        public List<FavoriteProduct> GetFavoriteProducts()
        {
            var query = _favoriteProductRepository.Table;
            return query.ToList();
        }
        public List<FavoriteProduct> GetFavoriteProductsByMainPartyId(int mainPartyId)
        {
            var query = _favoriteProductRepository.Table;
            return query.Where(x=>x.MainPartyId==mainPartyId).ToList();
        }

        public void DeleteFavoriteProduct(FavoriteProduct favoriteProduct)
        {
            if (favoriteProduct == null)
                throw new ArgumentNullException("favoriteProduct");

            _favoriteProductRepository.Delete(favoriteProduct);
        }

        public void InsertFavoriteProduct(FavoriteProduct favoriteProduct)
        {
            if (favoriteProduct == null)
                throw new ArgumentNullException("favoriteProduct");

            _favoriteProductRepository.Insert(favoriteProduct);
        }

        #endregion

    }
}
