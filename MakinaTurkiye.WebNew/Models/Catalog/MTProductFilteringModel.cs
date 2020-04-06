using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class MTProductFilteringModel
    {
        public MTProductFilteringModel()
        {
            this.SortOptionModels = new List<SortOptionModel>();
            this.CustomFilterModels = new List<CustomFilterModel>();
            this.DataFilterMoldes = new List<DataFilterModel>();            
        }

        public List<SortOptionModel> SortOptionModels { get; set; }
        public List<CustomFilterModel> CustomFilterModels { get; set; }
        public List<DataFilterModel> DataFilterMoldes { get; set; }

        public int TotalItemCount { get; set; }

    }
}