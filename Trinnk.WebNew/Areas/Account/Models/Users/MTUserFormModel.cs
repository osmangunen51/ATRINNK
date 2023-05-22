using System.ComponentModel.DataAnnotations;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Users
{
    public class MTUserFormModel
    {

        [Required(ErrorMessage = "İsim Zorunludur")]
        public string MemberName { get; set; }
        [Required(ErrorMessage = "Soyisim Zorunludur")]
        public string MemberSurname { get; set; }
        [Required(ErrorMessage = "Email Adresi Zorunludur")]
        public string MemberEmail { get; set; }
        [Required(ErrorMessage = "Şifre Zorunludur")]
        public string MemberPassword { get; set; }
        [Required(ErrorMessage = "Cinsiyet Zorunludur")]
        public byte Gender { get; set; }



    }
}