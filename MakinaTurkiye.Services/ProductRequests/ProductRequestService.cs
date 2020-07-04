using MakinaTurkiye.Core;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.ProductRequests;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.ProductRequests
{
    public class ProductRequestService:IProductRequestService
    {
        IRepository<ProductRequest> _productRequestRepository;

        public ProductRequestService(IRepository<ProductRequest> productRequestRepository)
        {
            this._productRequestRepository = productRequestRepository;
        }

        public void DeleteProductRequest(ProductRequest productRequest)
        {
            if (productRequest == null)
                throw new ArgumentNullException("productRequest");
            _productRequestRepository.Delete(productRequest);
        }

        public IPagedList<ProductRequest> GetAllProductRequests(int pageIndex, int pageSize)
        {
            var query = _productRequestRepository.Table;
            int totalRecord = query.Count();
            var source = query.OrderByDescending(x => x.ProductRequestId).Skip(pageIndex * pageSize - pageSize).Take(pageSize);
            return new PagedList<ProductRequest>(source, pageIndex, pageSize,totalRecord);
        }

        public ProductRequest GetProductRequestByProductRequestId(int productRequestId)
        {
            if (productRequestId == 0)
                throw new ArgumentException();
            var query = _productRequestRepository.Table;
            return query.FirstOrDefault(x => x.ProductRequestId == productRequestId);
        }

        public void InsertProductRequest(ProductRequest productRequest)
        {
            if (productRequest == null)
                throw new ArgumentNullException("productRequest");
            _productRequestRepository.Insert(productRequest);
        }

        public void UpdateProductRequest(ProductRequest productRequest)
        {
            if (productRequest == null)
                throw new ArgumentNullException("productRequest");
            _productRequestRepository.Update(productRequest);
        }
    }
}
