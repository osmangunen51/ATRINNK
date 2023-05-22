using Trinnk.Api.Helpers;
using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Catalog;
using Trinnk.Services.Members;
using System;
using System.Net;
using System.Net.Http;
using Trinnk.Api.View.Statistics;
using Trinnk.Services.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;

namespace Trinnk.Api.Controllers
{
    public class StatisticsController : BaseApiController
    {
        private readonly ICategoryService CategoryService;
        private readonly IMemberService _memberService;
        private readonly IStoreStatisticService _storeStatisticService;
        private readonly IStoreService _storeService;
        private readonly IProductStatisticService _productStatisticService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IProductService _productService;
        public StatisticsController()
        {
            CategoryService = EngineContext.Current.Resolve<ICategoryService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _storeStatisticService = EngineContext.Current.Resolve<IStoreStatisticService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _productStatisticService = EngineContext.Current.Resolve<IProductStatisticService>();
            _productService = EngineContext.Current.Resolve<IProductService>();
        }

        public HttpResponseMessage GetStore(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {

                        Trinnk.Api.View.Statistics.Store Result = new Trinnk.Api.View.Statistics.Store()
                        {
                            MainPartyId = MainPartyId
                        };
                        var lastWeek = DateTime.Now.AddDays(-7);
                        var today = DateTime.Now;
                        var storeStatistics = _storeStatisticService.GetStoreStatisticsByStoreIdAndDates(MainPartyId, lastWeek, today);


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

                        Result.VSeries = dates;
                        Result.VDatas = viewCounts;
                        Result.SingularViewCount = (long)store.SingularViewCount;
                        Result.ViewCount = (long)store.ViewCount;
                        processStatus.Result = Result;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }

                    else
                    {
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Statistics İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Statistics İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreDetail(int MainPartyId,DateTime StartDate, DateTime EndDate)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {

                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        if (StartDate == null)
                        {
                            StartDate = DateTime.Now.AddDays(-7);
                        }
                        if (EndDate == null)
                        {
                            StartDate = DateTime.Now;
                        }
                        if (StartDate > EndDate)
                        {
                            EndDate = StartDate.AddDays(7);
                        }
                        var lastWeek = StartDate;
                        var today = EndDate;
                        var storeStatistics = _storeStatisticService.GetStoreStatisticsByStoreIdAndDates(MainPartyId, lastWeek, today);

                        Trinnk.Api.View.Statistics.StoreDetail Result = new Trinnk.Api.View.Statistics.StoreDetail()
                        {
                            MainPartyId = MainPartyId
                        };

                        Result.TotalViewCount = storeStatistics.Select(x => (int)x.ViewCount).Sum();
                        Result.TotalUserCount = storeStatistics.Count();
                        Result.LastSingularViewCount = store.SingularViewCount.Value - Result.TotalViewCount;
                        Result.LastSingularViewCount = store.SingularViewCount.Value - Result.TotalUserCount;

                        foreach (var item in storeStatistics.Select(x => x.UserCity).Distinct())
                        {
                            string storeStatisticCountry = storeStatistics.FirstOrDefault(x => x.UserCity == item).UserCountry;
                            var storeStatisticCount = storeStatistics.Where(x => x.UserCity == item).Select(x => (int)x.ViewCount).Sum();
                            Result.DetailList.Add(new StoreDetailItem
                            {
                                ViewCount = storeStatisticCount,
                                City = item,
                                Country = storeStatisticCountry
                            });
                        }

                        processStatus.Result = Result;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }

                    else
                    {
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Statistics İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Statistics İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetAdvert(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        Trinnk.Api.View.Statistics.Advert Result = new Trinnk.Api.View.Statistics.Advert()
                        {
                            MainPartyId = MainPartyId
                        };
                        var Statistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(loginmember.MainPartyId, DateTime.Now, DateTime.Now, true).ToList();
                        List<string> dates = new List<string>();
                        List<int> viewCounts = new List<int>();
                        int totalViewCount = 0;

                        var productStatisticsDistincyByHour = Statistics.Select(x => x.Hour).Distinct();
                        foreach (var item in productStatisticsDistincyByHour.ToList())
                        {
                            var productStatisticsHours = Statistics.Where(x => x.Hour == item);
                            dates.Add(item.ToString() + ":00");
                            int count = productStatisticsHours.Select(x => (int)x.ViewCount).Sum();
                            viewCounts.Add(count);
                            totalViewCount += count;
                        }

                        Result.LblSeries = "Saat";
                        Result.LblDatas = DateTime.Now.Date.ToString("dd MMMM", CultureInfo.InvariantCulture);
                        Result.VDatas = viewCounts;
                        Result.VSeries = dates;
                        processStatus.Result = Result;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }

                    else
                    {
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }
                }
                else
                {
                    processStatus.Message.Header = "Statistics İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Statistics İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetAdvertDetail(int MainPartyId,int ProductId,DateTime? StartDate, DateTime? EndDate)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;
                var loginmember = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;
                if (loginmember != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(MainPartyId);
                    if (store != null)
                    {
                        Trinnk.Api.View.Statistics.AdvertDetail Result = new Trinnk.Api.View.Statistics.AdvertDetail()
                        {
                            MainPartyId = MainPartyId
                        };

                        var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(MainPartyId).Select(x => x.MemberMainPartyId).ToList();

                        List<int> productIds = _productService.GetAllProductsByMainPartyIds(mainPartyIds).Select(x => x.ProductId).ToList();
                        var productStatistics = new List<Trinnk.Entities.Tables.Catalog.ProductStatistic>();
                        List<string> dates = new List<string>();
                        List<int> viewCounts = new List<int>();
                        int totalViewCount = 0;
                        int userCount = 0;
                        DateTime dt1;
                        DateTime dt2;
                        var culturInfo = new CultureInfo("tr-tr");
                        productStatistics = _productStatisticService.GetProductStatisticsByMemberMainPartyIdAndDate(loginmember.MainPartyId, StartDate.Value, EndDate.Value, false).ToList();
                        var productStatisticsDistincyByDate = productStatistics.Select(x => x.RecordDate.Date).Distinct();
                        foreach (var item in productStatisticsDistincyByDate)
                        {
                            var productStatisticsDates = productStatistics.Where(x => x.RecordDate.Date == item);
                            dates.Add(productStatisticsDates.FirstOrDefault().RecordDate.ToString("dd MMMM", CultureInfo.InvariantCulture));
                            int count = productStatisticsDates.Select(x => (int)x.ViewCount).Sum();
                            viewCounts.Add(count);
                            totalViewCount += count;
                        }
                        Result.VDatas = viewCounts;
                        Result.VSeries = dates;

                        List<AdvertDetailItem> AdvertDetailItems = new List<AdvertDetailItem>();
                        foreach (var item in productStatistics.Select(x => x.UserCity).Distinct())
                        {
                            string storeStatisticCountry = productStatistics.FirstOrDefault(x => x.UserCity == item).UserCountry;
                            var storeStatisticCount = productStatistics.Where(x => x.UserCity == item).Select(x => (int)x.ViewCount).Sum();
                            AdvertDetailItems.Add(new AdvertDetailItem { 
                                ViewCount = storeStatisticCount, 
                                City = item, 
                                Country = storeStatisticCountry });
                        }

                        Result.DetailList = AdvertDetailItems;
                        userCount = productStatistics.Count;
                        Result.TotalViewCount = totalViewCount;
                        Result.TotalUserCount = userCount;
                        List<ProductDetailItem> ProductDetailItems = new List<ProductDetailItem>();
                        if (productStatistics.ToList().Count > 0)
                        {
                            var productsByProductIds = _productService.GetProductsByProductIds(productStatistics.Select(x => x.ProductId).ToList(), 20);
                            int productCount = productsByProductIds.Count;
                            foreach (var item in productsByProductIds)
                            {
                                int pViewCount = productStatistics.Where(x => x.ProductId == item.ProductId).Select(x => (int)x.ViewCount).Sum();
                                ProductDetailItems.Add(new ProductDetailItem { ProductId = item.ProductId, ProductName = item.ProductName, ViewCount = pViewCount });
                            }
                        }
                        Result.ProductDetailList = ProductDetailItems;
                        processStatus.Result = Result;
                        processStatus.ActiveResultRowCount = 1;
                        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }

                    else
                    {
                        processStatus.Message.Header = "Statistics İşlemleri";
                        processStatus.Message.Text = "Store Bulunamadı";
                        processStatus.Status = false;
                        processStatus.Result = null;
                        processStatus.Error = null;
                    }

                }
                else
                {
                    processStatus.Message.Header = "Statistics İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = null;
                }
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Statistics İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


    }
}