namespace NeoSistem.Trinnk.Web.Models
{

    //public class SeoModel
    //{
    //    private static Dictionary<string, string> SeoItems()
    //    {
    //        Dictionary<string, string> seoItems = new Dictionary<string, string>();
    //        seoItems.Add("Category", "{Kategori}");
    //        seoItems.Add("TopCategory", "{UstKategori}");
    //        seoItems.Add("Brand", "{Marka}");
    //        seoItems.Add("Model", "{Model}");
    //        seoItems.Add("Series", "{Seri}");
    //        seoItems.Add("ProductType", "{UrunTipi}");
    //        seoItems.Add("ProductStatu", "{UrunDurumu}");
    //        seoItems.Add("Price", "{Fiyati}");
    //        seoItems.Add("ModelYear", "{ModelYili}");
    //        seoItems.Add("ProductName", "{UrunAdi}");
    //        seoItems.Add("FirmName", "{FirmaAdi}");
    //        seoItems.Add("ProductGroupsNames", "{UrunGrubuIsimleri}");
    //        seoItems.Add("ProductSalesType", "{SatisDetayi}");
    //        seoItems.Add("BriefDetail", "{KisaDetay}");
    //        seoItems.Add("FirstTopCategory", "{IlkUstKategori}");
    //        seoItems.Add("ModelBrand", "{ModelMarka}");
    //        seoItems.Add("AltKategoriForAktifKategori", "{AltKategoriForAktifKategori}");
    //        seoItems.Add("Sehir", "{Sehir}");
    //        seoItems.Add("Ilce", "{Ilce}");
    //        seoItems.Add("ArananKelime", "{ArananKelime}");
    //        seoItems.Add("Ulke", "{Ulke}");
    //        seoItems.Add("KategoriBaslik", "{KategoriBaslik}");
    //        seoItems.Add("IlkUstKategoriBaslik", "{IlkUstKategoriBaslik}");
    //        seoItems.Add("HaberAdi", "{HaberAdi}");
    //        return seoItems;
    //    }

    //    public static class SeoProductParemeters
    //    {
    //        public static string Category = "{Kategori}";
    //        public static string TopCategory = "{UstKategori}";
    //        public static string FirstTopCategory = "{IlkUstKategori}";
    //        public static string Brand = "{Marka}";
    //        public static string ModelBrand = "{ModelMarka}";
    //        public static string Model = "{Model}";
    //        public static string Series = "{Seri}";
    //        public static string ProductType = "{UrunTipi}";
    //        public static string ProductStatu = "{UrunDurumu}";
    //        public static string ProductSalesType = "{SatisDetayi}";
    //        public static string BriefDetail = "{KisaDetay}";
    //        public static string Price = "{Fiyati}";
    //        public static string ModelYear = "{ModelYili}";
    //        public static string ProductName = "{UrunAdi}";
    //        public static string FirmName = "{FirmaAdi}";
    //        public static string FirmSeoTitle = "{FirmSeoTitle}";
    //        public static string FirmSeoDescription = "{FirmSeoDecsription}";
    //        public static string FirmSeoKeyword = "{FirmSeoKeyword}";
    //        public static string ProductGroupNames = "{UrunGrubuIsimleri}";
    //        public static string AltKategoriForAktifKategori = "{AltKategoriForAktifKategori}";
    //        public static string Ulke = "{Ulke}";
    //        public static string Sehir = "{Sehir}";
    //        public static string Ilce = "{Ilce}";
    //        public static string ArananKelime = "{ArananKelime}";
    //        public static string PagingSeoDec = "{PagingSeoDec}";
    //        public static string KategoriBaslik = "{KategoriBaslik}";
    //        public static string IlkUstKategoriBaslik = "{IlkUstKategoriBaslik}";

    //    }

    //    public static string GetSessionName(string name)
    //    {
    //        if (HttpContext.Current.Session[name] == null)
    //            return "";
    //        else
    //            return HttpContext.Current.Session[name].ToString();
    //    }



    //    public static IList<Seo> CurrentSeo
    //    {
    //        get
    //        {
    //            ISeoDefinitionService _seoService = EngineContext.Current.Resolve<ISeoDefinitionService>();
    //            return _seoService.GetAllSeos();
    //        }
    //    }

    //    public static Seo GeneralforAll(byte pageType = 0, int? categoryId = 0, int? page = 0)
    //    {
    //        try
    //        {
    //            var seo = CurrentSeo.Single(c => c.SeoId == pageType);

    //            var tempSeo = new Seo
    //            {
    //                Abstract = seo.Abstract,
    //                SeoId = seo.SeoId,
    //                Description = seo.Description,
    //                Keywords = seo.Keywords,
    //                PageName = seo.PageName,
    //                RevisitAfter = seo.RevisitAfter,
    //                Robots = seo.Robots,
    //                Title = seo.Title,
    //                Classification = seo.Classification
    //            };

    //            //ESKİ KATEGORİ SEO TİTLE VE DESCRİPTİON AYARLARI
    //            if (categoryId.HasValue && categoryId.Value > 0)
    //            {
    //                ICategoryService categoryService = EngineContext.Current.Resolve<ICategoryService>();
    //                var category = categoryService.GetCategoryByCategoryId(categoryId.Value);
    //                if (category != null)
    //                {
    //                    tempSeo.Title = !string.IsNullOrEmpty(category.Title) ? category.Title : tempSeo.Title;
    //                    tempSeo.Description = !string.IsNullOrEmpty(category.Description) ? category.Description : tempSeo.Description;
    //                    tempSeo.Keywords = !string.IsNullOrEmpty(category.Keywords) ? category.Keywords : tempSeo.Keywords;
    //                }

    //            }

    //            foreach (KeyValuePair<string, string> item in SeoItems())
    //            {
    //                if (tempSeo.Abstract != null && tempSeo.Abstract.Contains(item.Value))
    //                {
    //                    tempSeo.Abstract = tempSeo.Abstract.Replace(item.Value, GetSessionName(item.Value));
    //                }

    //                if (tempSeo.Classification != null && tempSeo.Classification.Contains(item.Value))
    //                {
    //                    tempSeo.Classification = tempSeo.Classification.Replace(item.Value, GetSessionName(item.Value));
    //                }

    //                if (tempSeo.Description != null && tempSeo.Description.Contains(item.Value))
    //                {
    //                    if(item.Value == SeoProductParemeters.FirmName && (byte)PageType.Store == pageType)
    //                    {
    //                        if (!string.IsNullOrEmpty(GetSessionName(SeoProductParemeters.FirmSeoDescription)))
    //                            tempSeo.Description = GetSessionName(SeoProductParemeters.FirmSeoDescription);
    //                        else
    //                            tempSeo.Description = tempSeo.Description.Replace(item.Value, GetSessionName(item.Value));
    //                    }
    //                    else
    //                    tempSeo.Description = tempSeo.Description.Replace(item.Value, GetSessionName(item.Value));
    //                }

    //                if (tempSeo.Keywords != null && tempSeo.Keywords.Contains(item.Value))
    //                {
    //                    if (item.Value == SeoProductParemeters.FirmName && (byte)PageType.Store==pageType)
    //                    {
    //                        if (!string.IsNullOrEmpty(GetSessionName(SeoProductParemeters.FirmSeoKeyword)))
    //                            tempSeo.Keywords = GetSessionName(SeoProductParemeters.FirmSeoKeyword);
    //                        else
    //                            tempSeo.Keywords = tempSeo.Keywords.Replace(item.Value, GetSessionName(item.Value));
    //                    }
    //                    else
    //                        tempSeo.Keywords = tempSeo.Keywords.Replace(item.Value, GetSessionName(item.Value));

    //                }

    //                if (tempSeo.RevisitAfter != null && tempSeo.RevisitAfter.Contains(item.Value))
    //                {
    //                    tempSeo.RevisitAfter = tempSeo.RevisitAfter.Replace(item.Value, GetSessionName(item.Value));
    //                }

    //                if (tempSeo.Robots != null && tempSeo.Robots.Contains(item.Value))
    //                {
    //                    tempSeo.Robots = tempSeo.Robots.Replace(item.Value, GetSessionName(item.Value));
    //                }

    //                if (tempSeo.Title != null && tempSeo.Title.Contains(item.Value))
    //                {
    //                    if (item.Value == SeoProductParemeters.FirmName && (byte)PageType.Store == pageType)
    //                    {
    //                        if (!string.IsNullOrEmpty(GetSessionName(SeoProductParemeters.FirmSeoTitle)))
    //                            tempSeo.Title = GetSessionName(SeoProductParemeters.FirmSeoTitle);
    //                        else
    //                            tempSeo.Title = tempSeo.Title.Replace(item.Value, GetSessionName(item.Value));
    //                    }
    //                    else
    //                        tempSeo.Title = tempSeo.Title.Replace(item.Value, GetSessionName(item.Value));
    //                }
    //            }

    //            if (page != 0 && page != null)
    //            {
    //                if (tempSeo.Title != null && tempSeo.Title.Contains("{PagingSeoDec}"))
    //                {

    //                    tempSeo.Title = tempSeo.Title.Replace("{PagingSeoDec}", " - " + page.ToString());


    //                }
    //                if (tempSeo.Description != null && tempSeo.Description.Contains("{PagingSeoDec}"))
    //                {
    //                    tempSeo.Description = tempSeo.Description.Replace("{PagingSeoDec}", " - " + page.ToString());

    //                }
    //            }
    //            else
    //            {
    //                if (tempSeo.Title != null && tempSeo.Title.Contains("{PagingSeoDec}"))
    //                {

    //                    tempSeo.Title = tempSeo.Title.Replace("{PagingSeoDec}", "");


    //                }
    //                if (tempSeo.Description != null && tempSeo.Description.Contains("{PagingSeoDec}"))
    //                {
    //                    tempSeo.Description = tempSeo.Description.Replace("{PagingSeoDec}", "");

    //                }
    //            }

    //            //TODO: Dublicate paging ignore
    //            foreach (KeyValuePair<string, string> item in SeoItems())
    //            {
    //                if (tempSeo.Title != null && item.Key == "PagingSeoDec" && !string.IsNullOrEmpty(item.Value) && tempSeo.Title.Contains("{PagingSeoDec}"))
    //                {

    //                    tempSeo.Title = tempSeo.Title.Replace(item.Value, GetSessionName(item.Value));


    //                }
    //                if (tempSeo.Description != null && item.Key == "PagingSeoDec" && !string.IsNullOrEmpty(item.Value) && tempSeo.Description.Contains("{PagingSeoDec}"))
    //                {
    //                    tempSeo.Description = tempSeo.Description.Replace(item.Value, GetSessionName(item.Value));

    //                }

    //                //TODO: Dublicate paging ignore end:nzmhtpg
    //            }
    //            if (page > 1)
    //            {
    //                tempSeo.Title += " - Sayfa " + page;
    //                tempSeo.Description += " - Sayfa " + page;
    //            }

    //            return tempSeo;
    //        }
    //        catch (Exception exc)
    //        {
    //            return new Seo { };
    //        }
    //    }
    //}
}