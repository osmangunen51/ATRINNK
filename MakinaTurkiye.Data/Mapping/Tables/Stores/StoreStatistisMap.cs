using MakinaTurkiye.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreStatisticMap : EntityTypeConfiguration<StoreStatistic>
    {
        public StoreStatisticMap()
        {
            this.ToTable("StoreStatistic");

        }

    }
}
