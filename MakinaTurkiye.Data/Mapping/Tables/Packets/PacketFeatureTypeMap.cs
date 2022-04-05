using MakinaTurkiye.Entities.Tables.Packets;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Packets
{
    public class PacketFeatureTypeMap : EntityTypeConfiguration<PacketFeatureType>
    {
        public PacketFeatureTypeMap()
        {
            this.ToTable("PacketFeatureType");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.PacketFeatureTypeId);
        }
    }
}
