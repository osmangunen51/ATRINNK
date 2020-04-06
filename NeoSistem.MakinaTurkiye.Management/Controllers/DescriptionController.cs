using MvcContrib.Pagination;
using NeoSistem.EnterpriseEntity.Extensions;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Management.Models;
using NeoSistem.MakinaTurkiye.Management.Models.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;
namespace NeoSistem.MakinaTurkiye.Management.Controllers
{
    public class DescriptionController : BaseController
    {

        //
        // GET: /Description/


        //MakinaTurkiyeEntities entities = new MakinaTurkiyeEntities();
        public ActionResult Index(int? page)
        {
            PAGEID = PermissionPage.UyeAciklama;
            var memberdesc = (from c in entities.MemberDescriptions.AsEnumerable() orderby c.Date descending select c.MainPartyId).Distinct();
            List<MemberDescription> tes = new List<MemberDescription>();
            foreach (var m in memberdesc)
            {
                var memberDesc = entities.MemberDescriptions.Where(c => c.MainPartyId == m).OrderByDescending(c => c.Date).FirstOrDefault();
                tes.Add(memberDesc);
            }

            var descs = tes.AsPagination(page ?? 1, 30);
            //List<BaseMemberDescription> listBaseMember = new List<BaseMemberDescription>();
            //foreach (var item in descs)
            //{
            //    BaseMemberDescription baseMember = new BaseMemberDescription();
            //    baseMember.ID = item.ID;
            //    baseMember.InputDate = item.InputDate;
            //    baseMember.LastDate = item.LastDate;
            //    baseMember.MainPartyId = item.MainPartyId;
            //    baseMember.Title = item.Title;
            //    baseMember.DescriptionDegree = item.DescriptionDegree;
            //    baseMember.Description = item.Description;
            //    listBaseMember.Add(baseMember);

            //}
            //var descs = (from c in entities.MemberDescriptions
            //             orderby c.Date descending
            //             select c).Distinct().AsPagination(page ?? 1, 100);

            ViewData["Sayfa"] = descs.TotalPages;
            ViewData["Curr"] = descs.PageNumber;
            ViewData["Tot"] = descs.TotalItems;
            return View(descs);
        }

        public string UpdateDate(int id)
        {
            var memdesc = entities.MemberDescriptions.Where(c => c.descId == id).SingleOrDefault();
            memdesc.Date = DateTime.Now;
            entities.SaveChanges();
            return DateTime.Now.Date.ToShortDateString();
        }

        public ActionResult Edit()
        {
            int descId = 0;
            if (Request.QueryString["descId"] != null)
            {
                descId = Request.QueryString["descId"].ToInt32();
            }
            short check = Request.QueryString["check"].ToSByte();
            var description = entities.MemberDescriptions.Where(c => c.descId == descId).SingleOrDefault();
            description.DescriptionDegree = check;
            entities.SaveChanges();

            return RedirectToAction("Index");
        }

        public string transferDate()
        {
            var memberdesc = (from c in entities.MemberDescriptions.AsEnumerable() orderby c.Date descending select c.MainPartyId).Distinct();
            int sayi = memberdesc.ToList().Count;
            foreach (var m in memberdesc)
            {
                if (entities.BaseMemberDescriptions.FirstOrDefault(x => x.MainPartyId == m) == null)
                {
                    BaseMemberDescription baseModel = new BaseMemberDescription();
                    var memberDesc = entities.MemberDescriptions.Where(c => c.MainPartyId == m).OrderByDescending(c => c.Date).FirstOrDefault();
                    baseModel.MainPartyId = memberDesc.MainPartyId;
                    baseModel.Title = memberDesc.Title;
                    baseModel.Description = memberDesc.Description;
                    baseModel.Date = memberDesc.Date;
                    baseModel.UpdateDate = memberDesc.UpdateDate;
                    baseModel.DescriptionDegree = memberDesc.DescriptionDegree;
                    entities.BaseMemberDescriptions.AddObject(baseModel);
                    entities.SaveChanges();
                }
            }

            return "basarili";
        }
   
        
    }
}
