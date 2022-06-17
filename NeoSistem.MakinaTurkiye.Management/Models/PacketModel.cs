namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using EnterpriseEntity.Extensions.Data;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class PacketModel
    {
        public int PacketId { get; set; }
        public string PacketName { get; set; }
        public string PacketDescription { get; set; }
        public int? PacketDay { get; set; }
        public decimal? PacketPrice { get; set; }
        public byte PacketOrder { get; set; }
        public string PacketColor { get; set; }
        public bool IsStandart { get; set; }
        public bool IsOnset { get; set; }
        public bool Registered { get; set; }
        public bool UnRegistered { get; set; }
        public bool SendReminderMail { get; set; }
        public bool IsDiscounted { get; set; }
        public string HeaderColor { get; set; }
        public byte? StarNumber { get; set; }
        public float? ProductFactor { get; set; }
        public bool IsDopingPacket { get; set; }
        public Int16? DopingPacketDay { get; set; }
        public bool IsTryPacket { get; set; }

        public bool ShowAdmin { get; set; }
        public bool ShowSetProcess { get; set; }

        public static SelectList Packets()
        {
            var packet = new Classes.Packet();
            var items = packet.GetDataSet().Tables[0].AsCollection<PacketModel>();
            items.Add(new PacketModel
            {
                PacketId = 0,
                PacketName = "-- Tüm Paketler --"
            });
            var list = new SelectList(items.OrderBy(c => c.PacketId), "PacketId", "PacketName", 0);
            return list;
        }

        public IList<PacketFeatureType> PacketFeatureTypeItems
        {
            get
            {
                IList<PacketFeatureType> pfTypeItems = null;
                using (var entities = new MakinaTurkiyeEntities())
                {
                    pfTypeItems = entities.PacketFeatureTypes.ToList();
                }
                return pfTypeItems;
            }
        }

        public IList<PacketFeature> PacketFeatureItems { get; set; }

    }

}