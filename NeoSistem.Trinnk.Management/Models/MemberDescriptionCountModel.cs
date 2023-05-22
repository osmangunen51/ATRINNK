using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Management.Models
{
    public class MemberDescriptionCountModel
    {
        public MemberDescriptionCountModel()
        {
            this.Usercounts = new List<Usercount>();
        }
        public int TotalCount { get; set; }
        public DateTime UpdateDateNew { get; set; }
        public List<Usercount> Usercounts { get; set; }
    }

    public class Usercount
    {
        public string UserName { get; set; }
        public int Count { get; set; }
    }

}