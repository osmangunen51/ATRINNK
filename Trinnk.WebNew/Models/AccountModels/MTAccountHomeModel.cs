using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Helpers;
using NeoSistem.Trinnk.Web.Models.ViewModels;
using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.AccountModels
{
    public class MTAccountHomeModel
    {
        public MTAccountHomeModel()
        {
            this.PacketFeatures = new List<PacketFeaturesViewModel>();
            this.AccountHomeCenterCenterModel = new MTAccountHomeCenterModel();


        }
        public bool hasStandartPacket { get; set; }
        public virtual List<PacketFeaturesViewModel> PacketFeatures { get; set; }
        public List<string> PacketFeatureTypeNames;
        public string StoreUrl { get; set; }
        public string PacketDescription { get; set; }
        public string PacketColor { get; set; }
        public MTAccountHomeCenterModel AccountHomeCenterCenterModel { get; set; }
        public ProfilRateResult ProfileFillRate { get; set; }
        public DateTime? OrderPacketEndDate { get; set; }
        public int PacketFinishDay { get; set; }
        public LeftMenuModel LeftMenuModel { get; set; }
    }
}