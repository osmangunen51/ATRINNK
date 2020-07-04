namespace MakinaTurkiye.Entities.Tables.Messages
{
    public class MobileMessage:BaseEntity
    {
        public int  ID { get; set; }
        public string MessageName { get; set; }
        public string MessageContent { get; set; }
        public byte? MessageType { get; set; }
    }
}
