namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using System.ComponentModel;
    using System.ComponentModel.DataAnnotations;

    public class AccountModel
    {
        [Required(ErrorMessage = " ")]
        [DisplayName("Kullanıcı Adı")]
        public string UserName { get; set; }

        [Required(ErrorMessage = " ")]
        [DataType(DataType.Password)]
        [DisplayName("Şifre")]
        public string UserPass { get; set; }
    }
}