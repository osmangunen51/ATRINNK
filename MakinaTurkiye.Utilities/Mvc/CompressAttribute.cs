using MakinaTurkiye.Core.Configuration;
using MakinaTurkiye.Core.Infrastructure;
using System.IO.Compression;
using System.Web;
using System.Web.Mvc;

namespace MakinaTurkiye.Utilities.Mvc
{
    public class CompressAttribute : ActionFilterAttribute
    {
        public override void OnResultExecuting(ResultExecutingContext filterContext)
        {

            MakinaTurkiyeConfig config = EngineContext.Current.Resolve<MakinaTurkiyeConfig>();
            if (config.CompressEnabled)
            {
                HttpRequestBase request = filterContext.HttpContext.Request;
                string acceptEncoding = request.Headers["Accept-Encoding"];
                if (string.IsNullOrEmpty(acceptEncoding)) return;
                acceptEncoding = acceptEncoding.ToLowerInvariant();
                HttpResponseBase response = filterContext.HttpContext.Response;
                if (acceptEncoding.Contains("gzip"))
                {
                    response.AppendHeader("Content-encoding", "gzip");
                    response.Filter = new GZipStream(response.Filter, CompressionMode.Compress);
                }
                else if (acceptEncoding.Contains("deflate"))
                {
                    response.AppendHeader("Content-encoding", "deflate");
                    response.Filter = new DeflateStream(response.Filter, CompressionMode.Compress);
                }
            }
        }
    }
}
