namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using Management.Models.Validation;
    using System;
    using System.Collections.Generic;
    using System.ComponentModel;

    public class UserModel
    {
        public byte UserId { get; set; }

        [DisplayName("Kullanıcı Adı")]
        [RequiredValidation, StringLengthValidation(20)]
        public string UserName { get; set; }

        [DisplayName("Şifre")]
        [RequiredValidation, StringLengthValidation(20)]
        public string UserPass { get; set; }
        [DisplayName("Mail Adresi")]
        public string UserMail { get; set; }
        [DisplayName("Smtp Sunucusu")]
        public string MailSmtp { get; set; }
        [DisplayName("Mail Şifresi")]
        public string MailPassword { get; set; }
        [DisplayName("Giden Posta Sunucu Kodu")]
        public int? SendCode { get; set; }
        [DisplayName("Renk Kodu")]
        public string UserColor { get; set; }

        public DateTime? EndWorkDate { get; set; }
        public bool Active { get; set; }
        public bool ActiveForDesc { get; set; }
        public bool MemberDescriptionTransferState { get; set; }
        public IEnumerable<UserGroupModel> Groups { get; set; }

        [DisplayName("Mail İmzası")]
        public string Signature { get; set; }

        [DisplayName("Ad")]
        public string Name { get; set; }

        [DisplayName("Soyad")]
        public string Surname { get; set; }

        [DisplayName("Call Center Adresi")]
        public string CallCenterUrl { get; set; }

    }
}