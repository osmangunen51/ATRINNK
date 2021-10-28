
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.Controllers;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Constants;
using NeoSistem.MakinaTurkiye.Web.Models;
using NeoSistem.MakinaTurkiye.Web.Models.Authentication;
using NeoSistem.MakinaTurkiye.Web.Models.Statistics;
using NeoSistem.MakinaTurkiye.Web.Models.ViewModels;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Controllers
{
    public class StatisticController : BaseAccountController
    {

        private readonly IStoreStatisticService _storeStatisticService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IProductService _productService;
        private readonly IProductStatisticService _productStatisticService;

        public StatisticController(IStoreStatisticService storeStatisticService,
            IMemberStoreService memberStoreService, IStoreService storeService,
            IProductService productService,
            IProductStatisticService productStatisticService)
        {
            this._storeStatisticService = storeStatisticService;
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._productStatisticService = productStatisticService;
            this._productService = productService;

            this._memberStoreService.CachingGetOrSetOperationEnabled = false;
            this._storeService.CachingGetOrSetOperationEnabled = false;
            this._productService.CachingGetOrSetOperationEnabled = false;

        }
        int TotalRecord = 0;
        byte pageDimension = 100;
        public ActionResult Index()
        {
            ICollection<ProductModel> data = new ProductModel[] { };
            var dataProduct = new Data.Product();
            var getProduct = new SearchModel<ProductModel>
            {
                CurrentPage = 1,
                PageDimension = pageDimension,
                TotalRecord = TotalRecord
            };

            data = dataProduct.GetSearchWebByProductActiveType(ref TotalRecord, getProduct.PageDimension, getProduct.CurrentPage, 4,
                AuthenticationUser.Membership.MainPartyId).AsCollection<ProductModel>();

            getProduct.Source = (from a in data
                                 orderby a.productrate
                                 descending
                                 select a).ToList() as ICollection<ProductModel>;
            getProduct.TotalRecord = TotalRecord;

            var model = new ProductStatisticModel
            {
                ProductItems = getProduct
            };

            if (AuthenticationUser.Membership.MemberType == 10)
            {
                //bireysel uye
            }
            else
            {
                //firma uyesi
            }
            MTStatisticModel statisticModel = new MTStatisticModel();

            if (Request.QueryString["pagetype"].ToString() == "3")
            {
                //var memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;

                //var productStatistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(memberMainPartyId, DateTime.Now, DateTime.Now, true).ToList();
                //List<string> dates = new List<string>();
                //List<int> viewCounts = new List<int>();
                //int totalViewCount = 0;
                //int userCount = 0;
                //var productStatisticsDistincyByHour = productStatistics.Select(x => x.Hour).Distinct();
                //foreach (var item in productStatisticsDistincyByHour.ToList())
                //{
                //    var productStatisticsHours = productStatistics.Where(x => x.Hour == item);
                //    dates.Add(item.ToString() + ":00");
                //    int count = productStatisticsHours.Select(x => (int)x.ViewCount).Sum();
                //    viewCounts.Add(count);
                //    totalViewCount += count;
                //}

                //statisticModel.LabelString = "Saat";
                //statisticModel.DateString = DateTime.Now.Date.ToString("dd MMMM", CultureInfo.InvariantCulture);
                //statisticModel.JsonDatas = JsonConvert.SerializeObject(viewCounts);
                //statisticModel.JsoonLabels = JsonConvert.SerializeObject(dates);

            }
            else
            {
                int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
                var lastWeek = DateTime.Now.AddDays(-7);
                var today = DateTime.Now;
                var storeStatistics = _storeStatisticService.GetStoreStatisticsByStoreIdAndDates(memberStore.StoreMainPartyId.Value, lastWeek, today);
                statisticModel = PrepareStatisticStore(storeStatistics, memberStore.StoreMainPartyId.Value);
            }
            model.MTStatisticModel = statisticModel;


            return View(model);
        }
        [HttpGet]
        public PartialViewResult GetProductStatisticDay()
        {
            var statisticModel = new MTStatisticModel();
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var productStatistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(memberMainPartyId, DateTime.Now, DateTime.Now, true).ToList();
            List<string> dates = new List<string>();
            List<int> viewCounts = new List<int>();
            int totalViewCount = 0;

            var productStatisticsDistincyByHour = productStatistics.Select(x => x.Hour).Distinct();
            foreach (var item in productStatisticsDistincyByHour.ToList())
            {
                var productStatisticsHours = productStatistics.Where(x => x.Hour == item);
                dates.Add(item.ToString() + ":00");
                int count = productStatisticsHours.Select(x => (int)x.ViewCount).Sum();
                viewCounts.Add(count);
                totalViewCount += count;
            }

            statisticModel.LabelString = "Saat";
            statisticModel.DateString = DateTime.Now.Date.ToString("dd MMMM", CultureInfo.InvariantCulture);
            statisticModel.JsonDatas = JsonConvert.SerializeObject(viewCounts);
            statisticModel.JsoonLabels = JsonConvert.SerializeObject(dates);
            return PartialView("_ProductStatistic", statisticModel);
        }
        public ActionResult StatisticStore()
        {
            var lastWeek = DateTime.Now.AddDays(-7);
            var today = DateTime.Now;
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var storeStatistics = _storeStatisticService.GetStoreStatisticsByStoreIdAndDates(memberStore.StoreMainPartyId.Value, lastWeek, today);
            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            MTStoreStatisticViewModel model = new MTStoreStatisticViewModel();
            var statisticModel = PrepareStatisticStore(storeStatistics, memberStore.StoreMainPartyId.Value);

            model.MTStoreStatisticModel = statisticModel;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Statistics, (byte)LeftMenuConstants.Statistic.StoreStatistics);
            model.TotalViewCount = storeStatistics.Select(x => (int)x.ViewCount).Sum();

            model.TotalUserCount = storeStatistics.Count;
            model.LastTotalViewCount = store.ViewCount.Value - (long)model.TotalViewCount;
            model.LastSingularViewCount = store.SingularViewCount.Value - (long)model.TotalUserCount;
            PrepareSatisticLocation(storeStatistics, model);

            //statisticModel.JsonDatas = JsonConvert.SerializeObject(storeStatistics.Select(x=>x.ViewCount).ToList());
            //statisticModel.JsoonLabels = JsonConvert.SerializeObject(storeStatistics.Select(x => x.RecordDate.ToString("dd/MM/yyyy hh:mm")));




            return View(model);
        }
        [HttpGet]
        public PartialViewResult GetStoreStatistic(string startDate, string endDate)
        {


            var cultur = new CultureInfo("tr-TR");
            var endDate1 = Convert.ToDateTime(endDate, cultur.DateTimeFormat);
            var startDate1 = Convert.ToDateTime(startDate, cultur.DateTimeFormat);

            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);
            var storeStatistics = _storeStatisticService.GetStoreStatisticsByStoreIdAndDates(memberStore.StoreMainPartyId.Value, startDate1, endDate1);

            var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);

            MTStoreStatisticViewModel model = new MTStoreStatisticViewModel();
            var statisticModel = PrepareStatisticStore(storeStatistics, memberStore.StoreMainPartyId.Value);



            model.MTStoreStatisticModel = statisticModel;
            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Statistics, (byte)LeftMenuConstants.Statistic.StoreStatistics);
            model.TotalViewCount = storeStatistics.Select(x => (int)x.ViewCount).Sum();

            model.TotalUserCount = storeStatistics.Count;
            model.LastTotalViewCount = store.ViewCount.Value - (long)model.TotalViewCount;
            model.LastSingularViewCount = store.SingularViewCount.Value - (long)model.TotalUserCount;
            PrepareSatisticLocation(storeStatistics, model);
            PrepareSatisticLocation(storeStatistics, model);
            return PartialView("_StoreStatisticCenter", model);
        }

        public void PrepareSatisticLocation(List<global::MakinaTurkiye.Entities.Tables.Stores.StoreStatistic> storeStatistics, MTStoreStatisticViewModel model)
        {

            List<MTStatisticLocationModel> statisticLocal = new List<MTStatisticLocationModel>();
            foreach (var item in storeStatistics.Select(x => x.UserCity).Distinct())
            {
                string storeStatisticCountry = storeStatistics.FirstOrDefault(x => x.UserCity == item).UserCountry;
                var storeStatisticCount = storeStatistics.Where(x => x.UserCity == item).Select(x => (int)x.ViewCount).Sum();
                statisticLocal.Add(new MTStatisticLocationModel { ViewCount = storeStatisticCount, City = item, Country = storeStatisticCountry });
            }
            model.MTStatisticLocationModels = statisticLocal;
        }
        public MTStatisticModel PrepareStatisticStore(List<global::MakinaTurkiye.Entities.Tables.Stores.StoreStatistic> storeStatistics, int storeMainPartyId)
        {

            MTStatisticModel statisticModel = new MTStatisticModel();
            List<string> dates = new List<string>();
            List<int> viewCounts = new List<int>();
            int userCount = storeStatistics.Count;
            int totalViewCount = storeStatistics.Select(x => (int)x.ViewCount).Sum();
            foreach (var item in storeStatistics.Select(x => x.RecordDate.Date).Distinct())
            {
                dates.Add(item.Date.ToString("dd MMMM", CultureInfo.InvariantCulture));

                var storeStatisticDates = storeStatistics.Where(x => x.RecordDate.Date == item.Date).Select(x => (int)x.ViewCount).ToList();

                viewCounts.Add(storeStatisticDates.Sum());
            }
            statisticModel.JsonDatas = JsonConvert.SerializeObject(viewCounts);
            statisticModel.JsoonLabels = JsonConvert.SerializeObject(dates);



            return statisticModel;
        }
        public ActionResult ProductStatistics(string ProductId, string betweenDate, string startDate, string endDate)
        {
            MTProductStatisticViewModel model = new MTProductStatisticViewModel();
            int memberMainPartyId = AuthenticationUser.CurrentUser.Membership.MainPartyId;
            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId);

            var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(memberStore.StoreMainPartyId.Value).Select(x => x.MemberMainPartyId).ToList();

            List<int> productIds = _productService.GetAllProductsByMainPartyIds(mainPartyIds).Select(x => x.ProductId).ToList();
            if (!string.IsNullOrEmpty(ProductId))
            {
                int productId = Convert.ToInt32(ProductId);
                productIds = new List<int> { Convert.ToInt32(productId) };
                var product = _productService.GetProductByProductId(productId);
                model.FilterItemModels.Add(new FilterItemModel { FilterBackUrl = "/Account/Statistic/ProductStatistics", FilterName = product.ProductName });
            }
            var productStatistics = new List<global::MakinaTurkiye.Entities.Tables.Catalog.ProductStatistic>();

            List<string> dates = new List<string>();
            List<int> viewCounts = new List<int>();
            MTStatisticModel statisticModel = new MTStatisticModel();
            int totalViewCount = 0;
            int userCount = 0;

            DateTime dt1;
            DateTime dt2;
            bool canParseDate = DateTime.TryParse(startDate, out dt1) && DateTime.TryParse(endDate, out dt2);
            if (canParseDate)
            {

                model.StartDate = startDate;
                model.EndDate = endDate;
                var culturInfo = new CultureInfo("tr-tr");
                DateTime beginDate = Convert.ToDateTime(startDate, culturInfo.DateTimeFormat);
                DateTime lastDate = Convert.ToDateTime(endDate, culturInfo.DateTimeFormat);
                productStatistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(memberMainPartyId, beginDate, lastDate, false).ToList();

                var productStatisticsDistincyByDate = productStatistics.Select(x => x.RecordDate.Date).Distinct();
                foreach (var item in productStatisticsDistincyByDate)
                {
                    var productStatisticsDates = productStatistics.Where(x => x.RecordDate.Date == item);
                    dates.Add(productStatisticsDates.FirstOrDefault().RecordDate.ToString("dd MMMM", CultureInfo.InvariantCulture));
                    int count = productStatisticsDates.Select(x => (int)x.ViewCount).Sum();
                    viewCounts.Add(count);
                    totalViewCount += count;

                }

                statisticModel.LabelString = "Tarih";


                statisticModel.DateString = beginDate.Date.ToString("dd MMMM", CultureInfo.InvariantCulture) + "-" + DateTime.Now.Date.ToString("dd MMMM", CultureInfo.InvariantCulture);


            }
            else
            {

                productStatistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(memberMainPartyId, DateTime.Now, DateTime.Now, true).ToList();

                var productStatisticsDistincyByHour = productStatistics.Select(x => x.Hour).Distinct();
                foreach (var item in productStatisticsDistincyByHour)
                {
                    var productStatisticsHours = productStatistics.Where(x => x.Hour == item);
                    dates.Add(item.ToString() + ":00");
                    int count = productStatisticsHours.Select(x => (int)x.ViewCount).Sum();
                    viewCounts.Add(count);
                    totalViewCount += count;
                }

                statisticModel.LabelString = "Saat";
                statisticModel.DateString = DateTime.Now.Date.ToString("dd MMMM", CultureInfo.InvariantCulture);

            }
            statisticModel.JsonDatas = JsonConvert.SerializeObject(viewCounts);
            statisticModel.JsoonLabels = JsonConvert.SerializeObject(dates);
            List<MTStatisticLocationModel> statisticLocal = new List<MTStatisticLocationModel>();
            foreach (var item in productStatistics.Select(x => x.UserCity).Distinct())
            {
                string storeStatisticCountry = productStatistics.FirstOrDefault(x => x.UserCity == item).UserCountry;
                var storeStatisticCount = productStatistics.Where(x => x.UserCity == item).Select(x => (int)x.ViewCount).Sum();
                statisticLocal.Add(new MTStatisticLocationModel { ViewCount = storeStatisticCount, City = item, Country = storeStatisticCountry });
            }



            model.MTStatisticLocationModels = statisticLocal;

            userCount = productStatistics.Count;
            model.TotalViewCount = totalViewCount;
            model.TotalUserCount = userCount;
            model.MTStatisticModel = statisticModel;
            if (productStatistics.ToList().Count > 0)
            {
                var productsByProductIds = _productService.GetProductsByProductIds(productStatistics.Select(x => x.ProductId).ToList(), 20);
                int productCount = productsByProductIds.Count;
                foreach (var item in productsByProductIds)
                {
                    int pViewCount = productStatistics.Where(x => x.ProductId == item.ProductId).Select(x => (int)x.ViewCount).Sum();
                    model.ProductItems.Add(new MTProductItem { ProductId = item.ProductId, ProductName = item.ProductName, ViewCount = pViewCount });

                }
            }

            model.LeftMenu = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.Statistics, (byte)LeftMenuConstants.Statistic.AdStatistics);


            return View(model);
        }
    }

}