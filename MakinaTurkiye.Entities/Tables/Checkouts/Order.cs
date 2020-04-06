using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Checkouts
{
    public class Order:BaseEntity
    {
        private ICollection<Payment> _payments;
        //private ICollection<ReturnInvoice> _returnInvoices;

        public int OrderId { get; set; }
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
        public int? AuthorizedId { get; set; }
        public byte ?AccountId { get; set; }
        public int? ProductId { get; set; }
        public Int16 ?CreditCardInstallmentId { get; set; }
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
        public string EBillNumber { get; set; }
        public bool ?InvoiceStatus { get; set; }
        public string InvoiceNumber { get; set; }
        public bool? PriceCheck { get; set; }
        public string OrderDescription { get; set; }
        public string IyzicoPaymentId { get; set; }
        public DateTime ? PayDate { get; set; }
        public bool? SendedMail { get; set; }
        public bool? OrderCancelled { get; set; }
        public int? PacketDay { get; set; }
        public byte? OrderPacketType { get; set; }
        public DateTime? PacketStartDate { get; set; }
        public bool? IsRenewPacket { get; set; }
        public DateTime? InvoiceDate { get; set; }
        //TODO:İlişkilendirme için sınıf propertyleri eklenecek
        public ICollection<Payment> Payments
        {
            get { return _payments ?? (_payments = new List<Payment>()); }
            protected set { _payments = value; }
        }

        //public ICollection<ReturnInvoice> ReturnInvoices
        //{
        //    get { return _returnInvoices ?? (_returnInvoices = new List<ReturnInvoice>()); }
        //    protected set { _returnInvoices = value; }
        //}

    }
}
