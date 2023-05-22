using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Api.View
{
    public class MTCategorySearchModel
    {
        public MTCategorySearchModel()
        {
            this.CategoryModel = new MTProductCategoryModel();
        }
        public MTProductCategoryModel CategoryModel { get; set; }
        public string SearchText { get; set; }
        public int TotalCount { get; set; }


    }
}
