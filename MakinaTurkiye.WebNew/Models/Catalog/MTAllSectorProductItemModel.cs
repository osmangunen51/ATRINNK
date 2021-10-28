namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTAllSectorProductItemModel
    {
        public MTAllSectorProductItemModel()
        {
            this.PicturePaths = new string[5];
        }
        public int ProductId { get; set; }
        public int SectorId { get; set; }
        public string ProductName { get; set; }
        public string[] PicturePaths { get; set; }
        public string BrandName { get; set; }
        public string ModelName { get; set; }
        public string Price { get; set; }
        public string ProductUrl { get; set; }
        public string CurrencyCss { get; set; }
        public string ProductContactUrl { get; set; }

    }
}