using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class ProductStatisticMap : EntityTypeConfiguration<ProductStatistic>
    {
        public ProductStatisticMap()
        {
            this.ToTable("ProductStatistic");
        }
    }
}
