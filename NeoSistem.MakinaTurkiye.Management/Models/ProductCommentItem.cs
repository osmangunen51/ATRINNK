namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class ProductCommentItem
    {
        public int ProductCommentId { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductNo { get; set; }
        public string MemberNameSurname { get; set; }
        public byte Rate { get; set; }
        public string CommenText { get; set; }
        public bool Status { get; set; }
        public string RecorDate { get; set; }
        public bool Reported { get; set; }
       public string MemberEmail { get; set; }

    }
}