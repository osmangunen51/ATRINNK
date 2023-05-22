using Trinnk.Entities.Tables.Stores;
using System;

namespace Trinnk.Entities.Tables.Logs
{
    public class LoginLog : BaseEntity
    {
        public int LoginLogId { get; set; }
        public int StoreMainPartyId { get; set; }
        public string IpAddress { get; set; }
        public DateTime LoginDate { get; set; }

        public virtual Store Store { get; set; }
    }
}
