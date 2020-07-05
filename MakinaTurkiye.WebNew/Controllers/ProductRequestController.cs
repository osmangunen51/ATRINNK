using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.ProductRequests;
using MakinaTurkiye.Entities.Tables.ProductRequests;

using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Catalog;
using NeoSistem.MakinaTurkiye.Web.Models.ProductRequests;
using NeoSistem.MakinaTurkiye.Web.Models;

using System;
using System.Linq;
using System.Web.Mvc;


namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class ProductRequestController : BaseController
    {
        private readonly ICategoryService _categoryService;
        private readonly IMemberService _memberService;
        private readonly IPhoneService _phoneService;
        private readonly IProductRequestService _productRequestService;

        public ProductRequestController(ICategoryService categoryService,IMemberService memberService,IPhoneService phoneService,
            IProductRequestService productRequestService)
        {
            this._categoryService = categoryService;
            this._memberService = memberService;
            this._phoneService = phoneService;
            this._productRequestService = productRequestService;
        }
        // GET: ProductRequest
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Step1()
        {
            if (AuthenticationUser.CurrentUser.Membership.MainPartyId > 0)
            {
                //SeoPageType = (byte)PageType.ProductRequest;

                MTProductRequestModel model = new MTProductRequestModel();
                var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);

                var phone = _phoneService.GetPhonesByMainPartyId(member.MainPartyId).FirstOrDefault(x => x.PhoneType == (byte)PhoneType.Gsm);
                if (phone != null)
                    model.MTProductRequestForm.PhoneNumber = phone.PhoneCulture + phone.PhoneAreaCode + " " + phone.PhoneNumber;
     
                var sectors = _categoryService.GetMainCategories();
                foreach (var item in sectors)
                {
                    model.SectorList.Add(new MTCategoryItemModel { CategoryContentTitle = item.CategoryContentTitle, CategoryId = item.CategoryId });
                }

                return View(model);            
            }
            else
            {
                TempData["MessageError"] ="Ürün talebinde bulunmak için üye girişi yapmanız gerekmektedir.";
                return Redirect("/uyelik/kullanicigirisi?ReturnUrl="+ Request.Url.AbsolutePath+Request.Url.Query);
            }
  
        }
        [HttpPost]
        public ActionResult step1(MTProductRequestModel model)
        {
            var productRequest = new ProductRequest();
            var member = _memberService.GetMemberByMainPartyId(AuthenticationUser.CurrentUser.Membership.MainPartyId);
            string nameSurname = member.MemberName + " " + member.MemberSurname;
           string email = member.MemberEmail;
            var fModel = model.MTProductRequestForm;
            productRequest.BrandId = fModel.BrandId;
            int categoyId = fModel.SectorId;
            if (fModel.ProductGroupId != 0)
                categoyId = fModel.ProductGroupId;
            if (fModel.CategoryId != 0)
                categoyId = fModel.CategoryId;
            productRequest.CategoryId = categoyId;
            productRequest.ModelId = fModel.ModelId;
            productRequest.SeriesId = fModel.SeriesId;
            productRequest.Email = email;
            productRequest.IsControlled = false;
            productRequest.MemberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            productRequest.Message = fModel.Message;
            productRequest.NameSurname = nameSurname;
            productRequest.PhoneNumber = fModel.PhoneNumber.Replace("(","").Replace(")","");
            productRequest.RecordDate = DateTime.Now;
            _productRequestService.InsertProductRequest(productRequest);
            TempData["success"] = true;
            return RedirectToAction("step1");

        }
    }
}