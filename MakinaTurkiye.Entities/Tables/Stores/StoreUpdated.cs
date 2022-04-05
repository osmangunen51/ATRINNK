using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreUpdated : BaseEntity
    {
        public int StoreUpdatedId { get; set; }
        public int MainPartyId { get; set; }
        public DateTime UpdatedDate { get; set; }
    }
}
