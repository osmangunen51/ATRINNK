using System;

namespace NeoSistem.Trinnk.Management.Models.Stores
{
    public class StoreNewItem
    {
        public int StoreNewId { get; set; }
        public string StoreName { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public string ImagePath { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public byte NewType { get; set; }
        public long ViewCount { get; set; }
        public bool Active { get; set; }
    }
}