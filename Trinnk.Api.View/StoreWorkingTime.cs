using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class StoreWorkingTime
    {
        public int PhoneId { get; set; }
        public string Phone { get; set; }
        public string PhoneName { get; set; }

        public string StartTime { get; set; }
        public string EndTime { get; set; }
        public bool IsSaturday { get; set; }
        public bool IsSunday { get; set; }
        public bool IsAllDays { get; set; }
        public int MainPartyId { get; set; } = 0;
    }
}