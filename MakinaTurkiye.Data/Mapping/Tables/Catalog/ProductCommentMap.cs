using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductCommentMap:EntityTypeConfiguration<ProductComment>
    {
        public ProductCommentMap()
        {
            this.ToTable("ProductComment");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ProductCommentId);

            this.HasRequired(x => x.Product).WithMany(x => x.ProductComments).HasForeignKey(x => x.ProductId);
            this.HasRequired(x => x.Member).WithMany(x => x.ProductComments).HasForeignKey(x => x.MemberMainPartyId);
        }
    }
}
