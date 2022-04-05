using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels
{
    public class BaseMenuModel
    {
        public BaseMenuModel()
        {
            this.BaseMenuItems = new List<BaseMenuItem>();
        }
        public List<BaseMenuItem> BaseMenuItems { get; set; }

    }
}