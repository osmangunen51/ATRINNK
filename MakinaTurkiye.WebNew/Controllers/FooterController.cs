using System.Web.Mvc;
using System.Linq;
using NeoSistem.MakinaTurkiye.Web.Models.Footer;
using System.Collections.Generic;
using MakinaTurkiye.Services.Content;
using System.Threading.Tasks;

namespace NeoSistem.MakinaTurkiye.Web.Controllers
{
    [AllowAnonymous]
    public class FooterController : BaseController
    {
        #region Fields

        private readonly IFooterService _footerService;

        #endregion

        #region Ctor

        public FooterController(IFooterService footerService)
        {
            this._footerService = footerService;
        }

        #endregion
     
        #region Methods

        [ChildActionOnly]
        public ActionResult Content()
        {
            var sayfa = Request.QueryString["page"];
            var footerParent = _footerService.GetAllFooterParent();
            List<MTFooterParentModel> footerParents = new List<MTFooterParentModel>();

            foreach (var item in footerParent)
            {
                MTFooterParentModel footerParentItem = new MTFooterParentModel();
                footerParentItem.FooterParentId = item.FooterParentId;
                footerParentItem.FooterParentName = item.FooterParentName;
                foreach (var item1 in item.FooterContents.OrderBy(x => x.DisplayOrder))
                {
                    MTFooterContentModel footerContentModel = new MTFooterContentModel
                    {
                        FooterContentName = item1.FooterContentName,
                        FooterContentUrl = item1.FooterContentUrl
                    };
                    footerParentItem.FooterContents.Add(footerContentModel);
                }
                footerParents.Add(footerParentItem);
            }
            return PartialView(footerParents);
        }

        #endregion

    }
}
