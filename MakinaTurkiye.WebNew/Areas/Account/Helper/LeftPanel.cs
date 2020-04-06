
using System;
using System.IO;
using System.Web;
using System.Web.Mvc;
namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Helper
{

  public class LeftPanel : IDisposable
  {

    private bool _disposed;
    private readonly TextWriter _writer;

    public LeftPanel(HttpResponseBase httpResponse)
    {
      if(httpResponse == null)
      {
        throw new ArgumentNullException("httpResponse");
      }
      this._writer = httpResponse.Output;
    }

    public LeftPanel(ViewContext viewContext)
    {
      if(viewContext == null)
      {
        throw new ArgumentNullException("viewContext");
      }
      this._writer = viewContext.Writer;
    }

    public void Dispose()
    {
      this.Dispose(true);
      GC.SuppressFinalize(this);
    }

    protected virtual void Dispose(bool disposing)
    {
      if(!this._disposed)
      {
        this._disposed = true;
        this._writer.Write("</div>");
      }
    }

    public void EndPanel()
    {
      this.Dispose(true);
    }

  }
}