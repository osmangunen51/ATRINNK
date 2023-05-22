using Trinnk.Entities.Tables.Seos;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Seos
{
    public class SeoDefinitionMap : EntityTypeConfiguration<SeoDefinition>
    {
        public SeoDefinitionMap()
        {
            this.ToTable("SeoDefinition");
            this.Ignore(s => s.Id);
            this.HasKey(s => s.SeoDefinitionId);
        }
    }
}
