using Trinnk.Entities.Tables.Packets;
using System.Collections.Generic;

namespace Trinnk.Services.Packets
{
    public interface IPacketService : ICachingSupported
    {
        IList<Packet> GetAllPacket();
        Packet GetPacketByPacketId(int packetId);
        Packet GetPacketByIsStandart(bool isStandart);

        IList<Packet> GetPacketIsOnsetFalseByDiscountType(bool isDiscounted);

        IList<PacketFeature> GetAllPacketFeatures();
        PacketFeature GetPacketFeatureByPacketIdAndPacketFeatureTypeId(int packetId, int packetFeatureTypeId);

        PacketFeatureType GetPacketFeatureTypeByPacketFeatureTypeId(int packetFeatureTypeId);
        IList<PacketFeatureType> GetAllPacketFeatureTypes();

    }
}
