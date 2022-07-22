using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MakinaTurkiye.Entities.Tables.Packets;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class PacketViewModel
    {
        public IList<Packet> PacketItems { get; set; }
        public IList<PacketFeatureType> PacketFeatureTypeItems { get; set; }
        public IList<PacketFeature> PacketFeatureItems { get; set; }

    }
}