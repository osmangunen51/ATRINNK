using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class StoreProductCreatePropertie : BaseEntity
    {
        public int StoreProductCreatePropertieId { get; set; }
        public int? ConstantType { get; set; }
        public string Title { get; set; }
    }
}
