using MakinaTurkiye.Caching;
using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class ProductCommentService : BaseService, IProductCommentService
    {

        #region Constants

        private const string PRODUCTCOMMENTS_BY_PRODUCT_ID_KEY = "makinaturkiye.productcommnet.byproductid-{0}-{1}";

        #endregion

        #region Fields

        private readonly IRepository<ProductComment> _productCommentRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public ProductCommentService(IRepository<ProductComment> productCommentRepository, ICacheManager cacheManager): base(cacheManager)
        {
            this._productCommentRepository = productCommentRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public ProductComment GetProductCommentByProductCommentId(int productCommentId)
        {
            if (productCommentId == 0)
                throw new ArgumentNullException("productCommentId");

            var query = _productCommentRepository.Table;
            return query.FirstOrDefault(x => x.ProductCommentId == productCommentId);
        }

        public IPagedList<ProductComment> GetProductComments(int pageSize, int pageIndex, int productId = 0, bool reported = false)
        {
            var query = _productCommentRepository.Table;

            if (productId != 0)
                query = query.Where(x => x.ProductId == productId);
            if (reported == true)
                query = query.Where(x => x.Reported == true);

            var productComments = query.OrderByDescending(x => x.ProductCommentId).Skip(pageSize * pageIndex - pageSize).Take(pageSize);
            return new PagedList<ProductComment>(productComments, pageIndex, pageSize, query.Count());
        }

        public IList<ProductComment> GetProductCommentsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _productCommentRepository.Table;
            query = query.Where(x => x.MemberMainPartyId == mainPartyId);

            return query.ToList();
        }
        public IList<ProductComment> GetProductCommentsByProductId(int productId, bool showHidden = false)
        {
            if (productId <= 0)
                throw new ArgumentNullException("productId");

            string key = string.Format(PRODUCTCOMMENTS_BY_PRODUCT_ID_KEY, productId, showHidden);
            return _cacheManager.Get(key, () =>
            {
                var query = _productCommentRepository.Table;
                if (!showHidden)
                    query = query.Where(p => p.Status);

                query = query.Where(p => p.ProductId == productId);

                query = query.OrderByDescending(p => p.ProductCommentId);

                var productComments = query.ToList();
                return productComments;
            });
        }

        public IList<ProductComment> GetProductCommentsForStoreByMemberMainPartyId(int storeMainPartyId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storemainPartyId");

            var query = _productCommentRepository.Table;
            query = query.Where(x => x.Product.MainPartyId == storeMainPartyId);
            return query.ToList();
        }


        public void DeleteProductComment(ProductComment productComment)
        {
            if (productComment == null)
                throw new ArgumentNullException("productComment");
            _productCommentRepository.Delete(productComment);
        }

        public void InsertProductComment(ProductComment productComment)
        {
            if (productComment == null)
                throw new ArgumentNullException("productComment");

            _productCommentRepository.Insert(productComment);
        }

        public void UpdateProductComment(ProductComment productComment)
        {
            if (productComment == null)
                throw new ArgumentNullException("productComment");

            _productCommentRepository.Update(productComment);
        }

        #endregion

    }
}
