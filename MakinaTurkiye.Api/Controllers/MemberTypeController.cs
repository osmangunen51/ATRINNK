using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using System;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class MemberTypeController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IStoreService _storeService;
        private readonly IPhoneService _phoneService;
        private readonly IAddressService _addressService;

        public MemberTypeController(IMemberService memberService, IStoreService storeService, IPhoneService phoneService, IAddressService addressService)
        {
            _memberService = memberService;
            _storeService = storeService;
            _phoneService = phoneService;
            _addressService = addressService;
        }

        public HttpResponseMessage CheckUserName(string username)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var checkUserName = _storeService.GetStoreByStoreUrlName(username);
                    if (checkUserName != null)
                        processStatus.Result = "false";
                    else
                        processStatus.Result = "true";

                    processStatus.Message.Header = "add Favorite Products";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "CheckUserName";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "CheckUserName";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //----------------------------------------------------------------------------FİRMA ÜYELİĞİ----------------------------------------------------------------------------\\

        public HttpResponseMessage InstitutionalStep(byte storeActiveType)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var address = _addressService.GetFisrtAddressByMainPartyId(member.MainPartyId);

                    if (address == null)
                    {
                        //return RedirectToAction("ChangeAddress", "Personal", new { gelenSayfa = "kurumsalaGec" });

                        processStatus.Result = "Kurumsal Üyelik için adres bilgisi tam ve eksiksiz olmalıdır.";
                        processStatus.Message.Header = "InstitutionalStep";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        if (address.CityId == null)
                        {
                            //return RedirectToAction("ChangeAddress", "Personal", new { gelenSayfa = "kurumsalaGec" });

                            processStatus.Result = "Kurumsal Üyelik için adres bilgisi tam ve eksiksiz olmalıdır.";
                            processStatus.Message.Header = "InstitutionalStep";
                            processStatus.Message.Text = "Başarısız";
                            processStatus.Status = false;
                        }
                        var phone = _phoneService.GetPhonesByMainPartyIdByPhoneType(member.MainPartyId, PhoneTypeEnum.Gsm);
                        if (phone != null)
                        {
                            if (phone.active != 1)
                            {
                                //return RedirectToAction("ChangeAddress", "Personal", new { error = "PhoneActive", gelenSayfa = "kurumsalaGec" });

                                processStatus.Result = "Kurumsal Üyelik için telefonunuzu onaylamış olmanız gereklidir.";
                                processStatus.Message.Header = "InstitutionalStep";
                                processStatus.Message.Text = "Başarısız";
                                processStatus.Status = false;
                            }
                            else
                            {
                                if (member.MemberType == (byte)MemberType.Enterprise)
                                {
                                    processStatus.Result = "Zaten Kurumsal Üyesiniz";
                                    processStatus.Message.Header = "InstitutionalStep";
                                    processStatus.Message.Text = "Başarısız";
                                    processStatus.Status = false;
                                }
                                else
                                {
                                    var resultObj = new
                                    {
                                        MemberType = (byte)MemberType.Enterprise,
                                        MemberTitleType = storeActiveType,
                                    };

                                    processStatus.Result = resultObj;
                                    processStatus.Message.Header = "InstitutionalStep";
                                    processStatus.Message.Text = "Başarılı";
                                    processStatus.Status = true;
                                }
                            }
                        }
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "InstitutionalStep";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "InstitutionalStep";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}