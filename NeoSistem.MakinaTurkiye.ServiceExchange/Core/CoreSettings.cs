using System;
using System.Diagnostics;
using System.Linq;
using System.Xml.Linq;

namespace NeoSistem.MakinaTurkiye.ExchangeService.Core
{
    public class CoreSettings
    {
        public static string GetSettingValue(string key)
        {
            var doc = XDocument.Load(AppDomain.CurrentDomain.BaseDirectory + "\\Settings.xml");
            var settingValue = (from x in doc.Descendants("ServiceSettings").Elements("add")
                                where x.Attribute("name").Value == key
                                select x.Attribute("value").Value).SingleOrDefault();

            return settingValue;
        }


        public static void SaveLog(System.Exception ex, EventLogEntryType icon)
        {
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
            string eSource = "MakinaTurkiyeExChange";
            if (!EventLog.Exists(eSource))
            {
                EventLog.CreateEventSource(eSource, eSource);
            }

            try
            {
                if ((ex.InnerException != null))
                {
                    EventLog.WriteEntry(eSource, "## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + "Inner Exception :" + ex.InnerException.ToString() + " ##" + "\r\n");
                }
                else
                {
                    EventLog.WriteEntry(eSource, "## " + errorDateTime + " ## " + "\r\n" + ex.StackTrace + "\r\n" + "\r\n" + "Message : " + ex.Message + "\r\n" + " ##" + "\r\n--------------------------------------------------------------------------------------------------------------------------------" + "\r\n", icon);
                }
            }
            catch
            {
            }
        }
    }
}
