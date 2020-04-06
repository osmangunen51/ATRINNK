using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductPictureModel
    {
       public string SmallPath { get; set; }
       public string LargePath { get; set; }
       public string MegaPicturePath { get; set; }
       public string PictureName { get; set; }
       public string ProductName { get; set; }
    }
}