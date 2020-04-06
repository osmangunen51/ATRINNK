using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class StoreDetailInformationModel
    {
        public StoreDetailInformationModel()
        {
            this.StoreInformationModel = new StoreInformationModel();
            this.MemberInformationModel = new MemberInformationModel();
            this.StoreContactInfoModel = new StoreContactInfoModel();
            this.StoreMemberDescriptionItems = new List<StoreMemberDescriptionItem>();
        }
        public MemberInformationModel MemberInformationModel { get; set; }
        public StoreInformationModel StoreInformationModel { get; set; }
        public StoreContactInfoModel StoreContactInfoModel { get; set; }
        public List<StoreMemberDescriptionItem> StoreMemberDescriptionItems { get; set; }
        public string StoreActivityTypes { get; set; }
        public int ActiveProductCount { get; set; }
        public int PasiveProductCount { get; set; }
        public long SingularProductViewCount { get; set; }
        public long ProductViewCount { get; set; }
        public int WhatsappClickCount { get; set; }
        public string MemberMainPartyId { get; set; }


    }

}