using Trinnk.Core;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Services.CallCenter
{
    public interface ICallCenterService
    {
        string Token { get; set; }
        ResponseModel<CallInfo> Calling(string destination, string number);
        ResponseModel<string> StopCall(string id);
    }
}
