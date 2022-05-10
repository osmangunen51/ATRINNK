using System.Collections.Generic;
namespace MakinaTurkiye.Api.View
{
    public class StoreCompanyPictureItem
    {
        public int CompanyPictureId { get; set; } = 0;
        public string Value { get; set; }
    }
    public class StoreCompanyPicture
    {
        public List<StoreCompanyPictureItem> List { get; set; } = new List<StoreCompanyPictureItem>();
        public int MainPartyId { get; set; } = 0;
    }
}