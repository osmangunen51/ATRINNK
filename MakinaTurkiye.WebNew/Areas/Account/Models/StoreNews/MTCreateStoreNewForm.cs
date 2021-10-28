namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.StoreNews
{
    public class MTCreateStoreNewForm
    {
        public MTCreateStoreNewForm()
        {
            this.LeftMenu = new LeftMenuModel();
        }
        public int StoreNewId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int StoreMainPartyId { get; set; }
        public string ImagePath { get; set; }
        public byte NewType { get; set; }
        public string PageTitle { get; set; }
        public LeftMenuModel LeftMenu { get; set; }

    }
}