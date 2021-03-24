using global::MakinaTurkiye.Services.Catalog;
namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using NeoSistem.MakinaTurkiye.Management.Models.ViewModel;
    using System;
    using System.Collections.Generic;
    using System.Collections.ObjectModel;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public class ConstantController : BaseController
    {
        const string STARTCOLUMN = "ConstantId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        private readonly ICategoryPropertieService _categoryPropertieService;

        public ConstantController(ICategoryPropertieService categoryPropertieService)
        {
            _categoryPropertieService = categoryPropertieService;
        }
        public ActionResult Index()
        {
            PAGEID = PermissionPage.SabitTipler;

            ICollection<ConstantModel> model = new Collection<ConstantModel>();
            return View(model);
        }

        public string ProductRateCalculate()
        {
            Data.Product dataProduct = new Data.Product();
            dataProduct.ProductRateCalculate();
            StringBuilder s = new StringBuilder();
            s.Append("Hesaplama Tamamlandı");
            return s.ToString();
        }

        [HttpPost]
        public ActionResult Create(string ConstantName, byte ConstantType, int Order, short ConstantId, string ConstantPropertie)
        {

            var curConstant = new Classes.Constant();

            if (ConstantId > 0)
            {
                using (var entities = new MakinaTurkiyeEntities())
                {

                    var duzelt = entities.Constants.FirstOrDefault(k => k.ConstantId == ConstantId);
                    if (duzelt != null)
                    {
                        duzelt.ConstantName = ConstantName;
                        duzelt.ConstantType = ConstantType;
                        duzelt.ContstantPropertie = ConstantPropertie;
                        duzelt.Order = Order;
                        entities.SaveChanges();
                    }
                }
            }
            else
            {
                using (var entities = new MakinaTurkiyeEntities())
                {

                    var constant = new Constant
                    {
                        ConstantName = ConstantName,
                        ConstantType = ConstantType,
                        ContstantPropertie = ConstantPropertie,
                        Order = Order
                    };
                    entities.Constants.AddObject(constant);
                    entities.SaveChanges();
                }
            }
            var dataConstant = new Data.Constant();
            var model = dataConstant.ConstantGetByConstantType(ConstantType).AsCollection<ConstantModel>();

            return View("ConstantList", model);
        }

        public ActionResult EditConstants(int id)
        {
            var model = entities.Constants.Where(c => c.ConstantId == id).SingleOrDefault();
            return View(model);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditConstants(int id, Constant model)
        {
            Constant newmodel = entities.Constants.Where(c => c.ConstantId == id).SingleOrDefault();
            if (newmodel.ConstantType == 14 || newmodel.ConstantType == 15)
            {
                if (newmodel.ConstantId != 247 && newmodel.ConstantId != 246 && newmodel.ConstantId != 248)
                {
                    MessagesMT template = entities.MessagesMTs.FirstOrDefault(c => c.MessagesMTName == "templatemail");
                    newmodel.ConstantMailContent = model.ConstantMailContent;
                    newmodel.ContstantPropertie = template.MessagesMTPropertie.Replace("#icerik#", newmodel.ConstantMailContent);
                }
            }
            newmodel.ConstantName = model.ConstantName;
            if ((newmodel.ConstantType != 14 && newmodel.ConstantType != 15) || (newmodel.ConstantId == 247 || newmodel.ConstantId == 246 || newmodel.ConstantId == 248))
            {
                newmodel.ContstantPropertie = model.ContstantPropertie;
            }

            newmodel.ConstantTitle = model.ConstantTitle;
            model.ConstantType = newmodel.ConstantType;
            entities.SaveChanges();
            return View(model);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var curConstant = new Classes.Constant();
            return Json(new { m = curConstant.Delete(id) });
        }

        [HttpPost]
        public ActionResult GetConstantType(byte ConstantType)
        {
            var dataConstant = new Data.Constant();
            var model = dataConstant.ConstantGetByConstantType(ConstantType).AsCollection<ConstantModel>();

            return View("ConstantList", model);
        }

        public ActionResult GetConstantName(short ConstantId)
        {
            var curConstant = new Classes.Constant();
            curConstant.LoadEntity(ConstantId);
            return Json(new { order = curConstant.Order, constantName = curConstant.ConstantName, constantPropertie = curConstant.ConstantPropertie });
            //return Content(curConstant.ConstantName);
        }

        #region country

        public ActionResult country(string id, string success)
        {
            var entities = new MakinaTurkiyeEntities();
            CountryViewModel countries = new CountryViewModel();
            if (success != null) ViewData["success"] = "true";
            if (id != null)
            {
                int ID = Convert.ToInt32(id);
                var country = entities.Countries.FirstOrDefault(x => x.CountryId == ID);
                countries.CountryName = country.CountryName;
                countries.CultureCode = country.CultureCode;
                countries.Active = (bool)country.Active;
                countries.Id = country.CountryId;
                ViewData["update"] = "1";
            }

            countries.CountryList = entities.Countries.ToList();
            return View(countries);
        }

        [HttpPost]
        public ActionResult country(CountryViewModel model, string update)
        {
            Country countryAdd;
            var entities = new MakinaTurkiyeEntities();
            if (update == "1")
            {
                countryAdd = entities.Countries.FirstOrDefault(x => x.CountryId == model.Id);

            }
            else
            {
                countryAdd = new Country();

            }
            if (ModelState.IsValid)
            {
                var countryExist = entities.Countries.Any(x => x.CountryName == model.CountryName || x.CountryId == model.Id || x.CultureCode == model.CultureCode);
                if (update == "1") countryExist = false;
                if (!countryExist)
                {

                    countryAdd.CountryId = model.Id;
                    countryAdd.CountryName = model.CountryName;
                    countryAdd.CultureCode = model.CultureCode;
                    countryAdd.CountryOrder = 2;
                    countryAdd.Active = model.Active;
                    if (update != "1")
                        entities.Countries.AddObject(countryAdd);
                    entities.SaveChanges();

                    return RedirectToAction("country", "Constant", new { success = "basarili" });
                }
                else
                {
                    ViewData["exist"] = "true";
                    model.CountryList = entities.Countries.ToList();
                    return View(model);
                }

            }
            else
            {
                model.CountryList = entities.Countries.ToList();
                return View(model);

            }
        }

        [HttpPost]
        public JsonResult DeleteCountry(int id)
        {
            var entities = new MakinaTurkiyeEntities();
            var myCountry = entities.Addresses.Any(x => x.CountryId == id);
            if (myCountry)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
            else
            {
                entities.Countries.DeleteObject(entities.Countries.First(x => x.CountryId == id));
                entities.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }


        }

        #endregion

        #region city

        public ActionResult subAddress(string sonuc)
        {
            if (sonuc == "basarili") ViewData["success"] = "true";
            else if (sonuc == "yanlis") ViewData["error"] = "true";
            SubAddressViewModel addresView = new SubAddressViewModel();
            List<Country> countryItems = entities.Countries.OrderBy(x => x.CountryOrder).ThenBy(x => x.CountryName).ToList();
            countryItems.Insert(0, new Country { CountryName = "<Lütfen Seçiniz>", CountryId = 0 });
            addresView.CountryItems = new SelectList(countryItems, "CountryId", "CountryName", 0);
            return View(addresView);
        }

        [HttpPost]
        public JsonResult subAddress(string cityId, string countryId, string localityId, string updateType, string updateTownId, string updateCityId, string updateLocalityId, string update, string constantValue, string type, string areaCode)
        {
            if (constantValue != "")
            {
                if (updateType != "0") type = updateType;
                if (type != "")
                {
                    if (type == "il")
                    {

                        if (areaCode == "")
                        {
                            ViewData["error"] = "true";

                            return Json(false, JsonRequestBehavior.AllowGet);
                        }
                        else
                        {
                            City cityAdd = new City();
                            if (update == "1")
                            {
                                int id = Convert.ToInt32(updateCityId);
                                cityAdd = entities.Cities.Where(x => x.CityId == id).First();
                            }
                            else
                            {
                                cityAdd.CityId = entities.Cities.FirstOrDefault(x => x.CityId == entities.Cities.Max(y => y.CityId)).CityId + 1;

                            }
                            cityAdd.CountryId = Convert.ToInt32(countryId);
                            cityAdd.CityName = constantValue;
                            cityAdd.CityName_Big = constantValue.ToUpper();
                            cityAdd.CityName_Small = constantValue.ToLower();
                            cityAdd.AreaCode = areaCode;
                            if (update == "0") entities.Cities.AddObject(cityAdd);
                            entities.SaveChanges();
                            return Json(true, JsonRequestBehavior.AllowGet);
                        }
                    }
                    else if (type == "ilce")
                    {
                        Locality locality = new Locality();
                        if (update == "1")
                        {
                            int id = Convert.ToInt32(updateLocalityId);
                            locality = entities.Localities.First(x => x.LocalityId == id);
                        }
                        else
                        {
                            locality.LocalityId = entities.Localities.FirstOrDefault(x => x.LocalityId == entities.Localities.Max(y => y.LocalityId)).LocalityId + 1;
                        }
                        locality.CountryId = Convert.ToInt32(countryId);
                        locality.CityId = Convert.ToInt32(cityId);
                        locality.LocalityName = constantValue;
                        locality.LocalityName_Big = constantValue.ToUpper();
                        locality.LocalithName_Small = constantValue.ToLower();
                        if (update == "0") entities.Localities.AddObject(locality);
                        entities.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);
                    }
                    else if (type == "mahalle")
                    {
                        Town town = new Town();
                        if (update == "1")
                        {
                            int id = Convert.ToInt32(updateTownId);
                            town = entities.Towns.First(x => x.TownId == id);
                        }
                        else
                        {
                            town.TownId = entities.Towns.FirstOrDefault(x => x.TownId == entities.Towns.Max(y => y.TownId)).TownId + 1;

                        }
                        town.CityId = Convert.ToInt32(cityId);
                        town.LocalityId = Convert.ToInt32(localityId);
                        town.TownName = constantValue;
                        town.TownName_Big = constantValue.ToUpper();
                        town.TownName_Small = constantValue.ToLower();
                        if (update == "0") entities.Towns.AddObject(town);
                        entities.SaveChanges();
                        return Json(true, JsonRequestBehavior.AllowGet);


                    }
                    else
                    {

                        return Json(false, JsonRequestBehavior.AllowGet);
                    }

                }
                else
                {

                    return Json(false, JsonRequestBehavior.AllowGet);
                }
            }
            else
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }

        }

        #endregion

        #region lists

        [HttpPost]
        public JsonResult CityList(int id)
        {
            IList<City> cityItems;
            using (var entities = new MakinaTurkiyeEntities())
            {
                cityItems = entities.Cities.Where(c => c.CountryId == id).OrderBy(c => c.CityOrder).ThenBy(n => n.CityName).ToList();
            }
            cityItems.Insert(0, new City { CityId = 0, CityName = "< Lütfen Seçiniz >" });
            return Json(new SelectList(cityItems, "CityId", "CityName"), JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult LocalityList(int countryid, int cityid)
        {
            IList<Locality> cityItems;
            using (var entities = new MakinaTurkiyeEntities())
            {
                cityItems = entities.Localities.Where(c => c.CountryId == countryid && c.CityId == cityid).ToList();
            }
            cityItems.Insert(0, new Locality { LocalityId = 0, LocalityName = "< Lütfen Seçiniz >" });
            return Json(new SelectList(cityItems, "LocalityId", "LocalityName"), JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult CityListGet(int? id)
        {
            var Result = (from obj in entities.Cities where obj.CountryId == id select new { cityID = obj.CityId, CityName = obj.CityName, AreaCode = obj.AreaCode }).ToList();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult LocationListGet(int countryid, int cityid)
        {
            var Result = (from loc in entities.Localities where loc.CountryId == countryid && loc.CityId == cityid orderby loc.LocalityName select new { id = loc.LocalityId, name = loc.LocalityName }).OrderBy(x => x.name).ToList();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult TownListGet(int cityid, int localityid)
        {
            var Result = (from t in entities.Towns where t.CityId == cityid && t.LocalityId == localityid orderby t.TownName select new { id = t.TownId, name = t.TownName }).OrderBy(x => x.name).ToList();
            return Json(Result, JsonRequestBehavior.AllowGet);
        }

        #endregion

        #region Crud SubAdress

        [HttpPost]
        public JsonResult DeleteCity(int id)
        {
            var addressExist = entities.Addresses.Any(x => x.CityId == id);
            if (!addressExist)
            {
                var city = entities.Cities.First(x => x.CityId == id);
                entities.Cities.DeleteObject(city);
                entities.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult DeleteLocality(int id)
        {
            var addressExist = entities.Addresses.Any(x => x.LocalityId == id);
            if (!addressExist)
            {
                var locality = entities.Localities.First(x => x.LocalityId == id);
                entities.Localities.DeleteObject(locality);
                entities.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult DeleteTown(int id)
        {
            var addressExist = entities.Addresses.Any(x => x.TownId == id);
            if (!addressExist)
            {
                var town = entities.Towns.First(x => x.TownId == id);
                entities.Towns.DeleteObject(town);
                entities.SaveChanges();
                return Json(true, JsonRequestBehavior.AllowGet);
            }
            else return Json(false, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public JsonResult GetCity(int id)
        {
            var city = entities.Cities.First(x => x.CityId == id);

            return Json(new { AreaCode = city.AreaCode, Name = city.CityName, ID = city.CityId });
        }

        [HttpPost]
        public JsonResult GetLocality(int id)
        {
            var locality = entities.Localities.First(x => x.LocalityId == id);
            return Json(new { Name = locality.LocalityName, ID = locality.LocalityId }, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public JsonResult GetTown(int id)
        {
            var town = entities.Towns.First(x => x.TownId == id);
            return Json(new { Name = town.TownName, ID = town.TownId }, JsonRequestBehavior.AllowGet);
        }

        #endregion

        public ActionResult Propertie(string id, string opr)
        {


            PropertieViewModel model = new PropertieViewModel();
            var propertie = new global::MakinaTurkiye.Entities.Tables.Catalog.Propertie();
            int ID = 0;
            if (!string.IsNullOrEmpty(id) && id != "0")
            {

                ID = Convert.ToInt32(id);
                propertie = _categoryPropertieService.GetPropertieById(ID);
                model.PropertieId = ID;

                model.PropertieName = propertie.PropertieName;

            }
            if (opr == "errorName")
            {
                ModelState.AddModelError("PropertieName", "Lütfen görünen ad ekleyiniz");
            }

            model.PropertieTypes.Add(new SelectListItem { Text = "Text", Value = "2", Selected = ((byte)PropertieType.Text == propertie.PropertieType) });
            model.PropertieTypes.Add(new SelectListItem { Text = "Çok Seçenekli", Value = "3", Selected = ((byte)PropertieType.MutipleOption == propertie.PropertieType) });

            model.PropertieTypes.Add(new SelectListItem { Text = "Editör", Value = "1", Selected = ((byte)PropertieType.Editor == propertie.PropertieType) });
            FilterModel<PropertieModel> properties = new FilterModel<PropertieModel>();

            var propertiesData = _categoryPropertieService.GetAllProperties();
            List<PropertieModel> propertieModelList = new List<PropertieModel>();
            foreach (var item in propertiesData.OrderByDescending(x => x.PropertieId).Skip(0).Take(25).ToList())
            {
                propertieModelList.Add(new PropertieModel { PropertieId = item.PropertieId, PropertieName = item.PropertieName, PropertieType = item.PropertieType });
            }
            properties.Source = propertieModelList;
            properties.CurrentPage = 1;
            properties.PageDimension = 25;
            properties.TotalRecord = propertiesData.Count;

            model.Properties = properties;
            return View(model);

        }
        [HttpPost]
        public ActionResult Propertie(PropertieViewModel model)
        {
            var propertie = new global::MakinaTurkiye.Entities.Tables.Catalog.Propertie();
            if (!string.IsNullOrEmpty(model.PropertieName))
            {
                if (model.PropertieId != 0)
                    propertie = _categoryPropertieService.GetPropertieById(model.PropertieId);
                propertie.PropertieName = model.PropertieName;
                propertie.PropertieType = model.PropertieType;
                if (model.PropertieId == 0)
                    _categoryPropertieService.InsertPropertie(propertie);
                else
                    _categoryPropertieService.UpdatePropertie(propertie);

                return RedirectToAction("Propertie", new { opr = "success" });
            }
            else
            {
                return RedirectToAction("Propertie", new { opr = "errorName", id = model.PropertieId });
            }
        }
        [HttpPost]
        public ActionResult DeletePropertie(int id)
        {
            var propertie = _categoryPropertieService.GetPropertieById(id);
            if (propertie != null)
                _categoryPropertieService.DeletePropertie(propertie);

            return Json(true, JsonRequestBehavior.AllowGet);

        }
        [HttpPost]
        public PartialViewResult GetProperties(int currentPage)
        {
            FilterModel<PropertieModel> properties = new FilterModel<PropertieModel>();

            var propertiesData = _categoryPropertieService.GetAllProperties();
            List<PropertieModel> propertieModelList = new List<PropertieModel>();
            foreach (var item in propertiesData.OrderByDescending(x => x.PropertieId).Skip(currentPage * 25 - 25).Take(25).ToList())
            {
                propertieModelList.Add(new PropertieModel { PropertieId = item.PropertieId, PropertieName = item.PropertieName, PropertieType = item.PropertieType });
            }
            properties.Source = propertieModelList;
            properties.CurrentPage = currentPage;
            properties.PageDimension = 25;
            properties.TotalRecord = propertiesData.Count;
            return PartialView("_PropertieItem", properties);
        }

        public ActionResult PropertieAttr(int id, string op)
        {
            PropertieAttrViewModel model = new PropertieAttrViewModel();
            var propertie = _categoryPropertieService.GetPropertieById(id);
            model.PropertieName = propertie.PropertieName;
            model.PropertieId = id;

            if (op == "errorName")
            {
                ModelState.AddModelError("PropertieAttrValue", "Lütfen seçenek içeriğini giriniz");
            }
            var propertiAttrs = _categoryPropertieService.GetPropertiesAttrByPropertieId(id);
            foreach (var item in propertiAttrs)
            {
                model.PropertieAttrs.Add(new PropertieAttrModel { PropertieAttrId = item.PropertieAttrId, Order = item.DisplayOrder != null ? item.DisplayOrder.Value : 0, PropertieAttrName = item.AttrValue, PropertieId = item.PropertieId });
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult PropertieAttr(PropertieAttrViewModel model)
        {
            if (!string.IsNullOrEmpty(model.PropertieAttrValue))
            {
                var propertieAttr = new global::MakinaTurkiye.Entities.Tables.Catalog.PropertieAttr();
                propertieAttr.AttrValue = model.PropertieAttrValue;
                propertieAttr.PropertieId = model.PropertieId;
                if (model.Order != null && model.Order != 0)
                    propertieAttr.DisplayOrder = Convert.ToByte(model.Order);
                _categoryPropertieService.InsertPropertieAttr(propertieAttr);
                return RedirectToAction("PropertieAttr", new { id = model.PropertieId });
            }
            else
            {
                return RedirectToAction("PropertieAttr", new { op = "errorName", id = model.PropertieId });

            }
        }
        [HttpGet]
        public ActionResult DeletePropertieAttr(int id, int propertieId)
        {
            var propertieAttr = _categoryPropertieService.GetPropertieAttrByPropertieAttrId(id);
            if (propertieAttr != null)
                _categoryPropertieService.DeletePropertieAttr(propertieAttr);
            return RedirectToAction("PropertieAttr", new { id = propertieId });
        }

        [HttpGet]
        public ActionResult SubContents(string id, string islem)
        {
            SubConstantModel model = new SubConstantModel();
            model.ConstantId = id;
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.SubConstant && x.ConstantTitle == id).ToList();
            foreach (var item in constants)
            {
                model.Contents.Add(item.ConstantId, item.ContstantPropertie);
            }
            if(!string.IsNullOrEmpty(islem) && islem.Equals("success"))
            {
                model.Message = "İşlem Başarılı"; 
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult SubContents(SubConstantModel model)
        {
            var constant = new Constant { ConstantTitle = model.ConstantId, ContstantPropertie = model.Content, ConstantType = (byte)ConstantType.SubConstant };
            entities.Constants.AddObject(constant);
            entities.SaveChanges();
            return RedirectToAction("SubContents", new { islem = "success", id = model.ConstantId });


        }
        [HttpGet]
        public JsonResult GetSubConstant(string id)
        {
            List<string> contents = new List<string>();
            
            var constants = entities.Constants.Where(x => x.ConstantType == (byte)ConstantType.SubConstant && x.ConstantTitle == id).ToList();
            foreach (var item in constants)
            {
                contents.Add(item.ContstantPropertie);
            }
            

            return Json(new { data = contents }, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult DeleteSubConstant(int id, string constantParentId)
        {
            var constant = entities.Constants.FirstOrDefault(x => x.ConstantId == id);
            entities.Constants.DeleteObject(constant);
            entities.SaveChanges();
            return RedirectToAction("SubContents", new { islem = "success", id = constantParentId });
        }
    }



}