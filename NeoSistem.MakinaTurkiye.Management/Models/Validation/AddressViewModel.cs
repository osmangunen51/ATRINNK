using System.Collections.Generic;
using System.Web.Mvc;
namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class AddressViewModel
    {
        public int AddressId { get; set; }

        public int MainPartyId { get; set; }

        public int CityId { get; set; }

        public int CountryId { get; set; }

        public int LocalityId { get; set; }

        public bool AddressDefault { get; set; }

        public SelectList CityItems
        {
            get
            {
                var dataCity = new Classes.City();
                return new SelectList(dataCity.GetDataSet().Tables[0].DefaultView, "CityId", "CityName", this.CityId);
            }
        }

        public SelectList CountryItems
        {
            get
            {
                var dataCountry = new Classes.Country();
                return new SelectList(dataCountry.GetDataSet().Tables[0].DefaultView, "CountryId", "CountryName", this.CountryId);
            }
        }

        public SelectList LocalityItems
        {
            get
            {
                var dataLocality = new Classes.Locality();
                return new SelectList(dataLocality.GetDataSet().Tables[0].DefaultView, "LocalityId", "LocalityName", this.LocalityId);
            }
        }

        public IList<PhoneModel> PhoneItems { get; set; }

    }
}