namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models
{
    #region Using Directives
    using System.Collections.Generic;
    #endregion

    public class LeftMenuModel
    {
        #region Public Properties

        public List<LeftMenuItems> Items { get; set; }

        #endregion

        #region Public Constructor
        public LeftMenuModel()
        {
            Items = new List<LeftMenuItems>();
        }
        #endregion
    }
}