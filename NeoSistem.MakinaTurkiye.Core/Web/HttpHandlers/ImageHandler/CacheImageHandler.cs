#region Using

using System;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web;

#endregion

namespace NeoSistem.MakinaTurkiye.Core.Web.HttpHandlers
{
    /// <summary>
    /// The ImageHanlder serves all images that is uploaded from
    /// the admin pages. 
    /// </summary>
    /// <remarks>
    /// By using a HttpHandler to serve images, it is very easy
    /// to add the capability to stop bandwidth leeching or
    /// to create a statistics analysis feature upon it.
    /// </remarks>
    public class CacheImageHandler : IHttpHandler
  {

    #region IHttpHandler Members

    /// <summary>
    /// Gets a value indicating whether another request can use the <see cref="T:System.Web.IHttpHandler"></see> instance.
    /// </summary>
    /// <value></value>
    /// <returns>true if the <see cref="T:System.Web.IHttpHandler"></see> instance is reusable; otherwise, false.</returns>
    public bool IsReusable
    {
      get { return false; }
    }


    /// <summary>
    /// Enables processing of HTTP Web requests by a custom HttpHandler that 
    /// implements the <see cref="T:System.Web.IHttpHandler"></see> interface.
    /// </summary>
    /// <param name="context">An <see cref="T:System.Web.HttpContext"></see> object 
    /// that provides references to the intrinsic server objects 
    /// (for example, Request, Response, Session, and Server) used to service HTTP requests.
    /// </param>
    public void ProcessRequest(HttpContext context)
    {
      string photoName = context.Request.QueryString["p"]; 

      string photoName_org =  photoName.Substring(photoName.LastIndexOf("/") + 1);

      string cachePath = Path.Combine(HttpRuntime.CodegenDir, photoName_org);

      OnServing(cachePath);

      if(File.Exists(cachePath)) {
        OutputCacheResponse(context, File.GetLastWriteTime(cachePath));
        context.Response.WriteFile(cachePath);
        return;
      }

      string path = context.Request.QueryString["path"];
      string photoPath = string.Empty;



      if(path == "user") {
        photoPath = context.Server.MapPath(CoreSettings.Instance.UserImageFolder) + photoName;
      }
      else {
        photoPath = context.Server.MapPath(CoreSettings.Instance.ImageFolder) + photoName;
      }

      Bitmap photo = null;
      try {
        photo = new Bitmap(photoPath);
      }
      catch(ArgumentException) {
        OnBadRequest(cachePath);
        context.Response.Redirect("/Shared/Error");
      }

      int index = cachePath.LastIndexOf(".") + 1;
      string extension = cachePath.Substring(index).ToUpperInvariant();

      // Fix for IE not handling jpg image types
      if(string.Compare(extension, "JPG") == 0)
        context.Response.ContentType = "image/jpeg";
      else
        context.Response.ContentType = "image/" + extension;

      using(var memoryStream = new MemoryStream()) {
        photo.Save(memoryStream, ImageFormat.Png);
        OutputCacheResponse(context, File.GetLastWriteTime(photoPath));
        using(var diskCacheStream = new FileStream(cachePath, FileMode.CreateNew)) {
          memoryStream.WriteTo(diskCacheStream);
        }
        memoryStream.WriteTo(context.Response.OutputStream);
      }
    }

    private static void OutputCacheResponse(HttpContext context, DateTime lastModified)
    {
      HttpCachePolicy cachePolicy = context.Response.Cache;
      cachePolicy.SetCacheability(HttpCacheability.Public);
      cachePolicy.VaryByParams["p"] = true; 
      cachePolicy.SetOmitVaryStar(true);
      cachePolicy.SetExpires(DateTime.Now + TimeSpan.FromDays(365));
      cachePolicy.SetValidUntilExpires(true);
      cachePolicy.SetLastModified(lastModified);
    }


    #endregion

    #region Events

    /// <summary>
    /// Occurs before the requested image is served.
    /// </summary>
    public static event EventHandler<EventArgs> Serving;
    private static void OnServing(string file)
    {
      if(Serving != null) {
        Serving(file, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Occurs when a file is served.
    /// </summary>
    public static event EventHandler<EventArgs> Served;
    private static void OnServed(string file)
    {
      if(Served != null) {
        Served(file, EventArgs.Empty);
      }
    }

    /// <summary>
    /// Occurs when the requested file does not exist.
    /// </summary>
    public static event EventHandler<EventArgs> BadRequest;
    private static void OnBadRequest(string file)
    {
      if(BadRequest != null) {
        BadRequest(file, EventArgs.Empty);
      }
    }

    #endregion

  }
}