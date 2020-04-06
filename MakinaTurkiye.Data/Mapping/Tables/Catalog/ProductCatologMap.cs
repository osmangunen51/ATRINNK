using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductCatologMap:EntityTypeConfiguration<ProductCatolog>
    {
        public ProductCatologMap()
        {
            this.ToTable("ProductCatolog");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ProductCatologId);

            //this.HasRequired(p => p.Product).WithMany(c => c.ProductCatologs).HasForeignKey(p => p.ProductId);
        }

    }
}
