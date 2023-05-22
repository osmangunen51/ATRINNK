using NeoSistem.Trinnk.Web.Models.Entities;
using NeoSistem.Trinnk.Web.Models.ViewModels;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
using Trinnk.Core;

namespace NeoSistem.Trinnk.Web.Models
{
    public class StoreSearchModel : SearchModel<StoreModel>
    {

        public TopCategory ActiveCategory { get; set; }
        public IList<TopCategory> TopCategoryItems { get; set; }

        public IList<BaseModule.Entities.Category> ParentCategoryItems { get; set; }
        public int CategoryId { get; set; }
        public int CityId { get; set; }

        public SelectList SectorItems
        {
            get
            {
                IList<BaseModule.Entities.Category> sectors;
                using (var entities = new BaseModule.Entities.TrinnkEntities())
                {
                    sectors = entities.Categories.Where(c => c.CategoryParentId == null && c.MainCategoryType == (byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryOrder).OrderBy(c => c.CategoryName).ToList();
                }
                return new SelectList(sectors, "CategoryId", "CategoryName");
            }
        }

        public SelectList CityItems
        {
            get
            {
                IList<BaseModule.Entities.City> cityItems;
                using (var entities = new Entities.TrinnkEntities())
                {
                    cityItems = (IList<BaseModule.Entities.City>)entities.Cities.AsEnumerable().ToList();
                    cityItems.Insert(0, new BaseModule.Entities.City { CityId = 0, CityName = "< Şehir Seçiniz >" });
                }
                var items = cityItems.Where(c => c.CountryId == AppSettings.Turkiye);
                return new SelectList(items, "CityId", "CityName");
            }
        }
        public List<BaseModule.Entities.Locality> localityItems { get; set; }
        public List<BaseModule.Entities.City> cityItems { get; set; }
        public List<ECStoreCityIds> cityIds { get; set; }
        public List<ECStoreLocalityIds> localityIds { get; set; }
    }
}