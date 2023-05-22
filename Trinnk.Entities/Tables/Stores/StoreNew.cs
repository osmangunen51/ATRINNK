using System;

namespace Trinnk.Entities.Tables.Stores
{
    public class StoreNew : BaseEntity
    {
        public int StoreNewId { get; set; }
        public int StoreMainPartyId { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime UpdateDate { get; set; }
        public bool Active { get; set; }
        public string ImageName { get; set; }
        public long ViewCount { get; set; }
        public byte NewType { get; set; }

        //  public virtual Store Store { get; set; }
    }
}
