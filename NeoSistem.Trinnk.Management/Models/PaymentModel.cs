using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class PaymentModel
    {
        public PaymentModel()
        {
            this.PaymentItems = new List<PaymentItemModel>();
            this.ReturnInvices = new List<ReturnInvoiceItemModel>();
            this.Banks = new List<SelectListItem>();
        }
        public string StoreName { get; set; }
        public int OrderId { get; set; }
        public string BankId { get; set; }
        public string PayDate { get; set; }
        public string Description { get; set; }
        public string PaidAmount { get; set; }
        public List<SelectListItem> Banks { get; set; }
        public decimal TotalPaidAmount { get; set; }
        public string Message { get; set; }
        public string PayUrl { get; set; }
        public string SenderNameSurname { get; set; }
        public List<PaymentItemModel> PaymentItems { get; set; }


        public List<ReturnInvoiceItemModel> ReturnInvices { get; set; }
    }
}