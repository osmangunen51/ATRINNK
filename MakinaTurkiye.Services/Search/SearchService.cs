using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Data;
using MakinaTurkiye.Entities.StoredProcedures.Search;
using Nest;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity.Infrastructure;
using System.Globalization;
using System.Linq;
namespace MakinaTurkiye.Services.Search
{
    public partial class SearchService : ISearchService
    {

        #region Fields

        private readonly IDbContext _dbContext;
        private readonly IDataProvider _dataProvider;
        private readonly ElasticSearchClient ElasticSearchClient;

        ConnectionSettings ElasticSearchClientSettings = new ConnectionSettings(new Uri("http://localhost:9200"));

        public string GlobalSuggetSearchIndexName { get; set; } = "GlobalSuggetSearchIndexName";
        public string GlobalSearchGenelIndexName { get; set; } = "GlobalSearchGenelIndexName";

        #endregion

        #region Ctor

        public SearchService(IDbContext dbContext,IDataProvider dataProvider)
        {
            this._dbContext = dbContext;
            this._dataProvider = dataProvider;
            ElasticSearchClient = new ElasticSearchClient(ElasticSearchClientSettings);
        }

        #endregion

        #region Methods

        public void CreateAndYukleSuggestSearchIndex()
        {
            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = "";
            pSearchText.DbType = DbType.String;
            ((IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 10000;
            List<SearchResult> Result = _dbContext.SqlQuery<SearchResult>("SP_GetSPSearch @SearchText", pSearchText).ToList();
            List<MakinaTurkiye.Services.Search.Product> Liste = new List<Search.Product>();
            int index = 0;
            foreach (var SonucItem in Result)
            {
                List<string> InputListesi = new List<string>();
                SonucItem.Name= new CultureInfo("tr-TR").TextInfo.ToTitleCase(SonucItem.Name.ToLower());
                InputListesi.AddRange(SonucItem.Name.Split(' ').ToList());
                bool Islem = true;
                string Metin = SonucItem.Name.Trim();
                int Ara = 0;
                while (Islem)
                {
                    Ara = SonucItem.Name.IndexOf(" ", Ara + 1);
                    if (Ara > -1)
                    {
                        string EklenecekMetin = SonucItem.Name.Substring(0, (Ara));
                        if (!InputListesi.Contains(EklenecekMetin))
                        {
                            InputListesi.Add(EklenecekMetin);
                        }
                    }
                    else
                    {
                        Islem = false;
                    }
                }
                if (!InputListesi.Contains(SonucItem.Name))
                {
                    InputListesi.Add(SonucItem.Name);
                }

                MakinaTurkiye.Services.Search.Product Kayit = new MakinaTurkiye.Services.Search.Product()
                {
                    Name = SonucItem.Name,
                    Url = "",
                    Suggest = new CompletionField()
                    {
                        Input = InputListesi
                                      ,
                        Weight = (int)SonucItem.Score
                    }
                };
                index++;
                Kayit.Id = index;
                Liste.Add(Kayit);
            }

            if (ElasticSearchClient.CheckIndex(GlobalSuggetSearchIndexName.ToLowerInvariant()))
            {
                ElasticSearchClient.DeleteIndex(GlobalSuggetSearchIndexName.ToLowerInvariant());
            }
            CreateProductSearchIndex(Liste);
        }

        public void CreateAndYukleSearchGenelIndex()
        {
            var pSearchText = _dataProvider.GetParameter();
            pSearchText.ParameterName = "SearchText";
            pSearchText.Value = "";
            pSearchText.DbType = DbType.String;
            ((IObjectContextAdapter)this._dbContext).ObjectContext.CommandTimeout = 10000;
            List<SearchResultCategory> Result = _dbContext.SqlQuery<SearchResultCategory>("SP_GetSPSearchGenel @SearchText", pSearchText).ToList();
            List<MakinaTurkiye.Services.Search.Product> Liste = new List<Search.Product>();
            int index = 0;
            foreach (var SonucItem in Result)
            {
                if (SonucItem.Name!=null)
                {
                    SonucItem.Name = new CultureInfo("tr-TR").TextInfo.ToTitleCase(SonucItem.Name.ToLower());
                    List<string> InputListesi = new List<string>();
                    if (!string.IsNullOrEmpty(SonucItem.Name))
                    {
                        InputListesi.Add(SonucItem.Name);
                    }
                    if (!string.IsNullOrEmpty(SonucItem.Path))
                    {
                        InputListesi.Add(SonucItem.Path);
                    }
                    bool Islem = true;
                    string Metin = SonucItem.Name.Trim();
                    int Ara = 0;
                    while (Islem)
                    {
                        if (SonucItem.Name != "")
                        {
                            Ara = SonucItem.Name.IndexOf(" ", Ara + 1);
                            if (Ara > -1)
                            {
                                string EklenecekMetin = SonucItem.Name.Substring(0, (Ara));
                                if (!InputListesi.Contains(EklenecekMetin) && EklenecekMetin!="")
                                {
                                    InputListesi.Add(EklenecekMetin);
                                }
                            }
                            else
                            {
                                Islem = false;
                            }
                        }
                        else
                        {
                            Islem = false;
                        }
                    }
                    if (!InputListesi.Contains(SonucItem.Name))
                    {
                        InputListesi.Add(SonucItem.Name);
                    }
                    MakinaTurkiye.Services.Search.Product Kayit = new MakinaTurkiye.Services.Search.Product()
                    {
                        Name =(!string.IsNullOrEmpty(SonucItem.Path) ? SonucItem.Path : SonucItem.Name),
                        Url = SonucItem.Url,
                        Category=SonucItem.Category,
                        Suggest = new CompletionField()
                        {
                            Input = InputListesi.Where(x=>x!=""),
                            Weight = (int)SonucItem.Score
                        }
                    };
                    index++;
                    Kayit.Id = index;
                    Liste.Add(Kayit);
                }

            }
            if (ElasticSearchClient.CheckIndex(GlobalSearchGenelIndexName.ToLowerInvariant()))
            {
                ElasticSearchClient.DeleteIndex(GlobalSearchGenelIndexName.ToLowerInvariant());
            }
            CreateCategorySearchIndex(Liste);
        }

        public List<SearchResult> VeriOnIslemeYap(List<SearchResult> Kayitlar)
        {
            List<string> BitisSinirKelimeler = new List<string>() {
                "makine ",
                "makina ",
                "makinesi ",
                "makinası ",
                " Dilimleme ",
                "Testeresi ",
                "Asansörü ",
                "makinaları ",
                "Adaptör",
                "Test Tapası ",
                "-",
                "(",
                "/",
                " Acs ",
                ","
            };
            List<string> OncesiDahilAtKelimeler = new List<string>()
            {
            };

            //List<string> OncesiDahilAtKelimeler = new List<string>()
            //{   " inç ",
            //    " Derece",
            //    " Ccr ",
            //    " Kw ",
            //    " mm ",
            //    "/saat ",
            //    " hp ",
            //    "Tl İle Çalışan ",
            //    "0-90 ",
            //    "1 Blok",
            //    "1 Renk Tabaka",
            //    "1 Sistem",
            //    "1 Tl İle Çalışan ",
            //    " Kcal /Saat ",
            //    " Kcal /Saat ",
            //    " Ton ",
            //    "Mt ",
            //    "Lt' Lik",
            //    " Lu ",
            //    " Lu ",
            //    " Renk ",
            //    " Renk ",
            //    "0 ",
            //    "0 ",
            //    " Tonluk ",
            //    "0 W ",
            //    "***",
            //     " Kefe ",
            //     " Kefeli ",
            //};

            List<SearchResult> Sonuc = new List<SearchResult>();
            try
            {
                foreach (SearchResult Kayit in Kayitlar)
                {
                    string KayitTxt = Kayit.Name.ToLower();
                    if (KayitTxt!=string.Empty)
                    {
                        int Start = -1;
                        int End = -1;
                        foreach (var item in OncesiDahilAtKelimeler)
                        {
                            string itemTxt = item.ToLower();
                            Start = KayitTxt.IndexOf(itemTxt);
                            if (Start > -1)
                            {
                                Start += item.Length;
                                break;
                            }
                        }
                        if (Start == -1)
                        {
                            Start = 0;
                        }
                        End = (KayitTxt.Length - Start)-1;
                        if (End>Start)
                        {
                            KayitTxt = KayitTxt.Substring(Start, End);
                            if (KayitTxt != string.Empty)
                            {
                                End = -1;
                                foreach (var item in BitisSinirKelimeler)
                                {
                                    string itemTxt = item.ToLower();
                                    End = KayitTxt.IndexOf(itemTxt);
                                    if (End > -1)
                                    {
                                        End += item.Length;
                                        break;
                                    }
                                }
                                if (End == -1)
                                {
                                    End = KayitTxt.Length;
                                }
                                if (End>Start)
                                {
                                    KayitTxt = Kayit.Name.Substring(Start, (End - Start) - 1);
                                    Kayit.Name = KayitTxt.Trim();
                                    Sonuc.Add(Kayit);
                                }
                            }
                        }
                    }
                }
            }
            catch (Exception Hata)
            {
                //Sonuc = new List<SearchResult>();
            }
            return Sonuc;
        }


        public void IndexAraVeriYukle(string AramaTxt)
        {

        }
        public void CreateProductSearchIndexSade()
        {
            bool isCreated = ElasticSearchClient.CreateIndex(GlobalSuggetSearchIndexName.ToLowerInvariant());
        }

        //public void CreateProductSearchIndex()
        //{
        //    bool isCreated = ElasticSearchClient.CreateIndex(GlobalSearchIndexName.ToLowerInvariant());
        //    if (isCreated)
        //        {
        //            var pSearchText = _dataProvider.GetParameter();
        //            pSearchText.ParameterName = "SearchText";
        //            pSearchText.Value = "";
        //            pSearchText.DbType = DbType.String;
        //            List<SearchResult> Result = _dbContext.SqlQuery<SearchResult>("SP_GetSPSearch @SearchText", pSearchText).ToList();
        //            List<string> ListeMetin = new List<string>();
        //            foreach (var item in Result)
        //            {
        //                ListeMetin.AddRange(item.Name.Split(' '));

        //            }
        //            ListeMetin = ListeMetin.Distinct().ToList();
        //            List<MakinaTurkiye.Services.Search.Product> Liste = new List<Search.Product>();
        //            foreach (var item in ListeMetin)
        //            {
        //                try
        //                {
        //                    var ppSearchText = _dataProvider.GetParameter();
        //                    ppSearchText.ParameterName = "SearchText";
        //                    ppSearchText.Value = item.Replace("~","").Replace("(", "").Replace(")", "").Replace("\t", "").Replace(",", "").Replace("\"","");
        //                    ppSearchText.DbType = DbType.String;
        //                    if (ppSearchText.Value !=null && !string.IsNullOrEmpty(ppSearchText.Value.ToString()))
        //                    {
        //                        List<SearchResult> pResult = _dbContext.SqlQuery<SearchResult>("SP_GetSPSearch @SearchText", ppSearchText).ToList();
        //                        Liste.AddRange(pResult.Select(Snc =>
        //                              new MakinaTurkiye.Services.Search.Product()
        //                              {
        //                                  Name = Snc.Name,
        //                                  Suggest = new CompletionField()
        //                                  {
        //                                      Input = Snc.Name.Split(' ')
        //                                      ,
        //                                      Weight = (int)Snc.Score
        //                                  }
        //                              }
        //                            ));
        //                    }
        //                }
        //                catch (Exception Hata)
        //                {
        //                    continue;
        //                }
        //            }
        //            int index = 0;
        //            Liste = Liste.Distinct<MakinaTurkiye.Services.Search.Product>().ToList();
        //            foreach (var item in Liste)
        //            {
        //                index++;
        //                item.Id = index;
        //            }
        //            ElasticSearchClient.IndexVeriYukle(GlobalSearchIndexName.ToLowerInvariant(), Liste);
        //    }
        //}

        public void CreateProductSearchIndex(List<MakinaTurkiye.Services.Search.Product> Liste)
        {
            bool isCreated = ElasticSearchClient.CreateIndex(GlobalSuggetSearchIndexName.ToLowerInvariant());
            if (isCreated)
            {
                ElasticSearchClient.IndexVeriYukle(GlobalSuggetSearchIndexName.ToLowerInvariant(), Liste);
            }
        }

        public void CreateCategorySearchIndex(List<MakinaTurkiye.Services.Search.Product> Liste)
        {
            bool isCreated = ElasticSearchClient.CreateIndex(GlobalSearchGenelIndexName.ToLowerInvariant());
            if (isCreated)
            {
                ElasticSearchClient.IndexVeriYukle(GlobalSearchGenelIndexName.ToLowerInvariant(), Liste);
            }
        }

        public IList<SearchResult> SearchSuggest(string SearchText)
        {
            List<SearchResult> Sonuc = new List<SearchResult>();
            ProductSuggestResponse SncSnc = ElasticSearchClient.Suggest(GlobalSuggetSearchIndexName, SearchText);
            Sonuc = SncSnc.Suggests.Select(Snc =>
                        new SearchResult()
                        {
                            Name = Snc.Name,
                            Category = "Oneri",
                            Score = Snc.Score,
                            Url = Snc.Url
                        }
                ).ToList();
            return Sonuc;
        }

        public IList<SearchResult> SearchCategory(string SearchText)
        {
            List<SearchResult> Sonuc = new List<SearchResult>();
            ProductSuggestResponse SncSnc = ElasticSearchClient.Search(GlobalSearchGenelIndexName, SearchText);
            Sonuc = SncSnc.Suggests.Select(Snc =>
                      new SearchResult()
                      {
                          Name = Snc.Name,
                          Category = Snc.Category,
                          Score = Snc.Score,
                          Url=Snc.Url
                      }
                ).ToList();
            return Sonuc;
        }
        #endregion

        public void Search(string SeacrhText)
        {

        }
    }
}
