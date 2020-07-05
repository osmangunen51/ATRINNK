namespace NeoSistem.MakinaTurkiye.Management.Models.ProductModels
{
    public class MTProductExcelItem
    {
        public int UrunId { get; set; }
        public string UrunNo { get; set; }
        public string UrunAdi { get; set; }
        public string ProductDescription { get; set; }
        public string ProductBriefDetail { get; set; }
        public string WarrantyPeriod { get; set; }
        public double ProductPrice { get; set; }
        public string Mensei { get; set; }
        public string Keywords { get; set; }
        public bool Active { get; set; }

        public string Kdv { get; set; }

    }
}