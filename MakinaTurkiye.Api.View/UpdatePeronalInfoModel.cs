using System;

namespace MakinaTurkiye.Api.View
{
    public class UpdatePeronalInfoModel
    {
        public int MemberMainPartyId { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public DateTime BirthDate { get; set; }
        public bool GenderMan { get; set; }
        public bool GenderWoman { get; set; }
    }
}