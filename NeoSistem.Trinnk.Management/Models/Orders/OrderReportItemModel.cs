using System;

namespace NeoSistem.Trinnk.Management.Models.Orders
{
    public class OrderReportItemModel
    {
        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public string InvoiceNumber { get; set; }
        public string orderNo { get; set; }
        public decimal? OrderPrice { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime? PayDate { get; set; }
        public string UserName { get; set; }
        public string StoreName { get; set; }
        public decimal? PaidAmount { get; set; }
        public decimal? RestAmount { get; set; }

        public int? CallingCount { get; set; }
        public string Description { get; set; }
        public string OrderType { get; set; }
        public byte? PacketStatu { get; set; }
        public string InvoiveStatus { get; set; }
        public string OrderCancelled { get; set; }

    }
}