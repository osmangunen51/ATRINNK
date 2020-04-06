using MakinaTurkiye.Services.Catalog;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace NeoSistem.MakinaTurkiye.Management.Controllers
{

    public class ProductComplainController : BaseController
    {
        #region Fields

        private IProductComplainService _productComplainService;

        #endregion

        #region Ctor

        public ProductComplainController(IProductComplainService productComplainService)
        {
            _productComplainService = productComplainService;
        }

        #endregion

        #region Methods

        public ActionResult Index(int? page)
        {
            int pageSize = 20;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
            ViewData["page"] = page == null ? 0 : (int)page;
            var productComplainList = _productComplainService.GetAllProductComplain();
            ViewData["pageNumbers"] = Convert.ToInt32(Math.Ceiling(productComplainList.ToList().Count / (float)pageSize));
            ViewData["Total"] = productComplainList.Count;
            List<ProductComplainModel> ComplainList = new List<ProductComplainModel>();
            var ListTake = productComplainList.OrderByDescending(x => x.ProductComplainId).Skip(skipRows).Take(pageSize);
            foreach (var item in ListTake)
            {
                ProductComplainModel itemComplain = new ProductComplainModel();
                itemComplain.ID = item.ProductComplainId;
                itemComplain.Name = item.UserName;
                itemComplain.Surname = item.UserSurname;
                if (item.Product != null)
                {
                    itemComplain.ProductName = item.Product.ProductName;
                    itemComplain.ProductNo = item.Product.ProductNo;
                }
                itemComplain.Email = item.UserEmail;
                itemComplain.ComplainDate = Convert.ToDateTime(item.CreatedDate);
                itemComplain.Comment = item.UserComment;
                var productComplainName = entities.ProductComplainDetails.Where(x => x.ProductComplainId == item.ProductComplainId);
                string complainNames = "";
                foreach (var item1 in productComplainName)
                {
                    var complainType = entities.ProductComplainTypes.First(x => x.ProductComplainTypeId == item1.ProductComplainTypeId);
                    complainNames = complainNames + "," + complainType.Name;
                }
                itemComplain.ComplainNames = complainNames;
                itemComplain.IsMember = item.IsMember;
                ComplainList.Add(itemComplain);
            }
            return View(ComplainList);
        }

        [HttpPost]
        public JsonResult DeleteComplain(int id)
        {
            var productComplainType = entities.ProductComplainDetails.Where(x => x.ProductComplainId == id);
            using (var entities1 = new MakinaTurkiyeEntities())
            {
                foreach (var item in productComplainType)
                {
                    var deleteObj = entities1.ProductComplainDetails.First(x => x.ProductComplainDetailId == item.ProductComplainDetailId);
                    entities1.ProductComplainDetails.DeleteObject(deleteObj);
                    entities1.SaveChanges();
                }
            }

            var demand = entities.ProductComplains.SingleOrDefault(x => x.ProductComplainId == id);
            entities.ProductComplains.DeleteObject(demand);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

        #endregion
    }
}