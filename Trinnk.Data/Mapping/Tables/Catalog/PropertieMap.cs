using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class PropertieMap : EntityTypeConfiguration<Propertie>
    {
        public PropertieMap()
        {
            this.ToTable("Propertie");
            this.HasKey(x => x.PropertieId);
            this.Ignore(x => x.Id);

        }
    }
}
