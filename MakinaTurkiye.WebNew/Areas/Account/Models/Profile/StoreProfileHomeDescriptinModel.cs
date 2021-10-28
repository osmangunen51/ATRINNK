namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Profile
{
    public class StoreProfileHomeDescriptinModel
    {
        public StoreProfileHomeDescriptinModel()
        {
            this.LeftMenu = new LeftMenuModel();
        }
        public string StoreProfileDescription { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}