using System;

namespace MakinaTurkiye.Entities.Tables.Packets
{
    public class PacketFeature:BaseEntity
    {
        public int PacketFeatureId { get; set; }
        public int PacketId { get; set; }
        public byte PacketFeatureTypeId { get; set; }
        public byte FeatureType { get; set; }
        public string FeatureContent { get; set; }
        public bool? FeatureActive { get; set; }
        public Int16? FeatureProcessCount { get; set;}

        public virtual Packet Packet { get; set; }
        public virtual PacketFeatureType PacketFeatureType { get; set; }
    }
}
