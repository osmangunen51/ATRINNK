using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Entities.Tables.Stores;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Stores;
using System;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class FavoriteController : BaseApiController
    {
        private readonly IProductService _productService;
        private readonly IMemberService _memberService;
        private readonly IFavoriteStoreService _favoriteStoreService;
        private readonly IStoreService _storeService;
        private readonly IFavoriteProductService _favoriteProductService;

        public FavoriteController()
        {
            _productService = EngineContext.Current.Resolve<IProductService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _favoriteStoreService = EngineContext.Current.Resolve<IFavoriteStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _favoriteProductService = EngineContext.Current.Resolve<IFavoriteProductService>();
        }

        //public FavoriteController(IProductService productService,
        //                            IMemberService memberService,
        //                            IFavoriteStoreService favoriteStoreService,
        //                            IStoreService storeService,
        //                            IFavoriteProductService favoriteProductService)
        //{
        //    this._productService = productService;
        //    this._memberService = memberService;
        //    this._favoriteStoreService = favoriteStoreService;
        //    this._favoriteProductService = favoriteProductService;
        //    this._storeService = storeService;
        //}
        public HttpResponseMessage GetFavoriteProductsByLoginUser()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var favoriteProducts = _favoriteProductService.GetFavoriteProducts().Where(x => x.MainPartyId == member.MainPartyId).ToList();
                    processStatus.Result = favoriteProducts;
                    processStatus.TotolRowCount = favoriteProducts.Count;
                    processStatus.Message.Header = "Favorite Products";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Login üye bulunamadı";
                    processStatus.Message.Header = "Favorite Products";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Favorite Products";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage InsertFavoriteProducts(int productId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    //var favoriteProducts = _favoriteProductService.GetFavoriteProducts().Where(x => x.MainPartyId == member.MainPartyId && x.ProductId == productId).ToList();
                    var favoriteProducts = _favoriteProductService.GetFavoriteProductByMainPartyIdWithProductId(member.MainPartyId, productId);

                    if (favoriteProducts != null)
                    {
                        processStatus.Result = "Bu ürünü zaten favorilere eklemişsiniz!";
                        processStatus.Message.Header = "add Favorite Products";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                    else
                    {
                        var comminProduct = _productService.GetProductByProductId(productId);
                        var favoriteProduct = new FavoriteProduct()
                        {
                            CreatedDate = DateTime.Now,
                            MainPartyId = member.MainPartyId,
                            ProductId = comminProduct.ProductId
                        };

                        _favoriteProductService.InsertFavoriteProduct(favoriteProduct);

                        processStatus.Result = "Ürün başarıyla favorilere eklenmiştir.";
                        processStatus.Message.Header = "add Favorite Products";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "add Favorite Products";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "add Favorite Products";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteFavoriteProducts(int productId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var favoriteProduct = _favoriteProductService.GetFavoriteProducts().Where(x => x.MainPartyId == member.MainPartyId && x.ProductId == productId).ToList();
                    if (favoriteProduct.Count != 0)
                    {
                        foreach (var favProd in favoriteProduct)
                            _favoriteProductService.DeleteFavoriteProduct(favProd);

                        processStatus.Result = "Ürün başarıyla favorilerden silinmiştir.";
                        processStatus.Message.Header = "delete Favorite Products";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Favorilerinizde olmayan ürünü çıkarmaya çalışıyorsunuz.";
                        processStatus.Message.Header = "delete Favorite Products";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "delete Favorite Products";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "delete Favorite Products";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetFavoriteStoresByLoginUser()
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var favoriteStores = _favoriteStoreService.GetFavoriteStoresByMemberMainPartyId(member.MainPartyId).ToList();
                    processStatus.Result = favoriteStores;
                    processStatus.TotolRowCount = favoriteStores.Count;
                    processStatus.Message.Header = "Favorite Stores";
                    processStatus.Message.Text = "Başarılı";
                    processStatus.Status = true;
                }
                else
                {
                    processStatus.Result = "Oturum açmadan bu işlemi yapamazsınız";
                    processStatus.Message.Header = "Favorite Stores";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Favorite Stores";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage InsertFavoriteStores(int storesId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var existFavoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(member.MainPartyId, storesId);
                    if (existFavoriteStore == null)
                    {
                        var comminStore = _storeService.GetStoreByMainPartyId(storesId);
                        var favoriteStore = new FavoriteStore()
                        {
                            MemberMainPartyId = member.MainPartyId,
                            StoreMainPartyId = comminStore.MainPartyId
                        };

                        _favoriteStoreService.InsertFavoriteStore(favoriteStore);

                        processStatus.Result = "Firma başarıyla favorilere eklenmiştir.";
                        processStatus.Message.Header = "add Favorite Store";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Bu Firmayı zaten favorilere eklemişsiniz!";
                        processStatus.Message.Header = "add Favorite Store";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Result = "Login üye bulunamadı";
                    processStatus.Message.Header = "add Favorite Store";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "add Favorite Store";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage DeleteFavoriteStore(int storesId)
        {
            ProcessResult processStatus = new ProcessResult();

            try
            {
                var LoginUserEmail = Request.CheckLoginUserClaims().LoginMemberEmail;

                var member = !string.IsNullOrEmpty(LoginUserEmail) ? _memberService.GetMemberByMemberEmail(LoginUserEmail) : null;

                if (member != null)
                {
                    var favoriteStore = _favoriteStoreService.GetFavoriteStoreByMemberMainPartyIdWithStoreMainPartyId(member.MainPartyId, storesId);
                    if (favoriteStore != null)
                    {
                        _favoriteStoreService.DeleteFavoriteStore(favoriteStore);

                        processStatus.Result = "Firma başarıyla favorilerden silinmiştir.";
                        processStatus.Message.Header = "delete Favorite Store";
                        processStatus.Message.Text = "Başarılı";
                        processStatus.Status = true;
                    }
                    else
                    {
                        processStatus.Result = "Favorilerinizde olmayan Firmayı çıkarmaya çalışıyorsunuz.";
                        processStatus.Message.Header = "delete Favorite Store";
                        processStatus.Message.Text = "Başarısız";
                        processStatus.Status = false;
                    }
                }
                else
                {
                    processStatus.Result = "Login üye bulunamadı";
                    processStatus.Message.Header = "delete Favorite Store";
                    processStatus.Message.Text = "Başarısız";
                    processStatus.Status = false;
                }
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