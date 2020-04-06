using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductComplainDetailMap : EntityTypeConfiguration<ProductComplainDetail>
    {
        public ProductComplainDetailMap()
        {
            this.ToTable("ProductComplainDetail");
            this.Ignore(c => c.Id);
            this.HasKey(c => c.ProductComplainDetailId);

            this.HasRequired(pcd => pcd.ProductComplainType).WithMany(pc => pc.ProductComplainDetails).HasForeignKey(pcd => pcd.ProductComplainTypeId);
            this.HasRequired(pcd => pcd.ProductComplain).WithMany(pc => pc.ProductComplainDetails).HasForeignKey(pcd => pcd.ProductComplainId);
        }
    }
}
