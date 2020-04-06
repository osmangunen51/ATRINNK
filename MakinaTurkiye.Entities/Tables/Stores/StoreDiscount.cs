using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreDiscount : BaseEntity
    {
        public int StoreDiscountId { get; set; }
        public int StoreMainPartyId { get; set; }
        public int UserId { get; set; }
        public int OrderId { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        
        public DateTime RecordDate { get; set; }
    }
}
