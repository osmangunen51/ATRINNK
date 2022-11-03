using MakinaTurkiye.Api.View.Result;
using MakinaTurkiye.Entities.StoredProcedures.Stores;
using System.Collections.Generic;

namespace MakinaTurkiye.Api.View
{
    public class StoreListItem
    {
        public WebSearchStoreResult Store { get; set; } = new WebSearchStoreResult();
        public List<ProductSearchResult> Products { get; set; } = new List<ProductSearchResult>();
    }
}