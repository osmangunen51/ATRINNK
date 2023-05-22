using NeoSistem.Trinnk.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.Trinnk.Management.Models
{
    public class BaseMemberDescriptionModel
    {
        public BaseMemberDescriptionModel()
        {
            this.BaseMemberDescriptionByUser = new List<BaseMemberDescriptionModel>();
            this.Users = new List<SelectListItem>();
            this.ConstantModel = new List<SelectListItem>();
            this.Member = new Member();
            this.SubConstants = new List<string>();
        }
        public int ID { get; set; }
        public int MainPartyId { get; set; }
        public string ConstantId { get; set; }
        public string Description { get; set; }
        public string Title { get; set; }
        public DateTime InputDate { get; set; }
        public DateTime? LastDate { get; set; }
        public int? DescriptionDegree { get; set; }
        public Member Member { get; set; }
        public string StoreName { get; set; }
        public string StoreShortName { get; set; }
        public int? StoreID { get; set; }
        public int? UserId { get; set; }
        public string DescriptionNew { get; set; }
        public string UserName { get; set; }
        public string ToUserName { get; set; }

        public string AuthorizeName { get; set; }
        public byte RegistrationType { get; set; }
        public int RegistrationStoreId { get; set; }
        public bool IsFirst { get; set; }
        public bool IsImmediate { get; set; }
        public string PortfoyName { get; set; }
        public string Color { get; set; }
        public List<string> SubConstants { get; set; }
        public List<SelectListItem> Users { get; set; }
        public List<BaseMemberDescriptionModel> BaseMemberDescriptionByUser;
        public List<SelectListItem> ConstantModel { get; set; }
        public string ContactNameSurname { get; set; }
        public string ContactPhoneNumber { get; set; }
        public string Contact { get; set; }
        public string City { get; set; }
        public string StoreWebUrl { get; set; }
        public string StoreUrl { get; set; } = "";
        public string StorePacket { get; set; } = "";
        public DateTime StorePacketEndDate { get; set; }

        public bool IsOpened { get; set; } = true;

    }
}