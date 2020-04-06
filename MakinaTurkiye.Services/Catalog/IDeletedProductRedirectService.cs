using MakinaTurkiye.Entities.Tables.Catalog;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.Catalog
{
    public interface IDeletedProductRedirectService
    {
        DeletedProductRedirect GetDeletedProductRedirectByProductId(int productId);
        void InsertDeletedProductRedirect(DeletedProductRedirect deletedProductRedirect);
    }
}
