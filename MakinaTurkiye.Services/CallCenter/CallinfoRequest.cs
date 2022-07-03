using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.CallCenter
{

    public class CallinfoRequest
    {
        public string application { get; set; }
        public string destination { get; set; }
        public string callerid { get; set; }
        public string responseurl { get; set; }
        public string variable { get; set; }
        public Caller caller { get; set; }
    }

    public class Caller
    {
        public string _1 { get; set; }
    }

}
