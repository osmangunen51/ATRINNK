using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
   public class ChangeEmailModel
    {
        public int MemberMainPartyId { get; set; }
        public string OldEmail { get; set; }
        public string NewEmail { get; set; }
        public string NewEmailAgain { get; set; }
        public string MemberPassword { get; set; }
    }
}
