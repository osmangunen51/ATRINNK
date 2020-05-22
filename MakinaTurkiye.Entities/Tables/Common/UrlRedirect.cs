using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Common
{
    public class UrlRedirect:BaseEntity
    {
        public int UrlRedirectId { get; set; }
        public string OldUrl { get; set; }
        public string NewUrl { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
