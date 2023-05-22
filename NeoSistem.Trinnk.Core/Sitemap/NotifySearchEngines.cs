using NeoSistem.Trinnk.Core.Helpers;
using System;
using System.Net;
using System.Web;

namespace NeoSistem.Trinnk.Core.Sitemap
{
    public class NotifySearchEngines
    {

        private static string __baseUrl = "http://www.trinnk.com/Sitemaps/";
        private WebClientEx _webClient;

        #region Request
        private string sitemapFileUrl;
        #endregion

        #region Response
        private bool _status = false;
        private string _result;

        public bool status { get { return _status; } }
        public string message { get { return _result; } }
        #endregion

        public enum SearchEngine
        {
            none, google, yandex, bing, baidu
        }

        private const string __googleUrl = "http://www.google.com/webmasters/tools/ping?sitemap=";
        private const string __yandexUrl = "http://webmaster.yandex.com/site/map.xml";
        private const string __bingUrl = "http://www.bing.com/webmaster/ping.aspx?siteMap=";
        private const string __baiduUrl = "http://zhanzhang.baidu.com/dashboard/index";

        public new NotifySearchEngines push(SearchEngine whichOne, string sitemapFileName)
        {
            try
            {
                if (whichOne.Equals(SearchEngine.none) || String.IsNullOrEmpty(sitemapFileName))
                {
                    //do nothing!
                }
                else
                {
                    this.sitemapFileUrl = __baseUrl + sitemapFileName;

                    string pushUrl = "";
                    switch (whichOne)
                    {
                        case SearchEngine.google:
                            pushUrl = __googleUrl;
                            break;
                        case SearchEngine.yandex:
                            pushUrl = __yandexUrl;
                            break;
                        case SearchEngine.bing:
                            pushUrl = __bingUrl;
                            break;
                        case SearchEngine.baidu:
                            pushUrl = __baiduUrl;
                            break;
                        default:
                            this._result = "Unknown!";
                            break;
                    }

                    pushUrl = pushUrl + HttpUtility.UrlEncode(this.sitemapFileUrl);
                    this._webClient = new WebClientEx();
                    string returnMessage = this._webClient.DownloadString(pushUrl);
                    if (this._webClient.StatusCode == HttpStatusCode.OK)
                    {
                        this._status = true;
                    }
                    this._result = returnMessage;
                }
            }
            catch (Exception ex)
            {
                this._result = ex.Message;
            }
            return this;
        }

    }
}
