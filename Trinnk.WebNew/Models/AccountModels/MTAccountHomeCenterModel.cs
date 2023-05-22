using NeoSistem.Trinnk.Web.Areas.Account.Models;
using NeoSistem.Trinnk.Web.Areas.Account.Models.Advert;
using NeoSistem.Trinnk.Web.Models.ViewModels;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Models.AccountModels
{
    public class MTAccountHomeCenterModel
    {
        public MTAccountHomeCenterModel()
        {
            this.HelpList = new List<MTHelpModeltem>();
            this.OrderPriceCheck = true;
            this.ProductComments = new SearchModel<MTProductCommentStoreItem>();
        }
        public string MemberName { get; set; }
        public string MemberSurname { get; set; }
        public byte MemberType { get; set; }
        public int MessageCount { get; set; }
        public long TotalProductCount { get; set; }
        public int CheckingProductCount { get; set; }
        public int CheckedProductCount { get; set; }
        public int NotCheckedProductCount { get; set; }
        public int DeletedProductCount { get; set; }
        public int ActiveProductCount { get; set; }
        public int PasiveProductCount { get; set; }
        public long ViewSingularCount { get; set; }
        public long ViewMultipleCount { get; set; }
        public long ViewProductSingularCount { get; set; }
        public long ViewProductMultipleCount { get; set; }
        public bool HasPacket { get; set; }
        public string LastPage { get; set; }
        public int InboxMessageCount { get; set; }
        public bool OrderPriceCheck { get; set; }
        public decimal OrderPrice { get; set; }
        public bool MessageSended { get; set; }

        public SearchModel<MTProductCommentStoreItem> ProductComments { get; set; }
        public virtual List<MTHelpModeltem> HelpList { get; set; }
        public long ViewVideoTotalCount { get; set; }


    }
}