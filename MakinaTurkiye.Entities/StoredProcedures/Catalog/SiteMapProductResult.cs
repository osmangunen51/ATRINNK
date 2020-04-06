using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Catalog
{
    public class SiteMapProductResult
    {
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public int CategoryId { get; set; }
        public string CategoryName { get; set; }
        public string CategoryProductGroupName { get; set; }
        public DateTime ProductRecordDate { get; set; }
        public bool ProductActive { get; set; }
    }
}
