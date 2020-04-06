﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Services
{
    public interface ICachingSupported
    {
        bool CachingGetOrSetOperationEnabled { get; set; }

        bool CachingRemoveOperationEnabled { get; set; }
    }
}
