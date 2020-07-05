namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Services.Stores;
    using Models;
    using System.Collections.Generic;
    using System.Web.Mvc;

    public class ActivityTypeController : BaseController
    {
        const string STARTCOLUMN = "ActivityTypeId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        static ICollection<ActivityTypeModel> collection = null;
        public IActivityTypeService _activityTypeService;
       
        public ActivityTypeController(IActivityTypeService activityTypeService)
        {
            _activityTypeService = activityTypeService;
        }

        public ActionResult Index()
        {
            PAGEID = PermissionPage.FaaliyetTipleri;

            int total = 0;
            var curActivityType = new Classes.ActivityType();
            collection = curActivityType.GetDataTable().AsCollection<ActivityTypeModel>();

            var model = new FilterModel<ActivityTypeModel>
            {
                CurrentPage = 1,
                TotalRecord = total,
                Order = ORDER,
                OrderName = STARTCOLUMN,
                Source = collection
            };

            return View(model);
        }

        public ActionResult Create()
        {
            PAGEID = PermissionPage.YeniFaaliyetTipi;

            var model = new ActivityTypeModel { };
            return View(model);
        }

        [HttpPost]
        public ActionResult Create(ActivityTypeModel model)
        {
            try
            {
                var curActivityType = new Classes.ActivityType
                {
                    ActivityName = model.ActivityName,
                    Order=model.Order
                };
                curActivityType.Save();

                return RedirectToAction("Index");
            }
            catch
            {
                return View(model);
            }
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var curActivityType = new Classes.ActivityType();
            return Json(new { m = curActivityType.Delete(id) });
        }

        public ActionResult Edit(int id)
        {
            PAGEID = PermissionPage.FaaliyetTipiDuzenle;

            var curActivityType = new Classes.ActivityType();
           
            if (curActivityType.LoadEntity(id))
            {
                var model = new ActivityTypeModel();
                UpdateClass(curActivityType, model);

                return View(model);
            }

            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateInput(false)]
        public ActionResult Edit(byte id, ActivityTypeModel model)
        {
       
                var activity = _activityTypeService.GetActivityTypeByActivityTypeId(id);
                activity.ActivityName = model.ActivityName;
                activity.Order = model.Order;
                _activityTypeService.UpdateActivityType(activity);
       

            return RedirectToAction("Edit", new { id=id});
        }

    }
}