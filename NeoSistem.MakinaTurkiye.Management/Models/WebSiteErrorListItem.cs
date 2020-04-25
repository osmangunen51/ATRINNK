using System;

namespace NeoSistem.MakinaTurkiye.Management.Models
{
    public class WebSiteErrorListItem
    {
        public int WebSiteErrorId { get; set; }
        public int UserId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string UserName { get; set; }
        public string FilePath { get; set; }
        public bool IsSolved { get; set; }
        public bool IsFirst { get; set; }
        public DateTime? RecordDate { get; set; }
        public string ProblemTypeText { get; set; }
        public bool IsWaiting { get; set; }
    }
}