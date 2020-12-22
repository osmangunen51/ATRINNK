using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Entities.Tables.Messages;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Services.Members;
using MakinaTurkiye.Services.Messages;
using MakinaTurkiye.Services.Stores;
using MakinaTurkiye.Utilities.MailHelpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Messages.Tasks
{
    public class SiteMapCreate : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            string html = string.Empty;
            List<string> UrlListesi = new List<string>()
            {
                "https://www.makinaturkiye.com/Sitemap/Index",
                "https://www.makinaturkiye.com/Sitemap/ProductSiteMapIndex",
                "https://www.makinaturkiye.com/Sitemap/VideoSiteMapIndex",
                "https://www.makinaturkiye.com/Sitemap/StoreSiteMapIndex"
            };
            foreach (var url in UrlListesi)
            {
                HttpWebRequest request = (HttpWebRequest)WebRequest.Create(url);
                request.AutomaticDecompression = DecompressionMethods.GZip;
                using (HttpWebResponse response = (HttpWebResponse)request.GetResponse())
                using (Stream stream = response.GetResponseStream())
                using (StreamReader reader = new StreamReader(stream))
                {
                    html = reader.ReadToEnd();
                }
            }
            return Task.CompletedTask;
        }
    }
}
