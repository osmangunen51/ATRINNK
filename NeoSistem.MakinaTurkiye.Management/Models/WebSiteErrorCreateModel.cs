using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class WebSiteErrorCreateModel
    {
        public WebSiteErrorCreateModel()
        {
            this.ErrorTypes = new List<SelectListItem>();
        }
        public int WebSiteErrorId { get; set; }
        [Required(ErrorMessage ="Lütfen Başlık Giriniz*")]
        public string Title { get; set; }
        public int UserId { get; set; }
        [Required(ErrorMessage ="Lütfen Açıklama Giriniz*")]
        public string Content { get; set; }
        [Required(ErrorMessage = "Lütfen Sorun Tipini Giriniz*")]
        public string ProblemType { get; set; }
        public bool IsAdvice { get; set; }
        public bool IsSolved { get; set; }
        public bool IsFirst { get; set; }
        public bool IsWaiting { get; set; }
       public List<SelectListItem> ErrorTypes { get; set; }
     }
}