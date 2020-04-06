using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class DeletedProductRedirect:BaseEntity
    {
        public int DeletedProductRedirectId { get; set; }
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
    }
}
