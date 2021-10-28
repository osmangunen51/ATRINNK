namespace NeoSistem.MakinaTurkiye.Web.Models
{

    using global::MakinaTurkiye.Entities.Tables.Catalog;
    using NeoSistem.MakinaTurkiye.Web.Models.Validation;
    using System;
    using System.ComponentModel;

    public class MessageModel
    {

        public string ProductNo { get; set; }
        public string MemberNo { get; set; }

        public int MessageId { get; set; }
        public string Subject { get; set; }
        [DisplayName("Açıklama")]
        [RequiredValidation]
        public string Content { get; set; }
        public bool MessageRead { get; set; }
        public DateTime MessageDate { get; set; }
        public bool Active { get; set; }
        public int ProductId { get; set; }
        public string MainPartyFullName { get; set; }
        public int MainPartyId { get; set; }
        public int MessageMainPartyId { get; set; }
        public Product ProductItem { get; set; }

        public string FileName { get; set; }
    }

}