using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.ProductRequests;

namespace MakinaTurkiye.Services.ProductRequests
{
    public interface IProductRequestService
    {
        IPagedList<ProductRequest> GetAllProductRequests(int pageIndex, int pageSize);
        ProductRequest GetProductRequestByProductRequestId(int productRequestId);

        void InsertProductRequest(ProductRequest productRequest);
        void DeleteProductRequest(ProductRequest productRequest);
        void UpdateProductRequest(ProductRequest productRequest);
    }
}
