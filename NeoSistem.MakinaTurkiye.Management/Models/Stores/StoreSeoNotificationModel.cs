using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.Stores
{
    public class StoreSeoNotificationModel
    {
        public StoreSeoNotificationModel()
        {
            this.BaseMemberDescriptionModelItems = new List<BaseMemberDescriptionModelItem>();
        }
        public List<BaseMemberDescriptionModelItem> BaseMemberDescriptionModelItems { get; set; }
        public string StoreName { get; set; }

    }
}