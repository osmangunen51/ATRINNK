using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class DataFilterModel
    {
        //test zorunlu



        public DataFilterModel()
        {
            this.ItemModels = new List<DataFilterItemModel>();
        }
        public int FilterId { get; set; }
        public string FilterName { get; set; }

        public bool SelectedFilter { get; set; }
        public string ClearFilterText { get; set; }
        public string ClearFilterUrl { get; set; }
        public string SelectedFilterItemName { get; set; }
        public int SelectedFilterItemCount { get; set; }
        public string SelectedFilterUrl { get; set; }

        public List<DataFilterItemModel> ItemModels { get; set; }
    }
}