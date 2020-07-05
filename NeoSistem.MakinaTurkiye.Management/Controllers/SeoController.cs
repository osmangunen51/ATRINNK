namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    public class SeoController : BaseController
    {
        #region Constants

        const string STARTCOLUMN = "SeoId";
        const string ORDER = "asc";
        const int PAGEDIMENSION = 20;

        #endregion

        static Data.Seo dataSeo = null; 
        static ICollection<SeoModel> collection = null;

        #region Methods

        public ActionResult Index()
        {
            PAGEID = PermissionPage.SeoYonetimi;

            int total = 0;
            dataSeo = new Data.Seo();

            collection = dataSeo.Search(ref total, PAGEDIMENSION, 1, string.Empty, STARTCOLUMN, ORDER).AsCollection<SeoModel>();

            var model = new FilterModel<SeoModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(SeoModel model, string OrderName, string Order, int? Page)
        {
            int total = 0;
            dataSeo = dataSeo ?? new Data.Seo();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";
            bool op = false;

            if (!string.IsNullOrWhiteSpace(model.PageName))
            {
                whereClause.AppendFormat(likeClaue, "PageName", model.PageName);
                op = true;
            }

            if (!string.IsNullOrWhiteSpace(model.Title))
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(likeClaue, "Title", model.Title);
                op = true;
            }


            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            collection = dataSeo.Search(ref total, PAGEDIMENSION, Page ?? 1, whereClause.ToString(), OrderName, "asc").AsCollection<SeoModel>();

            var items = new FilterModel<SeoModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = "asc",
                OrderName = OrderName,
                Source = collection
            };

            return View("SeoList", items);
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.SeoDuzenle;

            var seo = new Classes.Seo();
            bool hasRecord = seo.LoadEntity(id);
            if (hasRecord)
            {
                var model = new SeoModel();

                UpdateClass(seo, model);

                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, SeoModel model)
        {
            if (ModelState.IsValid)
            {
                var seo = new Classes.Seo();
                bool hasRecord = seo.LoadEntity(id);
                if (hasRecord)
                {
                  
                  
                    UpdateClass(model, seo);
                    seo.SeoId = id;
                    seo.Action = EnterpriseEntity.Business.EntityAction.Update;
                    seo.Save();
                }
                return RedirectToAction("Index");
            }
            return View(model);
        }
       
        #endregion

    }
}