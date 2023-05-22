using Trinnk.Core.Data;
using Trinnk.Entities.Tables.Catalog;
using System;
using System.Linq;

namespace Trinnk.Services.Catalog
{
    public class DeletedProductRedirectService : IDeletedProductRedirectService
    {
        #region fields

        private readonly IRepository<DeletedProductRedirect> _deletedProductRedirectRepository;

        #endregion

        public DeletedProductRedirectService(IRepository<DeletedProductRedirect> deletedProductRedirectRepository)
        {
            _deletedProductRedirectRepository = deletedProductRedirectRepository;
        }

        public DeletedProductRedirect GetDeletedProductRedirectByProductId(int productId)
        {
            var query = _deletedProductRedirectRepository.Table;
            return query.FirstOrDefault(x => x.ProductId == productId);

        }

        public void InsertDeletedProductRedirect(DeletedProductRedirect deletedProductRedirect)
        {
            if (deletedProductRedirect == null)
                throw new ArgumentNullException("deletedProductRedirect");
            _deletedProductRedirectRepository.Insert(deletedProductRedirect);
        }
    }
}
