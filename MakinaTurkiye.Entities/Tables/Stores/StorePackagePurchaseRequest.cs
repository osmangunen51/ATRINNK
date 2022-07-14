using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StorePackagePurchaseRequest:BaseEntity
    {
        public int MainPartyId { get; set; }
        public DateTime Date { get; set; }
    }
}
