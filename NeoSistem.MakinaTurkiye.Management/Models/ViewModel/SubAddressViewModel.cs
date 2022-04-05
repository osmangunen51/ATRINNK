using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class SubAddressViewModel
    {
        public int CountryID { get; set; }
        public SelectList CountryItems { get; set; }
        public City CityID { get; set; }
        public Locality Locality { get; set; }
        public SelectList CityItems { get; set; }
        public SelectList LocalityItems { get; set; }
        public Town TownID { get; set; }
        public SelectList TownItems { get; set; }
        public string CityName { get; set; }
        public string TownName { get; set; }

    }
}