namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using Core.Web.Helpers;
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using NeoSistem.MakinaTurkiye.Web.Models.UtilityModel;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using System.Web.Mvc;

    public class NewsController : BaseController
    {

        #region Constants

        const string STARTCOLUMN = "NewsId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        #endregion

        static Data.News dataNews = null;
        static ICollection<NewsModel> collection = null;

        #region Methods

        public ActionResult Index()
        {

            int total = 0;
            dataNews = new Data.News();

            string whereClause = string.Empty;

            collection = dataNews.Search(ref total, PAGEDIMENSION, 1, whereClause, STARTCOLUMN, ORDER).AsCollection<NewsModel>();

            var model = new FilterModel<NewsModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            DateTime.Now.AddMinutes(20);

            return View(model);
        }

        [HttpPost]
        public ActionResult Index(NewsModel model, string OrderName, string Order, int? Page)
        {
            dataNews = dataNews ?? new Data.News();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";
            string equalClause = " {0} = {1} ";
            bool op = false;

            if (!string.IsNullOrWhiteSpace(model.NewsTitle))
            {
                whereClause.AppendFormat(likeClaue, "NewsTitle", model.NewsTitle);
                op = true;
            }

            if (model.Active)
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                whereClause.AppendFormat(equalClause, "Active", model.Active ? 1 : 0);
                op = true;
            }


            if (model.NewsDate != new DateTime())
            {
                if (op)
                {
                    whereClause.Append("AND");
                }
                string dateEqual = " Cast(NewsDate as date) = Cast('{0}' as date) ";
                whereClause.AppendFormat(dateEqual, model.NewsDate.ToString("yyyyMMdd"));
            }


            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            int total = 0;

            collection = dataNews.Search(ref total, PAGEDIMENSION, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<NewsModel>();

            var filterItems = new FilterModel<NewsModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };

            return View("NewsList", filterItems);
        }

        public ActionResult Create()
        {

            var model = new NewsModel { NewsDate = DateTime.Now };
            return View(model);
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Create(NewsModel model)
        {

            return PostView(m =>
            {

                var news = new Classes.News();

                UpdateClass(model, news);

                string imageName = FileHelpers.ImageThumbnail(AppSettings.NewsImageFolder, Request.Files[0], 200, FileHelpers.ThumbnailType.Width);

                news.NewsPicturePath = imageName;

                news.Save();

                return RedirectToAction("Index");

            }, model);
        }

        public ActionResult Edit(int id)
        {
            var news = new Classes.News();

            if (news.LoadEntity(id))
            {
                var model = new NewsModel();
                UpdateClass(news, model);
                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(int id, NewsModel model)
        {
            return PostView((m, newsId) =>
            {

                var news = new Classes.News();
                if (news.LoadEntity(newsId))
                {

                    string oldImage = news.NewsPicturePath;
                    UpdateClass(model, news);

                    if (Request.Files.Count > 0)
                    {
                        if (oldImage != string.Empty)
                        {
                            FileHelpers.Delete(FileHelpers.ImageFolder + oldImage);
                            FileHelpers.Delete(FileHelpers.ImageFolder + oldImage.Replace(".", "_th"));
                        }
                        string imageName = FileHelpers.ImageThumbnail(AppSettings.NewsImageFolder, Request.Files[0], 200, FileHelpers.ThumbnailType.Width);
                        news.NewsPicturePath = imageName;
                    }

                    news.NewsId = newsId;
                    news.Save();

                    return RedirectToAction("Index");
                }

                return View(m);
            }, model, id);
        }

        #endregion

    }
}
