using System;

namespace NeoSistem.Trinnk.Management.Models
{
    public class FavoriteListItemModel
    {


        public int ProductId { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string AdedMemberName { get; set; }
        public int AddedMemberMainPartyId { get; set; }
        public string AddedStoreName { get; set; }
        public int ReceiverStoreMainPartyId { get; set; }
        public string ReceiverStoreName { get; set; }
        public DateTime? CreatedDate { get; set; }
    }
}