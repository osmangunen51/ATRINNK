using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Packets
{
    public class Packet:BaseEntity
    {
        //test yorum saıtır

        ICollection<PacketFeature> _packetFeatures;

        public int PacketId { get; set; }
        public string PacketName { get; set; }
        public string PacketDescription { get; set; }
        public decimal PacketPrice { get; set; }
        public int PacketDay { get; set; }
        public byte PacketOrder { get; set; }
        public string PacketColor { get; set; }
        public string HeaderColor { get; set; }
        public bool ?IsOnset { get; set; }
        public bool ?IsStandart { get; set; }
        public bool ?Registered { get; set; }
        public bool ?UnRegistered { get; set; }
        public bool IsDiscounted { get; set; }
        public bool SendReminderMail { get; set; }
        public byte? StarNumber { get; set; }
        public double ?ProductFactor { get; set;}
        public bool ? IsDopingPacket { get; set; }
        public Int16 ? DopingPacketDay { get; set; }

        public virtual ICollection<PacketFeature> PacketFeatures
        {
            get { return _packetFeatures ?? (_packetFeatures = new List<PacketFeature>()); }
            protected set { _packetFeatures = value; }
        }
        
    }
}
