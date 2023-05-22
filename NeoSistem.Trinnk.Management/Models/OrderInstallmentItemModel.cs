using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class OrderInstallmentItemModel
    {
        public int Id { get; set; }
        public decimal Amount { get; set; }
        public DateTime PayDate { get; set; }

        public bool IsPaid { get; set; }
    }
}