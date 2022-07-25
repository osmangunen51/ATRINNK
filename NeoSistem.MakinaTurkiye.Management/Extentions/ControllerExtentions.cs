using MakinaTurkiye.Core.Infrastructure;
using Microsoft.AspNet.SignalR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Extentions
{
    public static class ControllerExtentions
    {
        public static void SendMessageAsync(this Controller controller,string message,int UserID)
        {

            var HubSystem = EngineContext.Current.Resolve<NeoSistem.MakinaTurkiye.Management.Hubs.System>();
            HubSystem.SendMessage(message,UserID);
        }
    }
}