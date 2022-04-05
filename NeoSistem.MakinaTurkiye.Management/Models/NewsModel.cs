namespace NeoSistem.MakinaTurkiye.Management.Models
{
    using Core.Web.Helpers;
    using System;
    using System.ComponentModel;
    using Validation;

    public class NewsModel
    {
        public int NewsId { get; set; }

        public int? MainPartyId { get; set; }

        [RequiredValidation, StringLengthValidation(100)]
        [DisplayName("Haber Başlık")]
        public string NewsTitle { get; set; }

        [RequiredValidation]
        [DisplayName("Icerik")]
        public string NewsText { get; set; }

        [DisplayName("Resim")]
        public string NewsPicturePath { get; set; }

        public string ThumbPicture
        {
            get
            {
                return FileHelpers.ImageFolder + this.NewsPicturePath.Replace(".", "_th.");
            }
        }

        [RequiredValidation]
        [DisplayName("Tarih")]
        public DateTime NewsDate { get; set; }

        [DisplayName("Aktif")]
        public bool Active { get; set; }
    }
}