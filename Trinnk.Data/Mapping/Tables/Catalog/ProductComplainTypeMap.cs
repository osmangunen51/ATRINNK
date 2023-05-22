using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class ProductComplainTypeMap : EntityTypeConfiguration<ProductComplainType>
    {
        public ProductComplainTypeMap()
        {
            this.ToTable("ProductComplainType");
            this.Ignore(c => c.Id);
            this.HasKey(c => c.ProductComplainTypeId);
        }

    }
}
