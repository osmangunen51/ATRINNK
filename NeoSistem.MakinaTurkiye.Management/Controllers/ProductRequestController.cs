using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.ProductRequests;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.ProductRequests;
using System.Collections.Generic;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class ProductRequestController : BaseController
    {
        private readonly IProductRequestService _productRequestService;
        private readonly ICategoryService _categoryService;

        public ProductRequestController(IProductRequestService productRequestService,ICategoryService categoryService)
        {
            this._productRequestService = productRequestService;
            this._categoryService = categoryService;
        }

        // GET: ProductRequest
        public ActionResult Index()
        {
            int page = 1;
            FilterModel<ProductRequestItem> model = new FilterModel<ProductRequestItem>();
            PrepareProductRequestSource(model, page);
            return View(model);
        }
        public void PrepareProductRequestSource(FilterModel<ProductRequestItem> model,int page)
        {
            int pageSize = 20;
            var productRequests = _productRequestService.GetAllProductRequests(page, pageSize);

            List<ProductRequestItem> source = new List<ProductRequestItem>();
            foreach (var item in productRequests)
            {
                var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                string categoryName = "",brandName="",seriesName="",modelName="";
                if (category != null) categoryName = category.CategoryName;
                if (item.BrandId != 0)
                {
                    var brand = _categoryService.GetCategoryByCategoryId(item.BrandId);
                    if (brand != null)
                        brandName = brand.CategoryName;
                }
                if(item.SeriesId!=0)
                {
                    var series = _categoryService.GetCategoryByCategoryId(item.SeriesId);
                    if (series != null)
                        seriesName = series.CategoryName;
                }
                if (item.ModelId != 0)
                {
                    var modelN = _categoryService.GetCategoryByCategoryId(item.ModelId);
                    if (modelN != null)
                        modelName = modelN.CategoryName;
                }
             
                source.Add(new ProductRequestItem
                {
                    BrandName = brandName,
                    CategoryName = categoryName,
                    Email = item.Email,
                    IsControllled = item.IsControlled,
                    MemberNameSurname = item.NameSurname,
                    Message = item.Message,
                    ModelName = modelName,
                    PhoneNumber = item.PhoneNumber,
                    ProductRequestId = item.ProductRequestId,
                    RecordDate = item.RecordDate,
                    SeriesName = seriesName
                });
            }
            model.Source = source;
            model.PageDimension = pageSize;
            model.CurrentPage = page;
            model.TotalRecord = productRequests.TotalCount;
          
        }
        [HttpPost]
        public ActionResult Index(int page)
        {
            FilterModel<ProductRequestItem> model = new FilterModel<ProductRequestItem>();
            PrepareProductRequestSource(model, page);
            return PartialView("_Item", model);
        }
        [HttpPost]
        public JsonResult Delete(int id)
        {
            var productRequest = _productRequestService.GetProductRequestByProductRequestId(id);
            _productRequestService.DeleteProductRequest(productRequest);
            return Json(true, JsonRequestBehavior.AllowGet);
        }
    }
}