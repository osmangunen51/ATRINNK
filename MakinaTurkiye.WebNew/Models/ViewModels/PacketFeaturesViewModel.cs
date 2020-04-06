using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.ViewModels
{
    public class PacketFeaturesViewModel
    {
        public int PacketFeatureId { get; set; }
        public int PacketId { get; set; }
        public byte PacketFeatureTypeId { get; set; }


        public string FeatureContent { get; set; }
        public bool? FeatureActive { get; set; }
        public Int16? FeatureProcessCount { get; set; }

        public byte FeatureType { get; set; }
        public string PacketFeatureTypeName { get; set; }
    }
}