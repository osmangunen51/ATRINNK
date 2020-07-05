using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.Catolog
{
    public class CategoryImageModel
    {
        public CategoryImageModel()
        {
            this.IconModel = new IconModel();
            this.BannerItems = new List<Banner>();
        }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        
        public IconModel IconModel { get; set; }
            public string HomePageImagePath { get; set; }
        public List<Banner> BannerItems { get; set; }
    }
}