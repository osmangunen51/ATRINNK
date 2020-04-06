#region Using

using System;
using System.IO;
using System.Net;
using System.Web;
using System.Web.Caching;


#endregion

namespace NeoSistem.MakinaTurkiye.Core.Web.HttpHandlers
{
    /// <summary>
    /// Removes whitespace in all stylesheets added to the 
    /// header of the HTML document in site.master. 
    /// </summary>
    public class JavaScriptHandler : IHttpHandler
  {
    /// <summary>
    /// Enables processing of HTTP Web requests by a custom 
    /// HttpHandler that implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext"></see> object that provides 
    /// references to the intrinsic server objects 
    /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
    /// </param>
    public void ProcessRequest(HttpContext context)
    {
      string query = context.Request.ServerVariables["QUERY_STRING"];

      int indexOf = query.IndexOf('=');
      if (query.Length > 3 && indexOf > -1)
      {
        query = query.Substring(indexOf + 1);
      }

      string path = HttpUtility.UrlDecode(query);

      if (!string.IsNullOrEmpty(path))
      {
        path = CoreSettings.Instance.ScriptFolder + path;
        string script = null;
        if (context.Cache[path] == null)
        {
          if (path.StartsWith("http", StringComparison.OrdinalIgnoreCase))
          {
            script = RetrieveRemoteScript(path);
          }
          else
          {
            script = RetrieveLocalScript(path);
          }
        }

        script = (string)context.Cache[path];
        if (!string.IsNullOrEmpty(script))
        {
          SetHeaders(script.GetHashCode(), context);
          context.Response.Write(script);

          HttpModules.CompressionModule.CompressResponse(context);//Compress(context);
        }
      }
    }

    /// <summary>
    /// Retrieves the local script from the disk
    /// </summary>
    private static string RetrieveLocalScript(string file)
    {
      if (!file.EndsWith(".js", StringComparison.OrdinalIgnoreCase))
      {
        throw new System.Security.SecurityException("No access");
      }

      string path = HttpContext.Current.Server.MapPath(file);
      string script = null;

      if (File.Exists(path))
      {
        using (StreamReader reader = new StreamReader(path))
        {
          script = reader.ReadToEnd();
          HttpContext.Current.Cache.Insert(file, script, new CacheDependency(path));
        }
      }

      return script;
    }


    /// <summary>
    /// Retrieves the specified remote script using a WebClient.
    /// </summary>
    /// <param name="file">The remote URL</param>
    private static string RetrieveRemoteScript(string file)
    {
      string script = null;

      try
      {
        Uri url = new Uri(file, UriKind.Absolute);

        using (WebClient client = new WebClient())
        {
          client.Credentials = CredentialCache.DefaultNetworkCredentials;
          script = client.DownloadString(url);
          HttpContext.Current.Cache.Insert(file, script, null, Cache.NoAbsoluteExpiration, new TimeSpan(3, 0, 0, 0));
        }
      }
      catch (System.Net.Sockets.SocketException)
      {
        // The remote site is currently down. Try again next time.
      }
      catch (UriFormatException)
      {
        // Only valid absolute URLs are accepted
      }

      return script;
    }

    /// <summary>
    /// This will make the browser and server keep the output
    /// in its cache and thereby improve performance.
    /// </summary>
    private static void SetHeaders(int hash, HttpContext context)
    {
      context.Response.ContentType = "text/javascript";
      context.Response.Cache.VaryByHeaders["Accept-Encoding"] = true;

      context.Response.Cache.SetExpires(DateTime.Now.ToUniversalTime().AddDays(7));
      context.Response.Cache.SetMaxAge(new TimeSpan(7, 0, 0, 0));
      context.Response.Cache.SetRevalidation(HttpCacheRevalidation.AllCaches);

      string etag = "\"" + hash.ToString() + "\"";
      string incomingEtag = context.Request.Headers["If-None-Match"];

      context.Response.Cache.SetETag(etag);
      context.Response.Cache.SetCacheability(HttpCacheability.Public);

      if (String.Compare(incomingEtag, etag) == 0)
      {
        context.Response.Clear();
        context.Response.StatusCode = (int)System.Net.HttpStatusCode.NotModified;
        context.Response.SuppressContent = true;
      }
    }

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"></see> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
    public bool IsReusable
    {
      get { return false; }
    }

  }
}