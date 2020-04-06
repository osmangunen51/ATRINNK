namespace MakinaTurkiye.Entities.Tables.Messages
{
    public class MessageMainParty : BaseEntity
    {
        public int MessageMainPartyId { get; set; }
        public int MessageId { get; set; }
        public int MainPartyId { get; set; }
        public int InOutMainPartyId { get; set; }
        public byte MessageType { get; set; }

        //asdadsadsad

    }
}
