﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductStoreMessageModel
    {
       public string ProductName { get; set; }
       public string Email { get; set; }
       public string MemberPassword { get; set; }
       public string MemberName { get; set; }
       public string MemberSurname { get; set; }
       public string PhoneNumber { get; set; }
       public string MailTitle { get; set; }
       public string MailDescription { get; set; }
    }
}