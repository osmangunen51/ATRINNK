using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services.CallCenter
{

    public class CallInfoResponse
    {
        public CallInfo[] callInfo { get; set; }
    }

    public class CallInfo
    {
        public string id { get; set; }
        public string caller { get; set; }
        public string uniqueid { get; set; }
        public string status { get; set; }
        public string message { get; set; }
    }

}
