using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class UrlRedirectMap : EntityTypeConfiguration<UrlRedirect>
    {
        public UrlRedirectMap()
        {
            this.ToTable("UrlRedirect");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UrlRedirectId);
        }
    }
}
