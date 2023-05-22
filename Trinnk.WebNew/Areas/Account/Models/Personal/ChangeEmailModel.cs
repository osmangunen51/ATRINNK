#region Using Directives
using NeoSistem.Trinnk.Web.Models;
#endregion

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Personal
{
    public class ChangeEmailModel
    {
        #region Public Properties
        public MemberModel Member { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        #endregion
    }
}