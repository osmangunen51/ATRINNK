using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.Users
{
    public class UserFileModel
    {
        public UserFileModel()
        {
            this.FileTypes = new List<SelectListItem>();
            this.UserFileItems = new List<UserFileItemModel>();
        }
        public List<SelectListItem> FileTypes { get; set; }
        public List<UserFileItemModel> UserFileItems { get; set; }
    }

}