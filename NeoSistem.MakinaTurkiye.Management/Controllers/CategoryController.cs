using global::MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Content;
using MakinaTurkiye.Utilities.HttpHelpers;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Cache;
using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.BaseMenuModels;
using NeoSistem.MakinaTurkiye.Management.Models.Catolog;
using NeoSistem.MakinaTurkiye.Management.Models.ViewModel;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
using NPOI.HSSF.UserModel;
using NPOI.SS.UserModel;
using NPOI.XSSF.UserModel;
using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class CategoryController : BaseController
    {

        #region Fields

        private ICategoryService _categoryService;
        private IProductService _productService;
        private ICategoryPropertieService _categoryPropertieService;
        private IBannerService _bannerService;
        private IBaseMenuService _baseMenuService;
        private ICategoryPlaceChoiceService _categoryPlaceChoiceService;

        #endregion

        #region Ctor

        public CategoryController(ICategoryService categoryService,
            IBannerService bannerService,
            IProductService productService, ICategoryPropertieService categoryPropertieService,
            IBaseMenuService baseMenuService,
            ICategoryPlaceChoiceService categoryPlaceChoiceService)
        {
            this._categoryService = categoryService;
            this._productService = productService;
            this._categoryPropertieService = categoryPropertieService;
            this._bannerService = bannerService;
            this._baseMenuService = baseMenuService;
            this._categoryPlaceChoiceService = categoryPlaceChoiceService;
        }

        #endregion

        #region Methods

        public ActionResult Index()
        {
            PAGEID = PermissionPage.KategoriYonetimi;
            var categories = CacheUtilities.CategoryAllRows().AsCollection<CategoryModel>();
            var model = categories.Where(c => c.CategoryParentId == null);

            return View("");
        }

        public ActionResult ProductMove()
        {

            return View();

        }

        [HttpPost]
        public ActionResult ProductMove(int? SelectedCategory, int? SelectedCategory1, string fromCategoryName, string toCategoryName)
        {
            var selectedCategoryProduct = new List<NeoSistem.MakinaTurkiye.Management.Models.Entities.Product>();
            var selected1 = new NeoSistem.MakinaTurkiye.Management.Models.Entities.Category();
            var selected2 = new NeoSistem.MakinaTurkiye.Management.Models.Entities.Category();
            bool checkNames = true;
            if (fromCategoryName != "" && toCategoryName != "")
            {

                selected1 = entities.Categories.Where(x => x.CategoryName.ToLower() == fromCategoryName.ToLower()).FirstOrDefault();
                selected2 = entities.Categories.Where(x => x.CategoryName.ToLower() == toCategoryName.ToLower()).FirstOrDefault();
                if (selected1 != null && selected2 != null)
                {
                    int id = Convert.ToInt32(selected1.CategoryId);
                    selectedCategoryProduct = entities.Products.Where(p => p.ModelId == id).ToList();

                }
                else
                    checkNames = false;


            }
            else
            {
                selectedCategoryProduct = entities.Products.Where(c => c.ModelId == SelectedCategory).ToList();
                selected1 = entities.Categories.Where(c => c.CategoryId == SelectedCategory).SingleOrDefault();
                selected2 = entities.Categories.Where(c => c.CategoryId == SelectedCategory1).SingleOrDefault();
            }


            if (checkNames)
            {
                if (selected1.CategoryType == (byte)CategoryType.Model && selected2.CategoryType == (byte)CategoryType.Model && selectedCategoryProduct.Count > 0)
                {
                    var dataCategory = new Data.Category();
                    List<int> treeList;
                    string treeName = String.Empty;

                    int all = 0;

                    var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(Convert.ToInt32(SelectedCategory1)).AsCollection<CategoryModel>();
                    var ids = (from c in treeItems select c.CategoryId);
                    var categoryNames = (from c in treeItems select c.CategoryName);
                    treeList = ids.ToList();
                    for (int i = 0; i < treeList.Count; i++)
                    {
                        treeName += treeList[i].ToString() + ".";
                    }


                    foreach (var item in selectedCategoryProduct)
                    {

                        item.CategoryTreeName = treeName;
                        item.CategoryId = treeItems.Where(c => c.CategoryType == 1).LastOrDefault().CategoryId;
                        item.ProductGroupId = treeItems.Where(c => c.CategoryType == (byte)CategoryType.ProductGroup).FirstOrDefault().CategoryId;
                        item.BrandId = treeItems.Where(c => c.CategoryType == (byte)CategoryType.Brand).FirstOrDefault().CategoryId;
                        if (treeItems.Where(c => c.CategoryType == (byte)CategoryType.Series).FirstOrDefault() != null)
                        {
                            item.SeriesId = treeItems.Where(c => c.CategoryType == (byte)CategoryType.Series).FirstOrDefault().CategoryId;
                        }
                        else
                            item.SeriesId = null;
                        item.ModelId = SelectedCategory1;
                        all += 1;
                    }
                    selected2.ProductCount = selected1.ProductCount + selected2.ProductCount;
                    selected1.ProductCount = 0;
                    entities.SaveChanges();

                    ViewData["selecteddone"] = all + " adet ürünün markası " + selected1.CategoryName + " " + selected2.CategoryName + " olarak değiştirildi";
                }
                else
                {
                    ViewData["selecteddone"] = "seçtiğiniz kategori model değil veya ürün sayısı sıfır olan model seçtiniz.";
                }
            }
            else
            {
                ViewData["selecteddone"] = "Girdiğiniz model isimleri bulunamadı.";
            }


            return View();

        }

        [HttpPost]
        public ActionResult CategoryBind(int id)
        {
            var dataCategory = new Data.Category();
            var items = entities.Categories.Where(c => c.CategoryParentId == id).OrderBy(c => c.CategoryName).ToList();

            if (items == null || items.Count() == 0)
            {
                string hidden = "<input type=\"hidden\" name=\"SelectedCategory\" value=\"" + id.ToString() + "\" />";
                return Content(hidden + "<div class=\"buttonDiv\">&nbsp;&nbsp;<span style=\"display: block; margin-bottom:20px; font-size: 12px; font-weight: bold;\">Kategori Seçiminiz Başarıyla Tamamlanmıştır.</span></div>");
            }

            return View("CategoryParentView", items);
        }

        [HttpPost]
        public ActionResult CategoryBind1(int id)
        {
            var dataCategory = new Data.Category();
            var items = entities.Categories.Where(c => c.CategoryParentId == id).OrderBy(c => c.CategoryName).ToList();

            if (items == null || items.Count() == 0)
            {
                string hidden = "<input type=\"hidden\" name=\"SelectedCategory1\" value=\"" + id.ToString() + "\" />";
                return Content(hidden + "<div class=\"buttonDiv\">&nbsp;&nbsp;<span style=\"display: block; margin-bottom:20px; font-size: 12px; font-weight: bold;\">Kategori Seçiminiz Başarıyla Tamamlanmıştır.</span>&nbsp;<button type=\"submit\" class=\"postButton\">Devam</button></div>");
            }

            return View("CategoryParentViewSecond", items);
        }

        public ActionResult AllIndex()
        {
            PAGEID = PermissionPage.KategoriYonetimi;
            return View(new List<TreeViewNode>().AsEnumerable());

        }

        public ActionResult AllIndexHelp()
        {
            PAGEID = PermissionPage.Yardim;
            return View(new List<TreeViewNode>().AsEnumerable());

        }

        [HttpPost]
        public ActionResult TreeLoad(int id, string groupId)
        {

            ICollection<CategoryModel> items = null;

            var dataCategory = new Data.Category();
            items = CacheUtilities.CategoryAllRows().AsCollection<CategoryModel>();

            IEnumerable<CategoryModel> model = null;
            if (groupId == null)
            {
                model = items.Where(c => c.CategoryParentId == id).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder);
            }
            else
            {
                model = items.Where(c => c.CategoryParentId == id).OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder);
            }
            return View("ChildrenItems", model);
        }

        public JsonResult GetNodesHelp(string root)
        {
            try
            {
                var dataCategory = new Data.Category();

                var items = dataCategory.GetCategoryParentByCategoryId((root == "source" ? 0 : root.ToInt32()), (byte)MainCategoryType.Yardim).AsCollection<CategoryModel>().OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder);
                List<TreeViewNode> nodes = new List<TreeViewNode>();

                foreach (var item in items)
                {
                    TreeViewNode node = null;
                    if (root != "source")
                    {
                        node = new TreeViewNode
                        {
                            id = item.CategoryId.ToString(),
                            text = item.CategoryName,
                            classes = item.IsParent > 0 ? "folder" : "file",
                            rowclasses = GetTypeClass(item.CategoryType),
                            hasChildren = item.IsParent > 0,
                            expanded = false
                        };

                        node.tool = string.Format(@"<div class=""column sort"">{3}</div><div class=column>{0}</div><a parentid=""{1}""  treeid=""#{1}"" class=""column {4}"">{5}</a><a class=""column edit"" categoryid=""{1}"" href=""#{1}"">Düzenle  </a> <a class="" column content"" href=""EditContent/{1}"">İçerik</a><a class=column onclick=""return Delete('#{1}',{1});"" href=""#"">{2}</a>", ((CategoryType_tr)item.CategoryType).ToString("G").Replace("_", " "), item.CategoryId, "Sil", item.CategoryOrder, item.CategoryType < 3 || item.CategoryType == 6 ? "categoryButton" : "categoryButton2", item.CategoryType == 5 ? "&nbsp;" : "Alt Kategori Ekle");

                    }
                    else
                    {
                        node = new TreeViewNode
                        {
                            id = item.CategoryId.ToString(),
                            text = item.CategoryName,
                            classes = item.IsParent > 0 ? "folder" : "file",
                            hasChildren = item.IsParent > 0,
                            rowclasses = GetTypeClass(item.CategoryType),
                            expanded = false
                        };
                        string seoVar = "";
                        if (!string.IsNullOrEmpty(item.Description) && !string.IsNullOrEmpty(item.Keywords) &&
                            !string.IsNullOrEmpty(item.Title))
                            seoVar = "<b style='color: red'>(S)</b>";


                        node.tool = string.Format(@"<div class=""column sort"">{4}</div><div class=column>{0}</div><a parentid=""{1}"" treeid=""#{1}"" class=""column sector""  href='#'>Alt Kategori Ekle</a><a class=""column edit"" categoryid=""{1}"" href=""#{1}"">Düzenle  </a> <a class=column onclick=""return Delete('#{1}',{1});"" href=""#"">{3}</a>", "Ana Kategori", item.CategoryId, item.CategoryParentId, "Sil", item.CategoryOrder, seoVar);

                    }
                    nodes.Add(node);
                }

                return Json(nodes.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }

        public JsonResult GetNodes(string root)
        {
            try
            {
                var dataCategory = new Data.Category();

                var items = dataCategory.GetCategoryParentByCategoryId((root == "source" ? 0 : root.ToInt32()), (byte)MainCategoryType.Ana_Kategori).AsCollection<CategoryModel>().OrderBy(c => c.CategoryName).OrderBy(c => c.CategoryOrder);
                TempData["mtc2"] = TempData["mtc"];
                ViewData["id"] = TempData["mtc"];
                List<TreeViewNode> nodes = new List<TreeViewNode>();

                foreach (var item in items)
                {
                    TreeViewNode node = null;
                    if (root != "source")
                    {
                        node = new TreeViewNode
                        {
                            id = item.CategoryId.ToString(),
                            text = item.CategoryName,
                            classes = item.IsParent > 0 ? "folder" : "file",
                            rowclasses = GetTypeClass(item.CategoryType),
                            hasChildren = item.IsParent > 0,
                            expanded = false
                        };
                        string seoVar = "";
                        if (!string.IsNullOrEmpty(item.Description) && !string.IsNullOrEmpty(item.Keywords) &&
                            !string.IsNullOrEmpty(item.Title))
                            seoVar = "<b style='color: red'>(S)</b>";

                        string seoContentLink = @"<a class=""column"" target=""_blank"" href=""/SeoDefinition/Edit?entityId=" + item.CategoryId + "&entityTypeId=1" + "\"" + ">İçerik Ekle</a> ";
                        string seoStoreContentLink = @"<a class=""column"" target=""_blank"" href=""/SeoDefinition/Edit?entityId=" + item.CategoryId + "&entityTypeId=3" + "\"" + ">F. İçerik Ekle</a> ";
                        string excelLink = @"<a  target=""_blank"" href=""/Category/ExportSubCategories/{1}"">E</a>";

                        string iconLink = @"<a class=""column"" href=""/Category/addImages?categoryId=" + item.CategoryId + "" + "\"" + ">Görsel Ekle</a>";
                        node.tool = string.Format(@"<div class=""column sort"">{3}</div><div class=column>{0}</div>
                                    <a parentid=""{1}"" style='width:100px!important;'  treeid=""#{1}""class=""column {4}"">{5}</a>
                                    <a class=""column edit"" categoryid=""{1}"" href=""#{1}"">Düzenle {6}</a>
                                    <a class=column onclick=""return Delete('#{1}',{1});"" href=""#"">{2}</a>

                                <a class=column href=""/Banner/Edit/" + item.CategoryId.ToString() + "\"" + ">Banner Ekle</a>" + excelLink + seoContentLink + iconLink + seoStoreContentLink,
                                                 ((CategoryType_tr)item.CategoryType).ToString("G").Replace("_", " "), item.CategoryId, "Sil", item.CategoryOrder, item.CategoryType < 3 || item.CategoryType == 6 ? "categoryButton" : "categoryButton2", item.CategoryType == 5 ? "&nbsp;" : "Alt Kategori Ekle", seoVar);

                    }
                    else
                    {
                        node = new TreeViewNode
                        {
                            id = item.CategoryId.ToString(),
                            text = item.CategoryName,
                            classes = item.IsParent > 0 ? "folder" : "file",
                            hasChildren = item.IsParent > 0,
                            rowclasses = GetTypeClass(item.CategoryType),
                            expanded = false
                        };
                        string seoVar = "";
                        if (!string.IsNullOrEmpty(item.Description) && !string.IsNullOrEmpty(item.Keywords) &&
                            !string.IsNullOrEmpty(item.Title))
                            seoVar = "<b style='color: red'>(S)</b>";

                        string IconLink = @"<a class=""column"" href=""/Category/addImages?categoryId=" + item.CategoryId + "" + "\"" + ">Görsel Ekle</a>";
                        string seoContentLink = @"<a class=""column"" target=""_blank"" href=""/SeoDefinition/Edit?entityId=" + item.CategoryId + "&entityTypeId=1" + "\"" + ">İçerik Ekle</a> ";
                        string seoStoreContentLink = @"<a class=""column"" target=""_blank"" href=""/SeoDefinition/Edit?entityId=" + item.CategoryId + "&entityTypeId=3" + "\"" + ">F.İçerik Ekle</a> ";

                        node.tool = string.Format(@"<div class=""column sort"">{4}</div><div class=column>{0}</div>
                                        <a parentid=""{1}"" style='width:100px!important;' treeid=""#{1}"" class=""column sector""  href='#'>Alt Kategori Ekle</a>
                                        <a class=""column edit"" categoryid=""{1}"" href=""#{1}"">Düzenle {5}</a>
                                        <a class=column style='width:50px!important!' onclick=""return Delete('#{1}',{1});"" href=""#"">{3}</a>
                                        <a class=column href=""/Banner/Edit/" + item.CategoryId.ToString() + "\"" + ">Banner Ekle</a>" + seoContentLink + IconLink + seoStoreContentLink, "Sektör", item.CategoryId, item.CategoryParentId, "Sil", item.CategoryOrder, seoVar);

                    }
                    nodes.Add(node);
                }

                return Json(nodes.ToArray(), JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                throw;
            }
        }


        private static string GetTypeClass(byte typeId)
        {
            switch (typeId)
            {
                case 5:
                    return "model";
                case 4:
                    return "seri";
                case 3:
                    return "marka";
                default:
                    return null;
            }
        }

        [HttpPost]
        public ActionResult UpdateMove(int sourceId, int parentId, byte group)
        {

            try
            {
                if (sourceId == parentId)
                {
                    return Json(false);
                }

                //if (selected1.CategoryType == (byte)CategoryType.Brand && selected2.CategoryType == (byte)CategoryType.Model)
                //{
                //    ViewData["selecteddone"] = "Uyumsuz Taşıma Hatası";
                //    return View();
                //}

                //var category = new Classes.Category();
                var category = new global::MakinaTurkiye.Entities.Tables.Catalog.Category();

                //var parentCategory = new Classes.Category();
                //var parentCategory = _categoryService.GetCategoryByCategoryId(parentId);
                var parentCategory = new global::MakinaTurkiye.Entities.Tables.Catalog.Category();
                parentCategory = _categoryService.GetCategoryByCategoryId(parentId);
                if (parentCategory != null)
                {
                    category = _categoryService.GetCategoryByCategoryId(sourceId);
                    if (category != null)
                    {
                        if (
                            (category.CategoryType == (byte)CategoryType.ProductGroup && (parentCategory.CategoryType == (byte)CategoryType.ProductGroup || parentCategory.CategoryType == 0))
                            ||
                            (category.CategoryType == 1 &&
                             (parentCategory.CategoryType == 1 || parentCategory.CategoryType == (byte)CategoryType.ProductGroup || parentCategory.CategoryType == 0))
                            ||
                            (category.CategoryType == (byte)CategoryType.Brand && parentCategory.CategoryType == 1)
                            ||
                            (category.CategoryType == (byte)CategoryType.Series &&
                             parentCategory.CategoryType == (byte)CategoryType.Brand)
                            ||
                            (category.CategoryType == (byte)CategoryType.Model &&
                             (parentCategory.CategoryType == (byte)CategoryType.Series ||
                              parentCategory.CategoryType == (byte)CategoryType.Brand) || category.CategoryType == 7)

                            )
                        {
                            //Herşey uygun
                        }
                        else
                        {
                            return Json(new { success = false, responseText = "Hatalı Taşıma." }, JsonRequestBehavior.AllowGet);
                        }

                        if (category.CategoryType == 1 && parentCategory.CategoryType == 0)
                        {
                            //var alt = entities.Categories.Where(k => k.CategoryParentId == category.CategoryId);
                            //if (alt.Any(k => k.CategoryType == (byte)CategoryType.Brand))
                            //{
                            //    return Json(new { success = false, responseText = "Altında Marka olan kategori ürün grubu yapılamaz." }, JsonRequestBehavior.AllowGet);
                            //}
                            category.CategoryType = (byte)CategoryType.ProductGroup;// kategori sektör altına taşınacaksa kategorinin tipini ürün grubu yap
                        }
                        if (category.CategoryType == (byte)CategoryType.ProductGroup && parentCategory.CategoryType == (byte)CategoryType.ProductGroup)
                        {
                            category.CategoryType = 1;// ürün grubu başka bir ürün grubu altına taşınacaksa kategorinin tipini kategori yap.
                        }

                        //Update ProductCount Defualt Top Categories
                        var defaultTopCategories = _categoryService.GetSPTopCategories(category.CategoryId);
                        int olCategoryParentId = category.CategoryParentId.Value;
                        int productCount = 0;
                        if (category.ProductCount.HasValue)
                            productCount = category.ProductCount.Value;
                        foreach (var itemCat in defaultTopCategories)
                        {
                            var cat = _categoryService.GetCategoryByCategoryId(itemCat.CategoryId);

                            cat.ProductCount = cat.ProductCount - productCount;
                            _categoryService.UpdateCategory(cat);
                        }

                        category.CategoryParentId = parentCategory.CategoryId;

                        _categoryService.UpdateCategory(category);
                        var topCategoriesNames = _categoryService.GetSPTopCategories(category.CategoryId).Select(x => x.CategoryName).ToList();



                        category.CategoryPath = String.Join(" - ", topCategoriesNames);
                        category.CategoryPathUrl = GetCategoryPathUrl(category);
                        _categoryService.UpdateCategory(category);


                        entities.SP_DeleteCategoryBottomByKeyCategoryId(olCategoryParentId);
                        entities.SP_DeleteCategoryBottomByKeyCategoryId(category.CategoryParentId);

                        //var mainBottomCategories = _categoryService.GetSPBottomCategories(olCategoryParentId);
                        //var oldCategoryParent = _categoryService.GetCategoryByCategoryId(olCategoryParentId);
                        //string filterable = oldCategoryParent.FilterableCategoryIds;
                        //string[] filtrableIds = filterable.Split(',');
                        //string newFilterableId = "";
                        //int counter = 0;
                        //mainBottomCategories.Remove(mainBottomCategories.First());

                        //foreach (var item in mainBottomCategories)
                        //{
                        //    var catBottom = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                        //    newFilterableId = newFilterableId+ "," +item.CategoryId + "~";
                        //    if (filterable.Contains(item.CategoryId.ToString()) && filtrableIds[counter].IndexOf("~")>0)
                        //        newFilterableId = newFilterableId+ filtrableIds[counter].Split('~')[1];
                        //    else
                        //        newFilterableId = newFilterableId+ catBottom.ProductCount.ToString();
                        //    counter++;
                        //}
                        //newFilterableId = newFilterableId.Substring(1, newFilterableId.Length);

                        var newCategoriesTop = _categoryService.GetSPTopCategories(category.CategoryId);
                        foreach (var item in newCategoriesTop)
                        {
                            var cat = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                            cat.ProductCount = cat.ProductCount + productCount;
                            _categoryService.UpdateCategory(cat);
                        }


                        var bottomCategories = _categoryService.GetSPBottomCategories(category.CategoryId);
                        foreach (var item in bottomCategories)
                        {
                            var categoryBottom = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                            var topCategoriesNames1 = _categoryService.GetSPTopCategories(item.CategoryId).Select(x => x.CategoryContentTitle).ToList();

                            categoryBottom.CategoryPath = String.Join(" - ", topCategoriesNames1);
                            categoryBottom.CategoryPathUrl = GetCategoryPathUrl(categoryBottom);

                            _categoryService.UpdateCategory(categoryBottom);

                        }

                        string containid = sourceId.ToString();
                        string treeName = String.Empty;
                        var q = (from cus in entities.Products
                                 where cus.CategoryTreeName.Contains(containid)
                                 select cus).ToList();
                        //var categorytreeupdate = entities.Products.Where("it.CategoryTreeName LIKE @sourceId", new System.Data.Objects.ObjectParameter("sourceId", sourceId));
                        var dataCategory = new Data.Category();
                        List<int> treeList;
                        foreach (var item in q)
                        {
                            if (item.ModelId != null)
                            {
                                var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.ModelId.ToInt32()).AsCollection<CategoryModel>();
                                var ids = (from c in treeItems select c.CategoryId);
                                var categoryNames = (from c in treeItems select c.CategoryName);

                                treeList = ids.ToList();
                            }
                            else if (item.BrandId != null)
                            {
                                var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.BrandId.ToInt32()).AsCollection<CategoryModel>();
                                var ids = (from c in treeItems select c.CategoryId);
                                var categoryNames = (from c in treeItems select c.CategoryName);

                                treeList = ids.ToList();

                            }
                            else if (item.SeriesId != null)
                            {
                                var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.SeriesId.ToInt32()).AsCollection<CategoryModel>();
                                var ids = (from c in treeItems select c.CategoryId);
                                var categoryNames = (from c in treeItems select c.CategoryName);

                                treeList = ids.ToList();
                            }
                            else
                            {
                                var treeItems = dataCategory.CategoryTopCategoriesByCategoryId(item.CategoryId.ToInt32()).AsCollection<CategoryModel>();
                                var ids = (from c in treeItems select c.CategoryId);
                                var categoryNames = (from c in treeItems select c.CategoryName);

                                treeList = ids.ToList();

                            }

                            treeName = String.Empty;
                            for (int i = 0; i < treeList.Count; i++)
                            {
                                treeName += treeList[i].ToString() + ".";
                            }
                            item.CategoryTreeName = treeName;

                            if (category.CategoryType == (byte)CategoryType.Brand)
                            {

                                item.CategoryId = parentCategory.CategoryId;
                            }
                            else if (category.CategoryType == (byte)CategoryType.Model && parentCategory.CategoryType == (byte)CategoryType.Brand)
                            {
                                item.BrandId = parentCategory.CategoryId;
                                if (parentCategory.CategoryParentId != null)
                                {
                                    var parentCategoryBrand = _categoryService.GetCategoryByCategoryId(parentCategory.CategoryParentId.Value);
                                    if (parentCategoryBrand != null)
                                    {
                                        item.CategoryId = parentCategoryBrand.CategoryId;
                                    }
                                }
                            }
                            else if (category.CategoryType == (byte)CategoryType.Series && parentCategory.CategoryType == (byte)CategoryType.Model)
                            {
                                item.ModelId = parentCategory.CategoryId;
                            }

                        }

                        entities.SaveChanges();

                        category.CategoryPathUrl = GetCategoryPathUrl(category);
                        _categoryService.UpdateCategory(category);
                        foreach (var item in q)
                        {
                            try
                            {
                                entities.CheckProductSearch(item.ProductId);
                            }
                            catch
                            {

                            }
                        }

                    }
                }
            }
            catch (Exception ex)
            {
                return Json(new { success = false, responseText = "Hatalı Taşıma." }, JsonRequestBehavior.AllowGet);
            }
            return Json(new { success = true, responseText = "İşlem Tamam." }, JsonRequestBehavior.AllowGet);
        }
        private string GetCategoryPathUrl(Category category)
        {
            var categoryUrlName = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
            string categoryUrl = "";
            if (category.CategoryType == (byte)CategoryType.Sector)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.ProductGroup)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Category)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Brand)
            {
                var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetCategoryUrl(categoryParent.CategoryId, categoryUrlName, category.CategoryId, category.CategoryName);
            }
            else if (category.CategoryType == (byte)CategoryType.Model)
            {
                var categoryBrand = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetModelUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName, categoryParent.CategoryId);
            }
            else if (category.CategoryType == (byte)CategoryType.Series)
            {
                var categoryModel = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryBrand = _categoryService.GetCategoryByCategoryId(categoryModel.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetSerieUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName);
            }
            return categoryUrl;
        }
        private string GetCategoryPathUrl(MakinaTurkiye.Management.Models.Entities.Category category)
        {
            var categoryUrlName = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName;
            string categoryUrl = "";
            if (category.CategoryType == (byte)CategoryType.Sector)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.ProductGroup)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Category)
            {
                categoryUrl = UrlBuilder.GetCategoryUrl(category.CategoryId, categoryUrlName, null, string.Empty);
            }
            else if (category.CategoryType == (byte)CategoryType.Brand)
            {
                var categoryParent = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetCategoryUrl(categoryParent.CategoryId, categoryUrlName, category.CategoryId, category.CategoryName);
            }
            else if (category.CategoryType == (byte)CategoryType.Model)
            {
                var categoryBrand = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetModelUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName, categoryParent.CategoryId);
            }
            else if (category.CategoryType == (byte)CategoryType.Series)
            {
                var categoryModel = _categoryService.GetCategoryByCategoryId(category.CategoryParentId.Value);
                var categoryBrand = _categoryService.GetCategoryByCategoryId(categoryModel.CategoryParentId.Value);
                var categoryParent = _categoryService.GetCategoryByCategoryId(categoryBrand.CategoryParentId.Value);
                categoryUrlName = !string.IsNullOrEmpty(categoryParent.CategoryContentTitle) ? categoryParent.CategoryContentTitle : categoryParent.CategoryName;
                categoryUrl = UrlBuilder.GetSerieUrl(category.CategoryId, category.CategoryName, categoryBrand.CategoryName, categoryUrlName);
            }
            return categoryUrl;
        }
        public string LimitText(string text, short limit)
        {
            if (!string.IsNullOrEmpty(text))
            {
                if (text.Length > limit)
                {
                    return text.Substring(0, limit);
                }
                return text;
            }
            return "";
        }


        [HttpPost]
        public ActionResult InsertRow(CategoryModel model)
        {

            var parentCategory = new Classes.Category();
            try
            {
                var category = new Models.Entities.Category
                {
                    Active = /*model.Active*/ true,
                    CategoryName = model.CategoryName,
                    CategoryParentId = model.CategoryParentId,
                    CategoryOrder = model.CategoryOrder,
                    CategoryType = model.CategoryType,
                    RecordDate = DateTime.Now,
                    RecordCreatorId = 99,
                    MainCategoryType = (byte)MainCategoryType.Ana_Kategori,
                    LastUpdateDate = DateTime.Now,
                    LastUpdaterId = 99,
                    ProductCount = 0,
                    Title = "",
                    Keywords = "",
                    Description = "",
                    CategoryContentTitle = !string.IsNullOrEmpty(model.CategoryContentTitle) ? model.CategoryContentTitle : model.CategoryName

                };

                entities.Categories.AddObject(category);
                entities.SaveChanges();
                category.CategoryPathUrl = GetCategoryPathUrl(category);
                var topCategories = _categoryService.GetSPTopCategories(category.CategoryId);
                var topCategoriesNames = topCategories.Select(x => x.CategoryContentTitle != null).ToList();

                category.CategoryPath = String.Join(" - ", topCategoriesNames);

                entities.SaveChanges();

                return Json(new
                {
                    id = category.CategoryId,
                    tool = string.Format("<div class=\"column sort\">{3}</div><div class=column>{0}</div><a parentid=\"{1}\"  treeid=\"#{1}\" class=\"column {4}\">{5}</a><a class=\"column edit\" categoryid=\"{1}\" href=\"#{1}\">Düzenle</a><a class=column onclick=\"return Delete('#{1}',{1});\" href=\"#\">{2}</a>", ((CategoryType_tr)category.CategoryType).ToString("G").Replace("_", " "), category.CategoryId, "Sil", category.CategoryOrder, category.CategoryType < 3 || category.CategoryType == 6 ? "categoryButton" : "categoryButton2", category.CategoryType == 5 ? "" : "Alt Kategori Ekle")
                });
            }
            catch
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult InsertRowHelp(CategoryModel model)
        {

            var parentCategory = new Classes.Category();
            try
            {
                var category = new Models.Entities.Category
                {
                    Active = /*model.Active*/ true,
                    CategoryName = model.CategoryName,
                    CategoryParentId = model.CategoryParentId,
                    CategoryOrder = model.CategoryOrder,
                    CategoryType = model.CategoryType,
                    RecordDate = DateTime.Now,
                    RecordCreatorId = 99,
                    Content = model.Content,
                    MainCategoryType = (byte)MainCategoryType.Yardim,
                    LastUpdateDate = DateTime.Now,
                    LastUpdaterId = 99,
                    ProductCount = 0,
                    Title = "",
                    Keywords = "",
                    Description = "",
                    CategoryContentTitle = model.CategoryName
                };

                entities.Categories.AddObject(category);
                entities.SaveChanges();

                return Json(new
                {
                    id = category.CategoryId,
                    tool = string.Format("<div class=\"column sort\">{3}</div><div class=column>{0}</div><a parentid=\"{1}\"  treeid=\"#{1}\" class=\"column {4}\">{5}</a><a class=\"column edit\" categoryid=\"{1}\" href=\"#{1}\">Düzenle</a><a  class=\" column content\" href=\"EditContent/{1}\">İçerik</a><a class=column onclick=\"return Delete('#{1}',{1});\" href=\"#\">{2}</a>", ((CategoryType_tr)category.CategoryType).ToString("G").Replace("_", " "), category.CategoryId, "Sil", category.CategoryOrder, category.CategoryType < 3 || category.CategoryType == 6 ? "categoryButton" : "categoryButton2", category.CategoryType == 5 ? "" : "Alt Kategori Ekle")
                });
            }
            catch
            {
                throw;
            }
        }

        public ActionResult EditContent(int id)
        {
            var hc2 = (from c in entities.Categories
                       where c.CategoryId == id
                       select c).SingleOrDefault();
            ArrayList dizi = new ArrayList();
            int? catid = id;
            string a = "";
            if (hc2.CategoryParentId != null)
            {
                for (int i = 0; i < 20; i++)
                {
                    if (i == 0)
                    {
                        catid = hc2.CategoryParentId;
                        dizi.Add(hc2.CategoryName);
                    }



                    if (catid != null)
                    {
                        var hc = (from c in entities.Categories
                                  where c.CategoryId == catid
                                  select c).SingleOrDefault();
                        catid = hc.CategoryParentId;
                        dizi.Add(hc.CategoryName);
                    }
                    else
                    {
                        i = 20;
                    }


                }
                dizi.Reverse();
                foreach (string s in dizi)
                {
                    a += s + " / ";
                }

                ViewData["text"] = a;
            }
            else
                ViewData["text"] = "Ana Kategori";
            return View(hc2);
        }

        [ValidateInput(false)]
        [HttpPost]
        public ActionResult EditContent(int id, FormCollection collection)
        {
            NeoSistem.MakinaTurkiye.Management.Models.Entities.Category hc = (from c in entities.Categories
                                                                              where c.CategoryId == id
                                                                              select c).SingleOrDefault();

            if (ModelState.IsValid)
            {
                UpdateModel(hc);
                entities.SaveChanges();
            }

            return RedirectToAction("AllIndexHelp");

        }

        [HttpPost]
        public JsonResult EditCategory(int id)
        {
            var category = entities.Categories.First(x => x.CategoryId == id);
            string categoryUrl = "";

            if (!string.IsNullOrEmpty(category.CategoryContentTitle))
            {

                categoryUrl = UrlBuilder.ToUrl(category.CategoryContentTitle);
            }
            else
                categoryUrl = UrlBuilder.ToUrl(category.CategoryName);


            return Json(new
            {
                CategoryParentId = category.CategoryParentId,
                CategoryName = category.CategoryName,
                CategoryOrder = category.CategoryOrder,
                Active = category.Active,
                CategoryId = category.CategoryId,
                Title = string.IsNullOrEmpty(category.Title) ? "" : category.Title,
                Description = string.IsNullOrEmpty(category.Description) ? "" : category.Description,
                Keywords = string.IsNullOrEmpty(category.Keywords) ? "" : category.Keywords,
                CategoryContentTitle = string.IsNullOrEmpty(category.CategoryContentTitle) ? "" : category.CategoryContentTitle,
                StorePageTitle = string.IsNullOrEmpty(category.StorePageTitle) ? "" : category.StorePageTitle,
                CategoryUrl = categoryUrl,
                BaseMenuOrder = category.BaseMenuOrder
            });
        }

        [HttpPost]
        public JsonResult EditPost(CategoryModel model)
        {
            //var category = new Classes.Category();

            var category = entities.Categories.FirstOrDefault(x => x.CategoryId == model.CategoryId);
            if (category != null)
            {


                category.CategoryOrder = model.CategoryOrder;
                //category.Active = model.Active;
                category.LastUpdateDate = DateTime.Now;
                category.LastUpdaterId = 99;
                category.Title = model.Title;
                category.Description = model.Description;
                category.Keywords = model.Keywords;
                category.CategoryContentTitle = model.CategoryContentTitle;
                category.CategoryName = model.CategoryName;
                category.StorePageTitle = model.StorePageTitle;
                category.CategoryPathUrl = GetCategoryPathUrl(category);
                if (model.BaseMenuOrder.HasValue)
                    category.BaseMenuOrder = model.BaseMenuOrder.Value;
                if (category.CategoryType == (byte)CategoryType.Model || category.CategoryType == (byte)CategoryType.Brand)
                {
                    category.CategoryContentTitle = category.CategoryName;
                }
                entities.SaveChanges();
                var topCategories = _categoryService.GetSPTopCategories(category.CategoryId);
                var topCategoriesNames = topCategories.Select(x => x.CategoryName).ToList();

                category.CategoryPath = String.Join(" - ", topCategoriesNames);
                entities.SaveChanges();

                _productService.SPUpdateProductSearchCategoriesByCategoryId(model.CategoryId);

            }
            string categoryUrl = "";
            if (!string.IsNullOrEmpty(category.CategoryContentTitle))
            {
                categoryUrl = UrlBuilder.ToUrl(category.CategoryContentTitle);
            }
            else
                categoryUrl = UrlBuilder.ToUrl(category.CategoryName);


            return Json(new
            {
                CategoryId = category.CategoryId,
                CategoryName = category.CategoryName,
                CategoryOrder = category.CategoryOrder,
                Active = category.Active,
                Title = category.Title,
                Description = category.Description,
                Keywords = category.Keywords,
                StorePageTitle = category.StorePageTitle,
                CategoyUrl = categoryUrl
            });
        }

        [HttpPost]
        public JsonResult Delete(int cId)
        {
            var category = new Data.Category();
            var Dt = category.CategoryGetSectorItemsByCategoryParent(cId);

            var category1 = _categoryService.GetCategoryByCategoryId(cId);
            if (category1.CategoryType == (byte)CategoryType.Model || category1.CategoryType == (byte)CategoryType.Series)
            {
                if (category1.CategoryType == (byte)CategoryType.Series)
                {
                    if (entities.Products.Where(x => x.SeriesId == cId).Count() > 0)
                    {
                        return Json(false);
                    }


                }
                else
                {
                    if (entities.Products.Where(x => x.ModelId == cId).Count() > 0)
                    {
                        return Json(false);
                    }
                }
            }
            if (Dt.Rows.Count > 0)
            {
                return Json(false);
            }
            else
            {
                int row = category.Delete(cId);
                return Json(row <= 0);
            }
        }
        public ActionResult addImages(int categoryId)
        {
            var category = _categoryService.GetCategoryByCategoryId(categoryId);
            CategoryImageModel model = new CategoryImageModel();
            IconModel iconModel = new IconModel();

            iconModel.IconUrl = AppSettings.CategoryIconImageFolder + category.CategoryIcon;
            model.IconModel = iconModel;
            model.CategoryName = category.CategoryName;
            model.CategoryId = categoryId;
            var banners = _bannerService.GetBannersByCategoryId(categoryId).Where(x => x.BannerType == (byte)BannerType.CategorySlider).ToList();
            foreach (var item in banners)
            {
                item.BannerResource = AppSettings.CategoryBannerImagesFolder + item.BannerResource;
                model.BannerItems.Add(item);
            }
            if (!string.IsNullOrEmpty(category.HomeImagePath))
                model.HomePageImagePath = AppSettings.CategoryHomePageImageFolder + category.HomeImagePath;
            return View(model);
        }
        [HttpPost]
        public ActionResult addImages(CategoryImageModel model)
        {
            foreach (var inputTagName in Request.Files)
            {
                if (Request.Files[inputTagName.ToString()].ContentLength > 0)
                {
                    if (inputTagName.ToString() == "IconUrl")
                    {
                        var oldIcon = entities.Categories.SingleOrDefault(c => c.CategoryId == model.CategoryId);
                        if (oldIcon != null)
                        {
                            FileHelpers.Delete(AppSettings.CategoryIconImageFolder + oldIcon.CategoryIcon);
                        }
                        string fileName = FileHelpers.Upload(AppSettings.CategoryIconImageFolder, Request.Files[inputTagName.ToString()]);
                        oldIcon.CategoryIcon = fileName;
                        model.IconModel.IconUrl = AppSettings.CategoryIconImageFolder + fileName;
                        entities.SaveChanges();
                    }
                    else if (inputTagName.ToString() == "HomePageImagePath")
                    {
                        var category = entities.Categories.SingleOrDefault(c => c.CategoryId == model.CategoryId);
                        if (category != null)
                        {
                            FileHelpers.Delete(AppSettings.CategoryIconImageFolder + category.HomeImagePath);
                        }
                        string fileName = FileHelpers.Upload(AppSettings.CategoryHomePageImageFolder, Request.Files[inputTagName.ToString()]);
                        category.HomeImagePath = fileName;
                        model.HomePageImagePath = AppSettings.CategoryHomePageImageFolder + fileName;

                        entities.SaveChanges();
                    }
                }
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult AddBanner()
        {
            int categoryId = Convert.ToInt32(Request.Form["CategoryId"]);
            foreach (var inputTagName in Request.Files)
            {
                if (Request.Files[inputTagName.ToString()].ContentLength > 0)
                {

                    var oldBanner = entities.Banners.SingleOrDefault(c => c.CategoryId == null && c.BannerType == (byte)BannerType.ProductSideLeft);
                    //if (oldBanner != null)
                    //{
                    //    entities.Banners.DeleteObject(oldBanner);
                    //    FileHelpers.Delete(AppSettings.BannerImagesThumbFolder + oldBanner.BannerResource);
                    //    FileHelpers.Delete(AppSettings.BannerImagesFolder + oldBanner.BannerResource);
                    //    FileHelpers.Delete(AppSettings.BannerFlashFolder + oldBanner.BannerResource);
                    //    FileHelpers.Delete(AppSettings.BannerGifFolder + oldBanner.BannerResource);
                    //    entities.SaveChanges();
                    //}

                    var thumns = new Dictionary<string, string>();

                    string fileName = FileHelpers.ImageResize(AppSettings.CategoryBannerImagesFolder, Request.Files[inputTagName.ToString()], thumns, true);

                    var banner = new global::MakinaTurkiye.Entities.Tables.Common.Banner
                    {
                        BannerResource = fileName,

                        BannerType = (byte)BannerType.CategorySlider,
                        BannerLink = Request.Form["Link"],
                        BannerDescription = Request.Form["Description"],
                        BannerOrder = Request.Form["Order"],
                        CategoryId = categoryId,
                        BannerAltTag = Request.Form["BannerAltTag"],
                        BannerImageType = Convert.ToInt16(Request.Form.Get("ImageType"))
                    };

                    _bannerService.InsertBanner(banner);
                }

            }

            return RedirectToAction("addImages", new { categoryId = categoryId });

        }
        [HttpPost]
        public JsonResult BannerImageDelete(int bannerId)
        {
            var banner = _bannerService.GetBannerByBannerId(bannerId);
            if (banner != null)
            {
                FileHelpers.Delete(AppSettings.CategoryBannerImagesFolder + banner.BannerResource);
                _bannerService.DeleteBanner(banner);
            }

            return Json(true, JsonRequestBehavior.AllowGet);
        }
        [HttpGet]
        public ActionResult EditCategoryQuestion(int id, string opr)
        {
            CategoryPropertieViewModel model = new CategoryPropertieViewModel();
            model.CategoryId = id;
            if (opr == "error")
                ModelState.AddModelError("PropertieId", "Lütfen soru tipi seçiniz");
            var properties = _categoryPropertieService.GetAllProperties();
            model.Questions.Add(new SelectListItem { Text = "Seçin", Value = "0" });

            foreach (var item in properties)
            {
                model.Questions.Add(new SelectListItem { Text = item.PropertieName, Value = item.PropertieId.ToString() });

            }

            var propertiesForCat = _categoryPropertieService.GetPropertieByCategoryId(id);
            foreach (var item in propertiesForCat)
            {
                model.PropertieModels.Add(new PropertieModel { PropertieId = item.PropertieId, CategoryPropertieId = item.CategoryPropertieId, PropertieName = item.PropertieName, PropertieType = item.PropertieType });
            }

            return View(model);
        }
        [HttpPost]
        public ActionResult EditCategoryQuestion(CategoryPropertieViewModel model)
        {
            if (model.PropertieId == 0)
            {
                return RedirectToAction("EditCategoryQuestion", new { opr = "error", id = model.CategoryId });
            }
            else
            {
                var categoryPropertie = new global::MakinaTurkiye.Entities.Tables.Catalog.CategoryPropertie();
                categoryPropertie.CategoryId = model.CategoryId;
                categoryPropertie.PropertieId = model.PropertieId;
                _categoryPropertieService.InsertCategoryPropertie(categoryPropertie);
                return RedirectToAction("EditCategoryQuestion", new { id = model.CategoryId });
            }

        }
        [HttpGet]
        public ActionResult QuestionDelete(int Id)
        {
            var categoryPropertie = _categoryPropertieService.GetCategoryPropertieByCategoryPropertieId(Id);
            int categoryId = categoryPropertie.CategoryId;
            _categoryPropertieService.DeleteCategoryPropertie(categoryPropertie);
            return RedirectToAction("EditCategoryQuestion", new { id = categoryId });
        }
        #region CategoryPlace

        [HttpPost]
        public string EditCategoryPlaceType(string id, string CategoryId, string status, bool isProduct)
        {
            int categoryId = Convert.ToInt32(CategoryId);
            byte categoryPlaceType = Convert.ToByte(id);
            var categoryPlace = _categoryPlaceChoiceService.GetCategoryPlaceChoices().Where(x => x.CategoryId == categoryId && x.CategoryPlaceType == categoryPlaceType).FirstOrDefault();
            if (categoryPlace != null)
            {
                if (status == "0")
                {
                    if (categoryPlace.CategoryPlaceType == (byte)CategoryPlaceType.HomeChoicesed)
                    {
                        var products = _productService.GetProductsForChoiced().Where(x => x.CategoryTreeName.Contains(categoryPlace.CategoryId.ToString()));
                        foreach (var item in products.ToList())
                        {
                            item.ChoicedForCategoryIndex = false;
                            _productService.UpdateProduct(item);

                        }
                    }
                    _categoryPlaceChoiceService.DeleteCategoryPlaceChoice(categoryPlace);
                }
            }
            else
            {
                if (status == "1")
                {
                    CategoryPlaceChoice categoryTypeChoice = new CategoryPlaceChoice();
                    categoryTypeChoice.CategoryId = categoryId;
                    categoryTypeChoice.CategoryPlaceType = categoryPlaceType;
                    categoryTypeChoice.IsProductCategory = isProduct;
                    _categoryPlaceChoiceService.InsertCategoryPlaceChoice(categoryTypeChoice);

                }
            }

            return "true";
        }

        public ActionResult CategoryPlaces()
        {
            var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoices();
            var model = new CategoryPlaceModel();
            foreach (var item in categoryPlaces)
            {
                var categoryItemModelForplace = new CategoryItemModelForPlace();
                if (item.IsProductCategory)
                {
                    if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeCenter)
                    {
                        categoryItemModelForplace.CategoryType = "Anasayfa Orta";
                    }
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeLeftSide)
                        categoryItemModelForplace.CategoryType = "Ansayfa Sol";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeChoicesed)
                        categoryItemModelForplace.CategoryType = "Anasayfa Seçilmiş";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.ProductGroup)
                        categoryItemModelForplace.CategoryType = "Ürün Grubu";
                }
                else
                {
                    if (item.CategoryPlaceType == (byte)HelpCategoryPlace.AccountHome)
                        categoryItemModelForplace.CategoryType = "Hesabım Anasayfa";
                    else if (item.CategoryPlaceType == (byte)HelpCategoryPlace.ForMember)
                        categoryItemModelForplace.CategoryType = "Üyeler İçin";
                    else if (item.CategoryPlaceType == (byte)HelpCategoryPlace.ForStore)
                        categoryItemModelForplace.CategoryType = "Firma İçin";
                    else if (item.CategoryPlaceType == (byte)HelpCategoryPlace.PersonalAccount)
                        categoryItemModelForplace.CategoryType = "Kulanıcı Anasayfa";
                    else if (item.CategoryPlaceType == (byte)HelpCategoryPlace.StoreLogoUpdate)
                        categoryItemModelForplace.CategoryType = "Logo Güncelleme";

                }
                categoryItemModelForplace.CategoryId = item.CategoryId;
                categoryItemModelForplace.CategoryName = item.Category.CategoryName;
                categoryItemModelForplace.Order = (item.Order == null) ? (byte)0 : Convert.ToByte(item.Order);
                categoryItemModelForplace.CategoryPlaceChoiceId = item.CategoryPlaceChoiceId;
                model.Categories.Add(categoryItemModelForplace);

            }
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteCategoryPlace(int id)
        {
            var categoryPlace = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceChoiceId(id);
            _categoryPlaceChoiceService.DeleteCategoryPlaceChoice(categoryPlace);
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult GetCategoryPlacesByCategoryType(int type)
        {
            var model = new CategoryPlaceModel();
            if (type == 0)
            {
                var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoices();
                foreach (var item in categoryPlaces)
                {
                    var categoryItemModelForplace = new CategoryItemModelForPlace();
                    if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeCenter)
                    {
                        categoryItemModelForplace.CategoryType = "Anasayfa Orta";
                    }
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeLeftSide)
                        categoryItemModelForplace.CategoryType = "Ansayfa Sol";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeChoicesed)
                        categoryItemModelForplace.CategoryType = "Anasayfa Seçilmiş";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.ProductGroup)
                        categoryItemModelForplace.CategoryType = "Ürün Grubu";
                    categoryItemModelForplace.CategoryId = item.CategoryId;
                    categoryItemModelForplace.CategoryName = item.Category.CategoryName;
                    categoryItemModelForplace.Order = (item.Order == null) ? (byte)0 : Convert.ToByte(item.Order);
                    categoryItemModelForplace.CategoryPlaceChoiceId = item.CategoryPlaceChoiceId;
                    model.Categories.Add(categoryItemModelForplace);

                }
            }
            else
            {
                var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct(type, true);

                foreach (var item in categoryPlaces.ToList())
                {
                    var categoryItemModelForplace = new CategoryItemModelForPlace();
                    if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeCenter)
                    {
                        categoryItemModelForplace.CategoryType = "Seçilen Alt Kategori";
                    }
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeLeftSide)
                        categoryItemModelForplace.CategoryType = "Önerilen Sayfa";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.HomeChoicesed)
                        categoryItemModelForplace.CategoryType = "Seçilen Kategori";
                    else if (item.CategoryPlaceType == (byte)CategoryPlaceType.ProductGroup)
                        categoryItemModelForplace.CategoryType = "Ürün Grubu";
                    categoryItemModelForplace.CategoryId = item.CategoryId;
                    categoryItemModelForplace.CategoryName = item.Category.CategoryName;
                    categoryItemModelForplace.Order = (item.Order == null) ? (byte)0 : Convert.ToByte(item.Order);
                    categoryItemModelForplace.CategoryPlaceChoiceId = item.CategoryPlaceChoiceId;
                    model.Categories.Add(categoryItemModelForplace);

                }
            }
            return View("CategoryPlaceList", model.Categories);

        }

        [HttpPost]
        public JsonResult EditCategoryPlaceOrder(int id, string order)
        {
            var categoryPlaceItem = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceChoiceId(id);
            categoryPlaceItem.Order = Convert.ToByte(order);
            _categoryPlaceChoiceService.UpdateCategoryPlaceChoice(categoryPlaceItem);
            return Json(true, JsonRequestBehavior.AllowGet);

        }

        [HttpPost]
        public ActionResult CategoryPlacePaging(int currentPage, int targetPage, int? categoryType)
        { //TODO devam edilecek
            int pageSize = 20;
            int totalPages = 0;
            int totalCount = 0;
            CategoryPlaceModel model = new CategoryPlaceModel();
            if (categoryType == 0)
            {
                var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoices();
                totalCount = categoryPlaces.ToList().Count;
            }
            else
            {
                int type = Convert.ToInt32(categoryType);
                var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoiceByCategoryPlaceTypeByIsProduct(type, true);
                totalCount = categoryPlaces.Count;

            }
            totalPages = Convert.ToInt32(Math.Ceiling((double)totalCount / (double)totalPages));
            int take = currentPage * pageSize - pageSize;
            if (categoryType == 0)
            {

                var takeCategoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoices().Skip(take).Take(pageSize);

            }
            else
            {

            }
            return View("CategoryPlaceList", model.Categories);
        }

        [HttpPost]
        public string GetCategoryPlace(int id, bool isProduct)
        {
            var categoryPlaces = _categoryPlaceChoiceService.GetCategoryPlaceChoicesByCategoryId(id).ToList().Where(x => x.IsProductCategory == isProduct);
            string result = "";
            if (isProduct)
            {

                string homeCenter = "";
                string homeLeftSide = "";
                string homeChoiced = "";
                string productGroup = "";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)CategoryPlaceType.HomeCenter))
                    homeCenter = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)CategoryPlaceType.HomeLeftSide))
                    homeLeftSide = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)CategoryPlaceType.HomeChoicesed))
                    homeChoiced = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)CategoryPlaceType.ProductGroup))
                    productGroup = "checked";


                result = string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType' onchange='PlaceTypeChange()' {1}/>Seçilen Alt Kategori", (byte)CategoryPlaceType.HomeCenter, homeCenter, id);
                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Önerilen Sayfa", (byte)CategoryPlaceType.HomeLeftSide, homeLeftSide, id);

                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Seçilen Kategori", (byte)CategoryPlaceType.HomeChoicesed, homeChoiced, id);

                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Ürün Grup", (byte)CategoryPlaceType.ProductGroup, productGroup, id);

            }
            else
            {
                string forStore = "";
                string forMember = "";
                string accountHome = "";
                string storeLogoUpdate = "";
                string personalAccount = "";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)HelpCategoryPlace.ForStore))
                    forStore = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)HelpCategoryPlace.ForMember))
                    forMember = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)HelpCategoryPlace.AccountHome))
                    accountHome = "checked";
                if (categoryPlaces.Any(x => x.CategoryPlaceType == (byte)HelpCategoryPlace.StoreLogoUpdate))
                    storeLogoUpdate = "checked";


                result = string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType' onchange='PlaceTypeChange()' {1}/>Satıcılar İçin", (byte)HelpCategoryPlace.ForStore, forStore, id);
                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Alıcılar İçin", (byte)HelpCategoryPlace.ForMember, forMember, id);

                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Hesabım Anasayfa", (byte)HelpCategoryPlace.AccountHome, accountHome, id);

                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Firma Logo Düzenle", (byte)HelpCategoryPlace.StoreLogoUpdate, storeLogoUpdate, id);

                result += string.Format("<input type='checkbox' CatId='{2}' name='CategoryPlace' hd='{0}' class='categoryPlaceType'  onchange='PlaceTypeChange()' {1}/>Üye Sayfası", (byte)HelpCategoryPlace.PersonalAccount, personalAccount, id);

            }

            return result;
        }

        #endregion

        public ActionResult BaseMenus()
        {
            var baseMenus = _baseMenuService.GetAllBaseMenu();
            BaseMenuModel model = new BaseMenuModel();
            foreach (var item in baseMenus)
            {
                var baseMenuItem = new BaseMenuItem
                {
                    Active = item.Active,
                    BaseMenuId = item.BaseMenuId,
                    BaseMenuName = item.BaseMenuName,
                    BaseMenuCategories = item.BaseMenuCategories.ToList(),
                    CreatedTime = item.CreatedDate,
                    Order = item.Order,
                    HomePageOrder = item.HomePageOrder
                };
                foreach (var image in item.BaseMenuImages)
                {
                    baseMenuItem.ImagePaths.Add(AppSettings.BaseMenuImageFolder + image.MenuImagePath);
                }
                model.BaseMenuItems.Add(baseMenuItem);

            }
            return View(model);
        }

        public ActionResult CreateBaseMenu()
        {
            var categories = _categoryService.GetMainCategories();
            BaseMenuCreateModel model = new BaseMenuCreateModel();
            foreach (var item in categories)
            {
                model.SectorCategories.Add(new SelectListItem { Text = item.CategoryContentTitle, Value = item.CategoryId.ToString() });
            }
            model.Active = true;
            return View(model);
        }

        [HttpPost, ValidateInput(false)]
        public ActionResult CreateBaseMenu(BaseMenuCreateModel model, int[] SectorCategoriesForm)
        {

            var baseMenu = new global::MakinaTurkiye.Entities.Tables.Content.BaseMenu();
            baseMenu.Active = model.Active;
            baseMenu.BaseMenuName = model.BaseMenuName;
            baseMenu.CreatedDate = DateTime.Now;
            baseMenu.UpdatedDate = DateTime.Now;
            baseMenu.Order = model.Order;
            baseMenu.HomePageOrder = model.HomePageOrder;
            baseMenu.BackgroundCss = model.BackgroundCss;
            baseMenu.TabBackgroundCss = model.TabBackgroundCss;
            _baseMenuService.InsertBaseMenu(baseMenu);

            foreach (var item in SectorCategoriesForm)
            {
                var baseMenuCategory = new global::MakinaTurkiye.Entities.Tables.Content.BaseMenuCategory();

                baseMenuCategory.BaseMenuId = baseMenu.BaseMenuId;
                baseMenuCategory.CategoryId = item;
                baseMenuCategory.CreatedDate = DateTime.Now;
                baseMenuCategory.UpdatedDate = DateTime.Now;

                _baseMenuService.InsertBaseMenuCategory(baseMenuCategory);
            }
            return RedirectToAction("CreateBaseMenu");
        }

        public ActionResult EditBaseMenu(int id)
        {
            var categories = _categoryService.GetMainCategories();
            BaseMenuCreateModel model = new BaseMenuCreateModel();
            var baseMenu = _baseMenuService.GetBaseMenuByBaseMenuId(id);

            foreach (var item in categories)
            {
                bool selected = false;
                if (baseMenu.BaseMenuCategories.Any(x => x.CategoryId == item.CategoryId))
                    selected = true;
                model.SectorCategories.Add(new SelectListItem { Text = item.CategoryContentTitle, Value = item.CategoryId.ToString(), Selected = selected });
            }
            model.BaseMenuName = baseMenu.BaseMenuName;
            model.BaseMenuId = baseMenu.BaseMenuId;
            model.Active = baseMenu.Active;
            model.HomePageOrder = baseMenu.HomePageOrder;
            model.TabBackgroundCss = baseMenu.TabBackgroundCss;
            model.BackgroundCss = baseMenu.BackgroundCss;

            foreach (var item in baseMenu.BaseMenuImages)
            {
                model.BaseMenuImages.Add(item.BaseMenuImageId, AppSettings.BaseMenuImageFolder + item.MenuImagePath);
            }
            return View(model);

        }
        [HttpPost, ValidateInput(false)]
        public ActionResult EditBaseMenu(BaseMenuCreateModel model, int[] SectorCategoriesForm)
        {
            var baseMenu = _baseMenuService.GetBaseMenuByBaseMenuId(model.BaseMenuId);
            baseMenu.BaseMenuName = model.BaseMenuName;
            baseMenu.Active = model.Active;
            baseMenu.Order = model.Order;
            baseMenu.BackgroundCss = model.BackgroundCss;
            baseMenu.HomePageOrder = model.HomePageOrder;
            baseMenu.UpdatedDate = DateTime.Now;
            baseMenu.TabBackgroundCss = model.TabBackgroundCss;
            foreach (var item in baseMenu.BaseMenuCategories.ToList())
            {
                var baseMenuCategory = _baseMenuService.GetBaseMenuCategoryByBaseMenuCategoryId(item.BaseMenuCategoryId);
                _baseMenuService.DeleteBaseMenuCategory(baseMenuCategory);
            }

            foreach (var item in SectorCategoriesForm)
            {
                var baseMenuCategory = new global::MakinaTurkiye.Entities.Tables.Content.BaseMenuCategory();

                baseMenuCategory.BaseMenuId = baseMenu.BaseMenuId;
                baseMenuCategory.CategoryId = item;
                baseMenuCategory.CreatedDate = baseMenu.CreatedDate;
                baseMenuCategory.UpdatedDate = DateTime.Now;

                _baseMenuService.InsertBaseMenuCategory(baseMenuCategory);
            }




            TempData["success"] = "Düzenleme Başarılı.";
            return RedirectToAction("EditBaseMenu", new { id = baseMenu.BaseMenuId });
        }

        public ActionResult BaseMenuImage(int id, int? imageId)
        {
            BaseMenuImageModel model = new BaseMenuImageModel();
            model.Active = true;
            if (imageId.HasValue)
            {
                var baseMenuImage = _baseMenuService.GetBaseMenuImageByBaseMenuImageId(imageId.Value);
                model.BaseMenuImageId = imageId.Value;
                model.Url = baseMenuImage.Url;
                model.ImagePath = AppSettings.BaseMenuImageFolder + baseMenuImage.MenuImagePath;
                model.Active = baseMenuImage.Active;
            }
            model.BaseMenuId = id;
            return View(model);
        }

        [HttpPost]
        public ActionResult BaseMenuImage(BaseMenuImageModel menuImageModel)
        {
            var baseMenuImage = new global::MakinaTurkiye.Entities.Tables.Content.BaseMenuImage();
            if (Request.Files["image"].ContentLength > 0)
            {
                if (menuImageModel.BaseMenuImageId != 0)
                {
                    baseMenuImage = _baseMenuService.GetBaseMenuImageByBaseMenuImageId(menuImageModel.BaseMenuImageId);
                    FileHelpers.Delete(AppSettings.BaseMenuImageFolder + baseMenuImage.MenuImagePath);
                    _baseMenuService.DeleteBaseMenuImage(baseMenuImage);
                }

                foreach (var inputTagName in Request.Files)
                {
                    if (Request.Files[inputTagName.ToString()].ContentLength > 0)
                    {

                        baseMenuImage = new global::MakinaTurkiye.Entities.Tables.Content.BaseMenuImage();

                        string fileName = FileHelpers.Upload(AppSettings.BaseMenuImageFolder, Request.Files[inputTagName.ToString()]);
                        //oldIcon.CategoryIcon = fileName;
                        baseMenuImage.MenuImagePath = fileName;
                        baseMenuImage.BaseMenuId = menuImageModel.BaseMenuId;
                        baseMenuImage.CreateDate = DateTime.Now;
                        baseMenuImage.UpdatedDate = DateTime.Now;
                        baseMenuImage.Url = menuImageModel.Url;
                        baseMenuImage.Active = true;
                        _baseMenuService.InsertBaseMenuImage(baseMenuImage);
                    }
                }

            }
            else
            {
                baseMenuImage = _baseMenuService.GetBaseMenuImageByBaseMenuImageId(menuImageModel.BaseMenuImageId);
                baseMenuImage.Url = menuImageModel.Url;
                baseMenuImage.Active = menuImageModel.Active;
                _baseMenuService.UpdateBaseMenuImage(baseMenuImage);

            }

            TempData["result"] = "Başarıyla eklenmiştir";
            return RedirectToAction("BaseMenuImage", new { id = menuImageModel.BaseMenuId, imageId = baseMenuImage.BaseMenuImageId });
        }

        [HttpPost]
        public JsonResult DeleteBaseImage(int id)
        {
            var baseMenuImage = _baseMenuService.GetBaseMenuImageByBaseMenuImageId(id);
            if (baseMenuImage != null)
            {
                FileHelpers.Delete(AppSettings.BaseMenuImageFolder + baseMenuImage.MenuImagePath);
                _baseMenuService.DeleteBaseMenuImage(baseMenuImage);
            }
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpGet]
        public ActionResult ExportSubCategories(int id)
        {
            var subCategories = _categoryService.GetSPAllSubCategoriesByCategoryId(id);

            var category = _categoryService.GetCategoryByCategoryId(id);

            var items = new List<global::MakinaTurkiye.Entities.StoredProcedures.Catalog.AllSubCategoryItemResult>();
            items.Add(new global::MakinaTurkiye.Entities.StoredProcedures.Catalog.AllSubCategoryItemResult
            {
                CategoryId = category.CategoryId,
                CategoryContentTitle = category.CategoryContentTitle,
                CategoryName = category.CategoryName,
                CategoryParentId = category.CategoryParentId,
                CategoryType = category.CategoryType,
                Content = category.Content,
                Description = category.Description,
                StorePageTitle = category.StorePageTitle,
                Title = category.Title,
                Keywords = ""
            });
            items.AddRange(subCategories);

            var items2 = items.Select(
      o => new
      {
          o.CategoryId,
          CategoryParentId = o.CategoryParentId.HasValue ? o.CategoryParentId : 0,
          CategoryName = !string.IsNullOrEmpty(o.CategoryName) ? o.CategoryName : "",
          CategoryContentTitle = !string.IsNullOrEmpty(o.CategoryContentTitle) ? o.CategoryContentTitle : "",
          StorePageTitle = !string.IsNullOrEmpty(o.StorePageTitle) ? o.StorePageTitle : "",
          Keywords = !string.IsNullOrEmpty(o.Keywords) ? o.Keywords : "",
          Description = !string.IsNullOrEmpty(o.Description) ? o.Description : "",
          PageTitle = !string.IsNullOrEmpty(o.Title) ? o.Title : "",
          CategoryType = o.CategoryType.HasValue ? Enum.GetName(typeof(CategoryType), o.CategoryType) : "",
          Content = !string.IsNullOrEmpty(o.Content) ? o.Content : ""
      }).ToList();

            var workbook = new HSSFWorkbook();
            var sheet = workbook.CreateSheet("sheet1");

            var properties = new[] { "CategoryId", "CategoryParentId", "CategoryName", "CategoryContentTitle", "StorePageTitle", "Keywords", "Description", "PageTitle", "CategoryType", "Content" };
            var headers = new[] { "CategoryId", "CategoryParentId", "CategoryName", "CategoryContentTitle", "StorePageTitle", "Keywords", "Description", "PageTitle", "CategoryType", "Content" };
            var headerRow = sheet.CreateRow(0);

            // create header
            for (int i = 0; i < properties.Length; i++)
            {
                var cell = headerRow.CreateCell(i);
                cell.SetCellValue(headers[i]);
            }

            // fill content
            for (int i = 0; i < items2.Count; i++)
            {
                var rowIndex = i + 1;
                var row = sheet.CreateRow(rowIndex);

                for (int j = 0; j < properties.Length; j++)
                {
                    var cell = row.CreateCell(j);
                    var o = items2[i];
                    cell.SetCellValue(o.GetType().GetProperty(properties[j]).GetValue(o, null).ToString());
                }
            }

            string fileName = Helpers.ToUrl(category.CategoryName + " alt kategorileri") + ".xls";

            using (var stream = new MemoryStream())
            {
                workbook.Write(stream);
                stream.Close();
                return File(stream.ToArray(), "application/vnd.ms-excel", fileName);
            }


        }

        public ActionResult UpdateCategoryByExcelFile()
        {

            return View();
        }
        [HttpPost]
        public ActionResult UpdateCategoryByExcelFile(HttpPostedFileBase file)
        {
            bool updated = false;
            if (file != null && file.ContentLength > 0)
            {
                HttpPostedFileBase files = file; //Read the Posted Excel File
                ISheet sheet; //Create the ISheet object to read the sheet cell values
                string filename = Path.GetFileName(Server.MapPath(files.FileName)); //get the uploaded file name
                var fileExt = Path.GetExtension(filename); //get the extension of uploaded excel file
                string[] arr = new string[] { ".xls", ".xlsx" };
                if (arr.Contains(fileExt))
                {
                    if (fileExt == ".xls")
                    {
                        HSSFWorkbook hssfwb = new HSSFWorkbook(files.InputStream); //HSSWorkBook object will read the Excel 97-2000 formats
                        sheet = hssfwb.GetSheetAt(0); //get first Excel sheet from workbook
                    }
                    else
                    {
                        XSSFWorkbook hssfwb = new XSSFWorkbook(files.InputStream); //XSSFWorkBook will read 2007 Excel format
                        sheet = hssfwb.GetSheetAt(0); //get first Excel sheet from workbook
                    }

                    for (int row = 1; row <= sheet.LastRowNum; row++) //Loop the records upto filled row
                    {
                        if (sheet.GetRow(row) != null) //null is when the row only contains empty cells
                        {
                            string categoryId = sheet.GetRow(row).GetCell(0).StringCellValue; //Here for sample , I just save the value in "value" field, Here you can write your custom logics...
                            string categoryName = sheet.GetRow(row).GetCell(2).StringCellValue;
                            string categoryContentTitle = sheet.GetRow(row).GetCell(3).StringCellValue;
                            string storePageTitle = sheet.GetRow(row).GetCell(4).StringCellValue;
                            string keywords = sheet.GetRow(row).GetCell(5) != null ? sheet.GetRow(row).GetCell(5).StringCellValue : "";
                            string description = sheet.GetRow(row).GetCell(6).StringCellValue;
                            string pageTitle = sheet.GetRow(row).GetCell(7).StringCellValue;
                            string content = sheet.GetRow(row).GetCell(9).StringCellValue;

                            var category = _categoryService.GetCategoryByCategoryId(Convert.ToInt32(categoryId));
                            category.CategoryName = categoryName;
                            category.CategoryContentTitle = categoryContentTitle;
                            category.StorePageTitle = storePageTitle;
                            category.Title = pageTitle;
                            category.Keywords = keywords;
                            category.Content = content;
                            category.Description = category.Description;
                            _categoryService.UpdateCategory(category);
                            updated = true;
                        }
                    }
                }
            }
            ViewBag.Updated = updated;
            return View();

        }

        public ActionResult ClearAllCache()
        {
            _categoryService.ClearAllCache();
            TempData["Message"] = "Cache Sıfırlanmıştır";
            return RedirectToAction("AllIndex");
        }
        #endregion

    }
}
