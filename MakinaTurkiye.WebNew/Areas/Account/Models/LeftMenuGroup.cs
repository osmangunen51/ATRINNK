namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models
{
    #region Using Directives
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web;
    #endregion

    public class LeftMenuGroup
    {
        #region Public Properties
        public string Name { get; set; }
        public List<LeftMenuItems> Items { get; set; }
        #endregion

        #region Public Constructor
        public LeftMenuGroup()
        {
            Items = new List<LeftMenuItems>();
        }
        #endregion
    }
}