using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreUpdated : BaseEntity
    {
        public int StoreUpdatedId { get; set; }
        public int MainPartyId { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
