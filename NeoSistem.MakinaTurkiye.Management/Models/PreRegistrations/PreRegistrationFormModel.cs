using System.Linq;
using System.Web.Mvc;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;

namespace NeoSistem.MakinaTurkiye.Management.Models.PreRegistrations
{
    public class PreRegistrainFormModel
    {
        public int Id { get; set; }
        public string StoreName { get; set; }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public string Email { get; set; }
        public string PhoneNumber { get; set; }
        public string PhoneNumber2 { get; set; }
        public string PhoneNumber3 { get; set; }
        public string WebUrl { get; set; }
        public string City { get; set; }

        public string ContactPhoneNumber { get; set; }
        public string ContactNameSurname { get; set; }

        private SelectList myCountryItems;
        public SelectList CountryItems
        {
            get
            {
                var entities = new MakinaTurkiyeEntities();
                if (myCountryItems == null || myCountryItems.Count() <= 0)
                {
                    var curCountry = new Classes.Country();
                    var country = entities.Countries.OrderBy(c => c.CountryOrder).ThenBy(n => n.CountryName).ToList();
                    country.Insert(0, new Country { CountryId = 0, CountryName = "< Lütfen Seçiniz >" });
                    myCountryItems = new SelectList(country, "CountryId", "CountryName", 0);
                }
                return myCountryItems;
            }
            set { myCountryItems = value; }
        }

        public string LocalityName { get; set; }
        public string CityName { get; set; }
        public string CountryName { get; set; }

        private SelectList myCityItems;
        public SelectList CityItems
        {
            get
            {
                if (myCityItems == null || myCityItems.Count() <= 0)
                {
                    myCityItems = new SelectList(new[] { new { CityId = 0, CityName = "< Lütfen Seçiniz >" } }, "CityId", "CityName", 1);
                }
                return myCityItems;
            }
            set { myCityItems = value; }
        }

        private SelectList myLocalityItems;
        public SelectList LocalityItems
        {
            get
            {
                if (myLocalityItems == null || myLocalityItems.Count() <= 0)
                {
                    myLocalityItems = new SelectList(new[] { new { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" } }, "LocalityId", "LocalityName", 1);
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
                if (myTownItems == null || myTownItems.Count() <= 0)
                {
                    myTownItems = new SelectList(new[] { new { TownId = 0, TownName = "< Lütfen Seçiniz >" } }, "TownId", "TownName", 1);
                }
                return myTownItems;
            }
            set { myTownItems = value; }
        }

    }
}