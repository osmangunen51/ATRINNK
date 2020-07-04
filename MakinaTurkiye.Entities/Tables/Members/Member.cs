using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakinaTurkiye.Entities.Tables.Members
{
    public class Member: BaseEntity
    {
        //private ICollection<Phone> _phones;
        //private ICollection<ProductComplain> _productComplains;
        private ICollection<ProductComment> _productComments;
        //private ICollection<MemberSetting> _memberSettings;
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MainPartyId { get; set; }
        public string MemberNo { get; set; }
        public byte? MemberType { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string MemberEmail { get; set; }
        public string MemberPassword { get; set; }
        public byte? MemberTitleType { get; set; }
        public bool? Active { get; set; }
        public string ActivationCode { get; set; }
        public bool? Gender { get; set; }
        public DateTime? BirthDate { get; set; }
        public bool? ReceiveEmail { get; set; }
        public int? FastMemberShipType { get; set; }
        public string ForgetPasswodCode { get; set; }

        //public virtual ICollection<Phone> Phones
        //{
        //    get { return _phones ?? (_phones = new List<Phone>()); }
        //    protected set { _phones = value; }
        //}

        public virtual ICollection<ProductComment> ProductComments
        {
            get { return _productComments ?? (_productComments = new List<ProductComment>()); }
            protected set { _productComments = value; }
        }

        //public virtual ICollection<ProductComplain> ProductComplains
        //{
        //    get { return _productComplains ?? (_productComplains = new List<ProductComplain>()); }
        //    protected set { _productComplains = value; }
        //}

        //public virtual ICollection<MemberSetting> MemberSettings
        //{
        //    get { return _memberSettings ?? (_memberSettings = new List<MemberSetting>()); }
        //    protected set { _memberSettings = value; }
        //}
    }
}
