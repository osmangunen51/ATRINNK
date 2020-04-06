namespace NeoSistem.MakinaTurkiye.Web.Areas.Account
{
  using System.Web.Mvc;

  public class AccountAreaRegistration : AreaRegistration
  {
    public override string AreaName
    {
      get
      {
        return "Account";
      }
    }

    public override void RegisterArea(AreaRegistrationContext context)
    {


      context.MapRoute(
         "AdvertNotLimitedAccess",
          "Account/Advert/NotLimitedAccess",
          new { controller = "Advert", action = "NotLimitedAccess" }
      );
   context.MapRoute(
            "AccountHomeRoute1",
             "Account/Home",
             new { controller = "Home", action = "index2" }
         );
    
      context.MapRoute(
            "AccountHomeRoute",
             "Account/Home/index",
             new { controller = "Home", action = "index2" }
         );
      context.MapRoute(
         "AdvertNotMemberTypeAccess",
          "Account/Advert/NotMemberTypeAccess",
          new { controller = "Advert", action = "NotMemberTypeAccess" }
      );

            context.MapRoute(
"AdvertIndex",
"Account/ilan/Index",
new { controller = "Advert", action = "IndexNew" }
);
            context.MapRoute(
"AdvertIndex2",
"Account/Advert/Index",
new { controller = "Advert", action = "IndexNew" }
);

            context.MapRoute(
 "AdvertForCategory",
  "Account/ilan/CategoryProductGroup",
  new { controller = "Advert", action = "CategoryProductGroup" }
);
              context.MapRoute(
 "AdvertForCategoryBind",
  "Account/ilan/CategoryBind",
  new { controller = "Advert", action = "CategoryBind" }
);
            context.MapRoute(
"AdvertForMainImageEdit",
"Account/ilan/mainImageEdit",
new { controller = "Advert", action = "mainImageEdit" }
);
            context.MapRoute(
"PriceUpdate",
"Account/ilan/fiyatguncelle",
new { controller = "Advert", action = "ProductPriceUpdate" }
);


            context.MapRoute(
  "Advert_default",
  "Account/ilan/{action}/{id}",
  new { action = "Index",controller= "Advert", id = UrlParameter.Optional }
);
            context.MapRoute(
  "Account_default",
  "Account/{controller}/{action}/{id}",
  new { action = "Index", id = UrlParameter.Optional }
);
        
    }
  }
}