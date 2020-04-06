using System;

namespace MakinaTurkiye.Entities.Tables.Members
{
    public class MainParty:BaseEntity
    {
        public int MainPartyId { get; set; }
        public string MainPartyFullName { get; set; }
        public byte MainPartyType{get;set;}
        public bool Active { get; set; }
        public DateTime MainPartyRecordDate { get; set; }

    }
}
