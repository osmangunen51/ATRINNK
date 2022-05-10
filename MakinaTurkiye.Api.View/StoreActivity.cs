using System.Collections.Generic;
using System;

namespace MakinaTurkiye.Api.View
{
    public class StoreActivityItem
    {
        public int CategoryId { get; set; } = 0;
        public int ActivityId { get; set; } = 0;
        public string Name { get; set; } = "";
        public bool IsSelected { get; set; } = false;
    }

    public class StoreActivity
    {
        public List<StoreActivityItem> List { get; set; } = new List<StoreActivityItem>();
        public int MainPartyId { get; set; } = 0;
    }
}