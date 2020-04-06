namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System;
    public class OrderModel
    {
        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
    
        public byte AccountId { get; set; }
        public string OrderCode { get; set; }
        public string OrderNo { get; set; }
        public decimal OrderPrice { get; set; }
        public byte PacketStatu { get; set; }
        public DateTime RecordDate { get; set; }
        public string Address { get; set; }
        public string BankName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public string AccountName { get; set; }
        public string StoreName { get; set; }
        public string StoreEMail { get; set; }
        public string PacketName { get; set; }
        public DateTime? OrderPacketEndDate { get; set; }
        public string StoreNameForInvoice { get; set; }
        public short VirtualPosInstallmentId { get; set; }
        public byte OrderType { get; set; }
        public byte CreditCardCount { get; set; }
        public decimal CreditCardValue { get; set; }
        public string CreditCardName { get; set; }
        public bool? InvoiceStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public string TaxOfficeOrder { get; set; }
        public string TaxNoOrder { get; set; }
        public bool? PriceCheck { get; set; }
        public string OrderDescription { get; set; }
        public decimal? RestAmount { get; set; }
        public DateTime? PayDate { get; set; }
        public string SalesUserName { get; set; }
        public bool? OrderCancelled { get; set; }
        public bool? SendedMail { get; set; }
        public decimal? ReturnAmount { get; set; }
        public string EBillNumber { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public bool? IsRenewPacket { get; set; }
        public int CurrentStorePacketId { get; set; }
    }
}