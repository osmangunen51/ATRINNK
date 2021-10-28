namespace NeoSistem.MakinaTurkiye.Web.Models.Videos
{
    public class MTVideoCategoryItemModel
    {
        public string CategoryUrl { get; set; }
        public string CategoryName { get; set; }
        public byte CategoryType { get; set; }
        public int CategoryId { get; set; }
        public int? CategoryParentId { get; set; }
    }
}