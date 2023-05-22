namespace Trinnk.Entities.Tables.Messages
{
    public class MessagesMT : BaseEntity
    {
        public int MessagesMTId { get; set; }
        public string MessagesMTName { get; set; }
        public string MessagesMTTitle { get; set; }
        public string MessagesMTPropertie { get; set; }
        public string Mail { get; set; }
        public string MailPassword { get; set; }
        public string MailSendFromName { get; set; }
        public string MailContent { get; set; }
    }
}
