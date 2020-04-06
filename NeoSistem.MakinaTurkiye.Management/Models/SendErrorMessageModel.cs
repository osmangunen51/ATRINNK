namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class SendErrorMessageModel
    {
        public int ID { get; set; }
        public string SenderMemberNo { get; set; }
        public string MessageContent { get; set; }
        public string ReceiverMemberNo { get; set; }
        public string MessageSubject { get; set; }
        public string ErrorDate { get; set; }
        public string SenderName { get; set; }
        public string ProductNo { get; set; }
        public string ReceiverName { get; set; }

    }
}