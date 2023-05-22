using Trinnk.Entities.Tables.Stores;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Stores
{
    public class StoreDealerMap : EntityTypeConfiguration<StoreDealer>
    {
        public StoreDealerMap()
        {
            this.ToTable("StoreDealer");
            this.Ignore(sd => sd.Id);
            this.HasKey(sd => sd.StoreDealerId);



        }
    }
}
