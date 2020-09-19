using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Api.View.Account;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Logs;
using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.HttpHelpers;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Text.RegularExpressions;
using System.Web;

namespace MakinaTurkiye.Api.Controllers
{
    public class MemberTypeController : BaseApiController
    {
        private readonly IMemberService _memberService;
        private readonly IStoreService _storeService;
        private readonly IPhoneService _phoneService;
        private readonly IAddressService _addressService;
        private readonly IConstantService _constantService;
        private readonly IActivityTypeService _activityTypeService;
        private readonly IPacketService _packetService;
        private readonly IStoreActivityTypeService _storeActivityTypeService;
        private readonly IStoreActivityCategoryService _storeActivityCategoryService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly ICategoryService _categoryService;
        private readonly IStoreSectorService _storeSectorService;

        public MemberTypeController()
        {
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _constantService = EngineContext.Current.Resolve<IConstantService>();
            _activityTypeService = EngineContext.Current.Resolve<IActivityTypeService>();
            _packetService = EngineContext.Current.Resolve<IPacketService>();
            _storeActivityTypeService = EngineContext.Current.Resolve<IStoreActivityTypeService>();
            _storeActivityCategoryService = EngineContext.Current.Resolve<IStoreActivityCategoryService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
            _storeSectorService = EngineContext.Current.Resolve<IStoreSectorService>();
        }

        private static Regex sUserNameAllowedRegEx = new Regex("^[a-zA-Z0-9]+$", RegexOptions.Compiled);

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

        public HttpResponseMessage GetMemberTitle()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var membetTitleList = _constantService.GetConstantByConstantType(ConstantTypeEnum.MemberTitleType);

                    processStatus.Result = membetTitleList;

                    processStatus.Message.Header = "GetMemberTitle";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "GetMemberTitle";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "GetMemberTitle";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetActivityType()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var activityTypeList = _activityTypeService.GetAllActivityTypes();

                    processStatus.Result = activityTypeList;

                    processStatus.Message.Header = "GetActivityType";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "GetActivityType";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "GetActivityType";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetStoreActivityCategories()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var sectoreCategories = _categoryService.GetMainCategories();
                    var memberMainPartyId = member.MainPartyId;
                    int storeMainPartyId = _memberStoreService.GetMemberStoreByMemberMainPartyId(memberMainPartyId).StoreMainPartyId.Value;
                    var storeSectors = _storeSectorService.GetStoreSectorsByMainPartyId(storeMainPartyId);
                    MTStoreActivityModel mTStoreActivityModel = new MTStoreActivityModel();

                    foreach (var item in sectoreCategories)
                    {
                        var itemCategory = new
                        {
                            Value = item.CategoryId.ToString(),
                            Text = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                        };

                        mTStoreActivityModel.Categories.Add(itemCategory);
                    }
                    foreach (var item in storeSectors)
                    {
                        var category = _categoryService.GetCategoryByCategoryId(item.CategoryId);
                        var itemSectors = new
                        {
                            Value = item.StoreSectorId.ToString(),
                            Text = !string.IsNullOrEmpty(category.CategoryContentTitle) ? category.CategoryContentTitle : category.CategoryName
                        };
                        mTStoreActivityModel.StoreActivityCategories.Add(itemSectors);
                    }

                    processStatus.Result = mTStoreActivityModel;
                    processStatus.Message.Header = "GetStoreActivityCategories";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "GetStoreActivityCategories";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "GetStoreActivityCategories";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //----------------------------------------------------------------------------FİRMA ÜYELİĞİ----------------------------------------------------------------------------\\

        public HttpResponseMessage InstitutionalStep(InstutionalStepObject instutionalStepObject)
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
                                    var memberTitleList = _constantService.GetConstantByConstantType(ConstantTypeEnum.MemberTitleType).Where(x => x.ConstantId == instutionalStepObject.MemberTitleType).SingleOrDefault();
                                    if (memberTitleList != null)
                                    {
                                        instutionalStepObject.NextStep = "InstitutionalStep1";

                                        processStatus.Result = instutionalStepObject;
                                        processStatus.Message.Header = "InstitutionalStep";
                                        processStatus.Message.Text = "Başarılı";
                                        processStatus.Status = true;
                                    }
                                    else
                                    {
                                        processStatus.Result = "Geçersiz üye tipi";
                                        processStatus.Message.Header = "InstitutionalStep";
                                        processStatus.Message.Text = "Başarısız";
                                        processStatus.Status = false;
                                    }
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

        public HttpResponseMessage InstitutionalStep1(HttpPostedFileBase uploadedLogo, InstutionalStepObject instutionalStepObject, bool InsertButton)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    if (member.MemberType == (byte)MemberType.Enterprise)
                    {
                        processStatus.Result = "Zaten Kurumsal Üyesiniz";
                        processStatus.Message.Header = "InstitutionalStep1";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    if (member.MemberType == 0)
                    {
                        processStatus.Result = "İşlem zaman aşımına uğradı lütfen tekrar deneyin";
                        processStatus.Message.Header = "InstitutionalStep1";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        string StoreLogoThumb100x100Folder = ConfigurationManager.AppSettings["StoreLogoThumb100x100Folder"].ToString();
                        string StoreLogoThumb300x200Folder = ConfigurationManager.AppSettings["StoreLogoThumb300x200Folder"].ToString();
                        string StoreLogoFolder = ConfigurationManager.AppSettings["StoreLogoFolder"].ToString();

                        if (InsertButton)
                        {
                            if (!string.IsNullOrWhiteSpace(instutionalStepObject.StoreLogo))
                            {
                                FileHelpers.Delete(StoreLogoFolder + instutionalStepObject.StoreLogo);
                                FileHelpers.Delete(StoreLogoThumb100x100Folder + instutionalStepObject.StoreLogo);
                                FileHelpers.Delete(StoreLogoThumb300x200Folder + instutionalStepObject.StoreLogo);
                                //FileHelpers.Delete(AppSettings.StoreLogoThumb170x90Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                                //FileHelpers.Delete(AppSettings.StoreLogoThumb200x100Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                                //FileHelpers.Delete(AppSettings.StoreLogoThumb55x40Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                                //FileHelpers.Delete(AppSettings.StoreLogoThumb75x75Folder + SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreLogo);
                            }
                            string fileName = String.Empty;

                            if (uploadedLogo.ContentLength > 0)
                            {
                                //var thumns = new Dictionary<string, string>();
                                //thumns.Add(AppSettings.StoreLogoThumb110x110Folder, "110x110");
                                //thumns.Add(AppSettings.StoreLogoThumb150x90Folder, "150x90");
                                //thumns.Add(AppSettings.StoreLogoThumb170x90Folder, "170x90");
                                //thumns.Add(AppSettings.StoreLogoThumb200x100Folder, "200x100");
                                //thumns.Add(AppSettings.StoreLogoThumb55x40Folder, "55x40");
                                //thumns.Add(AppSettings.StoreLogoThumb75x75Folder, "75x75");
                                //fileName = FileHelpers.ImageResize(AppSettings.StoreLogoFolder, file, thumns);
                                var thumns = new Dictionary<string, string>();

                                thumns.Add(StoreLogoThumb100x100Folder, "100x100");
                                thumns.Add(StoreLogoThumb300x200Folder, "300x200");
                                fileName = FileHelpers.ImageResize(StoreLogoFolder, uploadedLogo, thumns);
                            }

                            instutionalStepObject.StoreLogo = fileName;
                            instutionalStepObject.NextStep = "InstitutionalStep3";

                            processStatus.Result = instutionalStepObject;
                            processStatus.Message.Header = "InstitutionalStep1";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                        }
                        else
                        {
                            string fileName = String.Empty;

                            if (uploadedLogo.ContentLength > 0)
                            {
                                if (!string.IsNullOrWhiteSpace(instutionalStepObject.StoreLogo))
                                {
                                    FileHelpers.Delete(StoreLogoFolder + instutionalStepObject.StoreLogo);
                                }

                                //fileName = FileHelpers.ImageThumbnail(AppSettings.StoreLogoFolder, file, 100, FileHelpers.ThumbnailType.Width);

                                var thumns = new Dictionary<string, string>();
                                thumns.Add(StoreLogoThumb100x100Folder, "100x100");
                                thumns.Add(StoreLogoThumb300x200Folder, "300x200");
                                fileName = FileHelpers.ImageResize(StoreLogoFolder, uploadedLogo, thumns);

                                instutionalStepObject.StoreLogo = fileName;
                                instutionalStepObject.NextStep = "InstitutionalStep3";

                                processStatus.Result = instutionalStepObject;
                                processStatus.Message.Header = "InstitutionalStep1";
                                processStatus.Message.Text = "Başarılı";
                                processStatus.Status = true;
                            }
                        }
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "InstitutionalStep1";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "InstitutionalStep1";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage InstitutionalStep3(InstutionalStepObject instutionalStepObject)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    if (member.MemberType == 0)
                    {
                        processStatus.Result = "İşlem zaman aşımına uğradı lütfen tekrar deneyin";
                        processStatus.Message.Header = "InstitutionalStep3";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    if (member.MemberType == (byte)MemberType.Enterprise)
                    {
                        processStatus.Result = "Zaten Kurumsal Üyesiniz";
                        processStatus.Message.Header = "InstitutionalStep3";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        if (instutionalStepObject.StoreUrlName == null)
                        {
                            processStatus.Result = "Firma Url boş";
                            processStatus.Message.Header = "InstitutionalStep3";
                            processStatus.Message.Text = "Başarısız";
                            processStatus.Status = false;
                        }
                        if (!sUserNameAllowedRegEx.IsMatch(instutionalStepObject.StoreUrlName))
                        {
                            instutionalStepObject.StoreUrlName = UrlBuilder.ToUrl(instutionalStepObject.StoreShortName);
                        }
                        var store = _storeService.GetStoreByStoreUrlName(instutionalStepObject.StoreUrlName);
                        if (store != null)
                        {
                            //ViewData["storeUrlCheck"] = "false";
                            //ViewData["leftMenu"] = LeftMenuConstants.CreateLeftMenuModel(LeftMenuConstants.GroupName.MyAccount);
                            //var model1 = SessionMembershipViewModel.MembershipViewModel;
                            //model1.ActivityItems = _activityTypeService.GetAllActivityTypes();
                            //model1.MembershipModel = model.MembershipModel;
                            //return View(model1);

                            processStatus.Result = "Firma Urlsi sistemde vardır.";
                            processStatus.Message.Header = "InstitutionalStep3";
                            processStatus.Message.Text = "Başarısız";
                            processStatus.Status = false;
                        }
                        else
                        {
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreName = instutionalStepObject.StoreName;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreWeb = instutionalStepObject.StoreWeb;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.ActivityName = instutionalStepObject.ActivityName;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreCapital = instutionalStepObject.StoreCapital;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreUrlName = instutionalStepObject.StoreUrlName;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreShortName = instutionalStepObject.StoreShortName;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEstablishmentDate = instutionalStepObject.StoreEstablishmentDate;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEmployeesCount = instutionalStepObject.StoreEmployeesCount;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreEndorsement = instutionalStepObject.StoreEndorsement;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreAbout = instutionalStepObject.StoreAbout;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentEmail = instutionalStepObject.PurchasingDepartmentEmail;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.PurchasingDepartmentName = instutionalStepObject.PurchasingDepartmentName;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.StoreType = instutionalStepObject.StoreType;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.ReceiveEmail = false;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxNumber = instutionalStepObject.TaxNumber;
                            //SessionMembershipViewModel.MembershipViewModel.MembershipModel.TaxOffice = instutionalStepObject.TaxOffice;
                            //return RedirectToAction("InstitutionalStep4", "MemberType");
                            instutionalStepObject.NextStep = "InstitutionalStep4";

                            processStatus.Result = instutionalStepObject;
                            processStatus.Message.Header = "InstitutionalStep3";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                        }
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "InstitutionalStep3";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "InstitutionalStep3";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage InstitutionalStep4(InstutionalStepObject instutionalStepObject)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    if (instutionalStepObject.MemberTitleType == 0)
                    {
                        processStatus.Result = "İşlem zaman aşımına uğradı lütfen tekrar deneyin";
                        processStatus.Message.Header = "InstitutionalStep4";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    if (member.MemberType == (byte)MemberType.Enterprise)
                    {
                        processStatus.Result = "Zaten Kurumsal Üyesiniz";
                        processStatus.Message.Header = "InstitutionalStep4";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        Store insertedStore = null;
                        var storeMainParty = new MainParty
                        {
                            Active = false,
                            MainPartyType = (byte)MainPartyType.Firm,
                            MainPartyRecordDate = DateTime.Now,
                            MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instutionalStepObject.StoreName.ToLower()),
                        };
                        _memberService.InsertMainParty(storeMainParty);

                        member.MemberTitleType = instutionalStepObject.MemberTitleType;
                        member.MemberType = (byte)MemberType.Enterprise;
                        member.FastMemberShipType = (byte)FastMembershipType.Normal;

                        int memberMainPartyId = member.MainPartyId;
                        string memberNo = "##";
                        for (int i = 0; i < 7 - memberMainPartyId.ToString().Length; i++)
                        {
                            memberNo = memberNo + "0";
                        }
                        memberNo = memberNo + memberMainPartyId;
                        member.MemberNo = memberNo;

                        _memberService.UpdateMember(member);

                        var storeMainPartyId = storeMainParty.MainPartyId;

                        var packet = _packetService.GetPacketByIsStandart(true);

                        var store = new Store
                        {
                            MainPartyId = storeMainPartyId,
                            PacketId = packet.PacketId,
                            StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instutionalStepObject.StoreName.ToLower()),
                            StoreEMail = member.MemberEmail,
                            StoreWeb = instutionalStepObject.StoreWeb,
                            StoreLogo = instutionalStepObject.StoreLogo,
                            StoreActiveType = (byte)PacketStatu.Inceleniyor,
                            StorePacketBeginDate = DateTime.Now,
                            StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                            StoreAbout = instutionalStepObject.StoreAbout,
                            StoreRecordDate = DateTime.Now,
                            StoreEstablishmentDate = instutionalStepObject.StoreEstablishmentDate.HasValue ? instutionalStepObject.StoreEstablishmentDate.Value : 0,
                            StoreCapital = instutionalStepObject.StoreCapital,
                            StoreEmployeesCount = instutionalStepObject.StoreEmployeesCount,
                            StoreEndorsement = instutionalStepObject.StoreEndorsement,
                            StoreType = instutionalStepObject.StoreType,
                            TaxOffice = instutionalStepObject.TaxOffice,
                            TaxNumber = instutionalStepObject.TaxNumber,
                            StoreUrlName = instutionalStepObject.StoreUrlName,
                            StoreShortName = instutionalStepObject.StoreShortName,
                            PurchasingDepartmentEmail = instutionalStepObject.PurchasingDepartmentEmail,
                            PurchasingDepartmentName = instutionalStepObject.PurchasingDepartmentName,
                            FounderText = string.Empty,
                            GeneralText = string.Empty,
                            HistoryText = string.Empty,
                            PhilosophyText = string.Empty,
                            ViewCount = 0,
                            SingularViewCount = 0,
                            StoreShowcase = false,
                        };

                        string storeNo = "###";
                        for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                        {
                            storeNo = storeNo + "0";
                        }
                        storeNo = storeNo + storeMainPartyId;
                        store.StoreNo = storeNo;

                        _storeService.InsertStore(store);

                        storeMainPartyId = store.MainPartyId;
                        insertedStore = store;

                        var address = _addressService.GetFisrtAddressByMainPartyId(member.MainPartyId);
                        if (address != null)
                        {
                            address.MainPartyId = storeMainPartyId;
                            _addressService.UpdateAddress(address);
                        }

                        //var phone = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == (byte)PhoneType.Phone);
                        //foreach (var phoneItem in phone.ToList())
                        //{
                        //    phoneItem.MainPartyId = storeMainPartyId;
                        //    entities.SaveChanges();
                        //}
                        //var phoneGsm = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == (Byte)PhoneType.Gsm).FirstOrDefault();
                        //if(phoneGsm!=null)
                        //{
                        // phoneGsm.MainPartyId = storeMainPartyId;
                        // entities.SaveChanges();
                        //}
                        //var phoneFax = entities.Phones.Where(x => x.MainPartyId == AuthenticationUser.Membership.MainPartyId && x.PhoneType == null).FirstOrDefault();
                        //if(phoneFax!=null)
                        //{
                        //  phoneFax.MainPartyId = storeMainPartyId;
                        //  entities.SaveChanges();
                        //}

                        var phones = _phoneService.GetPhonesByMainPartyId(member.MainPartyId);
                        foreach (var phoneItem in phones)
                        {
                            phoneItem.MainPartyId = storeMainPartyId;
                            _phoneService.UpdatePhone(phoneItem);
                        }

                        if (instutionalStepObject.ActivityName != null)
                        {
                            for (int i = 0; i < instutionalStepObject.ActivityName.Length; i++)
                            {
                                if (instutionalStepObject.ActivityName.GetValue(i).ToString() != "false")
                                {
                                    var storeActivityType = new StoreActivityType
                                    {
                                        StoreId = storeMainPartyId,
                                        ActivityTypeId = Convert.ToByte(instutionalStepObject.ActivityName.GetValue(i))
                                    };
                                    _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                                }
                            }
                        }
                        if (instutionalStepObject.StoreActivityCategoryIdList != null && instutionalStepObject.StoreActivityCategoryIdList.Length != 0)
                        {
                            foreach (var storeAct in instutionalStepObject.StoreActivityCategoryIdList)
                            {
                                var storeActivityCategory = new StoreActivityCategory
                                {
                                    MainPartyId = storeMainPartyId,
                                    CategoryId = int.Parse(storeAct)
                                };
                                _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);
                            }
                        }
                        var memberStore = new MemberStore
                        {
                            MemberMainPartyId = member.MainPartyId,
                            StoreMainPartyId = storeMainPartyId,
                            MemberStoreType = (byte)MemberStoreType.Owner
                        };
                        _memberStoreService.InsertMemberStore(memberStore);

                        //firma logo düzenle
                        if (!string.IsNullOrEmpty(insertedStore.StoreName))
                        {
                            string StoreLogoFolder = ConfigurationManager.AppSettings["StoreLogoFolder"].ToString();
                            string resizeStoreLogoFolder = ConfigurationManager.AppSettings["ResizeStoreLogoFolder"].ToString();
                            string StoreLogoThumbSizes = ConfigurationManager.AppSettings["StoreLogoThumbSizes"].ToString();

                            string storeLogoFolder = System.Web.HttpContext.Current.Server.MapPath(StoreLogoFolder);
                            string resizeStoreFolder = System.Web.HttpContext.Current.Server.MapPath(resizeStoreLogoFolder);
                            string storeLogoThumbSize = StoreLogoThumbSizes;
                            List<string> thumbSizesForStoreLogo = new List<string>();
                            thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                            var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, insertedStore.MainPartyId.ToString()));
                            di.CreateSubdirectory("thumbs");
                            string oldStoreLogoImageFilePath = string.Format("{0}{1}", storeLogoFolder, insertedStore.StoreLogo);
                            if (System.IO.File.Exists(oldStoreLogoImageFilePath))
                            {
                                // eski logoyu kopyala, varsa ustune yaz
                                string newStoreLogoImageFilePath = resizeStoreFolder + insertedStore.MainPartyId.ToString() + "\\";
                                string newStoreLogoImageFileName = insertedStore.StoreName.ToImageFileName() + "_logo.jpg";
                                System.IO.File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);
                                bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                                newStoreLogoImageFilePath + "thumbs\\" + insertedStore.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                                insertedStore = _storeService.GetStoreByMainPartyId(insertedStore.MainPartyId);
                                insertedStore.StoreLogo = newStoreLogoImageFileName;
                                _storeService.UpdateStore(insertedStore);
                            }
                        }

                        #region bireyseldenkurumsalagecis

                        //var settings = ConfigurationManager.AppSettings;
                        MailMessage mail = new MailMessage();
                        MessagesMT mailT = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");
                        mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                        mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                        mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                        string template = mailT.MessagesMTPropertie;
                        template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword).Replace("#firmaadi#", instutionalStepObject.StoreName);
                        mail.Body = template;                                                            //Mailin içeriği
                        mail.IsBodyHtml = true;
                        mail.Priority = MailPriority.Normal;
                        SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                        sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                        sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                        sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                        sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                        sc.Send(mail);

                        #endregion bireyseldenkurumsalagecis

                        #region bilgimakina

                        MailMessage mailb = new MailMessage();
                        MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                        mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                        mailb.To.Add("bilgi@makinaturkiye.com");
                        mailb.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                        //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                        //templatet = messagesmttemplate.MessagesMTPropertie;
                        string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                        bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Firma Üyelik");
                        mailb.Body = bilgimakinaicin;
                        mailb.IsBodyHtml = true;
                        mailb.Priority = MailPriority.Normal;
                        SmtpClient scr1 = new SmtpClient();
                        scr1.Port = 587;
                        scr1.Host = "smtp.gmail.com";
                        scr1.EnableSsl = true;
                        scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                        scr1.Send(mailb);

                        #endregion bilgimakina

                        //return RedirectToAction("Index", "Home", new { gelenSayfa = "KurumsalOnay" });

                        processStatus.Result = "Kurumsal Üyeliğiniz İncelenmektedir.İncelendikten Sonra Onaylanacaktır.";
                        processStatus.Message.Header = "InstitutionalStep4";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "InstitutionalStep4";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "InstitutionalStep4";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage InstitutionalStep5(InstutionalStepObject instutionalStepObject)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    if (member.MemberType == (byte)MemberType.Enterprise)
                    {
                        processStatus.Result = "Zaten Kurumsal Üyesiniz";
                        processStatus.Message.Header = "InstitutionalStep5";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        bool hasRecord = false;
                        Store insertedStore = null;
                        try
                        {
                            member.MemberType = (byte)MemberType.Enterprise;

                            string memberNo = "##";
                            for (int i = 0; i < 7 - member.MainPartyId.ToString().Length; i++)
                            {
                                memberNo = memberNo + "0";
                            }
                            memberNo = memberNo + member.MainPartyId;
                            member.MemberNo = memberNo;
                            _memberService.UpdateMember(member);

                            var curStoreMainParty = new MainParty
                            {
                                Active = false,
                                MainPartyType = (byte)MainPartyType.Firm,
                                MainPartyRecordDate = DateTime.Now,
                                MainPartyFullName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instutionalStepObject.StoreName.ToLower()),
                            };
                            _memberService.InsertMainParty(curStoreMainParty);

                            var storeMainPartyId = curStoreMainParty.MainPartyId;

                            var packet = _packetService.GetPacketByIsStandart(true);

                            var curStore = new Store
                            {
                                StoreAbout = instutionalStepObject.StoreAbout,
                                MainPartyId = storeMainPartyId,
                                PacketId = packet.PacketId,
                                StoreActiveType = (byte)PacketStatu.Inceleniyor,
                                StoreCapital = instutionalStepObject.StoreCapital,
                                StoreEMail = member.MemberEmail,
                                StoreEmployeesCount = instutionalStepObject.StoreEmployeesCount,
                                StoreEndorsement = instutionalStepObject.StoreEndorsement,
                                StoreEstablishmentDate = instutionalStepObject.StoreEstablishmentDate,
                                StoreLogo = instutionalStepObject.StoreLogo,
                                StorePacketBeginDate = DateTime.Now,
                                StorePacketEndDate = DateTime.Now.AddDays(packet.PacketDay),
                                StoreName = CultureInfo.CurrentCulture.TextInfo.ToTitleCase(instutionalStepObject.StoreName.ToLower()),
                                StoreType = instutionalStepObject.StoreType,
                                StoreWeb = instutionalStepObject.StoreWeb,
                                StoreRecordDate = DateTime.Now,
                                TaxNumber = instutionalStepObject.TaxNumber,
                                TaxOffice = instutionalStepObject.TaxOffice,
                                PurchasingDepartmentEmail = instutionalStepObject.PurchasingDepartmentEmail,
                                PurchasingDepartmentName = instutionalStepObject.PurchasingDepartmentName,
                            };

                            string storeNo = "###";
                            for (int i = 0; i < 6 - storeMainPartyId.ToString().Length; i++)
                            {
                                storeNo = storeNo + "0";
                            }
                            storeNo = storeNo + storeMainPartyId;
                            curStore.StoreNo = storeNo;

                            _storeService.InsertStore(curStore);

                            insertedStore = curStore;

                            var address = _addressService.GetFisrtAddressByMainPartyId(member.MainPartyId);
                            if (address != null)
                            {
                                address.MainPartyId = storeMainPartyId;
                                _addressService.UpdateAddress(address);
                            }

                            var phone = _phoneService.GetPhonesByMainPartyId(member.MainPartyId);
                            foreach (var item in phone.ToList())
                            {
                                item.MainPartyId = storeMainPartyId;
                                _phoneService.UpdatePhone(item);
                            }

                            if (instutionalStepObject.ActivityName != null)
                            {
                                for (int i = 0; i < instutionalStepObject.ActivityName.Length; i++)
                                {
                                    if (instutionalStepObject.ActivityName.GetValue(i).ToString() != "false")
                                    {
                                        var storeActivityType = new StoreActivityType
                                        {
                                            StoreId = storeMainPartyId,
                                            ActivityTypeId = Convert.ToByte(instutionalStepObject.ActivityName.GetValue(i))
                                        };
                                        _storeActivityTypeService.InsertStoreActivityType(storeActivityType);
                                    }
                                }
                            }

                            if (instutionalStepObject.StoreActivityCategoryIdList != null)
                            {
                                var relCategory = instutionalStepObject.StoreActivityCategoryIdList.ToList();
                                for (int i = 0; i < relCategory.Count(); i++)
                                {
                                    var storeActivityCategory = new StoreActivityCategory
                                    {
                                        MainPartyId = storeMainPartyId,
                                        CategoryId = int.Parse(relCategory[i])
                                    };
                                    _storeActivityCategoryService.InsertStoreActivityCategory(storeActivityCategory);
                                }
                            }

                            var curMemberStore = new MemberStore
                            {
                                MemberMainPartyId = member.MainPartyId,
                                StoreMainPartyId = storeMainPartyId
                            };
                            _memberStoreService.InsertMemberStore(curMemberStore);

                            UserLog lg = new UserLog
                            {
                                LogName = "B.K",
                                LogDescription = member.MemberNo,
                                LogStatus = 1,//success
                                LogType = (byte)LogType.MemberShip,
                                CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                            };
                            // _userLogService.InsertUserLog(lg);

                            if (!string.IsNullOrEmpty(insertedStore.StoreName))
                            {
                                string StoreLogoFolder = ConfigurationManager.AppSettings["StoreLogoFolder"].ToString();
                                string resizeStoreLogoFolder = ConfigurationManager.AppSettings["ResizeStoreLogoFolder"].ToString();
                                string StoreLogoThumbSizes = ConfigurationManager.AppSettings["StoreLogoThumbSizes"].ToString();

                                string storeLogoFolder = System.Web.HttpContext.Current.Server.MapPath(StoreLogoFolder);
                                string resizeStoreFolder = System.Web.HttpContext.Current.Server.MapPath(resizeStoreLogoFolder);
                                string storeLogoThumbSize = StoreLogoThumbSizes;

                                List<string> thumbSizesForStoreLogo = new List<string>();
                                thumbSizesForStoreLogo.AddRange(storeLogoThumbSize.Split(';'));
                                var di = Directory.CreateDirectory(string.Format("{0}{1}", resizeStoreFolder, insertedStore.MainPartyId.ToString()));
                                di.CreateSubdirectory("thumbs");
                                string oldStoreLogoImageFilePath = string.Format("{0}{1}", storeLogoFolder, insertedStore.StoreLogo);
                                if (System.IO.File.Exists(oldStoreLogoImageFilePath))
                                {
                                    // eski logoyu kopyala, varsa ustune yaz
                                    string newStoreLogoImageFilePath = resizeStoreFolder + insertedStore.MainPartyId.ToString() + "\\";
                                    string newStoreLogoImageFileName = insertedStore.StoreName.ToImageFileName() + "_logo.jpg";
                                    System.IO.File.Copy(oldStoreLogoImageFilePath, newStoreLogoImageFilePath + newStoreLogoImageFileName, true);
                                    bool thumbResult = ImageProcessHelper.ImageResize(newStoreLogoImageFilePath + newStoreLogoImageFileName,
                                    newStoreLogoImageFilePath + "thumbs\\" + insertedStore.StoreName.ToImageFileName(), thumbSizesForStoreLogo);

                                    insertedStore = _storeService.GetStoreByMainPartyId(insertedStore.MainPartyId);
                                    insertedStore.StoreLogo = newStoreLogoImageFileName;

                                    _storeService.UpdateStore(insertedStore);
                                }
                            }

                            #region bireyseldenkurumsalagecis

                            //var settings = ConfigurationManager.AppSettings;
                            MailMessage mail = new MailMessage();
                            MessagesMT mailT = _messagesMTService.GetMessagesMTByMessageMTName("storedesc");
                            mail.From = new MailAddress(mailT.Mail, mailT.MailSendFromName); //Mailin kimden gittiğini belirtiyoruz
                            mail.To.Add(member.MemberEmail);                                                              //Mailin kime gideceğini belirtiyoruz
                            mail.Subject = mailT.MessagesMTTitle;                                              //Mail konusu
                            string template = mailT.MessagesMTPropertie;
                            template = template.Replace("#kullaniciadi#", member.MemberName + " " + member.MemberSurname).Replace("#uyeeposta#", member.MemberEmail).Replace("#kullanicisifre#", member.MemberPassword).Replace("#firmaadi#", instutionalStepObject.StoreName);
                            mail.Body = template;                                                            //Mailin içeriği
                            mail.IsBodyHtml = true;
                            mail.Priority = MailPriority.Normal;
                            SmtpClient sc = new SmtpClient();                                                //sc adında SmtpClient nesnesi yaratıyoruz.
                            sc.Port = 587;                                                                   //Gmail için geçerli Portu bildiriyoruz
                            sc.Host = "smtp.gmail.com";                                                      //Gmailin smtp host adresini belirttik
                            sc.EnableSsl = true;                                                             //SSL’i etkinleştirdik
                            sc.Credentials = new NetworkCredential(mailT.Mail, mailT.MailPassword); //Gmail hesap kontrolü için bilgilerimizi girdi
                            sc.Send(mail);

                            #endregion bireyseldenkurumsalagecis

                            #region bilgimakina

                            MailMessage mailb = new MailMessage();
                            MessagesMT mailTmpInf = _messagesMTService.GetMessagesMTByMessageMTName("bilgimakinasayfası");

                            mailb.From = new MailAddress(mailTmpInf.Mail, mailTmpInf.MailSendFromName);
                            mailb.To.Add("bilgi@makinaturkiye.com");
                            mailb.Subject = "Firma Üyeliği " + member.MemberName + " " + member.MemberSurname;
                            //var messagesmttemplate = entities.MessagesMTs.Where(c => c.MessagesMTId == 2).SingleOrDefault();
                            //templatet = messagesmttemplate.MessagesMTPropertie;
                            string bilgimakinaicin = mailTmpInf.MessagesMTPropertie;
                            bilgimakinaicin = bilgimakinaicin.Replace("#kullanicimiz#", member.MemberName).Replace("#kullanicisoyadi#", member.MemberSurname).Replace("#kullanicitipi#", "Hızlı Üyelik");
                            mailb.Body = bilgimakinaicin;
                            mailb.IsBodyHtml = true;
                            mailb.Priority = MailPriority.Normal;
                            SmtpClient scr1 = new SmtpClient();
                            scr1.Port = 587;
                            scr1.Host = "smtp.gmail.com";
                            scr1.EnableSsl = true;
                            scr1.Credentials = new NetworkCredential(mailTmpInf.Mail, mailTmpInf.MailPassword);
                            scr1.Send(mailb);

                            #endregion bilgimakina

                            hasRecord = true;
                        }
                        catch (Exception ex)
                        {
                            var lg = new UserLog
                            {
                                LogDescription = ex.ToString(),
                                LogShortDescription = ex.Message,
                                LogName = "B.K",
                                LogType = (byte)LogType.MemberShip,
                                LogStatus = 0,
                                CreatedDate = DateTime.Now.ToString("dd-MM-yyyy HH:mm:ss")
                            };

                            // _userLogService.InsertUserLog(lg);
                        }

                        if (hasRecord)
                        {
                            processStatus.Result = "İşlem başarılı";
                            processStatus.Message.Header = "InstitutionalStep5";
                            processStatus.Message.Text = "Başarılı";
                            processStatus.Status = true;
                        }
                        else
                        {
                            processStatus.Result = "İşlem sırasında bir hata oluştu!";
                            processStatus.Message.Header = "InstitutionalStep5";
                            processStatus.Message.Text = "Başarısız";
                            processStatus.Status = false;
                        }
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "InstitutionalStep5";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "InstitutionalStep5";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        //----------------------------------------------------------------------------BİREYSEL ÜYELİK----------------------------------------------------------------------------\\
        public HttpResponseMessage Individual(InstutionalStepObject model, string type, string memberType, string TextInstitutionalPhoneAreaCode, string TextInstitutionalPhoneAreaCode2, string TextInstitutionalFaxAreaCode, Nullable<byte> GsmType, string DropDownInstitutionalPhoneAreaCode, string DropDownInstitutionalPhoneAreaCode2, string DropDownInstitutionalFaxAreaCode, string sonuc, string error, string urunNo, string uyeNo, string mtypePage)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    processStatus.Result = "Geliştirme devam ediyoruz.";
                    processStatus.Message.Header = "Individual";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Individual";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Individual";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }
    }
}