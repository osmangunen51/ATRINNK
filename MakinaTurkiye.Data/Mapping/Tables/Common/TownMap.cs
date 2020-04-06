using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class TownMap: EntityTypeConfiguration<Town>
    {
        public TownMap()
        {
            this.ToTable("Town");
            this.Ignore(t => t.Id);
            this.HasKey(t => t.TownId);
        }
    }
}
