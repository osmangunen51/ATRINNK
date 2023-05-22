using System;

namespace Trinnk.Entities.Tables.Users
{
    public class WebSiteError : BaseEntity
    {
        public int WebSiteErrorId { get; set; }
        public byte UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string FilePath { get; set; }
        public bool IsAdvice { get; set; }
        public bool IsSolved { get; set; }
        public bool IsFirst { get; set; }
        public DateTime? RecordDate { get; set; }
        public int? ProblemTypeId { get; set; }
        public bool? IsWaiting { get; set; }

    }
}
