using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Models.Bulletins
{
    public class BulletinMemberCreateModel
    {
        public BulletinMemberCreateModel()
        {
            this.SectorCategories = new List<SelectListItem>();
        }
        [Required(ErrorMessage = "Email Adresi Zorunludur")] public string Email { get; set; }

        public string MemberName { get; set; }

        public string MemberSurname { get; set; }
        public bool HaveMemberShip { get; set; }

        public List<SelectListItem> SectorCategories { get; set; }
    }
}