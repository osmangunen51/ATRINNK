using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Pinterest.Model.Input.ChangeBussines
{
    public class ChangeBussines
    {
        public Options options { get; set; }
        public Context context { get; set; }
    }

    public class Options
    {
        public string business_id { get; set; }
        public bool get_user { get; set; }
        public string owner_id { get; set; }
        public int app_type_from_client { get; set; }
        public object visited_pages_before_login { get; set; }
        public bool no_fetch_context_on_resource { get; set; }
    }

    public class Context
    {
    }

}
