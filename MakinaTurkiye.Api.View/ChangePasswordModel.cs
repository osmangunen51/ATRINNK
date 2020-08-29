using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
    public class ChangePasswordModel
    {
        public int MemberMainPartyId { get; set; }
        public string Email { get; set; }
        public string OldPassword { get; set; }
        public string NewPassword { get; set; }
        public string NewPasswordAgain { get; set; }

    }
}
