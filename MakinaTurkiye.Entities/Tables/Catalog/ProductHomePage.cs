using System;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductHomePage:BaseEntity
    {
        public int ProductHomePageId { get; set; }

  
        public int ProductId { get; set; }
        public int CategoryId { get; set; }
        public DateTime? BeginDate { get; set; }
        public DateTime? EndDate { get; set; }
        public DateTime RecordDate { get; set; }
         public bool Active { get; set; }
        public byte? Type { get; set; }
        public virtual Category Category { get; set; }
        public virtual Product Product { get; set; }

    }
}
