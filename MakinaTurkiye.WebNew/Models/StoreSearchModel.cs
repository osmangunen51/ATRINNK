using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using System.Web.Mvc;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using MakinaTurkiye.Entities.Tables.Common;
using MakinaTurkiye.Entities.Tables.Catalog;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
  //public class StoreSearchModel : SearchModel<StoreModel>
  //{

  //  public TopCategory ActiveCategory { get; set; }
  //  public IList<TopCategory> TopCategoryItems { get; set; }

  //  public IList<Category> ParentCategoryItems { get; set; }
  //  public int CategoryId { get; set; }
  //  public int CityId { get; set; }

  //  public SelectList SectorItems
  //  {
  //    get
  //    {
  //      IList<Category> sectors;
  //      using (var entities = new MakinaTurkiyeEntities())
  //      {
  //        sectors = entities.Categories.Where(c => c.CategoryParentId == null&&c.MainCategoryType==(byte)MainCategoryType.Ana_Kategori).OrderBy(c => c.CategoryOrder).OrderBy(c => c.CategoryName).ToList();
  //      }
  //      return new SelectList(sectors, "CategoryId", "CategoryName");
  //    }
  //  }

  //  public SelectList CityItems
  //  {
  //    get
  //    {
  //      IList<City> cityItems;
  //      using (var entities = new MakinaTurkiyeEntities())
  //      {
  //        cityItems = entities.Cities.AsEnumerable().ToList();
  //        cityItems.Insert(0, new City { CityId = 0, CityName = "< Şehir Seçiniz >" });
  //      }
  //      var items = cityItems.Where(c => c.CountryId == AppSettings.Turkiye);
  //      return new SelectList(items, "CityId", "CityName");
  //    }
  //  }

  //  public List<Locality> localityItems { get; set; }
  //  public List<City> cityItems { get; set; }

  //  public List<ECStoreCityIds> cityIds { get; set; }
  //  public List<ECStoreLocalityIds> localityIds { get; set; }


   

  //}
}