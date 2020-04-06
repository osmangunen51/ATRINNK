using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductComplainMap : EntityTypeConfiguration<ProductComplain>
    {
         public ProductComplainMap()
        {
            this.ToTable("ProductComplain");
            this.Ignore(pc => pc.Id);
            this.HasKey(pc => pc.ProductComplainId);

            //this.HasRequired(p => p.Product).WithMany(pc => pc.ProductComplains).HasForeignKey(pc => pc.ProductId);
            //this.HasRequired(p => p.Member).WithMany(m => m.ProductComplains).HasForeignKey(p => p.MemberMainPartyId);
        }
    }
}
