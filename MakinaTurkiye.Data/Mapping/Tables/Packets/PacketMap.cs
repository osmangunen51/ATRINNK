using MakinaTurkiye.Entities.Tables.Packets;
using System.Data.Entity.ModelConfiguration;

namespace MakinaTurkiye.Data.Mapping.Tables.Packets
{
    public class PacketMap : EntityTypeConfiguration<Packet>
    {
        public PacketMap()
        {
            this.ToTable("Packet");
            this.Ignore(p => p.Id);
            this.HasKey(p => p.PacketId);
        }
    }
}
