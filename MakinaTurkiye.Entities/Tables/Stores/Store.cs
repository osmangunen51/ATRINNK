using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Settings;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;

namespace MakinaTurkiye.Entities.Tables.Stores
{
    public class Store : BaseEntity
    {

        //private ICollection<StoreChangeHistory> _storechangeHistories;
        private ICollection<StoreActivityCategory> _storeActivityCategories;
        
        //private ICollection<DealerBrand> _dealerBrands;
        //private ICollection<WhatsappLog> _whatsaooLogs;
        //private ICollection<StoreNew> _storeNews;
        //private ICollection<MemberSetting> _memberSettings;
        //private ICollection<MemberDescription> _memberDescriptions;
        //private ICollection<AutoMailRecord> _autoMailRecords;
        //private ICollection<StoreSector> _storeSectors;
        //private ICollection<StoreCertificate> _storeCertificates;
        //private ICollection<StoreDealer> _storeDealers;
        //private ICollection<LoginLog> _loginLogs;

        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int MainPartyId { get; set; }
        public int PacketId { get; set; }
        public int? AuthorizedId { get; set; }
        public int? PortfoyUserId { get; set; }
        public string StoreNo { get; set; }
        public string StoreName { get; set; }
        public string StoreEMail { get; set; }
        public string StoreWeb { get; set; }
        public string StoreLogo { get; set; }
        public string OldStoreLogo { get; set; }
        public byte? StoreActiveType { get; set; }
        public DateTime? StorePacketBeginDate { get; set; }
        public DateTime? StorePacketEndDate { get; set; }
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
        public long? ViewCount { get; set; }
        public long? SingularViewCount { get; set; }
        public bool? StoreShowcase { get; set; }
        public string StorePicture { get; set; }
        public string OrderCode { get; set; }
        public decimal? storerate { get; set; }
        public int? ProductCount { get; set; }
        public bool? ReadyForSale { get; set; }
        public string StoreUniqueShortName { get; set; }
        public string TaxOffice { get; set; }
        public string TaxNumber { get; set; }
        public string MersisNo { get; set; }
        public string TradeRegistrNo { get; set; }
        public string StoreUrlName { get; set; }
        public string StoreShortName { get; set; }
        public string StoreBanner { get; set; }
        public bool? IsAllowProductSellUrl { get; set; }
        public string PurchasingDepartmentEmail { get; set; }
        public string PurchasingDepartmentName { get; set; }
        public string StoreProfileHomeDescription { get; set; }
        public string SeoTitle { get; set; }
        public string SeoDescription { get; set; }
        public string SeoKeyword { get; set; }
        public bool? IsProductAdded { get; set; }

        public virtual ICollection<StoreActivityCategory> StoreActivityCategories
        {
            get { return _storeActivityCategories ?? (_storeActivityCategories = new List<StoreActivityCategory>()); }
            protected set { _storeActivityCategories = value; }
        }

        //public ICollection<StoreChangeHistory> StoreChangeHistories
        //{
        //    get { return _storechangeHistories ?? (_storechangeHistories = new List<StoreChangeHistory>()); }
        //    protected set { _storechangeHistories = value; }
        //}

        //public ICollection<MemberDescription> MemberDescriptions
        //{
        //    get { return _memberDescriptions ?? (_memberDescriptions = new List<MemberDescription>()); }
        //    protected set { _memberDescriptions = value; }
        //}

        //public ICollection<LoginLog> LoginLogs
        //{
        //    get { return _loginLogs ?? (_loginLogs = new List<LoginLog>()); }
        //    protected set { _loginLogs = value; }
        //}

        //public ICollection<DealerBrand> DealarBrands
        //{
        //    get { return _dealerBrands ?? (_dealerBrands = new List<DealerBrand>()); }
        //    protected set { _dealerBrands = value; }
        //}

        //public ICollection<WhatsappLog> WhatsappLogs
        //{
        //    get { return _whatsaooLogs ?? (_whatsaooLogs = new List<WhatsappLog>()); }
        //    protected set { _whatsaooLogs = value; }
        //}

        //public ICollection<StoreNew> StoreNews
        //{
        //    get { return _storeNews ?? (_storeNews = new List<StoreNew>()); }
        //    protected set { _storeNews = value; }
        //}

        //public ICollection<MemberSetting> MemberSettings
        //{
        //    get { return _memberSettings ?? (_memberSettings = new List<MemberSetting>()); }
        //    protected set { _memberSettings = value; }
        //}

        //public ICollection<AutoMailRecord> AutoMailRecords
        //{
        //    get { return _autoMailRecords ?? (_autoMailRecords = new List<AutoMailRecord>()); }
        //    protected set { _autoMailRecords = value; }
        //}

        //public ICollection<StoreSector> StoreSectors
        //{
        //    get { return _storeSectors ?? (_storeSectors = new List<StoreSector>()); }
        //    protected set { _storeSectors = value; }
        //}


        //public ICollection<StoreCertificate> StoreCertificates
        //{
        //    get { return _storeCertificates ?? (_storeCertificates = new List<StoreCertificate>()); }
        //    protected set { _storeCertificates = value; }
        //}

        //public ICollection<StoreDealer> StoreDealers
        //{
        //    get { return _storeDealers ?? (_storeDealers = new List<StoreDealer>()); }
        //    protected set { _storeDealers = value; }
        //}
        //public ICollection<StoreInfoNumberShow> StoreInfoNumberShows
        //{
        //    get { return _storeInfoNumberShows ?? (_storeInfoNumberShows = new List<StoreInfoNumberShow>()); }
        //    protected set { _storeInfoNumberShows = value; }
        //}

    }
}
