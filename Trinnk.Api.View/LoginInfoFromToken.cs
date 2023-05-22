using System;

namespace Trinnk.Api.View
{
    public class LoginInfoFromToken
    {
        public string Key { get; set; } = "";
        public DateTime StartDate { get; set; } = DateTime.Now;
        public DateTime EndDate { get; set; } = DateTime.Now.AddDays(365);
        public string PrivateAnahtar { get; set; } = "";
        public string LoginMemberEmail { get; set; } = "";
        public string LoginMemberNameSurname { get; set; } = "";
        public int LoginMemberMainPartyId { get; set; }
    }
}