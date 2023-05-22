using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.Help
{
    public class MTHelpDetailModel
    {
        public MTHelpDetailModel()
        {
            SubMenuItemModels = new List<MTHelpMenuModel>();
            OrderConfirmationForm = new OrderConfirmationFormModel();
        }

        public int CategoryId { get; set; }

        public string CategoryName { get; set; }

        public string Content { get; set; }

        public string Canonical { get; set; }

        public OrderConfirmationFormModel OrderConfirmationForm;


        public IList<MTHelpMenuModel> SubMenuItemModels { get; set; }
    }
}