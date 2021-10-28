namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Stores.StoresViewModel
{
    public class MTSettingItemModel
    {
        public int PhoneId { get; set; }
        public string PhoneNumber { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public string PhoneTypeText { get; set; }
        public int StoreMainPartyId { get; set; }
        public bool SaturdayWorking { get; set; }
        public bool SundayWorking { get; set; }
    }
}