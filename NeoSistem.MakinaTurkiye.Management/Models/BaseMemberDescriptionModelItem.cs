using MakinaTurkiye.Entities.StoredProcedures.Orders;
using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class BaseMemberDescriptionModelItem
    {
        public int ID { get; set; }
        public int MainPartyId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int? DescriptionDegree { get; set; }

        public string StoreName { get; set; }
        public int? StoreID { get; set; }
        public int? UserId { get; set; }
        public string DescriptionNew { get; set; }
        public string UserName { get; set; }
        public string FromUserName { get; set; }
        public string ToUserName { get; set; }
        public string AuthorizeName { get; set; }
        public bool IsFirst { get; set; }
        public string PortfoyName { get; set; }
        public string Color { get; set; }

        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public int MemberMainPartyId { get; set; }
        public byte MemberType { get; set; }
        public decimal RestAmount { get; set; }
    }
}