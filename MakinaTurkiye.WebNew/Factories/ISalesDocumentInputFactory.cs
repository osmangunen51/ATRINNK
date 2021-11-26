using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace NeoSistem.MakinaTurkiye.Web.Factories
{
    public interface ISalesDocumentInputFactory
    {
        Dictionary<String, String> getPayload(int storeId, int orderId);

    }
}