namespace NeoSistem.Trinnk.Web.Areas.Account.Models
{
    #region Using Directives
    using System.Collections.Generic;
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