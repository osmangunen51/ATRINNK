using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Management.Models.ViewModel
{
    public class CountryViewModel
    {
        public int Id { get; set; }
        public string CountryName { get; set; }
        public bool Active { get; set; }
        public string CultureCode { get; set; }
        public IEnumerable<Country> CountryList { get; set; }


    }
}