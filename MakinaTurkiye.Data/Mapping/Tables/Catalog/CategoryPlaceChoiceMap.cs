using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class CategoryPlaceChoiceMap:EntityTypeConfiguration<CategoryPlaceChoice>
    {
        public CategoryPlaceChoiceMap()
        {
            this.HasKey(cp=>cp.CategoryPlaceChoiceId);
            this.Ignore(cp=>cp.Id);
            this.ToTable("CategoryPlaceChoice");
            
            //this.HasRequired(cp => cp.Category).WithMany(c => c.CategoryPlaceChoices).HasForeignKey(c => c.CategoryId);

        }
    }
}
