#region Using Directives
using NeoSistem.MakinaTurkiye.Web.Models;
#endregion

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal
{
    public class ChangeEmailModel
    {
        #region Public Properties
        public MemberModel Member { get; set; }
        public LeftMenuModel LeftMenu { get; set; }
        #endregion
    }
}