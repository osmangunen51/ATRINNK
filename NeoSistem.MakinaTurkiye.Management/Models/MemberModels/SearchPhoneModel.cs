using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.MemberModels
{
    public class SearchPhoneModel
    {
        public string NameSurname { get; set; }
        public int MemberType { get; set; }
        public string MemberTypeText { get; set; }
        public string PhoneNumber { get; set; }

        public string Url { get; set; }       
    }
}