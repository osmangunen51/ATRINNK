using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class NotificationModelBase
    {
        public NotificationModelBase()
        {
            this.Notifications = new FilterModel<NotificationModel>();
            this.Users = new List<SelectListItem>();
            this.UserTypes = new List<SelectListItem>();
            this.Titles = new List<SelectListItem>();
        }
        public List<SelectListItem> Users { get; set; }
        public FilterModel<NotificationModel> Notifications { get; set; }
        public List<SelectListItem> UserTypes { get; set; }
        public List<SelectListItem> Titles { get; set; }

    }
}