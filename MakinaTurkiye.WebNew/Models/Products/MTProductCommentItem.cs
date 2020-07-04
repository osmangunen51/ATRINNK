using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Models.Products
{
    public class MTProductCommentItem
    {
        public int ProductCommentId { get; set; }
        public string CommentText { get; set; }
        public byte Rate { get; set; }
        public string MemberProfilPhotoString { get; set; }
        public string MemberNameSurname { get; set; }
        public string Location { get; set; }
        public string RecordDate { get; set; }

    }
}