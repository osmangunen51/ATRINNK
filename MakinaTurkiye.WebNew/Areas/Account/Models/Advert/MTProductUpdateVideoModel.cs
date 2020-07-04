using NeoSistem.MakinaTurkiye.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class MTProductUpdateVideoModel
    {
        public MTProductUpdateVideoModel()
        {
            this.Videos = new List<VideoModel>();
            this.LeftMenu = new LeftMenuModel();
        }
        public List<VideoModel> Videos { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        public string Title { get; set; }
        public string ProductName { get; set; }
        public int ProductId { get; set; }
    }
}