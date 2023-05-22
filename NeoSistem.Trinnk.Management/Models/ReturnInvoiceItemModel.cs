using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class ReturnInvoiceItemModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime RecordDate { get; set; }
    }
}