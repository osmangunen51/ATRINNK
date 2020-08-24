using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.ImageHelpers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class StoreController : BaseApiController
    {
        private readonly IStoreService _storeService;

        public StoreController()
        {
            _storeService = EngineContext.Current.Resolve<IStoreService>();
        }
        public HttpResponseMessage GetWithName(string Name)
        {
            ProcessStatus processStatus = new ProcessStatus();
            try
            {
                var Result = _storeService.GetStoreSearchByStoreName(Name);
                foreach (var item in Result)
                {
                    item.StoreLogo = ImageHelper.GetStoreLogoParh(item.MainPartyId, item.StoreLogo,300);
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
    }
}
