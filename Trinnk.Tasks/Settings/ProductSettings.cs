namespace Trinnk.Tasks.Settings
{
    public class ProductSettings
    {

        public static string BaseFolder { get { return System.Diagnostics.Debugger.IsAttached ? "C:/Users/bilal/Desktop/MustafaProjects/NeoSistem.Trinnk/Trinnk.WebNew/UserFiles" : "C:/Web/s.trinnk.com"; } }

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
