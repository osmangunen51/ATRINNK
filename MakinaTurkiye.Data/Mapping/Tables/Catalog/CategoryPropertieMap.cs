using MakinaTurkiye.Entities.Tables.Catalog;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Catalog
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
