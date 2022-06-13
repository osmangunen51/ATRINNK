using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Media;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.ImageHelpers;

namespace MakinaTurkiye.Api.Controllers
{
    public class SearchController : BaseApiController
    {
        private readonly ISearchService SearchService;
        private readonly IProductService _productService;
        private readonly IAddressService _addressService;
        private readonly IPictureService _pictureService;
        private readonly IMemberStoreService _memberStoreService;
        private readonly IStoreService _storeService;
        private readonly IMemberService _memberService;
        private readonly IPhoneService _phoneService;
        private readonly IProductComplainService _productComplainService;
        private readonly IMessagesMTService _messagesMTService;
        private readonly ICategoryService _categoryService;
        public SearchController()
        {
            SearchService = EngineContext.Current.Resolve<ISearchService>();
            _productService = EngineContext.Current.Resolve<IProductService>();
            _addressService = EngineContext.Current.Resolve<IAddressService>();
            _pictureService = EngineContext.Current.Resolve<IPictureService>();
            _memberStoreService = EngineContext.Current.Resolve<IMemberStoreService>();
            _storeService = EngineContext.Current.Resolve<IStoreService>();
            _memberService = EngineContext.Current.Resolve<IMemberService>();
            _phoneService = EngineContext.Current.Resolve<IPhoneService>();
            _productComplainService = EngineContext.Current.Resolve<IProductComplainService>();
            _messagesMTService = EngineContext.Current.Resolve<IMessagesMTService>();
            _categoryService = EngineContext.Current.Resolve<ICategoryService>();
        }

        public HttpResponseMessage GetSuggestions(string query)
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                SearchAutoCompleteResult Sonuc = new SearchAutoCompleteResult();
                List<string> EklenenListesi = new List<string>();
                if (!string.IsNullOrEmpty(query))
                {
                    var DbSonucListesi = SearchService.SearchSuggest(query);
                    int EklenenSayisi = 1;
                    foreach (var item in DbSonucListesi.Distinct().OrderByDescending(x => x.Score).ToList())
                    {
                        SearchAutoCompleteItem ItemOneri;

                        ItemOneri = new SearchAutoCompleteItem()
                        {
                            Name = item.Name,
                            data = new data() { category = string.Format("{0}", item.Category) },
                            Url = item.Url
                        };

                        if (!EklenenListesi.Contains(ItemOneri.Name.Trim()))
                        {
                            EklenenSayisi++;
                            Sonuc.suggestions.Add(ItemOneri);
                            EklenenListesi.Add(ItemOneri.Name.Trim());
                            if (EklenenSayisi > 7)
                                EklenenListesi.Add(ItemOneri.Name);
                            if (EklenenSayisi > 7)
                            {
                                break;
                            }
                        }
                    }
                    DbSonucListesi = SearchService.SearchCategory(query);
                    #region Kategori
                    var DtKategori = DbSonucListesi.Where(x => x.Category == "Kategoriler").ToList();
                    DtKategori = DtKategori.Distinct().OrderByDescending(x => x.Score).ToList();
                    if (DtKategori.Count > 0)
                    {
                        EklenenSayisi = 0;
                        EklenenListesi.Clear();
                        SearchAutoCompleteItem ItemOneri;
                        SearchAutoCompleteItem ItemCategory;
                        ItemCategory = new SearchAutoCompleteItem()
                        {
                            Name = "",
                            data = new data() { category = "Kategoriler" },
                            Url = "#",
                            Value = ""
                        };
                        Sonuc.suggestions.Add(ItemCategory);
                        foreach (var item in DtKategori)
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url,
                                Value = item.Value,
                            };
                            if (!EklenenListesi.Contains(ItemOneri.Name))
                            {
                                EklenenSayisi++;
                                Sonuc.suggestions.Add(ItemOneri);
                                EklenenListesi.Add(ItemOneri.Name);
                                if (EklenenSayisi > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    #endregion Kategori

                    #region Firma
                    var DtFirma = DbSonucListesi.Where(x => x.Category == "Firmalar").ToList();
                    DtFirma = DtFirma.Distinct().OrderByDescending(x => x.Score).ToList();
                    if (DtFirma.Count > 0)
                    {
                        EklenenSayisi = 0;
                        EklenenListesi.Clear();
                        SearchAutoCompleteItem ItemCategory;
                        ItemCategory = new SearchAutoCompleteItem()
                        {
                            Name = "",
                            data = new data() { category = "Firmalar" },
                            Url = "#",
                            Value = ""
                        };
                        Sonuc.suggestions.Add(ItemCategory);

                        foreach (var item in DtFirma)
                        {
                            SearchAutoCompleteItem ItemOneri;
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url,
                                Value = item.Value,
                            };
                            if (!EklenenListesi.Contains(ItemOneri.Name))
                            {
                                EklenenSayisi++;
                                Sonuc.suggestions.Add(ItemOneri);
                                EklenenListesi.Add(ItemOneri.Name);
                                if (EklenenSayisi > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    #endregion Firma

                    #region Video
                    var DtVideo = DbSonucListesi.Where(x => x.Category == "Videolar").ToList();
                    DtVideo = DtVideo.Distinct().OrderByDescending(x => x.Score).ToList();

                    if (DtVideo.Count > 0)
                    {
                        EklenenSayisi = 0;
                        EklenenListesi.Clear();

                        SearchAutoCompleteItem ItemCategory;
                        ItemCategory = new SearchAutoCompleteItem()
                        {
                            Name = "",
                            data = new data() { category = "Videolar" },
                            Url = "#",
                            Value = ""
                        };
                        Sonuc.suggestions.Add(ItemCategory);
                        foreach (var item in DtVideo)
                        {
                            SearchAutoCompleteItem ItemOneri;
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url,
                                Value = item.Value,
                            };
                            if (!EklenenListesi.Contains(ItemOneri.Name))
                            {
                                EklenenSayisi++;
                                Sonuc.suggestions.Add(ItemOneri);
                                EklenenListesi.Add(ItemOneri.Name);
                                if (EklenenSayisi > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }

                    #endregion Video

                    #region Tedarik
                    var DtTedarik = DbSonucListesi.Where(x => x.Category == "Tedarikçiler").ToList();
                    DtTedarik = DtTedarik.Distinct().OrderByDescending(x => x.Score).ToList();
                    if (DtTedarik.Count > 0)
                    {

                        EklenenSayisi = 0;
                        EklenenListesi.Clear();

                        SearchAutoCompleteItem ItemCategory;
                        ItemCategory = new SearchAutoCompleteItem()
                        {
                            Name = "",
                            data = new data() { category = "Tedarikçiler" },
                            Url = "#",
                            Value = ""
                        };
                        Sonuc.suggestions.Add(ItemCategory);

                        EklenenListesi.Clear();
                        foreach (var item in DtTedarik)
                        {
                            SearchAutoCompleteItem ItemOneri;
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url,
                                Value = item.Value,
                            };
                            if (!EklenenListesi.Contains(ItemOneri.Name))
                            {
                                EklenenSayisi++;
                                Sonuc.suggestions.Add(ItemOneri);
                                EklenenListesi.Add(ItemOneri.Name);
                                if (EklenenSayisi > 3)
                                {
                                    break;
                                }
                            }
                        }
                    }
                    #endregion Tedarik
                }
                processStatus.Message.Header = "Search İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Result = Sonuc.suggestions.Where(x => !string.IsNullOrEmpty(x.Name));
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Search İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }

        public HttpResponseMessage GetSearchWithText(int categoryId=0, int pageIndex=0, int pageSize = 50,int country= 0,int city=0,int locality=0, string SearchText = "")
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                SearchAutoCompleteResult Sonuc = new SearchAutoCompleteResult();
                List<string> EklenenListesi = new List<string>();
                if (!string.IsNullOrEmpty(SearchText) & SearchText.Length>3)
                {
                    int customFilterId = 1;
                    var products = _productService.SPWebSearch(SearchText, categoryId, customFilterId, pageIndex, pageSize);
                    foreach (var product in products)
                    {
                        string picturePath = "";
                        var picture = _pictureService.GetFirstPictureByProductId(product.ProductId);
                        if (picture != null) picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(product.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
                        product.MainPicture = picturePath;
                        if (product.MainPicture == null)
                        {
                            product.MainPicture = "";
                        }
                    }
                    processStatus.Result = products;
                    processStatus.TotolRowCount = products.TotalCount;
                    processStatus.TotolPageCount = products.TotalPages;
                    processStatus.ActiveResultRowCount = products.Count;
                }
                processStatus.Message.Header = "Search İşlemleri";
                processStatus.Message.Text = "Başarılı";
                processStatus.Status = true;
                processStatus.Error = null;
            }
            catch (Exception ex)
            {
                processStatus.Message.Header = "Search İşlemleri";
                processStatus.Message.Text = "İşlem başarısız.";
                processStatus.Status = false;
                processStatus.Result = "Hata ile karşılaşıldı!";
                processStatus.Error = ex;
            }
            return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        }



    }
}