namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Stores
{
    public class MTContactSettingItemFormModel
    {
        public int PhoneId { get; set; }
        public int StoreMainPartyId { get; set; }
        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool AvaliableAlways { get; set; }
        public bool SaturdayWorking { get; set; }
        public bool SundayWorking { get; set; }
    }
}