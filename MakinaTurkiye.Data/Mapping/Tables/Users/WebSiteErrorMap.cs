using MakinaTurkiye.Entities.Tables.Users;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Users
{
    public class WebSiteErrorMap:EntityTypeConfiguration<WebSiteError>
    {
        public WebSiteErrorMap()
        {
            this.ToTable("WebSiteError");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.WebSiteErrorId);

        }
    }
}
