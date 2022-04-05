using System;

namespace MakinaTurkiye.Entities.Tables.Messages
{
    public class Message : BaseEntity
    {
        public int MessageId { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public bool MessageRead { get; set; }
        public DateTime MessageDate { get; set; }
        public string MessageFile { get; set; }
        public int ProductId { get; set; }
        public bool Active { get; set; }
        public bool? MessageSeenAdmin { get; set; }
    }
}
