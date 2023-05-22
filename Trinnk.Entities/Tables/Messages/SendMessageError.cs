namespace Trinnk.Entities.Tables.Messages
{
    public class SendMessageError : BaseEntity
    {
        public int MessageID { get; set; }
        public string MessageContent { get; set; }
        public string MessageSubject { get; set; }
        public int ProductID { get; set; }
        public int SenderID { get; set; }
        public int ReceiverID { get; set; }
        public string ErrorDate { get; set; }
    }
}
