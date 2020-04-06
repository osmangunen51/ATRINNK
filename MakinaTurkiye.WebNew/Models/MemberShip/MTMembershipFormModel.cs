using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.MemberShip
{
    public class MTMembershipFormModel
    {
        [Required(ErrorMessage="Email zorunludur")]
        public string MemberEmail { get; set; }
        [Required(ErrorMessage = "Şifre zorunludur")]
        public string MemberPassword { get; set; }
        [Required(ErrorMessage = "İsim zorunludur")]
        public string Name { get; set; }
        [Required(ErrorMessage = "Soyisim zorunludur")]
        public string Surname { get; set; }
        public string ErrorMessage { get; set; }
    }
}