namespace MakinaTurkiye.Entities.Tables.Messages
{
    public class UserMailTemplate:BaseEntity
    {
        public int UserMailTemplateId { get; set; }
        public int UserId { get; set; }
        public int SpecialMailId { get; set; }
        public int UserGroupId { get; set; }
        public string Subject { get; set; }
        public string MailContent { get; set; }


    }
}
