using MakinaTurkiye.Entities.Tables.Packets;
using System.Collections.Generic;
namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class PacketViewModel
    {
        public IList<Packet> PacketItems { get; set; }
        public IList<PacketFeatureType> PacketFeatureTypeItems { get; set; }
        public IList<PacketFeature> PacketFeatureItems { get; set; }
        public bool AnyOrder { get; set; }
        public bool LastPageAdvertAdd { get; set; }
        public string Description { get; set; }
        public string BottomDescription { get; set; }
        public NeoSistem.MakinaTurkiye.Web.Models.ViewModels.StorePackagePurchaseRequestViewModel StorePackagePurchaseRequestViewModel { get; set; } = new ViewModels.StorePackagePurchaseRequestViewModel();
    }
}