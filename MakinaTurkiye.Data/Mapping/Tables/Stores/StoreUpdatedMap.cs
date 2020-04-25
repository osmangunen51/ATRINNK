using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Stores
{
    public class StoreUpdatedMap:EntityTypeConfiguration<StoreUpdated>
    {
        public StoreUpdatedMap()
        {
            this.ToTable("StoreUpdated");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.StoreUpdatedId);
        }
    }
}
