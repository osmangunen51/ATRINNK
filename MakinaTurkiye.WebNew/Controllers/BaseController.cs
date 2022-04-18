using MakinaTurkiye.Core;
using NeoSistem.MakinaTurkiye.Web.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;


namespace NeoSistem.MakinaTurkiye.Web.Controllers
{

    public class SessionSingularViewCountType
    {
        static readonly string SingularViewCountType = "SingularViewCountType";

        internal static Dictionary<int, SingularViewCountType> SingularViewCountTypes
        {
            get
            {
                if (HttpContext.Current.Session[SingularViewCountType] == null)
                {
                    HttpContext.Current.Session[SingularViewCountType] = new Dictionary<int, SingularViewCountType>();
                }
                return HttpContext.Current.Session[SingularViewCountType] as Dictionary<int, SingularViewCountType>;
            }
            set { HttpContext.Current.Session[SingularViewCountType] = value; }
        }
    }

    public class SessionStatisticIds
    {
        static readonly string StatisticId = "StatisticId";



        internal static Dictionary<int, string> StatisticIds
        {
            get
            {
                if (HttpContext.Current.Session[StatisticId] == null)
                {
                    HttpContext.Current.Session[StatisticId] = new Dictionary<int, string>();
                }
                return HttpContext.Current.Session[StatisticId] as Dictionary<int, string>;
            }
            set { HttpContext.Current.Session[StatisticId] = value; }
        }

    }

    [HandleError]
    public class BaseController : Core.Web.Controller
    {


        private string _IpAdres = "";

        public string IpAdres
        {
            get
            {
                string userip = Request.UserHostAddress;
                if (Request.UserHostAddress != null)
                {
                    Int64 macinfo = new Int64();
                    string macSrc = macinfo.ToString("X");
                    if (macSrc == "0")
                    {
                        _IpAdres = userip;
                    }
                }
                return _IpAdres;
            }
        }

        protected void CreateCookie(string name, string value, DateTime expireDate)
        {
            HttpCookie cookieVisitor = new HttpCookie(name, value);
            cookieVisitor.Expires = expireDate;
            if (!Request.IsLocal)
                cookieVisitor.Domain = ".makinaturkiye.com";
            Response.Cookies.Add(cookieVisitor);
        }
        protected string GetCookie(string name)
        {
            if (Request.Cookies[name] != null)
            {
                return Request.Cookies[name].Value;
            }
            return null;
        }
        protected void DeleteCookie(string name)
        {
            if (Request.Cookies[name] != null)
            {
                Response.Cookies.Remove(name);
                Response.Cookies[name].Expires = DateTime.Now.AddDays(-1);
            }
        }

        private const string ViewNavigation = "ViewNavigation";

        protected string NavigationContent
        {
            set { ViewData[ViewNavigation] = value; }
        }



        public ActionResult StoreFail()
        {
            Response.RedirectPermanent("https://www.makinaturkiye.com" + Url.RouteUrl(new
            {
                controller = "Store",
                action = "Index"
            }));
            return null;
        }


        public static string RemoveQueryStringByKey(string url, string key)
        {
            var uri = new Uri(url);

            // this gets all the query string key value pairs as a collection
            var newQueryString = HttpUtility.ParseQueryString(uri.Query);

            // this removes the key if exists
            newQueryString.Remove(key);

            // this gets the page path from root without QueryString
            string pagePathWithoutQueryString = uri.GetLeftPart(UriPartial.Path);

            return newQueryString.Count > 0
                ? String.Format("{0}?{1}", pagePathWithoutQueryString, newQueryString)
                : pagePathWithoutQueryString;
        }

        public class RemoveDuplicateContentAttribute : ActionFilterAttribute

        {
            public override void OnActionExecuting(ActionExecutingContext filterContext)

            {
                var routes = RouteTable.Routes;
                var requestContext = filterContext.RequestContext;
                var routeData = requestContext.RouteData;
                var dataTokens = routeData.DataTokens;
                var vpd = routes.GetVirtualPathForArea(requestContext, routeData.Values);
                if (vpd != null)
                {
                    var virtualPath = vpd.VirtualPath.ToLower();
                    var request = requestContext.HttpContext.Request;


                    string url = request.Url.ToString();
                    if (url.Contains("SearchType"))
                    {
                        url = RemoveQueryStringByKey(url, "SearchType");
                    }
                    if (url.Contains("Gorunum"))
                        url = RemoveQueryStringByKey(url, "Gorunum");
                    if (url.Contains("Order"))
                        url = RemoveQueryStringByKey(url, "Order");

                    //if (request.Url.Query.Contains("page"))
                    //{

                    //    canonical += $"?page={request.QueryString["page"].ToString()}";

                    //}



                    filterContext.Controller.ViewBag.Canonical = url;

                }
            }

        }

        public class Navigation
        {
            private string title;
            public string Title
            {
                get { return title; }
                set { title = value; }
            }

            private string url;
            public string Url
            {
                get { return url; }
                set { url = value; }
            }

            public enum TargetType
            {
                _blank,
                _self
            }

            private TargetType target;
            public TargetType Target
            {
                get { return target; }
                set { target = value; }
            }

            public Navigation(string nTitle, string nUrl, TargetType nTarget = TargetType._self)
            {
                title = nTitle;
                url = nUrl;
                target = nTarget;
            }

        }




        protected T GetEnumValue<T>(int intValue) where T : struct, IConvertible
        {
            if (!typeof(T).IsEnum)
            {
                throw new Exception("T must be an Enumeration type.");
            }
            T val = ((T[])Enum.GetValues(typeof(T)))[0];

            foreach (T enumValue in (T[])Enum.GetValues(typeof(T)))
            {
                if (Convert.ToInt32(enumValue).Equals(intValue))
                {
                    val = enumValue;
                    break;
                }
            }
            return val;
        }
        public string temp;

        public void LoadNavigation(IList<Navigation> navMenu)
        {
            StringBuilder navigation = new StringBuilder();
            navigation.AppendLine("<ol class='breadcrumb breadcrumb-mt'>");
            for (int i = 0; i < navMenu.Count; i++)
            {
                Navigation nn = (Navigation)navMenu[i];
                if (String.IsNullOrEmpty(nn.Url) || i == navMenu.Count - 1)
                {
                    navigation.AppendLine("<li class='active'>" + nn.Title + "</li>");
                }
                else
                {
                    navigation.AppendLine(" <li><a target='" + nn.Target + "' href='" + AppSettings.SiteUrl + "" + nn.Url + "'>" + nn.Title + "</a></li>");
                }
            }
            navigation.AppendLine("</ol>");
            NavigationContent = navigation.ToString();
        }

        public string LoadNavigationV2(IList<Navigation> navMenu)
        {
            StringBuilder navigation = new StringBuilder();
            navigation.AppendLine("<ol class='breadcrumb breadcrumb-mt'>");
            for (int i = 0; i < navMenu.Count; i++)
            {
                Navigation nn = (Navigation)navMenu[i];
                if (String.IsNullOrEmpty(nn.Url) || i == navMenu.Count - 1)
                {
                    navigation.AppendLine("<li class='active'>" + nn.Title + "</li>");
                }
                else
                {
                    navigation.AppendLine(" <li><a target='" + nn.Target + "' href='" + "" + nn.Url + "'>" + nn.Title + "</a></li>");
                }
            }
            navigation.AppendLine("</ol>");
            return navigation.ToString();
        }

        public string LoadNavigationV3(IList<Navigation> navMenu)
        {
            StringBuilder navigation = new StringBuilder();
            navigation.AppendLine("<ol class='breadcrumb breadcrumb-mt'>");
            for (int i = 0; i < navMenu.Count; i++)
            {
                Navigation nn = (Navigation)navMenu[i];
                if (String.IsNullOrEmpty(nn.Url) || (i + 1 == navMenu.Count))
                {
                    navigation.AppendLine("<li class='active'>" + nn.Title + "</li>");
                }
                else
                {
                    navigation.AppendLine(" <li><a target='" + nn.Target + "' href='" + nn.Url + "'>" + nn.Title + "</a></li>");
                }
            }

            navigation.AppendLine("</ol>");
            return navigation.ToString();
        }

        //private const string SEOPAGETYPE = "SEOPAGETYPE";

        //protected byte SeoPageType
        //{
        //    set { ViewData[SEOPAGETYPE] = value; }
        //}

        //private const string SEOPAGESPECIAL = "SEOPAGESPECIAL";

        //protected int SeoPageSpecial
        //{
        //    set
        //    {
        //        try
        //        {
        //            ViewData[SEOPAGESPECIAL] = value;
        //        }
        //        catch
        //        {

        //            ViewData[SEOPAGESPECIAL] = 0;
        //        }
        //    }
        //}

        //protected void CreateSeoParameter(string name, string value)
        //{
        //    Session[name] = value;
        //}


        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            base.Initialize(requestContext);
        }


        #region QueryString Values

        public int GetCategoryIdQueryString()
        {
            int categoryId = 0;
            if (Request.QueryString["categoryId"] == null)
                return categoryId;

            int.TryParse(Request.QueryString["categoryId"], out categoryId);
            return categoryId;
        }

        public int GetCountryIdQueryString()
        {
            int countryId = 0;
            if (Request.QueryString["ulke"] == null)
                return countryId;

            if (Request.QueryString["ulke"].Contains("-"))
                int.TryParse(Request.QueryString["ulke"].Split('-')[1], out countryId);
            else
                int.TryParse(Request.QueryString["ulke"], out countryId);

            return countryId;
        }

        public int GetLocalityIdQueryString()
        {
            int localityId = 0;
            if (Request.QueryString["ilce"] == null)
                return localityId;

            if (Request.QueryString["ilce"].Contains("-"))
                int.TryParse(Request.QueryString["ilce"].Split('-')[1], out localityId);
            else
                int.TryParse(Request.QueryString["ilce"], out localityId);

            return localityId;
        }

        public int GetCityIdQueryString()
        {
            int cityId = 0;
            if (Request.QueryString["sehir"] == null)
                return cityId;

            if (Request.QueryString["sehir"].Contains("-"))
                int.TryParse(Request.QueryString["sehir"].Split('-')[1], out cityId);
            else
                int.TryParse(Request.QueryString["sehir"], out cityId);

            return cityId;
        }

        public string GetSearchTextRouteData()
        {
            if (RouteData.Values["SearchText"] == null)
                return string.Empty;

            return RouteData.Values["SearchText"].ToString();
        }

        public int GetCurrentPageQueryString()
        {
            int currentPage = 1;
            if (Request.QueryString["currentPage"] == null)
                return currentPage;

            int.TryParse(Request.QueryString["currentPage"], out currentPage);
            return currentPage;
        }

        public int GetPageQueryString()
        {
            int page = 1;
            if (Request.QueryString["page"] == null)
                return page;

            int.TryParse(Request.QueryString["page"], out page);
            return page == 0 ? 1 : page;
        }

        public string GetCustomFilterQueryString()
        {
            string customFilter = "";
            if (Request.QueryString["customFilter"] == null)
                return customFilter;

            customFilter = Request.QueryString["customFilter"].ToString();
            return customFilter;
        }

        public int GetActivityTypeQueryString()
        {
            var activityType = 0;
            if (Request.QueryString["filtre"] == null)
            {
                return activityType;
            }
            int.TryParse(Request.QueryString["filtre"], out activityType);
            return activityType;
        }

        public string GetActivityTypeFilterQueryString()
        {
            return Request.QueryString["filtre"] ?? string.Empty;
        }

        public int GetOrderByQueryString()
        {
            int orderby = 0;
            if (Request.QueryString["orderby"] == null)
                return orderby;

            int.TryParse(Request.QueryString["orderby"], out orderby);
            return orderby;
        }

        public string GetOrderQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString["Order"]))
                return string.Empty;

            return Request.QueryString["Order"];
        }

        public string GetSearchTextQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString["searchText"]))
                return string.Empty;

            return Request.QueryString["searchText"];
        }

        #region Url Kýsmýnda Þehir ve Ýlçe Ýsimlerinin Alýnmasý

        /// <summary>
        /// Url'deki cityId deðerini döndürür tanýmlý deðilse "0" döndürür
        /// </summary>
        /// <returns></returns>
        public string GetCityNameQueryString()
        {
            return Request.QueryString["cityId"] ?? "0";
        }
        /// <summary>
        /// Url'deki localityId deðerini döndürür tanýmlý deðilse "0" döndürür
        /// </summary>
        /// <returns></returns>
        public string GetLocalityNameQueryString()
        {
            return Request.QueryString["localityId"] ?? "0";
        }

        #endregion
        public List<int> GetLocalityIdsQueryString()
        {
            var result = new List<int>();
            if (Request.QueryString["localityId"] == null)
                return result;

            string alreadyFilteredLocaityIdStr = Request.QueryString["localityId"];
            if (String.IsNullOrWhiteSpace(alreadyFilteredLocaityIdStr))
                return result;

            foreach (var locality in alreadyFilteredLocaityIdStr.Split(new char[] { ',' }, StringSplitOptions.RemoveEmptyEntries))
            {
                int localityId = 0;
                int.TryParse(locality.Trim(), out localityId);
                if (!result.Contains(localityId))
                    result.Add(localityId);
            }
            return result;
        }
        /// <summary>
        /// Url'deki localityId deðerlerini string listesi olarak geri döndürür
        /// </summary>
        /// <returns></returns>
        public List<string> GetLocalityNamesQueryString()
        {
            var res = new List<string>();
            if (string.IsNullOrWhiteSpace(Request.QueryString["localityId"])) return res;
            var localityNames = Request.QueryString["localityId"];
            var splitLocalityNames = localityNames.Split(new[] { ',' }, StringSplitOptions.RemoveEmptyEntries);
            foreach (var locality in splitLocalityNames)
            {
                if (!res.Contains(locality)) res.Add(locality);
            }
            return res;
        }

        public string GetSearchTypeQueryString()
        {
            if (string.IsNullOrEmpty(Request.QueryString["SearchType"]))
                return string.Empty;

            return Request.QueryString["SearchType"];
        }

        public int GetPageSizeQueryString()
        {
            int pageSize = 36;
            if (Request.QueryString["pageSize"] == null)
                return pageSize;

            int.TryParse(Request.QueryString["pageSize"], out pageSize);
            return pageSize;
        }

        #endregion

        #region Route Data Values

        public int GetCategoryIdRouteData()
        {
            int categoryId = 0;
            if (this.RouteData.Values["categoryId"] == null)
                return categoryId;

            int.TryParse(this.RouteData.Values["categoryId"].ToString(), out categoryId);
            return categoryId;
        }

        public int GetSelectedCategoryIdRouteData()
        {
            int categoryId = 0;
            if (this.RouteData.Values["selectedCategoryId"] == null)
                return categoryId;

            int.TryParse(this.RouteData.Values["selectedCategoryId"].ToString(), out categoryId);
            return categoryId;
        }


        public byte GetNewTypeRouteData()
        {
            byte newType = 0;
            if (this.RouteData.Values["newType"] == null)
                return newType;

            byte.TryParse(this.RouteData.Values["newType"].ToString(), out newType);
            return newType;
        }

        public int GetNewIdRouteData()
        {
            int newId = 0;
            if (this.RouteData.Values["newId"] == null)
                return newId;

            int.TryParse(this.RouteData.Values["newId"].ToString(), out newId);
            return newId;
        }

        public int GetCountryIdRouteData()
        {
            int countryId = 0;
            if (this.RouteData.Values["CountryId"] == null)
                return countryId;

            int.TryParse(this.RouteData.Values["CountryId"].ToString(), out countryId);
            return countryId;
        }


        public int GetCityIdRouteData()
        {
            int cityId = 0;
            if (this.RouteData.Values["CityId"] == null)
                return cityId;

            int.TryParse(this.RouteData.Values["CityId"].ToString(), out cityId);
            return cityId;
        }



        public int GetLocalityIdRouteData()
        {
            int localityId = 0;
            if (this.RouteData.Values["LocalityId"] == null)
                return localityId;

            int.TryParse(this.RouteData.Values["LocalityId"].ToString(), out localityId);
            return localityId;
        }

        public int GetBrandIdRouteData()
        {
            int brandId = 0;
            if (this.RouteData.Values["categoryIddown"] == null)
                return brandId;

            int.TryParse(this.RouteData.Values["categoryIddown"].ToString(), out brandId);
            return brandId;
        }

        public int GetModelIdRoutData()
        {
            int modelId = 0;
            if (this.RouteData.Values["modelId"] == null)
                return modelId;

            int.TryParse(this.RouteData.Values["modelId"].ToString(), out modelId);
            return modelId;
        }

        public int GetSeriesIdRoutData()
        {
            int seriesId = 0;
            if (this.RouteData.Values["seriesId"] == null)
                return seriesId;

            int.TryParse(this.RouteData.Values["seriesId"].ToString(), out seriesId);
            return seriesId;
        }

        public int GetVideoIdRoutData()
        {
            int videoId = 0;
            if (this.RouteData.Values["VideoId"] == null)
                return videoId;

            int.TryParse(this.RouteData.Values["VideoId"].ToString(), out videoId);
            return videoId;
        }

        public string GetStoreUsernameRoutData()
        {
            string storeUsername = string.Empty;
            if (this.RouteData.Values["username"] == null)
                return storeUsername;

            storeUsername = this.RouteData.Values["username"].ToString();
            return storeUsername;
        }

        public int GetProductIdRoutData()
        {
            int productId = 0;
            if (this.RouteData.Values["productId"] == null)
                return productId;

            int.TryParse(this.RouteData.Values["productId"].ToString(), out productId);
            return productId;
        }

        #endregion
    }
}