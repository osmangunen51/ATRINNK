using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class MemberInfo
    {
        public int MainPartyId { get; set; }
        public string MemberNo { get; set; }
        public int? MemberType { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string MemberPassword { get; set; }
        public string MemberEmail { get; set; }
        public int? MemberTitleType { get; set; }
        public bool? Active { get; set; }
        public int Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public List<Address> Address { get; set; }
        public byte? StoreState { get; set; }
        public int StoreMainPartyId { get; set; }
        public bool? PhoneActive { get; set; }
        public string Phone { get; set; }
    }
}