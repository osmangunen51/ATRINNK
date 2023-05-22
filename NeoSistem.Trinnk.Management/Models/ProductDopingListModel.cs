using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class ProductDopingListModel
    {
        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ModelName { get; set; }
        public string BrandName { get; set; }
        public string SeriesName { get; set; }
        public string CategoryBreadCrumb { get; set; }
        public int CategoryId { get; set; }
        public DateTime? ProductDopingBeginDate { get; set; }
        public DateTime? ProductDopingEndDate { get; set; }
        public string StoreShortName { get; set; }
    }
}