using System.Collections.Generic;

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