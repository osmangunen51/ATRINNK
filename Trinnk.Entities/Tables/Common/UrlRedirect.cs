using System;

namespace Trinnk.Entities.Tables.Common
{
    public class UrlRedirect : BaseEntity
    {
        public int UrlRedirectId { get; set; }
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
