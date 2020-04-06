namespace NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels
{
    public class BaseMenuImageModel
    {
        public int BaseMenuImageId { get; set; }
        public int BaseMenuId { get; set; }
        public string ImagePath { get; set; }
        public bool Active { get; set; }
        public string Url { get; set; }
       
    }
}