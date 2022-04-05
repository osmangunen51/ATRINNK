using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class StoreSpecialMailModel
    {
        public StoreSpecialMailModel()
        {
            this.Files = new List<SelectListItem>();
        }
        public string Subject { get; set; }
        public string Content { get; set; }
        public string StoreMail { get; set; }
        public int MemberID { get; set; }
        public string Message { get; set; }
        public List<SelectListItem> Files { get; set; }

    }
}