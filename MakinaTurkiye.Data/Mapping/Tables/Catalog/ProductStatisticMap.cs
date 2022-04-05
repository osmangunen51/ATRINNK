using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
{
    public class ProductStatisticMap : EntityTypeConfiguration<ProductStatistic>
    {
        public ProductStatisticMap()
        {
            this.ToTable("ProductStatistic");
        }
    }
}
