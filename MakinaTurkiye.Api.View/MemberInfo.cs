using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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
        
        
    }
}
