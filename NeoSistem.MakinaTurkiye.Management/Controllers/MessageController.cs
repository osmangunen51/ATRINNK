namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using EnterpriseEntity.Extensions.Data;
    using global::MakinaTurkiye.Services.Members;
    using global::MakinaTurkiye.Services.Messages;
    using global::MakinaTurkiye.Services.Stores;
    using Models;
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class MessageController : BaseController
    {

        #region Constants


        const string STARTCOLUMN = "MessageId";
        const string ORDER = "Desc";
        const int PAGEDIMENSION = 20;

        #endregion
        public IMemberStoreService _memberStoreService;
        public IStoreService _storeService;
        public IMessageService _messageService;
        public MessageController(IMemberStoreService memberStoreService, IStoreService storeService, IMessageService messageService)
        {
            this._memberStoreService = memberStoreService;
            this._storeService = storeService;
            this._messageService = messageService;
        }

        static Data.Message dataMessage = null;
        static ICollection<MessageModel> collection = null;

        #region Methods

        public ActionResult Index()
        {
            PAGEID = PermissionPage.KullaniciMesajlari;

            int total = 0;
            dataMessage = new Data.Message();

            string whereClause = "";

            collection = dataMessage.Search(ref total, PAGEDIMENSION, 1, whereClause, STARTCOLUMN, ORDER).AsCollection<MessageModel>();
            foreach (var item in collection)
            {
                var id = item.InOutMainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(id);
                if (memberStore != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));
                    if (store != null)
                    {
                        item.ToMainPartyId = store.MainPartyId;
                        item.ToSecondName = store.StoreShortName;
                    }

                }

            }
            var model = new FilterModel<MessageModel>
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
        public ActionResult Index(MessageModel model, string OrderName, string Order, int? Page)
        {
            dataMessage = dataMessage ?? new Data.Message();

            int total = 0;

            collection = dataMessage.Search(ref total, PAGEDIMENSION, Page ?? 1, String.Empty, OrderName, Order).AsCollection<MessageModel>();
            foreach (var item in collection)
            {
                var id = item.InOutMainPartyId;
                var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(id);
                if (memberStore != null)
                {
                    var store = _storeService.GetStoreByMainPartyId(Convert.ToInt32(memberStore.StoreMainPartyId));
                    item.ToMainPartyId = store.MainPartyId;
                    item.ToSecondName = store.StoreShortName;
                }

            }
            var filterItems = new FilterModel<MessageModel>
            {
                CurrentPage = Page ?? 1,
                TotalRecord = total,
                Order = Order,
                OrderName = OrderName,
                Source = collection
            };

            return View("MessageList", filterItems);
        }

        [HttpPost]
        public JsonResult Delete(int id)
        {
            var message = new Classes.Message();
            return Json(new { m = message.Delete(id) });
        }

        public ActionResult View(int id)
        {

            var model = new MessageModel();

            model.Message = entities.Messages.SingleOrDefault(c => c.MessageId == id);
            model.Product = entities.Products.SingleOrDefault(c => c.ProductId == model.Message.ProductId);

            var messageMainParty = entities.MessageMainParties.FirstOrDefault(c => c.MessageId == id);
            model.MainPartyId = messageMainParty.MainPartyId;
            var memberStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == messageMainParty.MainPartyId);
            if (memberStore != null)
            {
                var store = entities.Stores.Select(g => new { g.MainPartyId, g.StoreName }).FirstOrDefault(x => x.MainPartyId == memberStore.StoreMainPartyId);
                if (store != null)
                {
                    model.FromSecondName = store.StoreName;
                    model.StoreUrl = "/Store/EditStore/" + store.MainPartyId;
                }
            }
            else
                model.FromSecondName = "Bireysel";

            model.InOutMainPartyId = messageMainParty.InOutMainPartyId;
            memberStore = entities.MemberStores.FirstOrDefault(x => x.MemberMainPartyId == messageMainParty.InOutMainPartyId);
            if (memberStore != null)
            {
                var store = entities.Stores.Select(g => new { g.MainPartyId, g.StoreName }).FirstOrDefault(x => x.MainPartyId == memberStore.StoreMainPartyId);
                if (store != null)
                {
                    model.ToSecondName = store.StoreName;
                    model.StoreUrl = "/Store/EditStore/" + store.MainPartyId;
                }
            }
            else
            {
                model.ToSecondName = "Bireysel";
            }

            return View(model);
        }
        [HttpPost]
        public JsonResult UpdateSeen(string id)
        {
            //string[] id = ids.Split(',');

            int ID = Convert.ToInt32(id);
            var message = entities.Messages.First(x => x.MessageId == ID);
            var messageMainParty = entities.MessageMainParties.FirstOrDefault(x => x.MessageId == message.MessageId);

            var memberStore = _memberStoreService.GetMemberStoreByMemberMainPartyId(messageMainParty.InOutMainPartyId);
            if (memberStore != null)
            {
                var messagesOld = from mw in entities.Messages join me in entities.MessageMainParties on mw.MessageId equals me.MessageId where me.InOutMainPartyId == messageMainParty.InOutMainPartyId select mw;
                foreach (var oldMessage in messagesOld)
                {
                    oldMessage.MessageSeenAdmin = true;

                }
            }
            message.MessageSeenAdmin = true;
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }
        #endregion
    }
}