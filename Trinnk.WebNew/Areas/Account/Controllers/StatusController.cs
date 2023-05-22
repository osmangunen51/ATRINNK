using System.Web.Mvc;

namespace NeoSistem.Trinnk.Web.Areas.Account.Controllers
{
    public class StatusController : Controller
    {

        //public ActionResult Index()
        //{
        //  var model = new ProductSalesListViewModel();
        //  Trinnk.Web.Models.Entities.TrinnkEntities entities = new Trinnk.Web.Models.Entities.TrinnkEntities();
        //  //alıcı
        //  var productsaleaboutbuyer = entities.ProductSales.Where(c => c.BuyerMainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();
        //  //satıcı
        //  var productsaleaboutdealer = entities.ProductSales.Where(c => c.DealerMainPartyId == AuthenticationUser.Membership.MainPartyId).ToList();

        //  if (productsaleaboutbuyer.Count != 0)
        //  {
        //    IList<Product> list = new List<Product>();
        // foreach (var item in productsaleaboutbuyer)
        // {
        //   var product = entities.Products.Where(c => c.ProductId == item.ProductId).SingleOrDefault();
        //   list.Add(product);

        // }

        // model.ProductSalesBuyer = list;
        //  }

        //  if (productsaleaboutdealer.Count != 0)
        //  {
        //    IList<Product> listdeal = new List<Product>();
        //    foreach (var item in productsaleaboutdealer)
        //    {

        //      listdeal.Add(entities.Products.Where(c => c.ProductId == item.ProductId).SingleOrDefault());

        //    }
        //    model.ProductSalesDealer = listdeal;

        //  }


        //    return View(model);
        //}

    }
}
