using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreDiscountMap:EntityTypeConfiguration<StoreDiscount>
    {
        public StoreDiscountMap()
        {
            this.ToTable("StoreDiscount");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreDiscountId);
        }
    }
}
