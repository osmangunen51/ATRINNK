using System.Collections.Generic;
using System;

namespace Trinnk.Api.View
{
    public class StoreSectorItem
    {
        public int CategoryId { get; set; } = 0;
        public int SectorId { get; set; } = 0;
        public string Name { get; set; } = "";
        public bool IsSelected { get; set; } = false;
    }

    public class StoreSector
    {
        public List<StoreSectorItem> List { get; set; } = new List<StoreSectorItem>();
        public int MainPartyId { get; set; } = 0;
    }
}