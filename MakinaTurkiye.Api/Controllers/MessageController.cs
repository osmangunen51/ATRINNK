using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Members;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class MessageController : BaseApiController
    {
        private readonly IMemberService _memberService;
        public MessageController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
        }

        public HttpResponseMessage SendPrivateMessage(int storesId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                //if (member != null)
                //{
                //    var favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(member.MainPartyId, storesId);

                //    _favoriteStoreService.DeleteFavoriteStore(favoriteStore);

                //    processStatus.Result = "Firma başarıyla favorilerden silinmiştir.";
                //    processStatus.Message.Header = "delete Favorite Store";
                //    processStatus.Message.Text = "Başarılı";
                //    processStatus.Status = true;
                //}
                //else
                //{
                //    processStatus.Result = "Login üye bulunamadı";
                //    processStatus.Message.Header = "delete Favorite Store";
                //    processStatus.Message.Text = "Başarısız";
                //    processStatus.Status = false;
                //}
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "delete Favorite Store";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }




    }
}
