using NeoSistem.MakinaTurkiye.ExchangeService.Core;
using System;
using System.Diagnostics;
using System.ServiceProcess;
using System.Xml.Linq;
//using Schedule;

namespace NeoSistem.MakinaTurkiye.ExchangeService
{
    public partial class Exchange : ServiceBase
    {
        public Exchange()
        {
            InitializeComponent();
        }

        //private ScheduleTimer TickTimer = new ScheduleTimer();

        private delegate void TickHandler(DateTime tick);

        protected override void OnStart(string[] args)
        {
            //#if (!DEBUG) 
            //        try
            //      {
            //        TickTimer.AddJob(new ScheduledTime(CoreSettings.GetSettingValue("EventTimeBase"), CoreSettings.GetSettingValue("TimeSpan")), new TickHandler(GetExchange));
            //        TickTimer.Start();

            //        Core.CoreSettings.SaveLog(new Exception("Kur Servisi başladı."), EventLogEntryType.Information);
            //      }
            //      catch (Exception ex)
            //      {
            //        Core.CoreSettings.SaveLog(ex, EventLogEntryType.Error);
            //      }
            //#else
            //      ConfigureAndStart();
            //#endif

            try
            {
                //TickTimer.AddJob(
                //    new ScheduledTime(
                //        CoreSettings.GetSettingValue("EventTimeBase"), CoreSettings.GetSettingValue("TimeSpan")
                //        ), new TickHandler(GetExchange));
                //TickTimer.Start();

                Core.CoreSettings.SaveLog(new Exception("Kur Servisi başladı."), EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Core.CoreSettings.SaveLog(ex, EventLogEntryType.Error);
            }
        }

        public void ConfigureAndStart()
        {
            try
            {
                //TickTimer.AddJob(new ScheduledTime(CoreSettings.GetSettingValue("EventTimeBase"), CoreSettings.GetSettingValue("TimeSpan")), new TickHandler(GetExchange));
                //TickTimer.Start();

                Core.CoreSettings.SaveLog(new Exception("Kur Servisi başladı."), EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Core.CoreSettings.SaveLog(ex, EventLogEntryType.Error);
            }
        }

        protected override void OnStop()
        {
            //TickTimer.Stop();
            //TickTimer.Dispose();
            Core.CoreSettings.SaveLog(new Exception("Kur Servisi durduruldu."), EventLogEntryType.Warning);
        }

        public void GetExchange(DateTime tick)
        {
            try
            {
                var doc = XDocument.Load(CoreSettings.GetSettingValue("KurXml"));
                var xmlExChangePath = CoreSettings.GetSettingValue("XmlExChangePath");
                doc.Save(xmlExChangePath);

                Core.CoreSettings.SaveLog(new Exception("Kurlar alındı."), EventLogEntryType.Information);
            }
            catch (Exception ex)
            {
                Core.CoreSettings.SaveLog(ex, EventLogEntryType.Error);
            }
        }

    }
}