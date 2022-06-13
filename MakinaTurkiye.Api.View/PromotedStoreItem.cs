using MakinaTurkiye.Api.View.Result;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class PromotedStoreItem
    {
        public MakinaTurkiye.Entities.Tables.Stores.Store Store { get; set; } = new MakinaTurkiye.Entities.Tables.Stores.Store();
        public List<ProductSearchResult> Products { get; set; } = new List<ProductSearchResult>();
    }
}
