using MakinaTurkiye.Entities.StoredProcedures.Catalog;
using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface ICategoryPropertieService : ICachingSupported
    {

        IList<Propertie> GetAllProperties();

        Propertie GetPropertieById(int propertieId);

        IList<CategoryPropertieResult> GetPropertieByCategoryId(int categoryId);

        void InsertPropertie(Propertie propertie);

        void DeletePropertie(Propertie propertie);

        void UpdatePropertie(Propertie propertie);
  

        IList<PropertieAttr> GetPropertiesAttrByPropertieId(int propertieId);

        PropertieAttr GetPropertieAttrByPropertieAttrId(int propertieAttrId);

        void InsertPropertieAttr(PropertieAttr propertieAttr);

        void DeletePropertieAttr(PropertieAttr propertieAttr);

        void UpdatePropertieAttr(PropertieAttr propertieAttr);


        IList<CategoryPropertie> GetCategoryPropertiesByCategoryId(int categoryId);

        CategoryPropertie GetCategoryPropertieByCategoryPropertieId(int id);

        void InsertCategoryPropertie(CategoryPropertie categoryPropertie);

        void DeleteCategoryPropertie(CategoryPropertie categoryPropertie);

        void UpdateCategoryPropertie(CategoryPropertie categoryPropertie);

        IList<ProductPropertieValue> GetProductPropertieValuesByProductId(int productId);

        IList<ProductPropertieValueResult> GetProductPropertieValuesResultByProductId(int productId);

        ProductPropertieValue GetProductPropertieValueByProductPropertieId(int productPropertieId);

        void InsertProductProertieValue(ProductPropertieValue productPropertieValue);

        void DeleteProductProertieValue(ProductPropertieValue productPropertieValue);

        void UpdateProductProertieValue(ProductPropertieValue productPropertieValue);



    }
}
