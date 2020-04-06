using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;

namespace MakinaTurkiye.Tasks.Settings
{
    public class ProductSettings
    {

        public static string BaseFolder { get { return System.Diagnostics.Debugger.IsAttached ? "C:/Users/bilal/Desktop/MustafaProjects/NeoSistem.MakinaTurkiye/MakinaTurkiye.WebNew/UserFiles" : "C:/Web/s.makinaturkiye.com"; } }

        public static string ProductImageFolder
        {
            get
            {
                return BaseFolder + "/Product/";
            }
        }


        public static string ProductThumbSizes { get { return "*x980;500x375;160x120;100x75;200x150;400x300;900x675;100x*"; } }
        public static string VideoFolder = BaseFolder + "/Video/";
        public static string VideoThumbFodler = BaseFolder + "/VideoThumb/";

    }
}
