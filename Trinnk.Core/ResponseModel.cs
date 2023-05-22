using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Trinnk.Core
{
    public class ResponseModel<T>
    {
        public bool IsSuccess { get; set; } = false;
        public T Result { get; set; }
        public string Message { get; set; } = "";
        public Exception Error { get; set; }=new Exception("");
    }
}
