namespace NeoSistem.Trinnk.Web.Areas.Account.Models
{
    #region Using Directives
    using System.Collections.Generic;
    #endregion

    public class LeftMenuItems
    {
        #region Public Properties

        public string Name { get; set; }

        public string Url { get; set; }

        public bool IsActive { get; set; }

        public int ControlNubmer { get; set; }

        public string IconName { get; set; }

        public List<LeftMenuGroup> GroupItems { get; set; }

        #endregion

        #region Public Constructor
        public LeftMenuItems()
        {
            GroupItems = new List<LeftMenuGroup>();
        }
        #endregion
    }
}