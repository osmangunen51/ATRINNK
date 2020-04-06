namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    using Models;
    using NeoSistem.MakinaTurkiye.Management.Models.Entities;
    using System.Collections.Generic;
    using System.Linq;
    using System.Web.Mvc;

    public class SendedErrorMessageController : BaseController
    {
        public ActionResult Index(int? page, int? status, int? id)
        {
            var entities = new MakinaTurkiyeEntities();

            int pageSize = 50;
            int skipRows = 0;
            skipRows = page == null || page == 0 ? 0 : (int)(page - 1) * pageSize;
           int PAGE = page == null ? 0 : (int)page;
            int totalRecord = entities.SendMessageErrors.ToList().Count;
            var messagelist = (from s in entities.SendMessageErrors join m in entities.Members on s.SenderID equals m.MainPartyId join ms in entities.MemberStores on s.ReceiverID equals ms.MemberMainPartyId join st in entities.Stores on ms.StoreMainPartyId equals st.MainPartyId join p in entities.Products on s.ProductID equals p.ProductId select new { SenderMemberNo = m.MemberNo, s.MessageContent, s.MessageID, s.MessageSubject, ReceiverMemberNo = st.StoreNo, ReceiverName = st.StoreName, SenderName = m.MemberName + " " + m.MemberSurname, s.ErrorDate, p.ProductNo }).ToList();
            messagelist= messagelist.OrderByDescending(x => x.MessageID).ThenBy(x => x.ErrorDate).Skip(skipRows).Take(pageSize).ToList(); ;
           
           List<SendErrorMessageModel> messageListModel = new List<SendErrorMessageModel>();
            foreach (var itemMessage in messagelist)
            {
                SendErrorMessageModel modelOnly = new SendErrorMessageModel();
                modelOnly.MessageContent = itemMessage.MessageContent;
                modelOnly.MessageSubject = itemMessage.MessageSubject;
                modelOnly.ID = itemMessage.MessageID;
                modelOnly.ReceiverMemberNo = itemMessage.ReceiverMemberNo;
                modelOnly.SenderMemberNo = itemMessage.SenderMemberNo;
                modelOnly.ErrorDate = itemMessage.ErrorDate;
                modelOnly.SenderName = itemMessage.SenderName;
                modelOnly.ProductNo = itemMessage.ProductNo;
                modelOnly.ReceiverName = itemMessage.ReceiverName;
                messageListModel.Add(modelOnly);
            }
            FilterModel<SendErrorMessageModel> model = new FilterModel<SendErrorMessageModel>();

            model.Source = messageListModel;
            model.TotalRecord = totalRecord;
            model.CurrentPage= (int)PAGE;
            model.PageDimension = pageSize;

            //var messageList = entities.SendMessageErrors.OrderByDescending(x => x.MessageID).ThenBy(x => x.ErrorDate).Skip(skipRows).Take(pageSize).ToList();
            return View(model);
        }

        [HttpPost]
        public JsonResult DeleteErrorMessage(int id)
        {
            var demand = entities.SendMessageErrors.SingleOrDefault(x => x.MessageID == id);
            entities.SendMessageErrors.DeleteObject(demand);
            entities.SaveChanges();
            return Json(true, JsonRequestBehavior.AllowGet);
        }

    }
}