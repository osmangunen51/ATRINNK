using MakinaTurkiye.Core;
using MakinaTurkiye.Entities.Tables.Catalog;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IProductCommentService  : ICachingSupported
    {
        IList<ProductComment> GetProductCommentsByProductId(int productId, bool showHidden = false);
        IList<ProductComment> GetProductCommentsByMainPartyId(int mainPartyId);
        void InsertProductComment(ProductComment productComment);
        void DeleteProductComment(ProductComment productComment);
        ProductComment GetProductCommentByProductCommentId(int productCommentId);
        void UpdateProductComment(ProductComment productComment);
        IPagedList<ProductComment> GetProductComments(int pageSize,int pageIndex,int productId=0,bool reported=false);
        IList<ProductComment> GetProductCommentsForStoreByMemberMainPartyId(int memberMainPartyId);
   


    }
}
