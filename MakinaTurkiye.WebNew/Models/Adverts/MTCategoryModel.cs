using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Adverts
{
    public class MTCategoryModel
    {
        public int CategoryId { get; set; }
        public byte CategoryOrder { get; set; }
        public int PropertyId { get; set; }
        public short CategoryGroupType { get; set; }
        public int? CategoryParentId { get; set; }
        public string CategoryTreeName { get; set; }
        public string CategoryName { get; set; }
        public byte ?CategoryType { get; set; }
        public byte CategoryRoute { get; set; }
        public bool ?Active { get; set; }
        public int IsParent { get; set; }
    }
}