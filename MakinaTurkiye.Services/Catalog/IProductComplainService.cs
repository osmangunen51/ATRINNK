using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductComplainService
    {

        IList<ProductComplain> GetAllProductComplain();
        IList<ProductComplainType> GetAllProductComplainType();
        void InsertProductComplain(ProductComplain productComplain);
        ProductComplain GetProductComplainByProductComplainId(int productComplainId);
        void DeleteProductComplain(ProductComplain productComplain);
       
    }
}
