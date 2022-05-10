﻿using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using System;
using System.Net;
using System.Net.Http;
using MakinaTurkiye.Api.View.Statistics;
using MakinaTurkiye.Services.Stores;
using System.Collections.Generic;
using System.Linq;
using System.Globalization;
using Newtonsoft.Json;

namespace MakinaTurkiye.Api.Controllers
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

                        MakinaTurkiye.Api.View.Statistics.Store Result = new MakinaTurkiye.Api.View.Statistics.Store()
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

                        MakinaTurkiye.Api.View.Statistics.StoreDetail Result = new MakinaTurkiye.Api.View.Statistics.StoreDetail()
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
                        MakinaTurkiye.Api.View.Statistics.Advert Result = new MakinaTurkiye.Api.View.Statistics.Advert()
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

        public HttpResponseMessage GetAdvertDetail(int MainPartyId,int? ProductId,DateTime? StartDate, DateTime? EndDate)
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
                        MakinaTurkiye.Api.View.Statistics.AdvertDetail Result = new MakinaTurkiye.Api.View.Statistics.AdvertDetail()
                        {
                            MainPartyId = MainPartyId
                        };

                        var mainPartyIds = _memberStoreService.GetMemberStoresByStoreMainPartyId(MainPartyId).Select(x => x.MemberMainPartyId).ToList();

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