using MakinaTurkiye.Entities.Tables.Content;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Content
{
    public class BaseMenuMap : EntityTypeConfiguration<BaseMenu>
    {
        public BaseMenuMap()
        {
            this.ToTable("BaseMenu");
            this.Ignore(b => b.Id);
            this.HasKey(b => b.BaseMenuId);
        }
    }
}
