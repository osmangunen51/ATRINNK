
using MakinaTurkiye.Entities.Tables.Content;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Content
{
    public class BaseMenuImageMap:EntityTypeConfiguration<BaseMenuImage>
    {
        public BaseMenuImageMap()
        {
            this.ToTable("BaseMenuImage");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.BaseMenuImageId);

            this.HasRequired(x => x.BaseMenu).WithMany(b => b.BaseMenuImages).HasForeignKey(x => x.BaseMenuId);
        }

    }
}
