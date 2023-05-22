namespace NeoSistem.Trinnk.Web.Models.ProductRequests
{
    public class MTProductRequestForm
    {

        public string PhoneNumber { get; set; }
        public string Message { get; set; }
        public int SectorId { get; set; }
        public int CategoryId { get; set; }
        public int ProductGroupId { get; set; }
        public int BrandId { get; set; }
        public int ModelId { get; set; }
        public int SeriesId { get; set; }
    }
}