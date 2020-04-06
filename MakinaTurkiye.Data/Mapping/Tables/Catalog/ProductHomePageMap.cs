using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductHomePageMap:EntityTypeConfiguration<ProductHomePage>
    {
        public ProductHomePageMap()
        {
            this.ToTable("ProductHomePage");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ProductHomePageId);
            //this.HasRequired(x => x.Category).WithMany(x => x.ProductHomePages).HasForeignKey(x => x.CategoryId);
        }
    }
}
