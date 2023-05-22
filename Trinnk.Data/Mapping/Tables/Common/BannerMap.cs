using Trinnk.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace Trinnk.Data.Mapping.Tables.Common
{
    public class BannerMap : EntityTypeConfiguration<Banner>
    {
        public BannerMap()
        {
            this.ToTable("Banner");
            this.Ignore(c => c.Id);
        }
    }
}
