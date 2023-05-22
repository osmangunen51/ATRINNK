using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Api.View
{
    public class MTProductSearchOneStepCategoryItemModel
    {
        public int CategoryId { get; set; }
        public int CategoryParentId { get; set; }
        public byte CategoryType { get; set; }
        public string CategoryName { get; set; }
        public int ProductCount { get; set; }
        public string CategoryUrl { get; set; }
        public string TruncatedCategoryName { get; set; }
    }
}
