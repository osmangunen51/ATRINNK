using System;

namespace Trinnk.Entities.Tables.Logs
{
    public class CreditCardLog : BaseEntity
    {
        public int CreditCardLogId { get; set; }
        public int MainPartyId { get; set; }
        public string Status { get; set; }
        public string PosName { get; set; }

        public DateTime CreatedDate { get; set; }
        public string IPAddress { get; set; }

        public string Code { get; set; }
        public string OrderType { get; set; }

        public string Detail { get; set; }
        public string Price { get; set; }
    }
}
