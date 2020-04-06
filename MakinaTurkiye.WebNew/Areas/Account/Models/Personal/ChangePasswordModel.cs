#region Using Diretives
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models;
#endregion

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal
{
    public class ChangePasswordModel
    {
        public MemberModel Member { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}