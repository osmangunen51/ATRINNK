namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Stores.StoresViewModel
{
    public class MTCreateCatologViewModel
    {
        public MTCreateCatologViewModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.CreateCatologForm = new MTCreateCatologForm();
        }
        public MTCreateCatologForm CreateCatologForm { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}