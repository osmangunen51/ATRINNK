﻿namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
    using NeoSistem.MakinaTurkiye.Web.Helpers;
    using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
    using System;
    using System.Collections.Generic;

    public class MyAccountHomeModel
    {
        public MyAccountHomeModel()
        {
            this.HelpList = new List<MTHelpModeltem>();
        }
        public int? InboxMessageCount { get; set; }

        public int? ProductCount { get; set; }

        public long? ProductTotalViewCount { get; set; }

        public bool hasStandartPacket { get; set; }
        public virtual List<MTHelpModeltem> HelpList { get; set; }

        public virtual List<PacketFeaturesViewModel> PacketFeatures { get; set; }

        public List<string> PacketFeatureTypeNames;
        public string StoreUrl { get; set; }
        public string MemberType { get; set; }
        public string PacketDescription { get; set; }
        public string PacketColor { get; set; }
        public bool hasPacket { get; set; }
        public string Pag { get; set; }

        public ProfilRateResult ProfileFillRate { get; set; }

        public DateTime? OrderPacketEndDate { get; set; }

        public LeftMenuModel LeftMenuModel { get; set; }
        public bool OrderPriceCheck { get; set; }

    }

}