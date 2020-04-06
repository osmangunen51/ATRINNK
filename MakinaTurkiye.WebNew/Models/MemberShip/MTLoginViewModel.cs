using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.MemberShip
{
    public class MTLoginViewModel
    {
        public MTLoginViewModel()
        {
            this.LoginModel = new LoginModel();
            this.MembershipViewModel = new MTMembershipFormModel();

        }
        public LoginModel LoginModel { get; set; }
        public MTMembershipFormModel MembershipViewModel { get; set; }
        public byte LoginTabType { get; set; }
    }
}