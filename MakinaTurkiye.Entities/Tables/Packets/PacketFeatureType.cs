using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Packets
{
    public class PacketFeatureType:BaseEntity
    {
        ICollection<PacketFeature> _packetFeatures;

        public byte PacketFeatureTypeId { get; set; }
        public string PacketFeatureTypeName { get; set; }
        public string PacketFeatureTypeDesc { get; set; }
        public byte PacketFeatureTypeOrder { get; set; }

        public virtual ICollection<PacketFeature> PacketFeatures
        {

            get { return _packetFeatures ?? (_packetFeatures = new List<PacketFeature>()); }
            protected set { _packetFeatures = value; }
        }
    }
}
