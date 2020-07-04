using System;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class OrderDescription:BaseEntity
    {
        public int OrderDescriptionId { get; set; }
        public int OrderId { get; set; }
        public DateTime PayDate { get; set; }
        public string Description { get; set; }
        public DateTime? RecordDate { get; set; }

    }
}
