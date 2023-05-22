namespace NeoSistem.Trinnk.Management.Models
{
    using NeoSistem.Trinnk.Management.Models.Entities;
    using System;

    public class MessageModel
    {
        public int MessageId { get; set; }
        public string MessageSubject { get; set; }
        public string MessageContent { get; set; }
        public bool MessageRead { get; set; }
        public DateTime MessageDate { get; set; }
        public bool Active { get; set; }

        public string FromMainPartyFullName { get; set; }
        public string ToMainPartyFullName { get; set; }

        public int FromMainPartyId { get; set; }
        public int ToMainPartyId { get; set; }

        public Product Product { get; set; }
        public Message Message { get; set; }

        public int InOutMainPartyId { get; set; }
        public int MainPartyId { get; set; }
        public string FromSecondName { get; set; }
        public string ToSecondName { get; set; }
        public string StoreUrl { get; set; }
        public bool? MessageSeenAdmin { get; set; }



    }

}