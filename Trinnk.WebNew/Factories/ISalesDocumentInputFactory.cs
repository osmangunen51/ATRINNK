using System;
using System.Collections.Generic;

namespace NeoSistem.Trinnk.Web.Factories
{
    public interface ISalesDocumentInputFactory
    {
        Dictionary<String, String> getPayload(int storeId, int orderId);

    }
}