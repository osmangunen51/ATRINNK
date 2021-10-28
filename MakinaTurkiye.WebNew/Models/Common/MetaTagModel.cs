namespace NeoSistem.MakinaTurkiye.Web.Models.Common
{
    public class MetaTagModel
    {
        public string Title { get; set; }
        public string Url { get; set; } = "";

        public string Image { get; set; } = "";


        public string Description { get; set; }
        public string Keywords { get; set; }
        public string Robots { get; set; }
    }
}