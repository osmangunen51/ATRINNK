using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Messages
{
    public class MemberMessageDetailModel
    {
        public MemberMessageDetailModel()
        {
            this.Member = new Member();
            this.Address = new Address();
            this.Phones = new List<Phone>();
        }
        public Member Member { get; set; }
        public Address Address { get; set; }
        public List<Phone> Phones { get; set; }

    }
}