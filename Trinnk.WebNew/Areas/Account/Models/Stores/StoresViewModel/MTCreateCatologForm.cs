using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Web;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Stores.StoresViewModel
{
    public class MTCreateCatologForm
    {
        [Required]
        public string CatologName { get; set; }
        [Required]
        public List<HttpPostedFileBase> FilePaths { get; set; }

    }
}