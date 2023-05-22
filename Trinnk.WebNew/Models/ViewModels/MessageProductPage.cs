


namespace NeoSistem.Trinnk.Web.Models
{
    using NeoSistem.Trinnk.Web.Models.Validation;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;


    public class MessageProductPage
    {
        [DisplayName("E-Posta Adresiniz")]
        [DataType(DataType.EmailAddress)]
        [RequiredValidation]
        public string Email { get; set; }
        public string Name { get; set; }
        public string Surname { get; set; }
        public string Telephone { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }

        [DisplayName("Şifreniz")]
        [DataType(DataType.Password)]
        public string MemberPassword { get; set; }

        [DisplayName("Adınız")]
        [RequiredValidation]
        public string MemberName { get; set; }

        [DisplayName("Soyadınız")]
        [RequiredValidation]
        public string MemberSurname { get; set; }
        public string PhoneCultureCode { get; set; }
        public string PhoneNumber { get; set; }
        public string mailTitle { get; set; }
        public string mailDescription { get; set; }


    }
}
