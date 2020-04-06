using MakinaTurkiye.Entities.Tables.Common;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class DistrictMap : EntityTypeConfiguration<District>
    {
        public DistrictMap()
        {
            this.ToTable("District");
            this.Ignore(d => d.Id);
            this.HasKey(d => d.DistrictId);
        }
    }
}
