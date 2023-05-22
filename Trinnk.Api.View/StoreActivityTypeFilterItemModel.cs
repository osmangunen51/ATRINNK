using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Api.View
{

    public class StoreActivityTypeFilterItemModel
    {
        public string FilterItemId { get; set; }
        public bool Filtered { get; set; }
        public string FilterItemName { get; set; }
        public string FilterUrl { get; set; }
        public int StoreCount { get; set; }
    }
}
