using NeoSistem.MakinaTurkiye.Web.Infrastructure;
using System.Web.Mvc;
using System.Web.Routing;

namespace NeoSistem.MakinaTurkiye.Web.App_Start
{
    public class RouteConfig
    {
        public static void RegisterRoutes(RouteCollection routes)
        {
            routes.IgnoreRoute("{resource}.axd/{*pathInfo}");
            routes.IgnoreRoute("{*jpg}", new { jpg = @".*\.jpg(/.*)?" });
            routes.IgnoreRoute("{*gif}", new { gif = @".*\.gif(/.*)?" });
            routes.IgnoreRoute("{*png}", new { png = @".*\.png(/.*)?" });
            routes.IgnoreRoute("{*jpeg}", new { jpg = @".*\.jpeg(/.*)?" });
            routes.IgnoreRoute("{*ico}", new { ico = @".*\.ico(/.*)?" });
            routes.IgnoreRoute("{*bmp}", new { bmp = @".*\.bmp(/.*)?" });
            routes.IgnoreRoute("{*swf}", new { swf = @".*\.swf(/.*)?" });
            routes.LowercaseUrls = true;

            routes.MapRoute(
                name: "Defaultdil-degistir",
                url: "dil-degistir",
                defaults: new { controller = "Dil", action = "Index" }
            );

            routes.Add("DomainRouteForWrongProductContact", new DomainRoute(
            "urun.makinaturkiye.com", // Domain with parameters
            "Product/ProductContact",    // URL with parameters

            new { controller = "Product", action = "WrongUrlRedirect" }  // Parameter defaults
            ));

            routes.Add("DomainRouteForWrongHelp", new DomainRoute(
            "urun.makinaturkiye.com", // Domain with parameters
            "{categoryname}-y-{CategoryId}",    // URL with parameters
            new { controller = "Product", action = "WrongUrlHelp" }  // Parameter defaults
            ));

            //routes.Add(new ProductCustomRoute());
            routes.Add("DomainRouteForCategoryHome", new DomainRoute(
            "urun.makinaturkiye.com", // Domain with parameters
            "",    // URL with parameters
            new { controller = "Category", action = "Index2" }  // Parameter defaults
            ));



            routes.Add("DomainRouteForNew", new DomainRoute(
                "haber.makinaturkiye.com", // Domain with parameters
                "",    // URL with parameters
                new { controller = "StoreNew", action = "Index" },
                   new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));

            routes.Add("DomainRouteForNewDetail", new DomainRoute(
                "urun.makinaturkiye.com", // Domain with parameters
                "{newname}-h-{newId}",    // URL with parameters

                new { controller = "StoreNew", action = "Detail" },
                       new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));

            routes.Add("DomainRouteForProduct", new DomainRoute(
                "urun.makinaturkiye.com", // Domain with parameters
                "{productName}-p-{productId}",    // URL with parameters

                new { controller = "Product", action = "DetailClear" }  // Parameter defaults
                ));

            //      routes.Add("DomainRouteForWrongCategoryVideo", new DomainRoute(
            //"video.makinaturkiye.com", // Domain with parameters
            //"{categoryname}-c-{CategoryId}",    // URL with parameters
            //new { controller = "Videos", action = "WrongUrlCategory" }  // Parameter defaults
            //));

            routes.Add("DomainRouteForVideo", new DomainRoute(
                "video.makinaturkiye.com", // Domain with parameters
                "",    // URL with parameters

                new { controller = "Videos", action = "Index" }));




            routes.Add("DomainStoreIndexNew1", new DomainRoute(
                "magaza.makinaturkiye.com",
                "",
                new
                {
                    controller = "Store",
                    action = "Index"
                },
              new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            ));

            routes.Add("DomainStoreWrongUrl", new DomainRoute(
                "magaza.makinaturkiye.com",
                "{categoryName}-sc-/{categoryId}",
                new
                {
                    controller = "Store",
                    action = "StoreWrongUrl"
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));

            routes.Add("DomainStoreWrongUrl4", new DomainRoute(
    "magaza.makinaturkiye.com",
    "search/index",
    new
    {
        controller = "Store",
        action = "StoreWrongUrl"
    },
    new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));

            routes.Add("DomainVideoWrongUrl", new DomainRoute(
"video.makinaturkiye.com",
"search/index",
new
{
    controller = "Video",
    action = "Redirect301"
},
new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));
            routes.Add("DomainVideoWrongUrl2", new DomainRoute(
"video.makinaturkiye.com",
"Home/GetStoreProductComment",
new
{
    controller = "Video",
    action = "Redirect301"
},
new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }));



            routes.Add("DomainStoreCategoryNew", new DomainRoute(
                "magaza.makinaturkiye.com", // Domain with parameters
                "{categoryName}-sc-{categoryId}",    // URL with parameters
                new
                {
                    controller = "Store",
                    action = "Index"
                }));


            routes.MapRoute("DomainStoreCategoryOldTo2",
                "{categoryId}/{categoryName}-firmalari",
                new
                {
                    controller = "Store",
                    action = "Index",
                    categoryId = UrlParameter.Optional,
                    categoryName = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" });

            routes.Add("DomainVideoSectorNew", new DomainRoute(
            "video.makinaturkiye.com", // Domain with parameters
            "{categoryName}-vc-{categoryId}/",    // URL with parameters
            new
            {
                controller = "Videos",
                action = "Index",
                categoryId = UrlParameter.Optional,
                categoryName = UrlParameter.Optional
            }  // Parameter defaults
            ));


            routes.MapRoute(
            "redirectController",
            "redirectController/{link}/",
            new
            {
                controller = "StoreProfile",
                action = "redirect",
                link = UrlParameter.Optional
            },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );



            routes.MapRoute(
            "StoreProfileCompany1",
            "Sirket/{MainPartyId}/{StoreName}/",
            new { controller = "StoreProfileNew", action = "CompanyProfileNew", MainPartyId = UrlParameter.Optional },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );


            routes.MapRoute("admindenlogin", "Home/aktar/{id}/",
                new { controller = "Home", action = "aktar", id = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute("justlogin", "justlogin",
                new { controller = "Home", action = "StoreLogin" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "productstorelogin",
                "uyelikgiris",
                new { controller = "Home", action = "StoreGetIn" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "ProductMessageskismi",
                "mesajgonder/{MemberNo}/{ProductNo}/",
                new
                {
                    controller = "Home",
                    action = "messageViewProduct",
                    MemberNo = UrlParameter.Optional,
                    ProductNo = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "ProductMessagesshow",
                "Productmessage",
                new { controller = "Membership", action = "MembershipProductMessage" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            //routes.MapRoute(
            //    "mesajgoderimibasarili",
            //    "succeed",
            //    new { controller = "Home", action = "succeed" },
            //    new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            //    );
            routes.MapRoute(
                "mesajdanhızlıuyelikkayıt",
                "membershipformessages",
                new { controller = "Membership", action = "Membershipformessages" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "MessageToMembership",
                "messagetomembership",
                new { controller = "Home", action = "MessageMemberShip" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            //routes.MapRoute(
            //    "forquestion",
            //    "sendquestion",
            //    new { controller = "Home", action = "messageView" },
            //    new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            //    );

            routes.MapRoute(
                "MembershipSalesTrialpackage",
                "UyelikSatis/Deneme",
                new { controller = "MembershipSales", action = "Trialpackage" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "MembershipSalesOneStep",
                "UyelikSatis/Adim1",
                new { controller = "MembershipSales", action = "OneStep" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            //routes.MapRoute(
            //    "MembershipSalesTwoStep",
            //    "UyelikSatis/Adim2",
            //    new {controller = "MembershipSales", action = "TwoStep"},
            //    new[] {"NeoSistem.MakinaTurkiye.Web.Controllers"}
            //    );
            routes.MapRoute(
                "MembershipSalesThreeStep",
                "UyelikSatis/Adim2",
                new { controller = "MembershipSales", action = "ThreeStep" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "MembershipSalesPostNetOdeme",
                "UyelikSatis/PostNetOdeme",
                new { controller = "MembershipSales", action = "PostnetOdeme" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipSalesFourStep",
                "UyelikSatis/Adim3",
                new { controller = "MembershipSales", action = "FourStep" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipSalesFinishSales",
                "UyelikSatis/Adim4",
                new { controller = "MembershipSales", action = "FinishSales" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipSalesFinishSalesComplete",
                "UyelikSatis/CStep",
                new { controller = "MembershipSales", action = "VirtualPosComplete" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipSalesNotificationForm",
                "UyelikSatis/BildirimFormu",
                new { controller = "MembershipSales", action = "NotificationForm" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "RssFirms",
                "Rss-Firmalar",
                new { controller = "Rss", action = "Firm" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "RssProducts",
                "Rss-Ilanlar",
                new { controller = "Rss", action = "Product" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "RssRecentlyAddedFirms",
                "Rss-EskiFirmalar",
                new { controller = "Rss", action = "RecentlyAddedFirm" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "RssRecentlyAddedProducts",
                "Rss-EskiIlanlar",
                new { controller = "Rss", action = "RecentlyAddedProduct" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "Store",
                "Sirketler",
                new { controller = "Store", action = "Index" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "top20Search",
                "Search/Top20",
                new { controller = "Home", action = "SearchResultsTop" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute("Error", "Error", new { controller = "Home", action = "Error" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" });

            #region Obsolete > /Error

            routes.MapRoute(
                "PageNotFound",
                "Hata/SayfaBulunamadi",
                new { controller = "Home", action = "PageNotFound" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "CodingError",
                "Hata/SistemHatasi",
                new { controller = "Home", action = "CodingError" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "UlasilamayanSayfa",
                "Hata/SayfaSilindi",
                new { controller = "Home", action = "Error" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            #endregion

            routes.MapRoute(
                "FastMembership",
                "Uyelik/HizliUyelik/UyelikTipi-{MemberType}",
                new { controller = "Membership", action = "FastMembership" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
             "BulletinMemberShip",
             "Uyelik/bulten-uyeligi",
             new { controller = "Membership", action = "BulletinRegister" },
             new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
             );
            routes.MapRoute(
                "UpdateRelCategories",
                "Uyelik/FaaliyetAlanlari",
                new { controller = "Membership", action = "UpdateRelCategories" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "HelpRoute",
                "Yardim",
                new { controller = "Help", action = "Index" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );


            routes.MapRoute(
            "DynamicSiteMapRoute",
            "sitemap",
            new { controller = "SiteMap", action = "CreateAndView" },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );

            routes.MapRoute(
                "TermsOfUse",
                "KullanimKosullari",
                new { controller = "Home", action = "TermsOfUse" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "SiteMapRoute",
                "SiteHaritasi",
                new { controller = "Home", action = "SiteMap" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "RememberPasswordRoute",
                "Uyelik/SifremiUnuttum/{userId}",
                new { controller = "Membership", action = "ForgettedPassowrd", userId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipErrorRoute",
                "Uyelik/Hata",
                new { controller = "Membership", action = "MembershipError" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipLogOnRoute",
                "Uyelik/KullaniciGirisi",
                new { controller = "Membership", action = "LogOn" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
            "MembershipLogOnAutoRoute",
            "uyelik/kullanicigirismail/{validateId}",
            new { controller = "Membership", action = "LogonAuto", validateId = UrlParameter.Optional },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );
            routes.MapRoute(
                "MembershipLogoutRoute",
                "Uyelik/OturumuKapat",
                new { controller = "Membership", action = "Logout" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "InstitutionalStep1Route",
                "Uyelik/KurumsalUyelik/Adim-1",
                new { controller = "Membership", action = "InstitutionalStep1" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "InstitutionalStep2Route",
                "Uyelik/KurumsalUyelik/Adim-2",
                new { controller = "Membership", action = "InstitutionalStep2" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "InstitutionalStep3Route",
                "Uyelik/KurumsalUyelik/Adim-3",
                new { controller = "Membership", action = "InstitutionalStep3" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "InstitutionalStep4Route",
                "Uyelik/KurumsalUyelik/Adim-4",
                new { controller = "Membership", action = "InstitutionalStep4" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "InstitutionalStep5Route",
                "Uyelik/KurumsalUyelik/Adim-5",
                new { controller = "Membership", action = "InstitutionalStep5" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "MembershipApprovalRoute",
                "Uyelik/Onay",
                new { controller = "Membership", action = "MembershipApproval" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "ActivationCodeRoute",
                "Uyelik/Aktivasyon/{code}",
                new { controller = "Membership", action = "Activation", code = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
             "CreatePasswordRoute",
             "Uyelik/sifreolustur/{id}",
             new { controller = "Membership", action = "createPassword", id = UrlParameter.Optional },
             new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
             );
            routes.MapRoute(
                "LoginErrorRoute",
                "Uyelik/HataliGiris",
                new { controller = "Membership", action = "LoginError" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "ProductSearchResultsRoute",
                "Urunler/AramaSonuclari",
                new { controller = "Category", action = "SearchResults " },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "SearchRoute",
                "{categoryName}-c-{categoryId}/kelime-arama",
                new { controller = "Category", action = "SearchResults" },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
            "ProductSeachOneStepRoute",
            "kelime-arama",
            new { controller = "Category", action = "ProductSearchOneStep" },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );
            routes.MapRoute(
            "VideoSearchResultsRoute",
            "Videolar/AramaSonuclari",
            new { controller = "Videos", action = "VideoSearch" },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );

            routes.MapRoute(
                "VideoSectorItemsRoute",
                "Videolar/{categoryId}/{categoryName}/",
                new
                {
                    controller = "Videos",
                    action = "Index",
                    categoryId = UrlParameter.Optional,
                    categoryName = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );


            routes.MapRoute(
                "VideoSectorRoute",
                "Videolar",
                new { controller = "Videos", action = "Index" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "VideoProduct",
                "Video/{VideoId}/{ProductName}",
                new
                {
                    controller = "Videos",
                    action = "VideoItems2",
                    VideoId = UrlParameter.Optional,
                    ProductName = UrlParameter.Optional
                }, // Parameter defaults
                new { VideoId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
             "VideoProductNew",
             "{ProductName}-v-{VideoId}",
             new
             {
                 controller = "Videos",
                 action = "VideoItems2",
                 VideoId = UrlParameter.Optional,
                 ProductName = UrlParameter.Optional
             }, // Parameter defaults
             new { VideoId = @"\d\d+" },
             new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
             );


            routes.Add("DomainRouteForNewVideo", new DomainRoute(
    "video.makinaturkiye.com", // Domain with parameters
    "{productName}-v-{VideoId}",    // URL with parameters

    new { controller = "Videos", action = "VideoItems2" }  // Parameter defaults
    ));


            routes.MapRoute(
                "FirmForCategory",
                "Sirketler/{categoryId}/{categoryName}",
                new { controller = "Store", action = "Index" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "Message",
                "Mesaj/UyeNo={MemberNo}/UrunNo={ProductNo}/",
                new { controller = "Message", action = "Send" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreDetailedSearchRoute",
                "{CategoryDesc}/{CategoryId}/{CategoryGroupType}/Fagfa",
                new
                {
                    controller = "StoreSearch",
                    action = "CategoryParent",
                    CategoryId = UrlParameter.Optional,
                    CategoryGroupType = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "CategoryRoute2CityLocality",
                "{productGroupName}/{categoryId}-{categoryIddown}--{CountryId}-{CityId}-{LocalityId}/{LocalityName}-{CityName}/",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    categoryIddown = UrlParameter.Optional,
                    CityId = UrlParameter.Optional,
                    LocalityId = UrlParameter.Optional
                },
                new
                {
                    categoryIddown = @"\d+",
                    categoryId = @"\d+",
                    CityId = @"\d+",
                    LocalityId = @"\d+",
                    CountryId = @"\d+"
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
               ).DataTokens.Add("RouteName", "CategoryRoute2CityLocality");

            routes.MapRoute(
                "CategoryRoute2City",
                "{productGroupName}/{categoryId}-{categoryIddown}--{CountryId}-{CityId}/{CityName}",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    categoryIddown = UrlParameter.Optional,
                    CityId = UrlParameter.Optional
                },
                new { categoryIddown = @"\d+", categoryId = @"\d+", CityId = @"\d+", CountryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                  ).DataTokens.Add("RouteName", "CategoryRoute2City");

            routes.MapRoute(
                "CategoryRoute2Country",
                "{productGroupName}/{categoryId}-{categoryIddown}--{CountryId}/{CountryName}",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    categoryIddown = UrlParameter.Optional
                },
                new { categoryId = @"\d+", CountryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
              ).DataTokens.Add("RouteName", "CategoryRoute2Country");

            routes.MapRoute(
                "CategoryRouteCityLocality",
                "{productGroupName}/{categoryId}--{CountryId}-{CityId}-{LocalityId}/{LocalityName}-{CityName}/",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    CityId = UrlParameter.Optional,
                    LocalityId = UrlParameter.Optional
                },
                new { CityId = @"\d+", categoryId = @"\d+", LocalityId = @"\d+", CountryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                  ).DataTokens.Add("RouteName", "CategoryRouteCityLocality");

            routes.MapRoute(
                "CategoryRouteCity",
                "{productGroupName}/{categoryId}--{CountryId}-{CityId}/{CityName}/",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    CityId = UrlParameter.Optional
                },
                new { CityId = @"\d+", categoryId = @"\d+", CountryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
               ).DataTokens.Add("RouteName", "CategoryRouteCity");


            routes.MapRoute(
                "CategoryRouteCountry",
                "{productGroupName}/{categoryId}--{CountryId}/{CountryName}",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional
                },
                new { categoryId = @"\d+", CountryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
               ).DataTokens.Add("RouteName", "CategoryRouteCountry");

            routes.MapRoute(
                "CategoryRoute2",
                "{productGroupName}/{categoryId}-{categoryIddown}/{categoryName}/",
                new
                {
                    controller = "Category",
                    action = "Index2",
                    categoryId = UrlParameter.Optional,
                    categoryIddown = UrlParameter.Optional
                },
                new { categoryIddown = @"\d+", categoryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "CategoryRoute2");
            routes.MapRoute(
                "CategoryRoute",
                "{productGroupName}/{categoryId}/{categoryName}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "CategoryRoute");

            routes.MapRoute(
                "CategoryRouteNew",
                "{categoryName}-c-{categoryId}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "CategoryRouteNew");

            routes.MapRoute(
          "CategoryRouteNewForRedirect",
          "-c-{categoryId}",
          new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
          new { categoryId = @"\d+" },
          new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
          ).DataTokens.Add("RouteName", "CategoryRouteNew");

            routes.MapRoute(
                "CategoryRouteOld",
                "Makina/{categoryId}/{categoryName}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "CategoryRouteOld");
            routes.MapRoute(
              "CategoryRouteOld1",
              "Makine/{categoryId}/{categoryName}",
              new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
              new { categoryId = @"\d+" },
              new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
              ).DataTokens.Add("RouteName", "CategoryRouteOld1");

            routes.MapRoute(
              "CategoryRouteOld3",
              "Makineler/{categoryId}/{categoryName}",
              new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
              new { categoryId = @"\d\d\d\d+" },
              new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
              ).DataTokens.Add("RouteName", "CategoryRouteOld3");

            routes.MapRoute(
                "CategoryRouteOld4",
                "Makinalar/{categoryId}/{categoryName}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "CategoryRouteOld4");

            routes.MapRoute(
               "CategoryBrandRouteNew",
               "{categoryName}-c-{categoryId}-{categoryIddown}",
               new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
               new { categoryId = @"\d\d\d\d+", categoryIddown = @"\d+" },
               new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
               ).DataTokens.Add("RouteName", "CategoryBrandRouteNew");
            routes.MapRoute(
                "BrandRouteNew",
                "{brandName}-{categoryName}-b-{categoryId}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional, categoryIddown = UrlParameter.Optional },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "BrandRouteNew");

            routes.MapRoute(
                "SeriesRouteNew",
                "{seriesName}-{brandName}-{categoryName}-s-{categoryId}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "SeriesRouteNew");

            routes.MapRoute(
                "ModelRouteNewOther",
                "{modelName}-{brandName}-{categoryName}-m-{categoryId}-{selectedCategoryId}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional, selectedCategoryId = UrlParameter.Optional, modelName = UrlParameter.Optional },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "ModelRouteNew");

            routes.MapRoute(
                "ModelRouteNew",
               "{modelName}-{brandName}-{categoryName}-m-{categoryId}",
                new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "ModelRouteNew");

            routes.MapRoute(
           "ModelRouteNewAnoter",
          "{modelName}-{brandName}-m-{categoryId}-{selectedCategoryId}",
           new { controller = "Category", action = "Index2", categoryId = UrlParameter.Optional },
           new { categoryId = @"\d\d\d\d+" },
           new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
           ).DataTokens.Add("RouteName", "ModelRouteNew");

            #region StoreProfile



            routes.MapRoute(
                "StoreProfileGeneral",
                "Sirket/{MainPartyId}/{StoreName}/Urunler",
                new { controller = "StoreProfileNew", action = "ProductsNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileAbout",
                "Sirket/{MainPartyId}/{StoreName}/Hakkimizda",
                new { controller = "StoreProfileNew", action = "AboutUsNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileVideos",
                "Sirket/{MainPartyId}/{StoreName}/Videolarimiz",
                new { controller = "StoreProfileNew", action = "VideosNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "PacketPermission",
                "PaketYetkilendirme/Durum-{id}",
                new { controller = "Home", action = "PacketPermission", id = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileCompany",
                "Sirket/{MainPartyId}/{StoreName}/SirketProfili",
                new { controller = "StoreProfileNew", action = "CompanyProfileNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
               "StoreProfileCompany2",
               "Sirket/{MainPartyId}/{StoreName}/sirketrofili",
               new { controller = "StoreProfile", action = "CompanyProfile1", MainPartyId = UrlParameter.Optional },
               new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
               );

            routes.MapRoute(
                "StoreProfileBrand",
                "Sirket/{MainPartyId}/{StoreName}/Markalarimiz",
                new { controller = "StoreProfileNew", action = "BrandNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileDealer",
                "Sirket/{MainPartyId}/{StoreName}/BayiAgimiz",
                new { controller = "StoreProfileNew", action = "DealerNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileDealership",
                "Sirket/{MainPartyId}/{StoreName}/Bayiliklerimiz",
                new { controller = "StoreProfileNew", action = "DealershipNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileAuthorizedServices",
                "Sirket/{MainPartyId}/{StoreName}/ServisAgimiz",
                new { controller = "StoreProfileNew", action = "AuthorizedServicesNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileBranch",
                "Sirket/{MainPartyId}/{StoreName}/Subelerimiz",
                new { controller = "StoreProfileNew", action = "BranchNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "StoreProfileConnection",
                "Sirket/{MainPartyId}/{StoreName}/Iletisim",
                new { controller = "StoreProfileNew", action = "ConnectionNew", MainPartyId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            #endregion


            routes.MapRoute(
                "ProductDetail",
                "{productGroupName}/{productId}/{firstCategoryName}/{productName}",
                new { controller = "Product", action = "DetailClear", productId = UrlParameter.Optional },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );
            routes.MapRoute(
                "ProductDetailNew",
                "{productName}-p-{productId}",
                new { controller = "Product", action = "DetailClear", productId = UrlParameter.Optional },
                new { productId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "ProductDetailNew");

            routes.MapRoute(
                "ProductDetailNewWrong",
                "-p-{productId}",
                new { controller = "Product", action = "DetailClear", productId = UrlParameter.Optional },
                new { productId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                ).DataTokens.Add("RouteName", "ProductDetailNew");




            routes.MapRoute(
                "VideoSectorItemsRouteNew",
                "{categoryName}-vc-{categoryId}/",
                new
                {
                    controller = "Videos",
                    action = "Index",
                    categoryId = UrlParameter.Optional,
                    categoryName = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "FirmForCategorNewy",
                "{categoryName}-sc-{categoryId}",
                new { controller = "Store", action = "Index" },
                new { categoryId = @"\d\d\d\d+" },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
                "SearchMainPage",
                "Videolar/{query}/",
                new
                {
                    controller = "Videos",
                    action = "Index",
                    categoryId = UrlParameter.Optional,
                    categoryName = UrlParameter.Optional
                },
                new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
                );

            routes.MapRoute(
            name: "AdvancedSearch",
            url: "detayli-arama",
            defaults: new { controller = "Category", action = "AdvancedSearch", username = UrlParameter.Optional }
            );

            routes.MapRoute(
           "HelpDetailRouteNew",
           "{categoryname}-y-{CategoryId}",
           new { controller = "Help", action = "YardimDetay", categoryId = UrlParameter.Optional },
           new { CategoryId = @"\d\d\d\d+" },
           new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
           );

            routes.MapRoute(
            "DefaultSearch",
            "Search/Index",
            new { controller = "Search", action = "Index" },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );



            routes.MapRoute(
     "NewDetail",
     "{newname}-h-{newId}",
     new { controller = "StoreNew", action = "Detail", newId = UrlParameter.Optional },
     new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
     );


            routes.MapRoute(
     "ProductRequest",
     "alim-talebi",
     new { controller = "ProductRequest", action = "step1", newId = UrlParameter.Optional },
     new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
     );


            routes.MapRoute(
     name: "CompanyProfileNewRoute",
     url: "{username}",
     defaults: new { controller = "StoreProfileNew", action = "CompanyProfileNew", username = UrlParameter.Optional },
      constraints: new { username = new UserNameConstraint() }
 );

            routes.MapRoute(
                name: "CompanyProfileNewRouteWithParam",
                url: "{username}/SirketProfili",
                defaults: new { controller = "StoreProfileNew", action = "CompanyProfileNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
              name: "StoreProfileProductsNewRoute",
              url: "{username}/Urunler",
              defaults: new { controller = "StoreProfileNew", action = "ProductsNew", username = UrlParameter.Optional },
               constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
                name: "StoreProfileProductsStoreVideos",
                url: "{username}/tanitim-videolar",
                defaults: new { controller = "StoreProfileNew", action = "StorePromotionVideo", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
              name: "StoreProfileProductsNewCategory",
              url: "{username}/{categoryname}-c-{CategoryId}",
              defaults: new { controller = "StoreProfileNew", action = "ProductsNew", username = UrlParameter.Optional, CategoryId = UrlParameter.Optional },
              constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
                name: "StoreProfileDealerNewRoute",
                url: "{username}/BayiAgimiz",
                defaults: new { controller = "StoreProfileNew", action = "DealerNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
                name: "StoreProfileAboutUsNewRoute",
                url: "{username}/Hakkimizda",
                defaults: new { controller = "StoreProfileNew", action = "AboutUsNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() });

            routes.MapRoute(
                name: "StoreProfileBranchNewRoute",
                url: "{username}/Subelerimiz",
                defaults: new { controller = "StoreProfileNew", action = "BranchNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() }
                );

            routes.MapRoute(
                name: "StoreProfileBrandNewRoute",
                url: "{username}/Markalarimiz",
                defaults: new { controller = "StoreProfileNew", action = "BrandNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() }
                );

            routes.MapRoute(
"AdvertİlanDeleteEdit",
"Account/ilan/DeletePictureEditPage",
new { controller = "Advert", action = "DeletePictureEditPage" },
namespaces: new[] { "Web.Areas.Account.Controllers" }

);
            routes.MapRoute(
                name: "StoreProfileVideosNewRoute",
                url: "{username}/Videolarimiz",
                defaults: new { controller = "StoreProfileNew", action = "VideosNew", username = UrlParameter.Optional },
                constraints: new { username = new UserNameConstraint() }
                );

            routes.MapRoute(
            name: "StoreProfileImagesNewRoute",
            url: "{username}/gorseller",
            defaults: new { controller = "StoreProfileNew", action = "StoreImagesNew", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );

            routes.MapRoute(
            name: "StoreProfileAuthorizedServicesNewRoute",
            url: "{username}/ServisAgimiz",
            defaults: new { controller = "StoreProfileNew", action = "AuthorizedServicesNew", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );



            routes.MapRoute(
            name: "StoreProfileDealerShipNewRoute",
            url: "{username}/Bayiliklerimiz",
            defaults: new { controller = "StoreProfileNew", action = "DealerShipNew", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );
            routes.MapRoute(
            name: "StoreProfileConnectionNewRoute",
            url: "{username}/Iletisim",
            defaults: new { controller = "StoreProfileNew", action = "ConnectionNew", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );

            routes.MapRoute(
            name: "StoreProfileCotologNewRoute",
            url: "{username}/Kataloglar",
            defaults: new { controller = "StoreProfileNew", action = "CatologNew", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );

            routes.MapRoute(
            name: "StoreProfileNewNewRoute",
            url: "{username}/haberler",
            defaults: new { controller = "StoreProfileNew", action = "News", username = UrlParameter.Optional },
            constraints: new { username = new UserNameConstraint() }
            );


            routes.MapRoute(
            name: "ResultPayTurkish",
            url: "membershipsales/odeme-sonuc",
            defaults: new { controller = "MembershipSales", action = "ResultPay", username = UrlParameter.Optional }
            );

            routes.MapRoute(
            "CatalogAjax",
            "ajax/{action}/{id}",
            new { controller = "CatologAjax", action = UrlParameter.Optional, id = UrlParameter.Optional },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );



            routes.MapRoute(
            "Default",
            "{controller}/{action}/{id}",
            new { controller = "Home", action = "Index", id = UrlParameter.Optional },
            new[] { "NeoSistem.MakinaTurkiye.Web.Controllers" }
            );




            routes.MapRoute(
                "NotFound",
                "{*url}",
                new { controller = "StoreProfile", action = "redirect" }
                );
        }
    }
}