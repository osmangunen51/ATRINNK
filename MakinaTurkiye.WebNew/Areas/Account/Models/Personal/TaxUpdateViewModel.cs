﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Personal
{
    public class TaxUpdateViewModel
    {
        public int StoreMainPartyId { get; set; }
        public string TaxNumber{get;set;}
        public string TaxOffice { get; set; }
        public string TradeRegistrNo { get; set; }
        public string MersisNo { get; set; }
        public bool TaxNumberShow { get; set; }
        public bool TaxOfficeShow { get; set; }
        public bool TradeRegistrNoShow { get; set; }
        public bool MersisNoShow { get; set; }

        public bool ShowTax { get; set; }
       
        public LeftMenuModel LeftMenu { get; set; } 
    }
}