using NeoSistem.MakinaTurkiye.Web.Models.Products;
using System.Collections.Generic;

namespace NeoSistem.MakinaTurkiye.Web.Models.Catalog
{
    public class ProductContactModel
    {
        public ProductContactModel()
        {
            StoreModel = new MTProductStoreModel();
            StoreMessage = new MTProductStoreMessageModel();
            this.WhatsappMessageItem = new MTWhatsappMessageItem();
        }
        //product overview

        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string MemberEmail { get; set; }
        public int MemberMainPartyId { get; set; }
 
        public string ProductPictureUrl { get; set; }
        public string StoreUrl { get; set; }
        public string ProductUrl { get; set; }
        public MTWhatsappMessageItem WhatsappMessageItem { get; set; }
        public MTProductStoreMessageModel StoreMessage{ get; set; }

        //satici ile alakali tum bilgiler bu classta oldugu icin ayni alanlari tekrar yazmadik
        public MTProductStoreModel StoreModel { get; set; }

    }
}