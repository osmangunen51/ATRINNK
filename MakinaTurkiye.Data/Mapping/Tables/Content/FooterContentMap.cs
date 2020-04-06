using MakinaTurkiye.Entities.Tables.Content;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Content
{
    public class FooterContentMap : EntityTypeConfiguration<FooterContent>
    {
        public FooterContentMap()
        {
            this.ToTable("FooterContent");
            this.Ignore(fc=>fc.Id);
            this.HasKey(fc=>fc.FooterContentId);

            this.HasRequired(fc => fc.FooterParent).WithMany(fp => fp.FooterContents).HasForeignKey(fp=>fp.FooterParentId);
        }
    }
}
