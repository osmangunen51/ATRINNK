using System;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class StoreChangeHistory : BaseEntity
    {


        public int StoreChangeHistoryId { get; set; }
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
        public string StoreNo { get; set; }
        public string StoreName { get; set; }
        public string StoreEMail { get; set; }
        public string StoreWeb { get; set; }
        public string StoreLogo { get; set; }
        public string OldStoreLogo { get; set; }
        public byte? StoreActiveType { get; set; }
        public DateTime? StorePacketBeginDate { get; set; }
        public DateTime? StorePAcketEndDate { get; set; }
        public string StoreAbout { get; set; }
        public DateTime? StoreRecordDate { get; set; }
        public int? StoreEstablishmentDate { get; set; }
        public byte? StoreEndorsement { get; set; }
        public byte? StoreType { get; set; }
        public byte? StoreCapital { get; set; }
        public byte? StoreEmployeesCount { get; set; }
        public string GeneralText { get; set; }
        public string HistoryText { get; set; }
        public string FounderText { get; set; }
        public string PhilosophyText { get; set; }
        public string StorePicture { get; set; }
        public string OrderCode { get; set; }
        public string StoreUniqueShortName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string MersisNo { get; set; }
        public string TradeRegistrNo { get; set; }
        public DateTime UpdatedDated { get; set; }

        public virtual Store Store { get; set; }


    }
}
