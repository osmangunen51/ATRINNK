using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
    public class UserRegister
    {
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public bool IsContractChecked { get; set; }
    }
}
