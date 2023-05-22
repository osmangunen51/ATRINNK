using Trinnk.Entities.Tables.Common;
using Trinnk.Entities.Tables.Members;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Areas.Account.Models.Messages
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