using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Caching
{
    public interface ICachingOperation
    {
        bool SetOperationEnabled { get; set; }

        bool GetOperationEnabled { get; set; }

        bool RemoveOperationEnabled { get; set; }

        bool AllOperationEnabled { get; set; }
    }
}
