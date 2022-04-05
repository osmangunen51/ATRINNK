using MakinaTurkiye.Entities.Tables.Payments;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Payments
{
    public class VirtualPosMap : EntityTypeConfiguration<VirtualPos>
    {
        public VirtualPosMap()
        {
            this.ToTable("VirtualPos");
            this.Ignore(vp => vp.Id);
            this.HasKey(vp => vp.VirtualPosId);
        }
    }
}
