﻿#region Using Diretives
using NeoSistem.Trinnk.Web.Models;
#endregion

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Personal
{
    public class ChangePasswordModel
    {
        public MemberModel Member { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
    }
}