namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System;

    public class NotificationFormModel
    {

        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string NotificationFormSubject { get; set; }
        public DateTime RecordDate { get; set; }
        public bool IsRead { get; set; }
        public string NotificationFormDescription { get; set; }
        public int NotificationFormId { get; set; }
    }
}