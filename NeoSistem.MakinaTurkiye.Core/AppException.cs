using System;
using System.IO;

namespace NeoSistem.MakinaTurkiye.Core
{
    public class AppException
  {
    public static void SaveException(System.Exception ex)
    {
      string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "\\UserFiles\\Logs\\ErrorLogs.txt";
      int GMT = DateTime.Compare(DateTime.Now, DateTime.UtcNow);
      string GMTstring = "";

      if (GMT > 0)
      {
        GMTstring = " (GMT + " + GMT.ToString() + ")";
      }
      else
      {
        GMTstring = GMTstring = " (GMT " + GMT.ToString() + ")";
      }

      string errorDateTime = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + " @ " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + GMTstring;

      StreamWriter strm = new StreamWriter(filePath, true);

      if ((ex.InnerException != null))
      {
        strm.WriteLine("## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + "Inner Exception :" + ex.InnerException.ToString() + " ##" + "\r\n");
        strm.Close();
        strm.Dispose();
        strm = null;
      }
      else
      {
        strm.WriteLine("## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + " ##" + "\r\n");
        strm.WriteLine("--------------------------------------------------------------------------------------------------------------------------------" + "\r\n"); strm.Close();
        strm.Dispose();
        strm = null;
      }
    }
    public static void SaveSAPException(System.Exception ex)
    {
      string filePath = System.Web.HttpContext.Current.Server.MapPath("~") + "/UserFiles/Logs/sapErrors.txt";
      int GMT = DateTime.Compare(DateTime.Now, DateTime.UtcNow);
      string GMTstring = "";

      if (GMT > 0)
      {
        GMTstring = " (GMT + " + GMT.ToString() + ")";
      }
      else
      {
        GMTstring = GMTstring = " (GMT " + GMT.ToString() + ")";
      }

      string errorDateTime = DateTime.Now.Year.ToString() + "." + DateTime.Now.Month.ToString() + "." + DateTime.Now.Day.ToString() + " @ " + DateTime.Now.Hour.ToString() + ":" + DateTime.Now.Minute.ToString() + ":" + DateTime.Now.Second.ToString() + GMTstring;

      StreamWriter strm = new StreamWriter(filePath, true);
      try
      {
        if ((ex.InnerException != null))
        {
          strm.WriteLine("## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + "Inner Exception :" + ex.InnerException.ToString() + " ##" + "\r\n");
        }
        else
        {
          strm.WriteLine("## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + " ##" + "\r\n");
          strm.WriteLine("--------------------------------------------------------------------------------------------------------------------------------" + "\r\n");
        }
      }
      catch
      {
      }
      finally
      {
        strm.Close();
        strm.Dispose();
        strm = null;
      }
    }
  }
}