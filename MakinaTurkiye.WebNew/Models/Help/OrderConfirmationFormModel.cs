using System;

namespace NeoSistem.MakinaTurkiye.Web.Models.Help
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