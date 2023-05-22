using Trinnk.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Common
{
    public class LocalityMap : EntityTypeConfiguration<Locality>
    {
        public LocalityMap()
        {
            this.ToTable("Locality");
            this.Ignore(l => l.Id);
        }
    }
}
