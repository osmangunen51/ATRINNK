using MakinaTurkiye.Entities.Tables.Members;
using System;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class ProductComment:BaseEntity
    {
       public int ProductCommentId { get; set; }
        public int ProductId { get; set; }
        public int MemberMainPartyId { get; set; }
        public string CommentText { get; set; }
        public DateTime RecordDate { get; set; }
        public byte? Rate { get; set; } 
        public bool Status { get; set; }
        public bool Viewed { get; set; }
        public bool Reported { get; set; }
        
        public virtual Product Product { get; set; }
        public virtual Member Member { get; set; }
    }
}
