using MakinaTurkiye.Api.Helpers;
using MakinaTurkiye.Api.View;
using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Common;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Packets;
using MakinaTurkiye.Services.Search;
using MakinaTurkiye.Services.Stores;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;

namespace MakinaTurkiye.Api.Controllers
{
    public class SearchController : BaseApiController
    {
        private readonly ISearchService SearchService;

        public SearchController()
        {
            SearchService = EngineContext.Current.Resolve<ISearchService>();
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
                            Url = "#"
                        };
                        Sonuc.suggestions.Add(ItemCategory);
                        foreach (var item in DtKategori)
                        {
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
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
                            Url = "#"
                        };
                        Sonuc.suggestions.Add(ItemCategory);

                        foreach (var item in DtFirma)
                        {
                            SearchAutoCompleteItem ItemOneri;
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
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
                            Url = "#"
                        };
                        Sonuc.suggestions.Add(ItemCategory);
                        foreach (var item in DtVideo)
                        {
                            SearchAutoCompleteItem ItemOneri;
                            ItemOneri = new SearchAutoCompleteItem()
                            {
                                Name = item.Name,
                                data = new data() { category = string.Format("{0}", item.Category) },
                                Url = item.Url
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
                            Url = "#"
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
                                Url = item.Url
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
                processStatus.Result = Sonuc.suggestions.Where(x=>!string.IsNullOrEmpty(x.Name));
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