using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class WhatsappLog:BaseEntity
    {
        public int WhatsappLogId { get; set; }
        public int MainPartyId { get; set; }
        public DateTime RecordDate { get; set; }
        public int ClickCount { get; set; }
   
        public virtual Store Store { get; set; }
    }
}
