using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.Stores
{
    public class StoreSeoNotificationFormModel
    {
        public StoreSeoNotificationFormModel()
        {
            this.ToUsers = new List<SelectListItem>();
            this.Titles = new List<SelectListItem>();
        }
        public int StoreMainPartyId { get; set; }
        public int ToUserId { get; set; }
        public string Text { get; set; }
        public string RemindDate { get; set; }
        public Int16 ConstantId { get; set; }
        public string PreviousText { get; set; }
        public int StoreSeoNotificationId { get; set; }


        public List<SelectListItem> ToUsers { get; set; }
        public List<SelectListItem> Titles { get; set; }
    }
}