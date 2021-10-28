using System;

namespace NeoSistem.MakinaTurkiye.Web.Areas.Account.Models.Advert
{
    public class MTProductCommentStoreItem
    {
        public int ProductCommentId { get; set; }
        public string CommentText { get; set; }
        public byte Rate { get; set; }
        public bool Status { get; set; }
        public bool Reported { get; set; }

        public string MemberNameSurname { get; set; }
        public string Location { get; set; }
        public DateTime RecordDate { get; set; }
        public string ProductNo { get; set; }
        public string ProductName { get; set; }
        public string ProductUrl { get; set; }
    }
}