using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.StoreProfiles
{
    public class MTStoreAboutModel
    {
        public MTStoreAboutModel()
        {
            this.StoreInfoNumberShow = new StoreInfoNumberShow();
        }
        public string AboutText { get; set; }
        public string AboutMoreText { get; set; }
        public bool IsAboutText { get; set; }
        public string AboutImagePath { get; set; }
        public string StoreUrl { get; set; }
        public string StoreName { get; set; }
        public string StoreActivity { get; set; }
        public string StoreAboutShort { get; set; }
        public string StoreEmployeeCount { get; set; }
        public string StoreType { get; set; }
        public string StoreEstablishmentDate { get; set; }
        public string StoreCapital { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string StoreEndorsement { get; set; }
        public string TradeRegistrNo { get; set; }
        public string MersisNo { get; set; }
        public string StoreShortName { get; set; }
       

        public  StoreInfoNumberShow StoreInfoNumberShow { get; set; }
    } 
}