using System.Collections.Generic;

namespace Trinnk.Api.View
{
    public class StoreCatalogItem
    {
        public int CatalogId { get; set; }
        public string Name { get; set; }
        public string File { get; set; }
        public int Order { get; set; }
    }

    public class StoreCatalog
    {
        public List<StoreCatalogItem> List { get; set; } = new List<StoreCatalogItem>();
        public int MainPartyId { get; set; } = 0;
    }
}