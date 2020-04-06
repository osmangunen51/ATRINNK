using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreSector : BaseEntity
    {
        public int StoreSectorId { get; set; }
        public int StoreMainPartyId { get; set; }
        public int CategoryId { get; set; }
        public DateTime RecordDate { get; set; }

        public virtual Store Store { get; set; }

    }
}
