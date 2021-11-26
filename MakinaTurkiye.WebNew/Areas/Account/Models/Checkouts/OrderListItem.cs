using System;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Checkouts
{
    public class OrderListItem
    {
        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
        public byte? AccountId { get; set; }
        public Int16? CreditCardInstallmentId { get; set; }
        public byte OrderType { get; set; }
        public string OrderCode { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderPrice { get; set; }
        public byte PacketStatu { get; set; }
        public DateTime RecordDate { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public string Address { get; set; }
        public DateTime? OrderPacketEndDate { get; set; }
        public string StoreNameForInvoice { get; set; }
        public bool? InvoiceStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public bool? PriceCheck { get; set; }
        public string OrderDescription { get; set; }
        public string PacketName { get; set; }
        public decimal RestAmount { get; set; }
        public int StoreId { get; set; }

    }
}