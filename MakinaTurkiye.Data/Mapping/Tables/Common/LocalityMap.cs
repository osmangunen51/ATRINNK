using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class LocalityMap:EntityTypeConfiguration<Locality>
    {
        public LocalityMap()
        {
            this.ToTable("Locality");
            this.Ignore(l => l.Id);
        }
    }
}
