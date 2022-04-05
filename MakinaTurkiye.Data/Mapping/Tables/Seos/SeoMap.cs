using MakinaTurkiye.Entities.Tables.Seos;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Seos
{
    public class SeoMap : EntityTypeConfiguration<Seo>
    {
        public SeoMap()
        {
            this.ToTable("Seo");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.SeoId);
        }


    }
}
