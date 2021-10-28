namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Videos
{
    public class MTVideoCreateModel
    {
        public MTVideoCreateModel()
        {
            this.LeftMenu = new LeftMenuModel();
        }
        public string VideoTitle { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}