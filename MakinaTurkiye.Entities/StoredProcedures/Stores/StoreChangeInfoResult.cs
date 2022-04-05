using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Stores
{
    public class StoreChangeInfoResult
    {
        public string StoreName { get; set; }
        public int MainPartyId { get; set; }
        public string ChangeType { get; set; }
        public DateTime UpdatedDated { get; set; }

    }
}
