using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.StoredProcedures.Members
{
    public class MemberByPhoneResult
    {
        public int MainPartyId { get; set; }
        public string PhoneNumber { get; set; }
        public byte MemberType { get; set; }
        public int PreRegistrationStoreId { get; set; }
    }
}
