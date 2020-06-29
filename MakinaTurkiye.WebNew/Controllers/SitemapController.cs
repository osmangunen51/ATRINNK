using NeoSistem.MakinaTurkiye.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Web.Mvc;
using System.Xml;
using System.Xml.Linq;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    using global::MakinaTurkiye.Core.Infrastructure;
    using global::MakinaTurkiye.Services.Catalog;
    using global::MakinaTurkiye.Services.Stores;
    using global::MakinaTurkiye.Services.Videos;
    using global::MakinaTurkiye.Utilities.FormatHelpers;
    using global::MakinaTurkiye.Utilities.HttpHelpers;
    using NeoSistem.MakinaTurkiye.Core.Helpers;
    using NeoSistem.MakinaTurkiye.Core.Sitemap;
    using NeoSistem.MakinaTurkiye.Core.Web.Helpers;
    using System.Web;

    [AllowAnonymous]
    public class SitemapController : Controller
    {
        private const string URL = "https://www.makinaturkiye.com/";
        private const string PRODUCT_URL = "https://urun.makinaturkiye.com/";
        private const string VIDEO_URL = "https://video.makinaturkiye.com/";
        private const string STORE_URL = "https://magaza.makinaturkiye.com/";


        public ActionResult Index()
        {

            IList<string> sitemapFiles = new List<string>();
            this.generateSitemap_categoryproductgroup();

            //  sitemapFiles.Add(this.generateSitemap_categoryproductgroup());


            //  sitemapFiles.Add(            this.generateSitemapForStores());
            sitemapFiles.Add(this.generateSitemapForNews());



            this.generateSitemap_categorysector();
            // sitemapFiles.Add(this.generateSitemap_categorysector());
            this.generateSitemap_categorybrand();
            //sitemapFiles.Add();

            sitemapFiles.Add(this.generateSitemap_productGroupBrand());
            this.generateSitemap_categoryorta();
          //  sitemapFiles.Add(this.generateSitemap_categoryorta());
            sitemapFiles.Add(this.generateSitemap_categoryserie());
            sitemapFiles = sitemapFiles.Union(this.generateSitemap_categorymodels()).ToList();
            sitemapFiles.Add(this.generateSitemap_categoryCountry());


            sitemapFiles.Add(this.generateSitemap_categoryCity());
            sitemapFiles.Add(this.generateSitemap_categoryLocality());


            //unedited


            //sitemapFiles.Add(this.generateSitemap_StoreCategorysecond());
            //var sitemapCategoryStores2 = this.generateSitemap_StoreCategory2();
            //foreach (var item in sitemapCategoryStores2)
            //{
            //    sitemapFiles.Add(item);
            //}




            //sitemapFiles.Add(this.generateSitemap_categorymodel1());


            //sitemapFiles.Add(this.generateSitemap_categoryLocality());
            //sitemapFiles.Add(this.generateSitemap_BrandCountry());
            //sitemapFiles.Add(this.generateSitemap_BrandCity());
            //sitemapFiles.Add(this.generateSitemap_BrandLocality());

            var smIndex = new SitemapIndexXml();
            foreach (string sitemapFile in sitemapFiles)
            {
                var smnIndex = new SitemapIndexNode
                {
                    lastmodified = DateTime.Now,
                    location = URL + "Sitemaps/" + sitemapFile
                };
                smIndex.items.Add(smnIndex);
            }
            string resultXml = XmlHelper.SerializeToString(smIndex, Encoding.UTF8);
            string rootSitemapFileName = "rootSitemap.xml";
            FileHelper.WriteToFile("/Sitemaps/" + rootSitemapFileName, resultXml);

            // push sitemaps to search engines
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, rootSitemapFileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, rootSitemapFileName);

            return Content("OK!");
        }


        public ActionResult ProductSiteMapIndex()
        {
            IList<string> sitemapFiles = new List<string>();

            sitemapFiles = sitemapFiles.Union(this.generateSitemapForProducts()).ToList();
            var smIndex = new SitemapIndexXml();
            foreach (string sitemapFile in sitemapFiles)
            {
                var smnIndex = new SitemapIndexNode
                {
                    lastmodified = DateTime.Now,
                    location = PRODUCT_URL + "Sitemaps/Products/" + sitemapFile
                };
                smIndex.items.Add(smnIndex);
            }

            string resultXml = XmlHelper.SerializeToString(smIndex, Encoding.UTF8);
            string rootSitemapFileName = "rootSitemap.xml";
         
            FileHelper.WriteToFile("/Sitemaps/Products/" + rootSitemapFileName, resultXml);

            // push sitemaps to search engines
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, "Products/" + rootSitemapFileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, "Products/"+rootSitemapFileName);

            return Content("OK!");
        }

        public ActionResult VideoSiteMapIndex()
        {
            IList<string> sitemapFiles = new List<string>();

            sitemapFiles.Add(this.generateSitemap_video());
            sitemapFiles.Add(this.generateSitemap_videocategory());

            var smIndex = new SitemapIndexXml();
            foreach (string sitemapFile in sitemapFiles)
            {
                var smnIndex = new SitemapIndexNode
                {
                    lastmodified = DateTime.Now,
                    location = VIDEO_URL + "Sitemaps/Videos/" + sitemapFile
                };
                smIndex.items.Add(smnIndex);
            }

            string resultXml = XmlHelper.SerializeToString(smIndex, Encoding.UTF8);
            string rootSitemapFileName = "rootSitemap.xml";
            FileHelper.WriteToFile("/Sitemaps/Videos/" + rootSitemapFileName, resultXml);

            // push sitemaps to search engines
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, "Videos/" + rootSitemapFileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, "Videos/" + rootSitemapFileName);

            return Content("OK!");
        }


        public ActionResult StoreSiteMapIndex()
        {
            IList<string> sitemapFiles = new List<string>();

            sitemapFiles.Add(this.generateSitemap_StoreCategory());

            var smIndex = new SitemapIndexXml();
            foreach (string sitemapFile in sitemapFiles)
            {
                var smnIndex = new SitemapIndexNode
                {
                    lastmodified = DateTime.Now,
                    location = STORE_URL + "Sitemaps/Stores/" + sitemapFile
                };
                smIndex.items.Add(smnIndex);
            }

            string resultXml = XmlHelper.SerializeToString(smIndex, Encoding.UTF8);
            string rootSitemapFileName ="rootSitemap.xml";
            FileHelper.WriteToFile("/Sitemaps/Stores/" + rootSitemapFileName, resultXml);

            // push sitemaps to search engines
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, "Stores/" + rootSitemapFileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, "Stores/"+ rootSitemapFileName);

            this.generateSitemapForStores();

            return Content("OK!");
        }

        #region Sitemap Generations
        protected List<string> generateSitemapForProducts()
        {

            IProductService productService = EngineContext.Current.Resolve<IProductService>();

            List<string> sitemapFiles = new List<string>();
            int slice = 5000;
            var products = productService.GetSiteMapProducts();

            int productTotalCount = products.Count();
            bool isAdded = false;
            for (int i = 0; i < (productTotalCount / slice) + 1; i++)
            {
                var sm = new SitemapXml();
                var productList = products.Skip(i * slice).Take(slice).ToList();
                if (isAdded == false)
                {
                    var smn1 = new SitemapNode
                    {
                        changefrequency = ChangeFrequency.daily,
                        lastmodified = DateTime.Now.Date,
                        location = "https://urun.makinaturkiye.com",
                        priority = 0.9f
                    };
                    sm.items.Add(smn1);
                    isAdded = true;
                }

                foreach (var product in productList)
                {

                    double itemUpdatedDateBefore = DateTime.Now.Subtract(product.ProductRecordDate).TotalDays;
                    var smn = new SitemapNode
                    {
                        changefrequency = ChangeFrequency.daily,
                        lastmodified = itemUpdatedDateBefore > 30 ? DateTime.Now.Date : product.ProductRecordDate.Date,
                        location = string.Format("{0}{1}", PRODUCT_URL, Helpers.ToUrl(product.ProductName + "-p-" + product.ProductId)),
                        priority = 0.9f
                    };
                    sm.items.Add(smn);
                }
                string resultXml = XmlHelper.SerializeToString(sm, Encoding.UTF8);
                string sitemapFileName = String.Format("sitemap_Products-{0}.xml", (i + 1).ToString("00"));
                FileHelper.WriteToFile("/Sitemaps/Products/" + sitemapFileName, resultXml);
                sitemapFiles.Add(sitemapFileName);
            }

            return sitemapFiles;
        }
        protected string generateSitemapForStores()
        {
            IStoreService storeService = EngineContext.Current.Resolve<IStoreService>();

            var stores = storeService.GetAllStores(StoreActiveTypeEnum.Seller);

            var sm = new SitemapXml();
            foreach (var store in stores)
            {
                var smn1 = new SitemapNode
                {
                    changefrequency = ChangeFrequency.daily,
                    location = String.Format("{0}{1}", URL, Helpers.ToUrl(store.StoreUrlName)),
                    priority = 0.9f
                };
                sm.items.Add(smn1);
            }
            string resultXml = XmlHelper.SerializeToString(sm, Encoding.UTF8);
            string sitemapFileName = "sitemap_Stores.xml";
            FileHelper.WriteToFile("/Sitemaps/" + sitemapFileName, resultXml);
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, sitemapFileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, sitemapFileName);
            return sitemapFileName;
        }
        protected string generateSitemapForNews()
        {
            IStoreNewService _storNewService = EngineContext.Current.Resolve<IStoreNewService>();
            var news = _storNewService.GetAllStoreNews((byte)StoreNewType.Normal).Where(x => x.Active == true).OrderByDescending(x => x.RecordDate);

            var sm = new SitemapXml();
            foreach (var item in news)
            {
                var smn1 = new SitemapNode
                {
                    changefrequency = ChangeFrequency.daily,
                    location = UrlBuilder.GetStoreNewUrl(item.StoreNewId, item.Title),
                    priority = 0.8f
                };
                sm.items.Add(smn1);
            }
            string resultXml = XmlHelper.SerializeToString(sm, Encoding.UTF8);
            string sitemapFileName = "sitemap_News.xml";
            FileHelper.WriteToFile("/Sitemaps/" + sitemapFileName, resultXml);
            return sitemapFileName;
        }

        protected string generateSitemap_StoreCategory()
        {

            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapStoreCategories();
            string fileName = "sitemap_StoreCategory.xml";
            var path = Server.MapPath("/sitemaps/Stores/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            // add namespaces
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");


            writer.WriteStartElement("url");
            writer.WriteElementString("loc", "https://magaza.makinaturkiye.com");
            // start:optional
            writer.WriteElementString("priority", "0.7");
            writer.WriteEndElement(); //url
            // fake loop
            foreach (var item in categories)
            {
                string link = "https://magaza.makinaturkiye.com/";
                writer.WriteStartElement("url");
                string storePageTitle = "";
                if (!string.IsNullOrEmpty(item.StorePageTitle))
                {
                    if (item.StorePageTitle.Contains("Firma"))
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.CategoryNameOnyl);
                    }
                    else
                    {
                        storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.StorePageTitle, CategorySyntaxType.Store);

                    }
                }
                else if (!string.IsNullOrEmpty(item.CategoryContentTitle))
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryContentTitle, CategorySyntaxType.Store);
                else
                    storePageTitle = FormatHelper.GetCategoryNameWithSynTax(item.CategoryName, CategorySyntaxType.Store);
                // required
                if (item.CategoryType == 3 || item.CategoryType == 5)
                {
                    //link = link + Helpers.ToUrl(item.CategoryName) + "-" + Helpers.ToUrl(item.parentcatName) + "-firmalari-sc-" + item.CategoryId;
                }
                else
                {
                    link = UrlBuilder.GetStoreCategoryUrl(item.CategoryId, storePageTitle);
                }

                writer.WriteElementString("loc", link);
                // start:optional
                writer.WriteElementString("priority", "0.7");
                writer.WriteEndElement(); //url
            }
            writer.WriteEndElement(); //urlset 
            writer.WriteEndDocument();
            writer.Close();
            return fileName;
        }

        protected string generateSitemap_video()
        {
            IVideoService videoService = EngineContext.Current.Resolve<IVideoService>();
            var videos = videoService.GetSiteMapVideos();

            string fileName = "sitemap_Video.xml";
            var path = Server.MapPath("/sitemaps/Videos/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;
            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            // add namespaces
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
            writer.WriteAttributeString("xmlns", "video", null, "http://www.google.com/schemas/sitemap-video/1.0");

            // fake loop

            foreach (var item in videos)
            {
                writer.WriteStartElement("url");
                // required
                string link = "https://video.makinaturkiye.com/" + Helpers.ToUrl(item.ProductName) + "-v-" + item.VideoId;
                writer.WriteElementString("loc", link);
                writer.WriteStartElement("video", "video", null);
                // start:optional
                writer.WriteElementString("video", "title", null, item.ProductName + " " + item.BrandName + " " + item.ModelName + " " + item.VideoId);
                writer.WriteElementString("video", "description", null, item.ProductName + " video izle " + item.VideoId);
                writer.WriteElementString("video", "thumbnail_loc", null, "https://www.makinaturkiye.com/UserFiles/VideoThumb/" + item.VideoPicturePath);
                //writer.WriteElementString("video", "family_friendly", null, "Yes");
                writer.WriteElementString("video", "content_loc", null, "https://www.makinaturkiye.com/UserFiles/Video/" + item.VideoPath + ".flv");
                //writer.WriteElementString("video", "duration", null, "100");

                //writer.WriteStartElement("video", "player_loc", null);
                //writer.WriteAttributeString("allow_embed", "true");
                //writer.WriteString("https://www.makinaturkiye.com/embeddedplayer.swf");
                //writer.WriteEndElement(); // video:player_loc
                // end:optional

                writer.WriteEndElement(); // video:video
                writer.WriteEndElement(); //url
            }
            writer.WriteEndElement(); //urlset 
            writer.WriteEndDocument();
            //XDocument sitemap = new XDocument();
            //sitemap.Save(writer);
            writer.Close();
            string xmlpatch = System.IO.File.ReadAllText(path);

            return fileName;
        }
        protected string generateSitemap_videocategory()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapVideoCategories();

            string fileName = "sitemap_CategoryVideo.xml";

            var path = Server.MapPath("/sitemaps/Videos/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            writer.Formatting = Formatting.Indented;

            writer.WriteStartDocument();
            writer.WriteStartElement("urlset");
            // add namespaces
            writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");


            writer.WriteStartElement("url");
            // required
            string urlBase = "https://video.makinaturkiye.com";
            writer.WriteElementString("loc", urlBase);
            // start:optional
            writer.WriteElementString("priority", "0.8");
            writer.WriteEndElement(); //url

            foreach (var item in categories)
            {
                writer.WriteStartElement("url");
                // required
                string link = "https://video.makinaturkiye.com/" + Helpers.ToUrl(item.CategoryName) + "-videolari-vc-" + item.CategoryId;
                writer.WriteElementString("loc", link);
                // start:optional
                writer.WriteElementString("priority", "0.8");
                writer.WriteEndElement(); //url
            }
            writer.WriteEndElement(); //urlset 
            writer.WriteEndDocument();
            //XDocument sitemap = new XDocument();
            //sitemap.Save(writer);
            writer.Close();

            return fileName;
        }

        protected string generateSitemap_categorysector()
        {

            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.Sector);

            categories.Add(new global::MakinaTurkiye.Entities.StoredProcedures.Catalog.SiteMapCategoryResult
            {
                BrandName = "",
                CategoryContentTitle = "",
                CategoryId = 0,
                CategoryName = "",
            });

            string fileName = "sitemap-category-sector.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var urls = categories.ToDictionary(entry =>
            entry.CategoryId!=0?
            UrlBuilder.GetCategoryUrl(entry.CategoryId,
                                                     !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName, null, ""):
                                                     "https://www.makinaturkiye.com",
                                                     entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),

                    new XElement(ns + "priority", "1.0")
                      )
                    )
                  );

            sitemap.Save(writer);
            writer.Close();
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, fileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, fileName);
            return fileName;
        }
        protected string generateSitemap_categorybrand()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.Brand);

            string fileName = "sitemap-category-brand.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            const string url = "https://www.makinaturkiye.com/";
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            var urls = categories.ToDictionary(entry =>
                                  url + Helpers.ToUrl(entry.CategoryName) + "-" + Helpers.ToUrl(!string.IsNullOrEmpty(entry.TopCategoryContentTitle) ? entry.TopCategoryContentTitle : entry.FirstCategoryName) + "-c-" + entry.CategoryParentId + "-" + entry.CategoryId, entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "0.9")
                      )
                    )
                  );

            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }
        protected string generateSitemap_productGroupBrand()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.ProductGroupBrand);

            string fileName = "sitemap-category-product-group-brand.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            //var entries = _context.CategorySiteMapping((byte)CategoryType.ProductGroup).ToList();
            var urls = categories.ToDictionary(entry => UrlBuilder.GetCategoryUrl(entry.TopCategoryParentId.Value, !string.IsNullOrEmpty(entry.TopCategoryContentTitle) ? entry.TopCategoryContentTitle : entry.TopCategoryName, entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName), entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "1.0")
                      )
                    )
                  );

            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }
        protected string generateSitemap_categoryproductgroup()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.ProductGroup);

            string fileName = "sitemap-category-product-group.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            //var entries = _context.CategorySiteMapping((byte)CategoryType.ProductGroup).ToList();
            var urls = categories.ToDictionary(entry =>
            UrlBuilder.GetCategoryUrl(entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName, null, ""),
            entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),

                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "1.0")
                      )
                    )

                  );

            sitemap.Save(writer);
            writer.Close();
            var resultGoogle = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.google, fileName);
            var resultBing = new NotifySearchEngines().push(NotifySearchEngines.SearchEngine.bing, fileName);
            return fileName;
        }
        protected string generateSitemap_categoryorta()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.Category);

            string fileName = "sitemap-category.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            //var entries = _context.CategorySiteMapping(1).ToList();

            var urls = categories.ToDictionary(entry => UrlBuilder.GetCategoryUrl(entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ?
                entry.CategoryContentTitle : entry.CategoryName, null, ""), entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "1.0")
                      )
                    )
                  );
            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }

        protected string generateSitemap_categoryserie()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.Series);

            string fileName = "sitemap-category-series.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

            //var entries = _context.CategorySiteMapping((byte)CategoryType.Series).ToList();

            var urls = categories.ToDictionary(entry => UrlBuilder.GetSerieUrl(entry.CategoryId, entry.CategoryName, entry.BrandName, entry.FirstCategoryName), entry => DateTime.Now);

            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "0.4")
                      )
                    )
                  );
            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }

        #endregion



        #region oldMethods

        //protected List<string> generateSitemap_StoreCategory2()
        //{
        //    List<string> sitemapFiles = new List<string>();
        //    int slice = 5000;
        //    var stores = _context.spStoreCategorySitemapsecond().ToList();
        //    int storeCount = stores.Count();
        //    for (int i = 0; i < (storeCount / slice) + 1; i++)
        //    {
        //        var sm = new SitemapXml();
        //        var storesNew = stores.Skip(i * slice).Take(slice).ToList();
        //        foreach (var item in storesNew)
        //        {

        //            var smn = new SitemapNode
        //            {
        //                changefrequency = ChangeFrequency.daily,

        //                priority = 0.7f
        //            };
        //            string link = "";
        //            if (item.CategoryType == 3 || item.CategoryType == 5)
        //            {
        //                link = link + Helpers.ToUrl(item.CategoryName) + "-" + Helpers.ToUrl(item.parentcatName) + "-firmalari-sc-" + item.CategoryId;
        //            }
        //            else
        //            {
        //                link = link + Helpers.ToUrl(item.CategoryName) + "-firmalari-sc-" + item.CategoryId;
        //            }
        //            smn.location = link;

        //            sm.items.Add(smn);

        //        }
        //        string resultXml = XmlHelper.SerializeToString(sm, Encoding.UTF8);
        //        string sitemapFileName = String.Format("sitemap_StoreCategory-{0}.xml", (i + 1).ToString("00"));
        //        FileHelper.WriteToFile("/Sitemaps/" + sitemapFileName, resultXml);
        //        sitemapFiles.Add(sitemapFileName);

        //    }
        //    return sitemapFiles;

        //}


        protected List<string> generateSitemap_categorymodels()
        {
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategories(SiteMapCategoryTypeEnum.Model);
            List<string> sitemapFiles = new List<string>();
            int categoryTotalCount = categories.Count();
            int slice = 4000;
            for (int i = 0; i < (categoryTotalCount / slice) + 1; i++)
            {
                var sm = new SitemapXml();
                var categoryList = categories.Skip(i * slice).Take(slice).ToList();
                foreach (var category in categoryList)
                {
                    var smn = new SitemapNode
                    {
                        changefrequency = ChangeFrequency.daily,
                        lastmodified = DateTime.Now,
                        location = UrlBuilder.GetModelUrl(category.CategoryId, category.CategoryName, category.BrandName, category.FirstCategoryName, category.CategoryParentId.Value),
                        priority = 0.9f
                    };
                    sm.items.Add(smn);
                }
                string resultXml = XmlHelper.SerializeToString(sm, Encoding.UTF8);
                string sitemapFileName = String.Format("sitemap_Category-model{0}.xml", (i + 1).ToString("00"));
                FileHelper.WriteToFile("/Sitemaps/" + sitemapFileName, resultXml);
                sitemapFiles.Add(sitemapFileName);
            }
            return sitemapFiles;
        }
        //protected string generateSitemap_categorymodel1()
        //{
        //    string fileName = "sitemap-category-model-02.xml";

        //    var path = Server.MapPath("/sitemaps/" + fileName);
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.ModelSitemap((byte)CategoryType.Model).ToList();

        //    var urls = entries.ToDictionary(entry => UrlBuilder.ModelUrl(entry.CategoryId, entry.CategoryName, entry.Brand, entry.FirsCategoryName, entry.CategoryParentIdForModel.Value, ""), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.4")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();

        //    return fileName;
        //}
        protected string generateSitemap_categoryCountry()
        {
            string fileName = "sitemap-category-Country.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);
            const string url = "https://www.makinaturkiye.com/";
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum.Country);

            var urls = categories.ToDictionary(entry => UrlBuilder.GetCategoryUrl(entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName, null, "") + "?ulke=" + Helpers.ToUrl(entry.PlaceName + "-" + entry.PlaceId), entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "0.2")
                      )
                    )
                  );
            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }
        protected string generateSitemap_categoryCity()
        {
            string fileName = "sitemap-category-City.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);

            var writer = new XmlTextWriter(path, Encoding.UTF8);
            const string url = "https://www.makinaturkiye.com/";
            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();
            var categories = siteMapCategoryService.GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum.City);
            var urls = categories.ToDictionary(entry => UrlBuilder.GetCategoryUrl(entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName, null, "") + HttpUtility.HtmlDecode("?ulke=" + entry.UpPlaceId + "&sehir=") + Helpers.ToUrl(entry.PlaceName + "-" + entry.PlaceId), entry => DateTime.Now);

            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "0.6")
                      )
                    )
                  );
            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }



        protected string generateSitemap_categoryLocality()
        {
            string fileName = "sitemap-category-Locality.xml";

            var path = Server.MapPath("/sitemaps/" + fileName);
            var writer = new XmlTextWriter(path, Encoding.UTF8);

            XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
            ISiteMapCategoryService siteMapCategoryService = EngineContext.Current.Resolve<ISiteMapCategoryService>();


            var categories = siteMapCategoryService.GetSiteMapCategoriesPlace(CategoryPlaceTypeEnum.Locality);
            var urls = categories.ToDictionary(entry => UrlBuilder.GetCategoryUrl(entry.CategoryId, !string.IsNullOrEmpty(entry.CategoryContentTitle) ? entry.CategoryContentTitle : entry.CategoryName, null, "") + "?ulke=" + entry.CountryId + "&sehir=" + entry.UpPlaceId + "&ilce=" + Helpers.ToUrl(entry.PlaceName + "-" + entry.PlaceId), entry => DateTime.Now);
            var sitemap = new XDocument(
                new XDeclaration("1.0", "utf-8", "yes"),
                new XElement(ns + "urlset",
                    from u in urls
                    select
                    new XElement(ns + "url",
                    new XElement(ns + "loc", u.Key),
                   new XElement(ns + "priority", "0.2")
                      )
                    )
                  );
            sitemap.Save(writer);
            writer.Close();

            return fileName;
        }
        //protected string generateSitemap_BrandCountry()
        //{
        //    string fileName = "sitemap-category-BrandCountry.xml";

        //    var path = Server.MapPath("/sitemaps/" + fileName);
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.spCategoryPlace(4).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.2")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();

        //    return fileName;
        //}
        //protected string generateSitemap_BrandCity()
        //{
        //    string fileName = "sitemap-category-BrandCity.xml";

        //    var path = Server.MapPath("/sitemaps/" + fileName);
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.spCategoryPlace(5).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.4")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();

        //    return fileName;
        //}
        //protected string generateSitemap_BrandLocality()
        //{
        //    string fileName = "sitemap-category-BrandLocality.xml";

        //    var path = Server.MapPath("/sitemaps/" + fileName);
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.spCategoryPlace(6).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--246-" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName + "-" + entry.UpPlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.2")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();

        //    return fileName;
        //}

        //[Obsolete]
        //public ContentResult sitemapStoreCategory()
        //{
        //    //firma Categori
        //    var path = Server.MapPath("/sitemaps/sitemapstorecategory.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    writer.Formatting = Formatting.Indented;
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("urlset");
        //    // add namespaces
        //    writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //    var videositemap = _context.spStoreCategorySitemap().ToList();
        //    // fake loop
        //    foreach (var item in videositemap)
        //    {
        //        string link = "https://www.makinaturkiye.com/sirketler/";
        //        writer.WriteStartElement("url");
        //        // required
        //        if (item.CategoryType == 3 || item.CategoryType == 5)
        //        {
        //            link = link + item.CategoryId + "/" + Helpers.ToUrl(item.CategoryName) + "-" + Helpers.ToUrl(item.parentcatName);
        //        }
        //        else
        //        {
        //            link = link + item.CategoryId + "/" + Helpers.ToUrl(item.CategoryName);
        //        }
        //        writer.WriteElementString("loc", link);
        //        writer.WriteElementString("priority", "0.5");
        //        writer.WriteEndElement(); //url
        //    }
        //    writer.WriteEndElement(); //urlset 
        //    writer.WriteEndDocument();
        //    writer.Close();
        //    string xmlpatch = System.IO.File.ReadAllText(path);
        //    return Content(xmlpatch, "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapStoreCategorysecond()
        //{
        //    //firma kategori2
        //    var path = Server.MapPath("/sitemaps/sitemapstorecategory2.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    writer.Formatting = Formatting.Indented;
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("urlset");
        //    // add namespaces
        //    writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //    var videositemap = _context.spStoreCategorySitemapsecond().ToList();
        //    // fake loop
        //    foreach (var item in videositemap)
        //    {
        //        string link = "https://www.makinaturkiye.com/sirketler/";
        //        writer.WriteStartElement("url");
        //        // required
        //        if (item.CategoryType == 3 || item.CategoryType == 5)
        //        {
        //            link = link + item.CategoryId + "/" + Helpers.ToUrl(item.CategoryName) + "-" + Helpers.ToUrl(item.parentcatName);
        //        }
        //        else
        //        {
        //            link = link + item.CategoryId + "/" + Helpers.ToUrl(item.CategoryName);
        //        }
        //        writer.WriteElementString("loc", link);
        //        writer.WriteElementString("priority", "0.5");
        //        writer.WriteEndElement(); //url
        //    }
        //    writer.WriteEndElement(); //urlset 
        //    writer.WriteEndDocument();
        //    writer.Close();
        //    string xmlpatch = System.IO.File.ReadAllText(path);
        //    return Content(xmlpatch, "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategorysector()
        //{
        //    const string url = "https://www.makinaturkiye.com/";
        //    var path = Server.MapPath("/sitemaps/sitemap-category-sector.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.CategorySiteMapping((byte)CategoryType.Sector).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "/Sektor", entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //            new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategorybrand()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemap-category-brand.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.CategorySiteMapping((byte)CategoryType.Brand).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "/" + Helpers.ToUrl(entry.TopCategory), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //            new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );

        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}

        //[Obsolete]
        //public ContentResult sitemapcategoryproductgroup()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemap-category-product-group.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var entries = _context.CategorySiteMapping((byte)CategoryType.ProductGroup).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.TopCategory) + "/" + entry.CategoryId + "/" + Helpers.ToUrl(entry.CategoryName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //            new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );

        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategoryorta()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemap-category.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.CategorySiteMapping(1).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "/" + Helpers.ToPlural(Helpers.ToUrl(entry.ProductGroup)), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategoryserie()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemap-category-series.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.CategorySiteMapping((byte)CategoryType.Series).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.TopCategory) + "/" + entry.CategoryId + "/" + Helpers.ToUrl(entry.CategoryName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategorymodel()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemap-category-model.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.CategorySiteMapping((byte)CategoryType.Model).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.Brand) + "/" + entry.CategoryId + "/" + Helpers.ToUrl(entry.CategoryName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategoryCountry()
        //{
        //    //ülke kategori
        //    var path = Server.MapPath("/sitemaps/sitemap-category-Country.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(1).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "--" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategoryCity()
        //{
        //    //şehir kategori
        //    var path = Server.MapPath("/sitemaps/sitemap-category-City.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(2).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "--" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "1.0")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapcategoryLocality()
        //{
        //    //locality kategori
        //    var path = Server.MapPath("/sitemaps/sitemap-category-Locality.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(3).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName) + "/" + entry.CategoryId + "--246-" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.UpPlaceName + "-" + entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapBrandCountry()
        //{
        //    //Ülke brand
        //    var path = Server.MapPath("/sitemaps/sitemap-category-BrandCountry.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(4).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapBrandCity()
        //{
        //    //Şehir Brand
        //    var path = Server.MapPath("/sitemaps/sitemap-category-BrandCity.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(5).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapBrandLocality()
        //{
        //    //locality Brand
        //    var path = Server.MapPath("/sitemaps/sitemap-category-BrandLocality.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    const string url = "https://www.makinaturkiye.com/";
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";

        //    var entries = _context.spCategoryPlace(6).ToList();
        //    var urls = entries.ToDictionary(entry => url + Helpers.ToUrl(entry.CategoryName + "-" + entry.UpCategoryName) + "/" + entry.CategoryId + "--246-" + entry.UpPlaceId + "-" + entry.PlaceId + "/" + Helpers.ToUrl(entry.PlaceName + "-" + entry.UpPlaceName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}

        //[Obsolete]
        //public ContentResult sitemapvideo()
        //{
        //    //sitemap videoya özgü
        //    var path = Server.MapPath("/sitemaps/sitemapvideo.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    writer.Formatting = Formatting.Indented;
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("urlset");
        //    // add namespaces
        //    writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //    writer.WriteAttributeString("xmlns", "video", null, "http://www.google.com/schemas/sitemap-video/1.0");
        //    var videositemap = _context.videositemap();
        //    // fake loop
        //    foreach (var item in videositemap)
        //    {
        //        writer.WriteStartElement("url");
        //        // required
        //        string link = "https://www.makinaturkiye.com/video/" + item.VideoId + "/" + Helpers.ToUrl(item.ProductName);
        //        writer.WriteElementString("loc", link);
        //        writer.WriteStartElement("video", "video", null);
        //        // start:optional
        //        writer.WriteElementString("video", "title", null, item.ProductName + " " + item.BrandName + " " + item.ModelName + " " + item.VideoId);
        //        writer.WriteElementString("video", "description", null, item.ProductName + " video izle " + item.VideoId);
        //        writer.WriteElementString("video", "thumbnail_loc", null, "https://www.makinaturkiye.com/UserFiles/VideoThumb/" + item.VideoPicturePath);
        //        //writer.WriteElementString("video", "family_friendly", null, "Yes");
        //        writer.WriteElementString("video", "content_loc", null, "https://www.makinaturkiye.com/UserFiles/Video/" + item.VideoPath + ".flv");
        //        //writer.WriteElementString("video", "duration", null, "100");
        //        //writer.WriteStartElement("video", "player_loc", null);
        //        //writer.WriteAttributeString("allow_embed", "true");
        //        //writer.WriteString("https://www.makinaturkiye.com/embeddedplayer.swf");
        //        //writer.WriteEndElement(); // video:player_loc
        //        // end:optional
        //        writer.WriteEndElement(); // video:video
        //        writer.WriteEndElement(); //url
        //    }
        //    writer.WriteEndElement(); //urlset 
        //    writer.WriteEndDocument();
        //    writer.Close();
        //    string xmlpatch = System.IO.File.ReadAllText(path);
        //    return Content(xmlpatch, "text/xml");
        //}
        //[Obsolete]
        //public ContentResult sitemapvideocategory()
        //{
        //    //sitemap videoya özgü
        //    var path = Server.MapPath("/sitemaps/sitemapcategoryvideo.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    writer.Formatting = Formatting.Indented;
        //    writer.WriteStartDocument();
        //    writer.WriteStartElement("urlset");
        //    // add namespaces
        //    writer.WriteAttributeString("xmlns", "http://www.sitemaps.org/schemas/sitemap/0.9");
        //    var videocategorysitemap = _context.spsitemapVideoCategory();
        //    // fake loop
        //    foreach (var item in videocategorysitemap)
        //    {
        //        writer.WriteStartElement("url");
        //        string link = "https://www.makinaturkiye.com/videolar/" + item.categoryid + "/" + Helpers.ToUrl(item.categoryname);
        //        writer.WriteElementString("loc", link);
        //        // start:optional
        //        writer.WriteElementString("priority", "0.5");
        //        writer.WriteEndElement(); //url
        //    }
        //    writer.WriteEndElement(); //urlset 
        //    writer.WriteEndDocument();
        //    writer.Close();
        //    string xmlpatch = System.IO.File.ReadAllText(path);
        //    return Content(xmlpatch, "text/xml");
        //}

        //[Obsolete("data included with created generate method")]
        //public ContentResult sitemapfirma()
        //{
        //    //sitemap firma tarafı içinde yapılacak
        //    var path = Server.MapPath("/sitemaps/sitemapstore.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);
        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    const string url = "https://www.makinaturkiye.com/sirket/";
        //    var _context = new MakinaTurkiyeEntities();
        //    var entries = _context.firmagetir().ToList();
        //    //var urls = entries.ToDictionary(entry => url + entry.MainPartyId + "/" + entry.StoreName, entry => entry.MainPartyId);
        //    var urls = entries.ToDictionary(entry => url + entry.MainPartyId + "/" + Helpers.ToUrl(entry.StoreName) + "/SirketProfili", entry => DateTime.Now);
        //    var urls1 = entries.ToDictionary(entry => url + entry.MainPartyId + "/" + Helpers.ToUrl(entry.StoreName) + "/Urunler", entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            //ikinci kısım olan Urunleryazılacak 
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              ),
        //              from u in urls1
        //              select
        //              new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //            new XElement(ns + "lastmod", String.Format("{0:yyyy-MM-dd}", u.Value)),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );
        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete("data included with created generate method")]
        //public ContentResult productsitemapfirst()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemapproduct11.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);

        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var _context = new MakinaTurkiyeEntities();
        //    var entries = _context.productfirst().ToList();
        //    var urls = entries.ToDictionary(entry => "https://www.makinaturkiye.com" + Helpers.ProductUrl(entry.ProductId, entry.ProductName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );

        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}
        //[Obsolete("data included with created generate method")]
        //public ContentResult productsitemapsecond()
        //{
        //    var path = Server.MapPath("/sitemaps/sitemapproduct12.xml");
        //    var writer = new XmlTextWriter(path, Encoding.UTF8);

        //    XNamespace ns = "http://www.sitemaps.org/schemas/sitemap/0.9";
        //    var _context = new MakinaTurkiyeEntities();
        //    var entries = _context.productsecond().ToList();
        //    var urls = entries.ToDictionary(entry => "https://www.makinaturkiye.com" + Helpers.ProductUrl(entry.ProductId, entry.ProductName), entry => DateTime.Now);
        //    var sitemap = new XDocument(
        //        new XDeclaration("1.0", "utf-8", "yes"),
        //        new XElement(ns + "urlset",
        //            from u in urls
        //            select
        //            new XElement(ns + "url",
        //            new XElement(ns + "loc", u.Key),
        //           new XElement(ns + "priority", "0.5")
        //              )
        //            )
        //          );

        //    sitemap.Save(writer);
        //    writer.Close();
        //    return Content(sitemap.ToString(), "text/xml");
        //}

        #endregion
    }
}
