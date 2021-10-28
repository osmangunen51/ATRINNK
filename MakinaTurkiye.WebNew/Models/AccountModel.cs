namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using MakinaTurkiye.Web.Models.Validation;
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class LoginModel
    {
        [RequiredValidation, StringLengthValidation(320), EmailValidation]
        [DisplayName("E-Posta")]
        public string Email { get; set; }

        [RequiredValidation, StringLengthValidation(50)]
        [DataType(DataType.Password)]
        [DisplayName("Parola")]
        public string Password { get; set; }

        public bool Remember { get; set; }
        public string ReturnUrl { get; set; }
    }

}