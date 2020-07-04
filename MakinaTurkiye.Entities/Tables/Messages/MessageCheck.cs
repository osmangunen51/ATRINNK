namespace MakinaTurkiye.Entities.Tables.Messages
{
    public class MessageCheck : BaseEntity
    {
        public int MainPartyId { get; set; }
        public int MessageId { get; set; }
        public int Check { get; set; }
    }
}
