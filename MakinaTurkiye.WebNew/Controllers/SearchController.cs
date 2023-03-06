﻿using MakinaTurkiye.Core;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Search;
using MakinaTurkiye.Utilities.Mvc;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    public class SearchAutoCompleteResult
    {
        public List<SearchAutoCompleteItem> suggestions { get; set; } = new List<SearchAutoCompleteItem>();
    }
    public class SearchAutoCompleteItem
    {
        public string Name { get; set; } = "";
        public string Url { get; set; } = "";
        public data data { get; set; } = new data();
        public string Value { get; set; } = "";
    }

    public class data
    {
        public string category { get; set; } = "None";
    }

    [AllowAnonymous]
    [AllowSameSite]
    public class SearchController : BaseController
    {
        private readonly ISearchService SearchService;
        private readonly IProductStatisticService _productStatisticService;

        public SearchController(ISearchService SearchService, IProductStatisticService productStatisticService)
        {
            this.SearchService = SearchService;
            this._productStatisticService = productStatisticService;

        }
        public ActionResult IndexOlustur()
        {
            SearchService.CreateAndYukleSuggestSearchIndex();
            SearchService.CreateAndYukleSearchGenelIndex();
            return View();
        }

        [HttpGet]
        public ActionResult Index(string query)
        {
            if (Request.Url.AbsoluteUri.ToString().Contains("urun"))
            {
                return RedirectPermanent(AppSettings.SiteUrlWithoutLastSlash + "/search/index");
            }
            bool DeveloperGosterim = false;
            List<string> IpAdresListesi = new List<string>();
            List<string> EklenenListesi = new List<string>();
            SearchAutoCompleteResult Sonuc = new SearchAutoCompleteResult();

            if (!string.IsNullOrEmpty(query))
            {

                if (!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ElasticSearch:IpAdresListesi")))
                {
                    IpAdresListesi.AddRange(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ElasticSearch:IpAdresListesi").Split(',').ToList());
                }
                DeveloperGosterim = IpAdresListesi.Contains(this.IpAdres);
                var DbSonucListesi = SearchService.SearchSuggest(query);
                int EklenenSayisi = 1;
                foreach (var item in DbSonucListesi.Distinct().OrderByDescending(x => x.Score).ToList())
                {
                    SearchAutoCompleteItem ItemOneri;

                    if (DeveloperGosterim)
                    {
                        ItemOneri = new SearchAutoCompleteItem()
                        {
                            Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                            data = new data() { category = string.Format("{0}", item.Category) },
                            Url = item.Url,
                        };
                    }
                    else
                    {
                        ItemOneri = new SearchAutoCompleteItem()
                        {
                            Name = item.Name,
                            data = new data() { category = string.Format("{0}", item.Category) },
                            Url = item.Url,
                        };
                    }
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
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemCategory);
                    foreach (var item in DtKategori)
                    {
                        if (DeveloperGosterim)
                        {

                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
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
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemCategory);

                    foreach (var item in DtFirma)
                    {
                        SearchAutoCompleteItem ItemOneri;

                        if (DeveloperGosterim)
                        {

                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
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
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemCategory);
                    foreach (var item in DtVideo)
                    {
                        SearchAutoCompleteItem ItemOneri;
                        if (DeveloperGosterim)
                        {

                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
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
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemCategory);

                    EklenenListesi.Clear();
                    foreach (var item in DtTedarik)
                    {
                        SearchAutoCompleteItem ItemOneri;

                        if (DeveloperGosterim)
                        {

                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
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
            return Json(Sonuc, JsonRequestBehavior.AllowGet);
        }

    }
}