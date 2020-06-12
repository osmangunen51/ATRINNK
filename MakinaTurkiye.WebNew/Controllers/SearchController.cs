using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Search;
using MakinaTurkiye.Utilities.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web;
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
        public SearchController(ISearchService SearchService)
        {
            this.SearchService = SearchService;

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
            bool DeveloperGosterim = false;
            List<string> IpAdresListesi = new List<string>();
            List<string> EklenenListesi = new List<string>();
            SearchAutoCompleteResult Sonuc = new SearchAutoCompleteResult();
            SearchAutoCompleteItem ItemOneri;
            if (query.Length == 0)
            {
                string cookieName = "Makinaturkiye_SearhTexts";
                string searchTexts = GetCookie(cookieName);
                if (!string.IsNullOrEmpty(searchTexts))
                {
                    Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = "Geçmiş Aramalar", data = new data { category = "Gecmis" }, Url = "#" });
                    if (searchTexts.Contains(","))
                    {
                        string[] itemSearchTexts = searchTexts.Split(',');
                       var reversed =   itemSearchTexts.Reverse().ToList();
                        int numbers = reversed.Count() > 12 ? 12 : reversed.Count();
                        
                        for (int i = 0; i < numbers; i++)
                        {
                            Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = reversed[i], data = new data { category = "Gecmis" }, Url = "" });
                        }
                    }
                    else
                    {
                        Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = searchTexts, data = new data { category = "Gecmis" }, Url = "" });
                    }
                }
            }
            else
            //if (query.Length == 0)
            //{
            //    string cookieName = "Makinaturkiye_SearhTexts";
            //    string searchTexts = GetCookie(cookieName);
            //    if (!string.IsNullOrEmpty(searchTexts))
            //    {
            //        Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = "Geçmiş Aramalar", data = new data { category = "Gecmis" }, Url = "#" });
            //        if (searchTexts.Contains(","))
            //        {
            //            string[] itemSearchTexts = searchTexts.Split(',');
            //           var reversed =   itemSearchTexts.Reverse().ToList();
            //            int numbers = reversed.Count() > 12 ? 12 : reversed.Count();

            //            for (int i = 0; i < numbers; i++)
            //            {
            //                Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = reversed[i], data = new data { category = "Gecmis" }, Url = "" });
            //            }
            //        }
            //        else
            //        {
            //            Sonuc.suggestions.Add(new SearchAutoCompleteItem { Name = searchTexts, data = new data { category = "Gecmis" }, Url = "" });
            //        }

            //    }

            //}
            //else
            //{
            if (!string.IsNullOrEmpty(query))
            {

                if (!string.IsNullOrEmpty(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ElasticSearch:IpAdresListesi")))
                {
                    IpAdresListesi.AddRange(System.Web.Configuration.WebConfigurationManager.AppSettings.Get("ElasticSearch:IpAdresListesi").Split(',').ToList());
                }
                DeveloperGosterim = IpAdresListesi.Contains(this.IpAdres);
                // DeveloperGosterim = true;

                var DbSonucListesi = SearchService.SearchSuggest(query);
                int EklenenSayisi = 1;
                foreach (var item in DbSonucListesi.Distinct().OrderByDescending(x => x.Score).ToList())
                {
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
                            Url = item.Url
                        };
                    }

                    if (!EklenenListesi.Contains(ItemOneri.Name.Trim()))
                    {
                        EklenenSayisi++;
                        Sonuc.suggestions.Add(ItemOneri);
                        EklenenListesi.Add(ItemOneri.Name.Trim());
                        if (EklenenSayisi>7)
                        EklenenListesi.Add(ItemOneri.Name);
                        if (EklenenSayisi > 7)
                        {
                            break;
                        }
                    }
                }

                DbSonucListesi = SearchService.SearchCategory(query);
                var DtKategori = DbSonucListesi.Where(x => x.Category == "Ürün Kategorileri").ToList();
                if (DtKategori.Count > 0)
                {
                    SearchAutoCompleteItem ItemCategory;
                    ItemCategory = new SearchAutoCompleteItem()
                    {
                        Name = "",
                        data = new data() { category = "Ürün Kategorileri" },
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemCategory);
                    foreach (var item in DtKategori.Distinct().OrderByDescending(x => x.Score).Take(3).ToList())
                    {
                        if (DeveloperGosterim)
                        {

                            ItemCategory = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemCategory = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        Sonuc.suggestions.Add(ItemCategory);
                        EklenenListesi.Add(ItemCategory.Name.Trim());
                    }
                }

                var DtMarka = DbSonucListesi.Where(x => x.Category == "Markalar").ToList();
                if (DtMarka.Count > 0)
                {
                    EklenenListesi = new List<string>();
                    SearchAutoCompleteItem ItemMarka;
                    ItemMarka = new SearchAutoCompleteItem()
                    {
                        Name = "",
                        data = new data() { category = "Markalar" },
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemMarka);
                    foreach (var item in DtMarka.Distinct().OrderByDescending(x => x.Score).Take(3).ToList())
                    {
                        if (DeveloperGosterim)
                        {
                            ItemMarka = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemMarka = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        if (!EklenenListesi.Contains(ItemMarka.Name))
                        {
                            Sonuc.suggestions.Add(ItemMarka);
                            EklenenListesi.Add(ItemMarka.Name);
                        }
                    }
                }

                var DtStore = DbSonucListesi.Where(x => x.Category == "Firma Kategorileri").ToList();
                if (DtStore.Count > 0)
                {
                    EklenenListesi = new List<string>();
                    SearchAutoCompleteItem ItemStore;
                    ItemStore = new SearchAutoCompleteItem()
                    {
                        Name = "",
                        data = new data() { category = "Firma Kategorileri" },
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemStore);
                    foreach (var item in DtStore.Distinct().OrderByDescending(x => x.Score).Take(3).ToList())
                    {
                        if (DeveloperGosterim)
                        {
                            ItemStore = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemStore = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        if (!EklenenListesi.Contains(ItemStore.Name))
                        {
                            Sonuc.suggestions.Add(ItemStore);
                            EklenenListesi.Add(ItemStore.Name);
                        }
                    }
                }

                var DtVideo = DbSonucListesi.Where(x => x.Category == "Videolar").ToList();
                if (DtVideo.Count > 0)
                {
                    EklenenListesi = new List<string>();
                    SearchAutoCompleteItem ItemVideo;
                    ItemVideo = new SearchAutoCompleteItem()
                    {
                        Name = "",
                        data = new data() { category = "Videolar" },
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemVideo);
                    foreach (var item in DtVideo.Distinct().OrderByDescending(x => x.Score).Take(3).ToList())
                    {
                        if (DeveloperGosterim)
                        {
                            ItemVideo = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemVideo = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        if (!EklenenListesi.Contains(ItemVideo.Name))
                        {
                            Sonuc.suggestions.Add(ItemVideo);
                            EklenenListesi.Add(ItemVideo.Name);
                        }
                    }
                }

                var DtTedarikci = DbSonucListesi.Where(x => x.Category == "Tedarikçiler").ToList();
                if (DtTedarikci.Count > 0)
                {
                    EklenenListesi = new List<string>();
                    SearchAutoCompleteItem ItemTedarikci;
                    ItemTedarikci = new SearchAutoCompleteItem()
                    {
                        Name = "",
                        data = new data() { category = "Tedarikçiler" },
                        Url = "#"
                    };
                    Sonuc.suggestions.Add(ItemTedarikci);
                    foreach (var item in DtTedarikci.Distinct().OrderByDescending(x => x.Score).Take(3).ToList())
                    {
                        if (DeveloperGosterim)
                        {
                            ItemTedarikci = new SearchAutoCompleteItem()
                            {
                                Name = string.Format("{0}  | Skoru : {1}", item.Name, item.Score),
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        else
                        {
                            ItemTedarikci = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
                            };
                        }
                        if (!EklenenListesi.Contains(ItemTedarikci.Name))
                        {
                            Sonuc.suggestions.Add(ItemTedarikci);
                            EklenenListesi.Add(ItemTedarikci.Name);
                        }
                    }
                }
            }
            return Json(Sonuc, JsonRequestBehavior.AllowGet);

        }
    }
}