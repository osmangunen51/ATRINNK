using Trinnk.Api.View.Result;
using Trinnk.Entities.StoredProcedures.Stores;
using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class PromotedStoreItem
    {
        public Trinnk.Entities.Tables.Stores.Store Store { get; set; } = new Trinnk.Entities.Tables.Stores.Store();
        public List<ProductSearchResult> Products { get; set; } = new List<ProductSearchResult>();
    }
}
