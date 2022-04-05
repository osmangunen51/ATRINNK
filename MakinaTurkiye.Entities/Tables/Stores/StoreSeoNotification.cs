using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreSeoNotification : BaseEntity
    {
        public int StoreSeoNotificationId { get; set; }
        public int StoreMainPartyId { get; set; }
        public Int16? ConstantId { get; set; }
        public byte FromUserId { get; set; }
        public byte ToUserId { get; set; }
        public string Text { get; set; }
        public DateTime CreatedDate { get; set; }
        public DateTime? RemindDate { get; set; }
        public byte Status { get; set; }

        public bool? IsFirst { get; set; }

    }
}
