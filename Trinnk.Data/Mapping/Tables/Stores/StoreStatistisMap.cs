using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreStatisticMap : EntityTypeConfiguration<StoreStatistic>
    {
        public StoreStatisticMap()
        {
            this.ToTable("StoreStatistic");

        }

    }
}
