using System;

namespace NeoSistem.Trinnk.Web.Models.Help
{
    public class OrderConfirmationFormModel
    {
        public int OrderId { get; set; }
        public int StoreMainPartyId { get; set; }
        public string ReturnUrl { get; set; }

        public bool IsConfirmed { get; set; }

        public DateTime? RecordDate { get; set; }

    }
}