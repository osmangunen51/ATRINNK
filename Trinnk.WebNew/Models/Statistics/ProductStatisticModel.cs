using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Models.ViewModels;

namespace NeoSistem.Trinnk.Web.Models.Statistics
{
    public class ProductStatisticModel
    {
        public ProductStatisticModel()
        {
            this.LeftMenu = new LeftMenuModel();
            this.ProductItems = new SearchModel<ProductModel>();
            this.MTStatisticModel = new MTStatisticModel();
        }
        public SearchModel<ProductModel> ProductItems { get; set; }

        public LeftMenuModel LeftMenu { get; set; }
        public MTStatisticModel MTStatisticModel { get; set; }
    }
}