using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class MailTemplateFormModel
    {
        public MailTemplateFormModel()
        {
            this.SpecialMails = new List<SelectListItem>();
            this.UserGroups = new List<SelectListItem>();
            this.UserMailTemplateListItemModels = new List<UserMailTemplateListItemModel>();

        }
        public int MailTemplateId { get; set; }

        public int UserId { get; set; }
        [Required(ErrorMessage = "Mail Template Adı Seçiniz")]
        [DisplayName("Mail Template Adı")]
        public int SpecialId { get; set; }
        [Required(ErrorMessage = "Kullanıcı Grup Tipini Seçiniz")]
        [DisplayName("Kullanıcı Grubu")]
        public int UserGroupId { get; set; }
        [Required(ErrorMessage = "Konu Giriniz")]
        [DisplayName("Konu")]
        public string Subject { get; set; }
        [Required(ErrorMessage = "İçerik Giriniz")]
        [DisplayName("Mail İçerik")]
        public string MailContent { get; set; }

        public List<UserMailTemplateListItemModel> UserMailTemplateListItemModels { get; set; }

        public List<SelectListItem> SpecialMails { get; set; }
        public List<SelectListItem> UserGroups { get; set; }

    }
}