using System;

namespace MakinaTurkiye.Api.View
{
    public class Token
    {
        public string Key { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(365);
        public string PrivateAnahtar { get; set; } = "";
    }
}
