using Trinnk.Entities.Tables.Members;
using Trinnk.Entities.Tables.Stores;
using System;

namespace Trinnk.Entities.Tables.Messages
{
    public class AutoMailRecord : BaseEntity
    {
        public int AutoMailRecordId { get; set; }
        public int? MessagesMTId { get; set; }
        public int? MemberMainPartyId { get; set; }
        public int? StoreMainPartyId { get; set; }
        public DateTime CreatedDate { get; set; }

        public virtual Store Store { get; set; }
        public virtual Member Member { get; set; }
    }
}
