using MakinaTurkiye.Entities.Tables.Content;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Content
{
    public class BaseMenuCategoryMap : EntityTypeConfiguration<BaseMenuCategory>
    {
        public BaseMenuCategoryMap()
        {
            this.ToTable("BaseMenuCategory");
            this.Ignore(b => b.Id);
            this.HasKey(b => b.BaseMenuCategoryId);

            this.HasRequired(b => b.BaseMenu).WithMany(b => b.BaseMenuCategories).HasForeignKey(b => b.BaseMenuId);
            //this.HasRequired(b => b.Category).WithMany(b => b.BaseMenuCategories).HasForeignKey(b => b.CategoryId);
        }

    }
}
