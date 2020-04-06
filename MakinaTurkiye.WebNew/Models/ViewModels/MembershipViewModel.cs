namespace NeoSistem.MakinaTurkiye.Web.Models
{

    using System.Web.Mvc;
    using System.Collections.Generic;
    using System.Linq;
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Entities.Tables.Common;
    using global::MakinaTurkiye.Entities.Tables.Catalog;
    using global::MakinaTurkiye.Entities.Tables.Stores;
    using global::MakinaTurkiye.Entities.Tables.Members;
    using global::MakinaTurkiye.Services.Common;
    using global::MakinaTurkiye.Core.Infrastructure;


    //[AttemptMustMatchEncryptedSolution("Attempt", "EncrypedSolution", Constants.SecretKey, ErrorMessage = "Güvenlik Kodu Yanlış")]
    public class MembershipViewModel
    {
        //test zorunlu

        private MembershipModel myMembership;
        public MembershipModel MembershipModel
        {
            get
            {
                if (myMembership == null)
                {
                    myMembership = new MembershipModel();
                }
                return myMembership;
            }
            set { myMembership = value; }
        }

        public IEnumerable<Phone> PhoneItems { get; set; }

        public IEnumerable<Category> CategoryItems { get; set; }


        public IList<ActivityType> ActivityItems { get; set; }

        private SelectList myCityItems;
        public SelectList CityItems
        {
            get
            {
                if (myCityItems == null || myCityItems.Count() == 0)
                {
                    return new SelectList(new[] { new { CityId = 0, CityName = "< Lütfen Seçiniz >" } }, "CityId", "CityName", 1);
                }
                return myCityItems;
            }
            set { myCityItems = value; }
        }

        public CompanyDemandMembership companyMembershipDemand { get; set; }

        private SelectList myCountryItems;
        public string ErrorMessage { get; set; }
        public SelectList CountryItems
        {
            get
            {
                IAddressService addressService = EngineContext.Current.Resolve<IAddressService>();

                IList<Country> countryItems = addressService.GetAllCountries();

                if (myCountryItems == null || myCountryItems.Count() <= 0)
                {
                    countryItems.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
                    myCountryItems = new SelectList(countryItems, "CountryId", "CountryName", 0);
                }
                return myCountryItems;
            }
            set { myCountryItems = value; }
        }


        private SelectList myLocalityItems;
        public SelectList LocalityItems
        {
            get
            {
                if (myLocalityItems == null || myLocalityItems.Count() == 0)
                {
                    return new SelectList(new[] { new { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "LocalityName", 1);
                }
                return myLocalityItems;
            }
            set { myLocalityItems = value; }
        }


        private SelectList myTownItems;
        public SelectList TownItems
        {
            get
            {
                if (myTownItems == null || myTownItems.Count() == 0)
                {
                    myTownItems = new SelectList(new[] { new { TownId = 0, TownName = "< Lütfen Seçiniz >" } }, "TownId", "TownName", 1);
                }
                return myTownItems;
            }
            set { myTownItems = value; }
        }


        private SelectList myDistrictItems;
        public SelectList DistrictItems
        {
            get
            {
                if (myDistrictItems == null || myDistrictItems.Count() == 0)
                {
                    myDistrictItems = new SelectList(new[] { new { DistrictId = 0, DistrictName = "< Lütfen Seçiniz >" } }, "DistrictId", "DistrictName", 1);
                }
                return myDistrictItems;
            }
            set { myDistrictItems = value; }
        }

        public string Attempt { get; set; }
        public string Name { get; set; }
        public string Comment { get; set; }
        public string EncrypedSolution { get; set; }

        public string StoreEMail { get; set; }
        public string StoreEMailAgain { get; set; }

        public ICollection<CategoryModel> CategoryParentItemsByCategoryId(int CategoryId)
        {
            var dataCategory = new Data.Category();
            return dataCategory.GetCategoryParentByCategoryId(CategoryId, (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>();
        }

        public ICollection<CategoryModel> CategoryGroupParentItemsByCategoryId(int CategoryId)
        {
            var dataCategory = new Data.Category();
            return dataCategory.CategoryGroupParentItemsByCategoryId(CategoryId).AsCollection<CategoryModel>();
        }


        public Phone Phone { get; set; }

    }

    internal class Constants
    {
        public const string SecretKey = "mySecretKey";
    }
}