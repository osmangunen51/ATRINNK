using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreActivityCategoryMap : EntityTypeConfiguration<StoreActivityCategory>
    {
        public StoreActivityCategoryMap()
        {
            this.ToTable("StoreActivityCategory");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreActivityCategoryId);

            //this.HasRequired(s => s.Category).WithMany(c => c.StoreActivityCategories).HasForeignKey(x => x.CategoryId);

            this.HasRequired(s => s.Store).WithMany(st => st.StoreActivityCategories).HasForeignKey(s => s.MainPartyId);
        }

    }
}
