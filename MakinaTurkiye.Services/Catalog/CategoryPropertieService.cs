using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Catalog
{
    public class CategoryPropertieService: BaseService, ICategoryPropertieService
    {
        
        #region Constants

        private const string CATEGORYPROPERTIES_BY_CATEGORYPROPERTIE_ID_KEY = "makinaturkiye.categorypropertie.byid-{0}";
        private const string CATEGORYPROPERTIES_BY_CATEGORYID_ID_KEY = "makinaturkiye.categorypropertie.bycategoryid-{0}";

        private const string CATEGORYPROPERTIERESULTS_BY_CATEGORYID_ID_KEY = "makinaturkiye.categorypropertieresult.bycategoryid-{0}";

        private const string PRODUCTPROPERTIEVALUES_BY_PRODUCTPROPERTIEVE_ID_KEY = "makinaturkiye.productpropertievalue.byid-{0}";
        private const string PRODUCTPROPERTIEVALUES_BY_PRODUCT_ID_KEY = "makinaturkiye.productpropertievalue.byproductid-{0}";

        private const string PRODUCTPROPERTIEVALUERESULTS_BY_PRODUCT_ID_KEY = "makinaturkiye.productpropertievalueresult.byproductid-{0}";

        private const string PROPERTIES_BY_PROPERTIE_ID_KEY = "makinaturkiye.propertie.byid-{0}";

        private const string PROPERTIEATTRS_BY_PROPERTIEATTR_ID_KEY = "makinaturkiye.propertieattr.byid-{0}";
        private const string PROPERTIEATTRS_BY_PROPERTIE_ID_KEY = "makinaturkiye.propertieattr.byproductid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<CategoryPropertie> _categoryPropertieRepository;
        private readonly IRepository<Propertie> _propertieRepository;
        private readonly IRepository<PropertieAttr> _propertieAttrRepository;
        private readonly IRepository<ProductPropertieValue> _productPropertieValue;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public CategoryPropertieService(IRepository<CategoryPropertie> categoryPropertieRepository, 
            IRepository<ProductPropertieValue> productPropertieValue, 
            IRepository<Propertie> propertieRepository, 
            IRepository<PropertieAttr> propertieAttrReository, ICacheManager cacheManager): base(cacheManager)
        {
            this._categoryPropertieRepository = categoryPropertieRepository;
            this._propertieAttrRepository = propertieAttrReository;
            this._propertieRepository = propertieRepository;
            this._productPropertieValue = productPropertieValue;
            this._cacheManager = cacheManager;
        }

        #endregion

        #region Get methods

        public IList<Propertie> GetAllProperties()
        {
            var query = _propertieRepository.Table;

            return query.ToList();
        }

        public CategoryPropertie GetCategoryPropertieByCategoryPropertieId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException("id");

            string key = string.Format(CATEGORYPROPERTIES_BY_CATEGORYPROPERTIE_ID_KEY, categoryId);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryPropertieRepository.Table;
                return query.FirstOrDefault(x => x.CategoryPropertieId == categoryId);
            });
        }

        public IList<CategoryPropertie> GetCategoryPropertiesByCategoryId(int categoryId)
        {
            if (categoryId == 0)
                throw new ArgumentNullException("CategoryId");

            string key = string.Format(CATEGORYPROPERTIES_BY_CATEGORYID_ID_KEY, categoryId);
            return _cacheManager.Get(key, () => 
            {
                var query = _categoryPropertieRepository.Table;
                query = query.Where(x => x.CategoryId == categoryId);

                var categoryProperies = query.ToList();
                return categoryProperies;
            });
        }

        public PropertieAttr GetPropertieAttrByPropertieAttrId(int propertieAttrId)
        {
            if (propertieAttrId == 0)
                throw new ArgumentNullException("propertieAttrd");

            string key = string.Format(PROPERTIEATTRS_BY_PROPERTIEATTR_ID_KEY, propertieAttrId);
            return _cacheManager.Get(key, () => 
            {
                var query = _propertieAttrRepository.Table;
                return query.FirstOrDefault(x => x.PropertieAttrId == propertieAttrId);
            });
        }

        public IList<CategoryPropertieResult> GetPropertieByCategoryId(int categoryId)
        {
            if(categoryId==0)
            throw new ArgumentNullException("categoryId");

            string key = string.Format(CATEGORYPROPERTIERESULTS_BY_CATEGORYID_ID_KEY, categoryId);
            return _cacheManager.Get(key, () => 
            {
                var query = from p in _propertieRepository.Table
                            join ca in _categoryPropertieRepository.Table on p.PropertieId equals ca.PropertieId
                            where ca.CategoryId == categoryId
                            select new CategoryPropertieResult
                            {
                                PropertieId = p.PropertieId,
                                PropertieName = p.PropertieName,
                                PropertieType = p.PropertieType,
                                CategoryPropertieId = ca.CategoryPropertieId
                            };

                var categoryProperties = query.ToList();
                return categoryProperties;
            });
        }

        public Propertie GetPropertieById(int propertieId)
        {
            if (propertieId == 0)
                throw new ArgumentNullException("propertieId");

            string key = string.Format(PROPERTIES_BY_PROPERTIE_ID_KEY, propertieId);
            return _cacheManager.Get(key, () => 
            {
                var query = _propertieRepository.Table;
                return query.FirstOrDefault(X => X.PropertieId == propertieId);
            });
        }

        public IList<PropertieAttr> GetPropertiesAttrByPropertieId(int propertieId)
        {
            if (propertieId == 0)
                throw new ArgumentNullException("propertieId");

            string key = string.Format(PROPERTIEATTRS_BY_PROPERTIE_ID_KEY, propertieId);
            return _cacheManager.Get(key, () => 
            {
                var query = _propertieAttrRepository.Table;
                return query.Where(x => x.PropertieId == propertieId).ToList();
            });
        }

        public IList<ProductPropertieValue> GetProductPropertieValuesByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");

            string key = string.Format(PRODUCTPROPERTIEVALUES_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () => 
            {
                var query = _productPropertieValue.Table;
                return query.Where(x => x.ProductId == productId).ToList();
            });
        }

        public IList<ProductPropertieValueResult> GetProductPropertieValuesResultByProductId(int productId)
        {
            if (productId == 0)
                throw new ArgumentNullException("productId");

            string key = string.Format(PRODUCTPROPERTIEVALUERESULTS_BY_PRODUCT_ID_KEY, productId);
            return _cacheManager.Get(key, () => 
            {
                var query = from pv in _productPropertieValue.Table
                            join pr in _propertieRepository.Table on pv.PropertieId equals pr.PropertieId
                            where pv.ProductId == productId
                            select new ProductPropertieValueResult
                            {
                                PropertieName = pr.PropertieName,
                                PropertieValue = pv.Value,
                                Type = pv.PropertieType
                            };

                var productProperties = query.ToList();
                return productProperties;
            });
        }

        public ProductPropertieValue GetProductPropertieValueByProductPropertieId(int productPropertieId)
        {
            if (productPropertieId == 0)
                throw new ArgumentNullException("productPropertieId");

            string key = string.Format(PRODUCTPROPERTIEVALUES_BY_PRODUCTPROPERTIEVE_ID_KEY, productPropertieId);
            return _cacheManager.Get(key, () => 
            {
                var query = _productPropertieValue.Table;
                return query.FirstOrDefault(x => x.ProductPropertieId == productPropertieId);
            });
        }

        #endregion

        #region Insert delete update methods

        public void DeleteCategoryPropertie(CategoryPropertie categoryPropertie)
        {
            if (categoryPropertie == null)
                throw new ArgumentNullException("categoryPropertie");

            _categoryPropertieRepository.Delete(categoryPropertie);
        }

        public void DeletePropertie(Propertie propertie)
        {
            if (propertie == null)
                throw new ArgumentNullException("propertie");

            _propertieRepository.Delete(propertie);
        }

        public void DeletePropertieAttr(PropertieAttr propertieAttr)
        {
            if (propertieAttr == null)
                throw new ArgumentNullException("propertieAttr");

            _propertieAttrRepository.Delete(propertieAttr);
        }

        public void InsertCategoryPropertie(CategoryPropertie categoryPropertie)
        {
            if (categoryPropertie == null)
                throw new ArgumentNullException("categoryPropertie");

            _categoryPropertieRepository.Insert(categoryPropertie);
        }

        public void InsertPropertie(Propertie propertie)
        {
            if (propertie == null)
                throw new ArgumentNullException("propertie");

            _propertieRepository.Insert(propertie);
        }

        public void InsertPropertieAttr(PropertieAttr propertieAttr)
        {
            if (propertieAttr == null)
                throw new ArgumentNullException("propertieAttr");

            _propertieAttrRepository.Insert(propertieAttr);
        }

        public void UpdateCategoryPropertie(CategoryPropertie categoryPropertie)
        {
            if (categoryPropertie == null)
                throw new ArgumentNullException("categoryPropertie");

            _categoryPropertieRepository.Update(categoryPropertie);
        }

        public void UpdatePropertie(Propertie propertie)
        {
            if (propertie == null)
                throw new ArgumentNullException("propertie");

            _propertieRepository.Update(propertie);
        }

        public void UpdatePropertieAttr(PropertieAttr propertieAttr)
        {
            if (propertieAttr == null)
                throw new ArgumentNullException("propertieAttr");

            _propertieAttrRepository.Update(propertieAttr);
        }

        public void InsertProductProertieValue(ProductPropertieValue productPropertieValue)
        {
            if (productPropertieValue == null)
                throw new ArgumentNullException("productPropertieValue");

            _productPropertieValue.Insert(productPropertieValue);

        }

        public void DeleteProductProertieValue(ProductPropertieValue productPropertieValue)
        {
            if (productPropertieValue == null)
                throw new ArgumentNullException("productPropertieValue");

            _productPropertieValue.Delete(productPropertieValue);
        }

        public void UpdateProductProertieValue(ProductPropertieValue productPropertieValue)
        {
            if (productPropertieValue == null)
                throw new ArgumentNullException("productPropertieValue");

            _productPropertieValue.Update(productPropertieValue);
        }

        #endregion


    }
}
