using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductService : ICachingSupported
    {
        IList<ProductForStoreResult> GetSPProductForStoreByCategoryId(int categoryId = 0, int memberMainPartyId = 0, int topCount = 10);

        IList<ProductForStoreResult> GetSPProductForStoreByBrandId(int brandId = 0, int memberMainPartyId = 0, int topCount = 10);

        IList<ProductForStoreResult> GetSPProductForStoreByModelId(int modelId = 0, int memberMainPartyId = 0, int topCount = 10);

        IPagedList<WebSearchProductResult> SPWebSearch(string searchText, int categoryId = 0, int customFilterId = 0, int pageIndex = 0, int pageSize = 0);

        IPagedList<WebCategoryProductResult> SPWebCategoryProduct(out List<FilterableCategoriesResult> filterableCategoryIds,
            out List<int> filterableCountryIds,
            out List<int> filterableCityIds, out List<int> filterableLocalityIds, out List<int> filterableBrandIds,
            out List<int> filterableModelIds, out List<int> filterableSeriesIds,
            out int newProductCount, out int usedProductCount, out int serviceProductCount,
            int categoryId, int brandId,
            int modelId, int seriresId, int searchTypeId,int mainPartyId, int countryId = 0, int cityId = 0,
            int localityId = 0, int orderById = 0, int pageIndex = 0, int pageSize = 0, string searchText = "");

        IList<PopularProductResult> GetSPPopularProducts();
        IList<ProductRecomandationResult> GetSPProductRecomandation(string categoryId, string modelId, string brandId);


        Product GetProductByProductId(int productId);

        Product GetProductByProductNo(string productNo);


        IList<Product> GetProductsByMainPartIdAndNonProductId(int mainPartId, int nonProductId, int topCount = 12);

        IList<Product> GetProductsByCategoryIdAndNonProductId(int categoryId, int nonProductId, int topCount = 12);

        IList<Product> GetProductsByCategoryId(int categoryId);
        IList<Product> GetProductsByCategoryIds(List<int> CatList);

        IList<Product> GetProductsByMainPartyId(int mainPartyId, bool showHidden=false);
        IList<Product> GetAllProductsByMainPartyIds(List<int?> mainPartyIds, bool includeBrand=false);

        IList<Product> GetProductsByProductName(string productName);

        void InsertProduct(Product product);
        
        void UpdateProduct(Product product, bool removeCache = false);
        void DeleteProduct(Product product);

        IList<MostViewProductResult> GetSPMostViewProductsByCategoryId(int categoryId, int topCount = 12);

        IList<MostViewProductResult> GetSPMostViewProductsByCategoryIdRemind(int categoryId, int topCount, int selectedCategoryId);

        CategoryProductsResult GetCategoryProducts(int categoryId, int brandId, int modelId, int seriresId, int searchTypeId, int mainPartyId, int countryId = 0, int cityId = 0,
                                                                  int localityId = 0, int orderById = 0, int pageIndex = 0, int pageSize = 0, string searchText = "");

        IList<StoreProfileProductsResult> GetSPProductsByStoreMainPartyId(int pageDimension, int page, int storeMainPartyId,int mainPartyId=0, byte searchType=0);
        IList<StoreProfileProductsResult> GetSPProductsCountByStoreMainPartyIdAndSearchType(out int totalRecord, int mainPartyId,  byte searchType, int categoryId);


        IList<StoreProfileProductsResult> GetSPProductsByStoreMainPartyIdAndCategoryId(out int totalRecord, int pageDimension, int page, int storeMainPartyId, int categoryId, int userMainPartyId=0);

        IList<Product> GetRandomProductsByBill(int take);

        int GetProductCountBySearchType(int categoryId, int brandId, int modelId, int seriresId, int searchTypeId, string searchText, int countryId = 0, int cityId = 0,int localityId = 0);

        int GetNumberOfProductsByMainPartyId(int mainPartyId);//Count

        long GetViewOfProductsByMainPartyId(int mainPartyId);//Sum

        IList<Product> GetProductsForChoiced();

        IList<String> GetProductsByTerm(string term, int pageSize);

        IList<AllSectorRandomProductResultModel> GetSectorRandomProductsByCategoryId(int categoryId);

        void CalculateSPProductRate();

        void CheckSPProductSearch(int productId);

        void SPUpdateProductSearchCategoriesByCategoryId(int categoryId);

        IList<Product> GetProductsByProductIds(List<int> ProductIds,int take = 0);
        IList<Product> GetProductsByShowCase();

        IList<Product> GetSPFavoriteProductsByMainPartyId(int mainPartyId, int page, int pageSize, out int totalRecord);
        IList<Product> GetProductByProductActiveType(ProductActiveTypeEnum productActiveTypeEnum);

        IList<SiteMapProductResult> GetSiteMapProducts();
        

    }
}
