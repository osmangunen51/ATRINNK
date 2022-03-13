using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class StoreController : BaseApiController
    {
        private readonly IStoreService _storeService;
        private readonly IStoreNewService _storeNewService;
        private readonly ICategoryService _categoryService;
        public StoreController()
        {
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _storeNewService = EngineContext.Current.Resolve<IStoreNewService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
        }

        //public StoreController(IStoreService storeService)
        //{
        //    this._storeService = storeService;
        //}

        public HttpResponseMessage GetWithName(string Name)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreSearchByStoreName(Name);
                foreach (var item in Result)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetStoreByStoreUrlName(string storeUrlName)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByStoreUrlName(storeUrlName);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetByMainPartyId(int MainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByMainPartyId(MainPartyId);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetStoreByStoreEmail(string storeEmail)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetStoreByStoreEmail(storeEmail);
                Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = 1;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetStoresByMainPartyIds(List<int?> mainPartyIds)
        {
            ProcessResult processStatus = new ProcessResult(); 
            try 
            {

                var Result = _storeService.GetStoresByMainPartyIds(mainPartyIds);
                foreach (var item in Result)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count();
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetSPGetStoreForCategoryByCategoryIdAndBrandId(int categoryId = 0, int brandId = 0)
        {
            ProcessResult processStatus = new ProcessResult(); 
                try
                {
                    var Result = _storeService.GetSPGetStoreForCategoryByCategoryIdAndBrandId(categoryId,brandId);
                    foreach (var item in Result)
                    {
                        item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetBransList(int StoreMainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _categoryService.GetCategoriesByStoreMainPartyId(StoreMainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Brand).ToList();
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
        public HttpResponseMessage GetModelList(int StoreMainPartyId)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _categoryService.GetCategoriesByStoreMainPartyId(StoreMainPartyId).Where(x => x.CategoryType == (byte)CategoryType.Model).ToList();
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count;
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        public HttpResponseMessage GetAllStores(int pageDimension, int page, StoreActiveTypeEnum? storeActiveType = null)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeNewService.GetAllStoreNews(pageDimension, page,(byte)storeActiveType);
                foreach (var item in Result)
                {
                    item.ImageName = ImageHelper.GetStoreNewImagePath(item.ImageName, StoreNewImageSize.px300x300.ToString());
                }
                processStatus.Result = Result;
                processStatus.ActiveResultRowCount = Result.Count();
                processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }


        //public HttpResponseMessage GetAllStores(StoreActiveTypeEnum? storeActiveType = null)
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {

        //        var Result = _storeService.GetAllStores(storeActiveType);
        //        foreach (var item in Result)
        //        {
        //            item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
        //        }
        //        processStatus.Result = Result;
        //        processStatus.ActiveResultRowCount = Result.Count();
        //        processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
        //        processStatus.Message.Header = "Store İşlemleri";
        //        processStatus.Message.Text = "Başarılı"; 
        //        processStatus.Status = true;
        //    }
        //    catch (Exception Error)
        //    {
        //        processStatus.Message.Header = "Store İşlemleri";
        //        processStatus.Message.Text = "Başarısız";
        //        processStatus.Status = false;
        //        processStatus.Result = null;
        //        processStatus.Error = Error;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}
        public HttpResponseMessage GetCategoryStores(int categoryId = 0, int modelId = 0, int brandId = 0,
            int cityId = 0, IList<int> localityIds = null, string searchText = "",
            int orderBy = 0, int pageIndex = 0, int pageSize = 0, string activityType = "")
        {
            {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var Result = _storeService.GetCategoryStores(categoryId, modelId, brandId, cityId,
                localityIds, searchText, orderBy, pageIndex, pageSize, activityType);
                    foreach (var item in Result.Stores)
                {
                    item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                }
                processStatus.Result = Result.Stores;
                processStatus.ActiveResultRowCount = Result.Stores.Count;
                processStatus.TotolRowCount = Result.TotalPages;
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
            }
            catch (Exception Error)
            {
                processStatus.Message.Header = "Store İşlemleri";
                processStatus.Message.Text = "Başarısız";
                processStatus.Status = false;
                processStatus.Result = null;
                processStatus.Error = Error;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

    }


        public HttpResponseMessage GetStoresForCategoryByCategoryId(int categoryId = 0)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetSPStoresForCategoryByCategoryId(categoryId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = 1;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreForVideoSearch(string searchText)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreForVideoSearch(searchText);
                    Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }
        public HttpResponseMessage GetHomeStores(int pageSize = int.MaxValue)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetHomeStores(pageSize);
                    foreach (var item in Result)
                    {
                        item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }
        public HttpResponseMessage GetStoreSearchByStoreName(string storeName)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreSearchByStoreName(storeName);
                    foreach (var item in Result)
                    {
                        item.StoreLogo = !string.IsNullOrEmpty(item.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo, 300) : null;
                    }
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreByStoreNo(string storeNo)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreByStoreNo(storeNo);
                    Result.StoreLogo = !string.IsNullOrEmpty(Result.StoreLogo) ? "https:" + ImageHelper.GetStoreLogoParh(Result.MainPartyId, Result.StoreLogo, 300) : null;
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = 1;
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }


        public HttpResponseMessage GetStoreCertificatesByMainPartyId(int mainPartyId)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }

        public HttpResponseMessage GetStoreCertificateByStoreCertificateId(int mainPartyId)
        {
            {
                ProcessResult processStatus = new ProcessResult();
                try
                {
                    var Result = _storeService.GetStoreCertificatesByMainPartyId(mainPartyId);
                    processStatus.Result = Result;
                    processStatus.ActiveResultRowCount = Result.Count();
                    processStatus.TotolRowCount = processStatus.ActiveResultRowCount;
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                catch (Exception Error)
                {
                    processStatus.Message.Header = "Store İşlemleri";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                    processStatus.Result = null;
                    processStatus.Error = Error;
                }
                return Request.CreateResponse(HttpStatusCode.OK, processStatus);
            }
        }
    }
}