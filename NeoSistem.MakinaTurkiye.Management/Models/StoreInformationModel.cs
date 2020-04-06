using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class StoreInformationModel
    {
        public int StoreMainPartyId { get; set; }
        public string StoreNo { get; set; }
        public string StoreName { get; set; }
        public string StoreShortName { get; set; }
        public string PortfoyName { get; set; }
        public string SalesManagerName { get; set; }
        public string StoreUrl { get; set; }
        public string StoreEmail { get; set; }
        public string PacketName { get; set; }
        public string PacketType { get; set; }
        public long StoreSingularViewCount { get; set; }
        public long ViewCount { get; set; }
        public DateTime? PacketEndDate { get; set; }
        public string StoreFullName { get; set; }
        public string StoreWebUrl { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string StoreLogo { get; set; }
        public string StoreOtherInfo { get; set; }
        public string StoreShortDetail { get; set; }
        

    }
}