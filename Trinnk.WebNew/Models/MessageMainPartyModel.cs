namespace NeoSistem.Trinnk.Web.Models
{
    public class MessageMainPartyModel
    {
        public int MessageId { get; set; }
        public int MessageMainPartyId { get; set; }
        public int MainPartyId { get; set; }
        public int InOutMainPartyId { get; set; }
        public byte MessageType { get; set; }
    }

}