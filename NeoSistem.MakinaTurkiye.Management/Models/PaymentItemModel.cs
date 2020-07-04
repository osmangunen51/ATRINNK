using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class PaymentItemModel
    {
        public int PaymentId { get; set; }
        public decimal PaidAmount { get; set; }
        public decimal RestAmount { get; set; }
        public byte? PaymentType { get; set; }
        public DateTime RecordDate { get; set; }
        public string PaymentDate { get; set; }
        public string Description { get; set; }
        public string BankName { get; set; }
        public string SenderNameSurname { get; set; }


    }
}