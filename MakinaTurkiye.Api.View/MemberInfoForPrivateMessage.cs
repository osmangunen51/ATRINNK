﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Api.View
{
  public  class MemberInfoForPrivateMessage
    {
        public int MainPartyId { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string MemberEmail { get; set; }
        public bool? Active { get; set; }
    }
}
