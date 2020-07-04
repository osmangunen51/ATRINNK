using MakinaTurkiye.Entities.Tables.Content;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Content
{
    public class FooterParentMap:EntityTypeConfiguration<FooterParent>
    {
        public FooterParentMap()
        {
            this.ToTable("FooterParent");
            this.HasKey(fp=>fp.FooterParentId);
            this.Ignore(fp=>fp.Id);
            
        }
    }
}
