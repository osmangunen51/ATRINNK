using Trinnk.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Catalog
{
    public class CategoryPropertieMap : EntityTypeConfiguration<CategoryPropertie>
    {
        public CategoryPropertieMap()
        {
            this.ToTable("CategoryPropertie");
            this.HasKey(x => x.CategoryPropertieId);
            this.Ignore(x => x.Id);

        }
    }
}
