using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Pinterest.Model.Input.PinDelete
{ 
    public class PinDelete
    {
        public Options options { get; set; }=new Options(); 
        public Context context { get; set; }=new Context();
    }

    public class Options
    {
        public string id { get; set; } = "";
        public bool no_fetch_context_on_resource { get; set; } = false;
    }

    public class Context
    {
    }


}
