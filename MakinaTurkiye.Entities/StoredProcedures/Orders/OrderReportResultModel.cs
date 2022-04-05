using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Orders
{
    public class OrderReportResultModel
    {
        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public string InvoiceNumber { get; set; }
        public string orderNo { get; set; }
        public decimal? OrderPrice { get; set; }
        public DateTime RecordDateOrder { get; set; }
        public string UserName { get; set; }
        public DateTime? PayDate { get; set; }
        public string StoreName { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RestAmount { get; set; }

        public int? CallingCount { get; set; }
        public string Description { get; set; }
        public byte? OrderType { get; set; }
        public byte? PacketStatu { get; set; }
        public bool? InvoiveStatus { get; set; }
        public bool? OrderCancelled { get; set; }

    }
}
