
using System.Collections.Generic;
using System.Web;
using System.Web.Mvc;
using System.Linq;
using System.Xml.Linq;
using NeoSistem.EnterpriseEntity.Extensions.Data;
using NeoSistem.MakinaTurkiye.Web.Areas.Account.Models;
using MakinaTurkiye.Entities.Tables.Catalog;
using MakinaTurkiye.Services.Catalog;
using MakinaTurkiye.Core.Infrastructure;

namespace NeoSistem.MakinaTurkiye.Web.Models
{
    public class AdvertViewModel
    {
        public int DropDownSector { get; set; }
        public int DropDownProductGroup { get; set; }
        public int DropDownCategory { get; set; }
        public int? DropDownBrand { get; set; }
        public int? DropDownSeries { get; set; }
        public int? DropDownModel { get; set; }
        public string OtherBrand { get; set; }
        public string OtherModel { get; set; }
        public string OtherSeries { get; set; }
        public int CategoryId { get; set; }

        public LeftMenuModel LeftMenu { get; set; }

        public IList<Category> CategoryList { get; set; }

        public CategorizationModel CategorizationSessionModel
        {
            get
            {
                if (HttpContext.Current.Session["CategorizationItem"] == null)
                {
                    HttpContext.Current.Session["CategorizationItem"] = new CategorizationModel();
                }
                return HttpContext.Current.Session["CategorizationItem"] as CategorizationModel;
            }
            set { HttpContext.Current.Session["CategorizationItem"] = value; }
        }

        public List<PictureModel> PictureList { get; set; }

        public IEnumerable<Category> SectorItems
        {
            get
            {
                IList<Category> sectorItems = null;

                ICategoryService _categoryService = EngineContext.Current.Resolve<ICategoryService>();
                sectorItems = _categoryService.GetMainCategories();
                return sectorItems.OrderBy(c => c.CategoryOrder).ThenBy(c => c.CategoryName);
            }
        }

        public List<SelectListItem> PrivateSectorCategories { get; set; }

        public IList<CategoryModel> CategoryBrandItems { get; set; }


        public IList<CategoryModel> CategorySeriesItems { get; set; }

        public IList<CategoryModel> CategoryModelItems { get; set; }

        public ICollection<VideoModel> VideoItems { get; set; }

    }
}