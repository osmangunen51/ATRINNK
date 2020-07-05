using System;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Core.Web.Controls
{
    public class AdminPanel : IDisposable
  { 
    private bool _disposed;
    private readonly TextWriter _writer;
     
    public AdminPanel(HttpResponseBase httpResponse)
    {
      if(httpResponse == null) {
        throw new ArgumentNullException("httpResponse");
      }
      this._writer = httpResponse.Output;
    }

    public AdminPanel(ViewContext viewContext)
    {
      if(viewContext == null) {
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
      if(!this._disposed) {
        this._disposed = true;
        this._writer.Write("</td><td class=\"right\"></td></tr><tr><td class=\"bottomleft\"></td><td class=\"bottom\"></td><td class=\"bottomright\"></td></tr></table>");
      }
    }

    public void EndPanel()
    {
      this.Dispose(true);
    }
  } 
}
