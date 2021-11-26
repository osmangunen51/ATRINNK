using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class OrderConfirmation: BaseEntity
    {
        public int OrderConfirmationId { get; set; }
        public int StoreMainPartyId { get; set; }
        public int OrderId { get; set; }
        public DateTime RecordDate { get; set; }

        public string IpAddress;
    }
}
