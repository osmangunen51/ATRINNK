#region Using

using System;
using System.Drawing;
using System.Drawing.Drawing2D;
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
    public class ThumbnailHandler : IHttpHandler
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
      int _thumbnailSize = Convert.ToInt32(context.Request.QueryString["s"]);

      string photoName_org =
        photoName.Substring(photoName.LastIndexOf("/") + 1);

      string cachePath = Path.Combine(HttpRuntime.CodegenDir, photoName_org.Replace(".", string.Format("x{0}.", _thumbnailSize)));

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

      //int width, height;

      Size thumbSize = Size.Empty;

      if(photo.Width > photo.Height) {
        //width = _thumbnailSize * photo.Width / photo.Height;
        //height = _thumbnailSize; 
        thumbSize = new Size(_thumbnailSize, Convert.ToInt32(((double)photo.Height / photo.Width) * _thumbnailSize));
      }
      else {
        //width = _thumbnailSize;
        //height = _thumbnailSize * photo.Height / photo.Width;
        thumbSize = new Size(Convert.ToInt32(((double)photo.Width / photo.Height) * _thumbnailSize), _thumbnailSize);
      }
      var target = new Bitmap(photo, thumbSize);

      using(var graphics = Graphics.FromImage(target)) {
        try {
          graphics.CompositingQuality = CompositingQuality.HighSpeed;
          graphics.InterpolationMode = InterpolationMode.HighQualityBicubic;
          graphics.CompositingMode = CompositingMode.SourceCopy;

          var rectDestination = new Rectangle(Point.Empty, thumbSize);
          graphics.DrawImage(photo, rectDestination, 0, 0, photo.Width, photo.Height, GraphicsUnit.Pixel);

          //graphics.DrawImage(photo, (_thumbnailSize - thumbSize.Width) / 2, (_thumbnailSize - height) / 2, width, height);

          using(var memoryStream = new MemoryStream()) {
            target.Save(memoryStream, ImageFormat.Png);
            OutputCacheResponse(context, File.GetLastWriteTime(photoPath));
            using(var diskCacheStream = new FileStream(cachePath, FileMode.CreateNew)) {
              memoryStream.WriteTo(diskCacheStream);
            }
            memoryStream.WriteTo(context.Response.OutputStream);
          }
        }
        catch(Exception ex) {
          throw ex;
        }
      }
    }

    private static void OutputCacheResponse(HttpContext context, DateTime lastModified)
    {
      HttpCachePolicy cachePolicy = context.Response.Cache;
      cachePolicy.SetCacheability(HttpCacheability.Public);
      cachePolicy.VaryByParams["p"] = true;
      cachePolicy.VaryByParams["s"] = true;
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