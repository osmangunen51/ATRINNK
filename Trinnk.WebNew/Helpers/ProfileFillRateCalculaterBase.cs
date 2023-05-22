using NeoSistem.Trinnk.Web.Models;
using System.Collections.Generic;
using System.Linq;
using Trinnk.Core;
namespace NeoSistem.Trinnk.Web.Helpers
{
    public class ProfileFillRateCalculaterBase
    {
        #region urldefination
        private string AddressPhoneUpdateLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/Personal/ChangeAddress";
        private string PersonalUpdateLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/Personal/Update";
        private string MemberTitleTypeUpdateLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/MemberType/InstitutionalStep";
        private string UpdateLogoLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/Store/UpdateLogo";
        private string UpdateStoreInfoLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/Store/UpdateStore";
        private string UpdateTaxsLink = AppSettings.SiteUrlWithoutLastSlash + "/Account/Store/UpdateStore";
        #endregion

        public int NamePer { get; set; }
        public int SurnamePer { get; set; }
        public int BirthDatePer { get; set; }
        public int PhoneGsmPer { get; set; }
        public int AddressPer { get; set; }
        public int PhonePer { get; set; }
        public int PhonePerOther { get; set; }
        public int FaxPer { get; set; }
        public int AuthorizedCaptionPer { get; set; }
        public int StoreLogoPer { get; set; }
        public int StoreCaptionPer { get; set; }
        public int StoreWebUrlPer { get; set; }
        public int CompanyCreatedDatePer { get; set; }
        public int ActivityTypePer { get; set; }
        public int FundPer { get; set; }
        public int EmplooyerNumberPer { get; set; }
        public int StoreEndorsementPer { get; set; }
        public int StoreTypePer { get; set; }
        public int TaxOfficePer { get; set; }
        public int TaxNumberPer { get; set; }
        public int MersisNoPer { get; set; }
        public int StoreDescriptionPer { get; set; }
        public global::Trinnk.Entities.Tables.Members.Member Member;
        public global::Trinnk.Entities.Tables.Stores.Store Store;

        public ProfileFillRateCalculaterBase(global::Trinnk.Entities.Tables.Members.Member member, global::Trinnk.Entities.Tables.Stores.Store store)
        {
            this.Member = member;
            this.Store = store;

            if (this.Store != null)
            {
                NamePer = 1;
                SurnamePer = 1;
                BirthDatePer = 3;
                PhoneGsmPer = 5;
                AddressPer = 5;
                PhonePer = 12;
                PhonePerOther = 4;
                FaxPer = 8;
                AuthorizedCaptionPer = 2;
                StoreLogoPer = 5;
                StoreCaptionPer = 3;
                StoreWebUrlPer = 3;
                CompanyCreatedDatePer = 3;
                ActivityTypePer = 3;
                FundPer = 3;
                EmplooyerNumberPer = 3;
                StoreEndorsementPer = 3;
                StoreTypePer = 5;
                TaxOfficePer = 7;
                TaxNumberPer = 5;
                MersisNoPer = 7;
                StoreDescriptionPer = 5;
            }
            else
            {
                NamePer = 5;
                SurnamePer = 5;
                BirthDatePer = 10;
                PhoneGsmPer = 20;
                AddressPer = 10;
                PhonePer = 25;
                PhonePerOther = 5;
                FaxPer = 20;
            }
        }
        public ProfilRateResult GetStoreProfileFillPercent(global::Trinnk.Entities.Tables.Common.Address address, IList<global::Trinnk.Entities.Tables.Common.Phone> Phones)
        {
            ProfilRateResult result = new ProfilRateResult();

            int profileFillRate = 0;
            if (!string.IsNullOrEmpty(Member.MemberName)) profileFillRate = profileFillRate + NamePer;
            else result.ProfilUpdateLink.Add(PersonalUpdateLink);

            if (!string.IsNullOrEmpty(Member.MemberSurname)) profileFillRate = profileFillRate + SurnamePer;
            else result.ProfilUpdateLink.Add(PersonalUpdateLink);
            if (Member.BirthDate != null) profileFillRate = profileFillRate + BirthDatePer;
            else result.ProfilUpdateLink.Add(PersonalUpdateLink);
            if (address != null)
                profileFillRate = profileFillRate + AddressPer;
            else
                result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);

            if (Phones != null)
            {
                var phoneGsm = Phones.FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                if (phoneGsm != null && phoneGsm.active == 1)
                    profileFillRate = profileFillRate + PhoneGsmPer;
                else result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);

                var phoneandphoenOtherNumber = Phones.Where(x => x.PhoneType == (byte)PhoneType.Phone);
                if (phoneandphoenOtherNumber.ToList().Count != 0)
                {
                    if (phoneandphoenOtherNumber.ToList().Count == 2)
                        profileFillRate = profileFillRate + PhonePer + PhonePerOther;
                    else if (phoneandphoenOtherNumber.ToList().Count == 1)
                    {
                        profileFillRate = profileFillRate + PhonePer;
                        result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);
                    }
                    else
                        result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);
                }

                var phoneFax = Phones.Where(x => x.PhoneType == (byte)PhoneType.Fax);
                if (phoneFax.ToList().Count != 0)
                {
                    profileFillRate = profileFillRate + FaxPer;
                }
                else result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);


            }
            else result.ProfilUpdateLink.Add(AddressPhoneUpdateLink);

            if (Store != null)
            {

                if (!string.IsNullOrEmpty(Store.StoreName)) profileFillRate = profileFillRate + StoreCaptionPer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);
                if (!string.IsNullOrEmpty(Store.StoreWeb)) profileFillRate = profileFillRate + StoreWebUrlPer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);
                if (Member.MemberTitleType != null) profileFillRate = profileFillRate + AuthorizedCaptionPer;
                else result.ProfilUpdateLink.Add(MemberTitleTypeUpdateLink);

                if (!string.IsNullOrEmpty(Store.StoreLogo)) profileFillRate = profileFillRate + StoreLogoPer;
                else result.ProfilUpdateLink.Add(UpdateLogoLink);
                if (Store.StoreRecordDate != null) profileFillRate += CompanyCreatedDatePer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);
                if (Store.StoreActiveType != null) profileFillRate += ActivityTypePer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);
                if (Store.StoreCapital != null) profileFillRate += FundPer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);

                if (Store.StoreEmployeesCount != null) profileFillRate += EmplooyerNumberPer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);

                if (Store.StoreEndorsement != null) profileFillRate += StoreEndorsementPer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);

                if (Store.StoreType != null) profileFillRate += StoreTypePer;
                else result.ProfilUpdateLink.Add(UpdateStoreInfoLink);

                if (!string.IsNullOrEmpty(Store.TaxOffice)) profileFillRate += TaxOfficePer;
                else result.ProfilUpdateLink.Add(UpdateTaxsLink);
                if (!string.IsNullOrEmpty(Store.TaxNumber)) profileFillRate += TaxNumberPer;
                else result.ProfilUpdateLink.Add(UpdateTaxsLink);
                if (!string.IsNullOrEmpty(Store.MersisNo)) profileFillRate += MersisNoPer;
                else result.ProfilUpdateLink.Add(UpdateTaxsLink);

            }
            result.ProfileRate = profileFillRate;
            result.ProfilUpdateLink.Distinct();
            return result;
        }
    }
    public class ProfilRateResult
    {
        public int ProfileRate { get; set; }
        public List<string> ProfilUpdateLink { get; set; }
        public ProfilRateResult()
        {
            this.ProfilUpdateLink = new List<string>();
        }
    }
}