using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class ProductPropertieValueMap : EntityTypeConfiguration<ProductPropertieValue>
    {
        public ProductPropertieValueMap()
        {
            this.ToTable("ProductPropertieValue");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.ProductPropertieId);
        }
    }
}
