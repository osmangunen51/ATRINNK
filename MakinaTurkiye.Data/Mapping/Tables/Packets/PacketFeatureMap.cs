using MakinaTurkiye.Entities.Tables.Packets;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Packets
{
    public class PacketFeatureMap:EntityTypeConfiguration<PacketFeature>
    {
        public PacketFeatureMap()
        {
            this.ToTable("PacketFeature");
            this.Ignore(pf=>pf.Id);
            this.HasKey(pf=>pf.PacketFeatureId);

            this.HasRequired(pf => pf.Packet).WithMany(p => p.PacketFeatures).HasForeignKey(pf=>pf.PacketId);
            this.HasRequired(pf => pf.PacketFeatureType).WithMany(pft => pft.PacketFeatures).HasForeignKey(pf=>pf.PacketFeatureTypeId);
        }
    }
}
