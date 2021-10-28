namespace NeoSistem.MakinaTurkiye.Web.Models
{
    using System;

    public class ProductCommentModel
    {
        public int ProductCommentId { get; set; }
        public int ProductId { get; set; }
        public int MainPartyId { get; set; }
        public string ProductCommentText { get; set; }
        public bool Active { get; set; }
        public DateTime RecordDate { get; set; }

        public string MainPartyFullName { get; set; }
    }
}

