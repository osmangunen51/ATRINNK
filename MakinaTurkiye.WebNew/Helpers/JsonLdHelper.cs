using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Web;
using NeoSistem.MakinaTurkiye.Web.Controllers;
using Schema.NET;
using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;

namespace NeoSistem.MakinaTurkiye.Web.Helpers
{
  public static class JsonLdHelper
  {



        public static BreadcrumbList SetBreadcrumbList(IList<BaseController.Navigation> navigations)
        {     
            List<ListItem> breadCrumbListItem = navigations.Select((t, i) => new ListItem
            {
              Position = new Values<int?, string>(1 + i),
             
              Item = new OneOrMany<IThing>(new Thing
              {
                Id =new Uri(ConfigurationManager.AppSettings["SiteUrl"].Substring(0,AppSettings.SiteUrl.Length-1) + t.Url.CheckNullString().Replace("https://wwww.makinaturkiye.com","")),
                Name = t.Title.CheckNullString()
              })
            }).ToList();

            List<IListItem> itemListElement = new List<IListItem>();
            itemListElement.AddRange(breadCrumbListItem);

            var breadCrumbList = new BreadcrumbList
            {
                NumberOfItems = new OneOrMany<int?>(navigations.Count),
                ItemListElement = itemListElement 
            };
            return breadCrumbList;
        }
  }
}