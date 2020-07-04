﻿using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class MTProductViewModel
    {
        public MTProductViewModel()
        {
            this.MTProducts = new SearchModel<MTProductItem>();
            this.MTAdvertsTopViewModel = new MTAdvertsTopViewModel();
            this.LeftMenuModel = new LeftMenuModel();
        }

        public int ProductActiveType { get; set; }
        public byte DisplayType { get; set; }
        public int ProductActive { get; set; }
        public string PageTitle { get; set; }
        public byte OrderType { get; set; }

        public MTAdvertsTopViewModel MTAdvertsTopViewModel { get; set; }
        public SearchModel<MTProductItem> MTProducts { get; set; }
        public LeftMenuModel LeftMenuModel { get; set; }

    }
}