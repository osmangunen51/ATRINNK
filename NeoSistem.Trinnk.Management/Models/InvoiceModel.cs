using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class InvoiceModel
    {
        public int InvoiceId { get; set; }
        public string InvoiceNumer { get; set; }
        public string StoreName { get; set; }
        public string OrderTypeName { get; set; }
        public string PacketName { get; set; }
        public decimal Price { get; set; }
        public decimal NormalPrice { get; set; }
        public decimal TaxPrice { get; set; }
        public byte TaxValue { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNo { get; set; }
        public string InvoiceAddress { get; set; }
        public DateTime PacketBeginDate { get; set; }
        public DateTime PacketEndDate { get; set; }
        public string StoreEmail { get; set; }
        public string PriceWord { get; set; }
        public int? AccountId { get; set; }
        public string OrderDescription { get; set; }
        public string OrderNo { get; set; }
        public decimal? DiscountPercentage { get; set; }
        public decimal? DiscountAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
    }
}