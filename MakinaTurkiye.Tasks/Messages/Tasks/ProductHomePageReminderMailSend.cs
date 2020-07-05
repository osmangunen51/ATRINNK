using MakinaTurkiye.Core.Infrastructure;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Utilities.MailHelpers;
using Quartz;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Tasks.Messages.Tasks
{
    public class ProductHomePageReminderMailSend : IJob
    {
        public Task Execute(IJobExecutionContext context)
        {
            IProductHomePageService productHomePageService = EngineContext.Current.Resolve<IProductHomePageService>();
            IProductService productService = EngineContext.Current.Resolve<IProductService>();

            var productHomePages = productHomePageService.GetProductHomePages();
            if (productHomePages.Count > 0)
            {
                var products = productService.GetProductsByProductIds(productHomePages.Select(x => x.ProductId).ToList()).Where(x => x.ProductActive == false);
                foreach (var item in products)
                {
                    var productHomePage = productHomePageService.GetProductHomePageByProductId(item.ProductId);
                    productHomePageService.DeleteProductHomePage(productHomePage);
                }
            }

            productHomePages = productHomePageService.GetProductHomePages().Where(x => x.EndDate.Value.Date < DateTime.Now.Date).ToList();

            if (productHomePages.Count > 0)
            {
                var productNos = productService.GetProductsByProductIds(productHomePages.Select(x => x.ProductId).ToList()).Select(x => x.ProductNo).ToList();
                string mailContent = "";
                mailContent = string.Join(", ", productNos);


                string title = "Anasayfa Sektör Ürünleri Hatırlatma Servisi";
                string toEmail = "sistem@anexxa.net";

                mailContent = mailContent + "<br> ürünlerinin anasayfa sektör ürünlerinde süresi bitmiştir. Lütfen ürünleri kontrol edin veya kaldırın.";
                MailHelper mailHelper = new MailHelper(title, mailContent,
       "makinaturkiye@makinaturkiye.com", toEmail, "haciosman7777", "Makinaturkiye Sistem Mesajı");

                mailHelper.Send();

            }

            return Task.CompletedTask;
        }
    }
}
