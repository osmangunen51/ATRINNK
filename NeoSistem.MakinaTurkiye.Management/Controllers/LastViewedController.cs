using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Management.Models;
using System;
using System.Collections.Generic;
using System.Text;
using System.Web.Mvc;

namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class LastViewedController : BaseController
    {

        const string STARTCOLUMN = "P.SingularViewCount";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        const string SessionPage = "product_PAGEDIMENSION";

        static Data.Product dataProduct = null;
        static ICollection<ProductModel> collection = null;

        public ActionResult LVProductIndex()
        {
            try
            {
                if (Session[SessionPage] == null)
                {
                    Session[SessionPage] = PAGEDIMENSION;
                }

                int total = 0;
                dataProduct = new Data.Product();

                string whereClause = string.Empty;

                collection = dataProduct.Search(ref total, (int)Session[SessionPage], 1, string.Empty, STARTCOLUMN, ORDER).AsCollection<ProductModel>();

                var model = new FilterModel<ProductModel>
                {
                    CurrentPage = 1,
                    TotalRecord = total,
                    Order = ORDER,
                    OrderName = STARTCOLUMN,
                    Source = collection
                };

                return View(model);
            }
            catch (Exception)
            {
                throw;
            }
        }

        [HttpPost]
        public ActionResult LVProductIndex(ProductModel model, string OrderName, string Order, int? Page, int PageDimension)
        {
            try
            {
                dataProduct = dataProduct ?? new Data.Product();

                var whereClause = new StringBuilder("Where");

                string likeClaue = " {0} LIKE N'{1}%' ";
                string equalClause = " {0} = {1} ";
                bool op = false;


                if (!string.IsNullOrWhiteSpace(model.ProductNo) && model.ProductNo.Length == 9)
                {
                    whereClause.AppendFormat(likeClaue, "ProductNo", model.ProductNo);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.ProductName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "ProductName", model.ProductName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.FirstCategoryName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CMain.CategoryName", model.FirstCategoryName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameBrand))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CBrand.CategoryName", model.NameBrand);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameModel))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CModel.CategoryName", model.NameModel);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.NameSeries))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "CSeries.CategoryName", model.NameSeries);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.OtherBrand))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "OtherBrand", model.OtherBrand);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.OtherModel))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "OtherModel", model.OtherModel);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.UserName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "MainPartyFullName", model.UserName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.StoreName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                    op = true;
                }

                if (!string.IsNullOrWhiteSpace(model.StoreName))
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(likeClaue, "StoreName", model.StoreName);
                    op = true;
                }

                if (model.MemberType > 0)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "MemberType", model.MemberType);
                    op = true;
                }

                if (model.StoreMainPartyId > 0)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "P.MainPartyId", model.StoreMainPartyId);
                    op = true;
                }

                if (model.ProductPrice > 0M)
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    whereClause.AppendFormat(equalClause, "ProductPrice", model.ProductPrice);
                    op = true;
                }

                if (model.ProductRecordDate != new DateTime())
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = " Cast(ProductRecordDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, model.ProductRecordDate.ToString("yyyMMdd"));
                }

                if (model.ProductLastViewDate != new DateTime())
                {
                    if (op)
                    {
                        whereClause.Append("AND");
                    }
                    string dateEqual = " Cast(ProductLastViewDate as date) = Cast('{0}' as date) ";
                    whereClause.AppendFormat(dateEqual, model.ProductLastViewDate.ToString("yyyMMdd"));
                }


                if (whereClause.ToString() == "Where")
                {
                    whereClause.Clear();
                }
                int total = 0;

                Session[SessionPage] = PageDimension;
                collection =
                  dataProduct.Search(ref total, PageDimension, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<ProductModel>();

                var filterItems = new FilterModel<ProductModel>
                {
                    CurrentPage = Page ?? 1,
                    TotalRecord = total,
                    Order = Order,
                    OrderName = OrderName,
                    Source = collection
                };

                return View("LVProductList", filterItems);
            }
            catch (Exception)
            {
                throw;
            }
        }

    }
}
