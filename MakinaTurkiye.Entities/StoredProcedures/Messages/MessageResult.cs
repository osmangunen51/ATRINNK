using System;

namespace MakinaTurkiye.Entities.StoredProcedures.Messages
{
    public class MessageResult
    {
        public int MessageId { get; set; }
        public byte MessageType { get; set; }
        public string MessageContent { get; set; }
        public string MessageSubject { get; set; }
        public bool MessageRead { get; set; }
        public DateTime MessageDate { get; set; }
        public string MainPartyFullName { get; set; }
        public int MainPartyId { get; set; }
        public int InOutMainPartyId { get; set; }
    }
}
