using Trinnk.Api.View;
using Trinnk.Core.Infrastructure;
using Trinnk.Services.Search;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using Trinnk.Services.Catalog;
using Trinnk.Services.Common;
using Trinnk.Services.Media;
using Trinnk.Services.Members;
using Trinnk.Services.Messages;
using Trinnk.Services.Stores;
using Trinnk.Utilities.ImageHelpers;
using Trinnk.Entities.Tables.Catalog;
using Trinnk.Utilities.HttpHelpers;

namespace Trinnk.Api.Controllers
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

        //public HttpResponseMessage GetSearchWithText(int categoryId=0, int pageIndex=0, int pageSize = 50,int country= 0,int city=0,int locality=0, string SearchText = "")
        //{
        //    ProcessResult processStatus = new ProcessResult();
        //    try
        //    {
        //        List<string> EklenenListesi = new List<string>();
        //        if (!string.IsNullOrEmpty(SearchText))
        //        {
        //            int customFilterId = 1;
        //            var Result = _productService.SPWebSearch(SearchText, categoryId, customFilterId, pageIndex, pageSize);
        //            var rproducts = _productService.GetProductByProductsIds(Result.Select(x => x.ProductId).ToList());
        //            List<Trinnk.Api.View.Result.ProductSearchResult> TmpResult = Result.Select(Snc =>
        //                                    new Trinnk.Api.View.Result.ProductSearchResult
        //                                    {
        //                                        ProductId = Snc.ProductId,
        //                                        CurrencyCodeName = rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).GetCurrency(),
        //                                        ProductName = Snc.ProductName,
        //                                        BrandName = Snc.CategoryName,
        //                                        ModelName = Snc.ModelName,
        //                                        CategoryProductGroupName = Snc.CategoryProductGroupName,
        //                                        CategoryName = Snc.CategoryName,
        //                                        MainPicture = "",
        //                                        StoreName = "",
        //                                        MainPartyId = (int)rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId)?.MainPartyId,
        //                                        ProductPrice = (Snc.ProductPrice.HasValue ? Snc.ProductPrice.Value : 0),
        //                                        ProductPriceType = (byte)rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId)?.ProductPriceType,
        //                                        ProductPriceLast = (rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).ProductPriceLast.HasValue ? rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).ProductPriceLast.Value : 0),
        //                                        ProductPriceBegin = (rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).ProductPriceBegin.HasValue ? rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).ProductPriceBegin.Value : 0),
        //                                        HasVideo = rproducts.FirstOrDefault(x => x.ProductId == Snc.ProductId).HasVideo,
        //                                    }
        //                                ).ToList();

        //            foreach (var item in TmpResult)
        //            {
        //                string picturePath = "";
        //                var picture = _pictureService.GetFirstPictureByProductId(item.ProductId);
        //                if (picture != null)
        //                    picturePath = !string.IsNullOrEmpty(picture.PicturePath) ? "https:" + ImageHelper.GetProductImagePath(item.ProductId, picture.PicturePath, ProductImageSize.px500x375) : null;
        //                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(item.MainPartyId);
        //                var store = _storeService.GetStoreByMainPartyId(memberStore.StoreMainPartyId.Value);
        //                item.MainPicture = (picturePath == null ? "" : picturePath);
        //                item.StoreName = store.StoreName;
        //            }
        //            processStatus.Result = TmpResult;
        //            processStatus.TotolRowCount = Result.TotalCount;
        //            processStatus.TotolPageCount = Result.TotalPages;
        //            processStatus.ActiveResultRowCount = TmpResult.Count;
        //        }
        //        processStatus.Message.Header = "Search İşlemleri";
        //        processStatus.Message.Text = "Başarılı";
        //        processStatus.Status = true;
        //        processStatus.Error = null;
        //    }
        //    catch (Exception ex)
        //    {
        //        processStatus.Message.Header = "Search İşlemleri";
        //        processStatus.Message.Text = "İşlem başarısız.";
        //        processStatus.Status = false;
        //        processStatus.Result = "Hata ile karşılaşıldı!";
        //        processStatus.Error = ex;
        //    }
        //    return Request.CreateResponse(HttpStatusCode.OK, processStatus);
        //}

        public HttpResponseMessage GetSearchWithText(string searchText = "")
        {
            ProcessResult processStatus = new ProcessResult();
            try
            {
                List<string> EklenenListesi = new List<string>();
                if (!string.IsNullOrEmpty(searchText))
                {
                    MTCategorySearchModel model = new MTCategorySearchModel();

                    var productCategories = _categoryService.GetSPProductCategoryForSearchProductOneStep(searchText);
                    var topCategories = productCategories.Where(x => x.CategoryType == 0);

                    foreach (var item in topCategories)
                    {
                        int categoryId = item.CategoryId;

                        if (categoryId > 0)
                        {
                            string categoryNameUrl = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName;
                            //string categoryUrl = QueryStringBuilder.RemoveQueryString(Request.Url.ToString().Replace("/kelime-arama", ""), PAGE_INDEX_QUERY_STRING_KEY);
                            //categoryUrl = categoryUrl.Insert(categoryUrl.IndexOf("?"), UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, ""));
                            string categoryUrl = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, "", searchText);

                            //string urlCat = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);

                            var categoryItemModel = new MTCategoryItemModel
                            {
                                CategoryId = item.CategoryId,
                                CategoryName = item.CategoryName,
                                CategoryUrl = categoryUrl,
                                DefaultCategoryName = item.CategoryName,
                                ProductCount = item.ProductCount,
                                CategoryContentTitle = !string.IsNullOrEmpty(item.CategoryContentTitle) ? item.CategoryContentTitle : item.CategoryName
                            };

                            if (string.IsNullOrEmpty(item.CategoryIcon))
                            {
                                categoryItemModel.CategoryIcon = "https://trinnk.com/Content/RibbonImages/BlogCategory.png";
                            }
                            else
                            {
                                categoryItemModel.CategoryIcon = "https://trinnk.com/UserFiles/Images/CategoryIconImageFolder/" + item.CategoryIcon;
                            }
                            model.CategoryModel.TopCategoryItemModels.Add(categoryItemModel);

                            var productGroups = productCategories.Where(x => x.CategoryParentId == item.CategoryId && x.CategoryType == (byte)CategoryTypeEnum.ProductGroup);

                            foreach (var item2 in productGroups)
                            {
                                string categoryNameUrl1 = !string.IsNullOrEmpty(item2.CategoryContentTitle) ? item2.CategoryContentTitle : item2.CategoryName;

                                string categoryUrlCustom = UrlBuilder.GetCategoryUrl(item2.CategoryId, categoryNameUrl1, null, "", searchText);

                                //string urlCat1 = UrlBuilder.GetCategoryUrl(item.CategoryId, categoryNameUrl, null, string.Empty);
                                var categoryItemModel1 = new MTProductSearchOneStepCategoryItemModel
                                {
                                    CategoryId = item2.CategoryId,
                                    CategoryParentId = item.CategoryId,
                                    CategoryName = item2.CategoryName,
                                    CategoryUrl = categoryUrlCustom,
                                    ProductCount = item2.ProductCount
                                };
                                model.CategoryModel.SubCategories.Add(categoryItemModel1);
                            }
                        }
                    }
                    model.TotalCount = productCategories.Where(x => x.CategoryType == 0).Sum(x => x.ProductCount);
                    processStatus.Result = model;
                    processStatus.TotolRowCount = model.TotalCount;
                    processStatus.TotolPageCount = 1;
                    processStatus.ActiveResultRowCount = model.TotalCount;
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