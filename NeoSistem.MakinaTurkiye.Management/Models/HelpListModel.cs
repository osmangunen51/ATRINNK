using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class HelpListModel
    {
        public HelpListModel()
        {
            this.HelpModels = new List<HelpModel>();
        }
        public List<HelpModel> HelpModels { get; set; }
        public List<HelpModel> WHelpModels { get; set; } = new List<HelpModel>();
        public int TotalPage { get; set; }
        public int CurrentPage { get; set; }


    }
}