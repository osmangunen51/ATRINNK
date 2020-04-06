using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Management.Models.Stores
{
    public class MTStoreExcelItem
    {
        public int StoreMainPartyId { get; set; }
        public string StoreName { get; set; }
        public string StoreEmail { get; set; }
        public string StoreWeb { get; set; }
        public string StoreAbout { get; set; }
        public int? StoreEstablishDate { get; set; }
        public byte? StoreCapital { get; set; }
        public byte? StoreEmployeesCount { get; set; }
        public byte? StoreEndorsement { get; set; }
        public string PurchasingDepartmentName { get; set; }
        public string PurchasingDepartmentEmail { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string MersisNo { get; set; }
        public string TradeRegistrNo { get; set; }
        public string StoreUrlName { get; set; }
        public string StoreShortName { get; set; }
        public string StoreProfileHomeDescription { get; set; }
        public string AccountLink { get; set; }
        public string SeoTitle { get; set; }
        public string SeoKeyword { get; set; }
        public string SeoDescription { get; set; }
        public string Gsm { get; set; }
        public string Phone1 { get; set; }
        public string Phone2 { get; set; }
        public string Whatsapp { get; set; }
        public string Status { get; set; }
    }
}