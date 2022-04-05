using MakinaTurkiye.Entities.Tables.Catalog;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreActivityCategory : BaseEntity
    {
        public int StoreActivityCategoryId { get; set; }
        public int CategoryId { get; set; }
        public int MainPartyId { get; set; }

        public virtual Store Store { get; set; }
        public virtual Category Category { get; set; }


    }
}
