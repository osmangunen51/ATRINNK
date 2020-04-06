using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class MTAdvertsTopViewModel
    {
        public MTAdvertsTopViewModel()
        {
            this.MTAdvertFilterItemModel = new List<MTAdvertFilterItemModel>();
            this.MTOrderFilter = new List<MTAdvertFilterItemModel>();
            this.MTCategoriesFilter = new List<MTAdvertFilterItemModel>();
        }
        public List<MTAdvertFilterItemModel> MTAdvertFilterItemModel { get; set; }
        public List<MTAdvertFilterItemModel> MTOrderFilter { get; set; }
        public int TotalProductCount { get; set; }
        public List<MTAdvertFilterItemModel> MTCategoriesFilter { get; set; }
    }
}