namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using Models;
    using NeoSistem.EnterpriseEntity.Business;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Web.Mvc;

    public class SurveyController : BaseController
    {

        #region Constants

        const string STARTCOLUMN = "SurveyId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        #endregion

        static Data.Survey dataSurvey = null;
        static ICollection<SurveyModel> collection = null;

        #region Methods

        public ActionResult Index()
        {
            //Title = "Anket Listesi";
            //Description = "Anketleri görüntüleyebilir, silebilir, arama yapabilir ve düzenleyebilirsiniz.";

            int total = 0;
            dataSurvey = new Data.Survey();

            collection = dataSurvey.Search(ref total, PAGEDIMENSION, 1, string.Empty, STARTCOLUMN, ORDER).AsCollection<SurveyModel>();

            var model = new FilterModel<SurveyModel>
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
        public ActionResult Index(SurveyModel model, string OrderName, string Order, int? Page)
        {
            int total = 0;
            dataSurvey = dataSurvey ?? new Data.Survey();

            var whereClause = new StringBuilder("Where");

            string likeClaue = " {0} LIKE N'%{1}%' ";

            if (!string.IsNullOrWhiteSpace(model.SurveyQuestion))
            {
                whereClause.AppendFormat(likeClaue, "SurveyQuestion", model.SurveyQuestion);
            }

            if (whereClause.ToString() == "Where")
            {
                whereClause.Clear();
            }

            collection = dataSurvey.Search(ref total, PAGEDIMENSION, Page ?? 1, whereClause.ToString(), OrderName, Order).AsCollection<SurveyModel>();

            var items = new FilterModel<SurveyModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };

            return View("SurveyList", items);
        }

        public ActionResult Create()
        {
            var model = new SurveyModel
            {
                SurveyOptions = new List<SurveyOptionModel>()
            };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(SurveyModel model)
        {
            return PostView(m =>
            {

                using (var transaction = new TransactionUI())
                {
                    try
                    {
                        var survey = new Classes.Survey();
                        UpdateClass(model, survey);

                        survey.RecordDate = DateTime.Now;
                        survey.LastUpdateDate = DateTime.Now;
                        survey.RecordCreatorId = 99;
                        survey.LastUpdaterId = 99;

                        survey.Save(transaction);

                        Classes.SurveyOption option = null;

                        foreach (var item in OptionCollection)
                        {
                            option = new Classes.SurveyOption
                            {
                                SurveyId = survey.SurveyId,
                                OptionContent = item.OptionContent
                            };
                            option.Save(transaction);
                        }

                        transaction.Commit();

                        OptionCollection.Clear();
                        OptionCollection = null;

                        RemoveOptionCollection.Clear();
                        RemoveOptionCollection = null;
                    }
                    catch
                    {
                        transaction.Rollback();
                        throw;
                    }
                }
                return RedirectToAction("Index");
            }, model);
        }

        public IList<SurveyOptionModel> OptionCollection
        {
            get
            {
                if (Session["OptionCollection"] == null)
                {
                    Session["OptionCollection"] = new List<SurveyOptionModel>();
                }
                return Session["OptionCollection"] as List<SurveyOptionModel>;
            }
            set { Session["OptionCollection"] = value; }
        }

        public IList<SurveyOptionModel> RemoveOptionCollection
        {
            get
            {
                if (Session["RemoveOptionCollection"] == null)
                {
                    Session["RemoveOptionCollection"] = new List<SurveyOptionModel>();
                }
                return Session["RemoveOptionCollection"] as List<SurveyOptionModel>;
            }
            set { Session["RemoveOptionCollection"] = value; }
        }

        [HttpPost]
        public ActionResult AddOption(SurveyOptionModel model)
        {
            model.OptionId = OptionCollection.Count + 1;
            OptionCollection.Add(model);
            return View("SurveyOptions", OptionCollection);
        }

        [HttpPost]
        public JsonResult RemoveOption(int id)
        {
            var item = OptionCollection.SingleOrDefault(c => c.OptionId == id);
            if (item == null)
            {
                return Json(false);
            }
            RemoveOptionCollection.Add(item);
            bool result = OptionCollection.Remove(item);

            return Json(true);
        }

        public ActionResult Edit(int id)
        {
            var survey = new Classes.Survey();
            bool hasRecord = survey.LoadEntity(id);
            if (hasRecord)
            {
                var model = new SurveyModel();
                UpdateClass(survey, model);
                var dataSurveyOption = new Data.SurveyOption();
                var options = dataSurveyOption.GetItemsBySurveyId(id).AsCollection<SurveyOptionModel>();
                OptionCollection = options.ToList();
                model.SurveyOptions = OptionCollection;
                return View(model);
            }
            return RedirectToAction("Index");
        }

        [HttpPost]
        public ActionResult Edit(int id, SurveyModel model)
        {
            return PostView(m =>
            {

                using (var transaction = new TransactionUI())
                {
                    try
                    {
                        var survey = new Classes.Survey();
                        bool hasRecord = survey.LoadEntity(id);
                        if (hasRecord)
                        {

                            survey.SurveyQuestion = model.SurveyQuestion;
                            survey.Active = model.Active;
                            survey.SurveyId = id;
                            survey.LastUpdateDate = DateTime.Now;
                            survey.LastUpdaterId = 99;
                            survey.Save(transaction);

                            Classes.SurveyOption option = null;

                            foreach (var item in OptionCollection)
                            {
                                option = new Classes.SurveyOption();
                                option.SurveyId = survey.SurveyId;
                                option.OptionContent = item.OptionContent;
                                option.Save(transaction);
                            }

                            option = new Classes.SurveyOption();

                            foreach (var item in RemoveOptionCollection)
                            {
                                option.Delete(item.OptionId, transaction);
                            }

                            transaction.Commit();

                            OptionCollection.Clear();
                            OptionCollection = null;

                            RemoveOptionCollection.Clear();
                            RemoveOptionCollection = null;
                        }
                    }
                    catch
                    {
                        transaction.Rollback();
                    }
                }
                return RedirectToAction("Index");
            }, model);
        }

        public ActionResult Delete(int id)
        {
            return View();
        }

        #endregion

    }
}
