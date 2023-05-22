using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class PropertieAttrMap : EntityTypeConfiguration<PropertieAttr>
    {
        public PropertieAttrMap()
        {
            this.ToTable("PropertieAttr");
            this.HasKey(x => x.PropertieAttrId);
            this.Ignore(x => x.Id);
        }
    }
}
