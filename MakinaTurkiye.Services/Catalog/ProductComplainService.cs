using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class ProductComplainService:IProductComplainService
    {
        #region Constants
      
        //private const string PRODUCTCOMPLAINTYPE_PATTERN_KEY = "makinaturkiye.productcomplaintype";
        private const string PRODUCTCOMPLAINTYPE_All_KEY = "makinaturkiye.productcomplaintype.byall";
       
        #endregion

        #region Fields

        private readonly IRepository<ProductComplain> _productComplainRepository;
        private readonly IRepository<ProductComplainType> _productComplainTypeRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public ProductComplainService(IRepository<ProductComplain> productComplainRepository, IRepository<ProductComplainType> productComplainTypeRepository, ICacheManager cacheManager)
        {
            this._productComplainRepository = productComplainRepository;
            this._productComplainTypeRepository = productComplainTypeRepository;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Methods

        public IList<ProductComplain> GetAllProductComplain()
        {
            var query = _productComplainRepository.Table;
            return query.ToList();
        }
        
        public IList<ProductComplainType> GetAllProductComplainType()
        {
            string key = string.Format(PRODUCTCOMPLAINTYPE_All_KEY);
            return _cacheManager.Get(key, () =>
             {
                 var query = _productComplainTypeRepository.Table;
                 return query.OrderBy(p => p.DisplayOrder).ToList();
             });
        }
                
        public ProductComplain GetProductComplainByProductComplainId(int productComplainId)
        {
            if (productComplainId == 0)
                return null;

            var query = _productComplainRepository.Table;
            return query.FirstOrDefault(pc => pc.ProductComplainId == productComplainId);
        }

        public void InsertProductComplain(ProductComplain productComplain)
        {
            if (productComplain == null)
                throw new ArgumentNullException("productComplain");

            _productComplainRepository.Insert(productComplain);
        }

        public void DeleteProductComplain(ProductComplain productComplain)
        {
            if (productComplain == null)
                throw new ArgumentNullException();
            _productComplainRepository.Delete(productComplain);
        }

        #endregion

    }
}
