using System;

namespace Trinnk.Entities.Tables.Common
{
    public class Currency : BaseEntity
    {
        public byte CurrencyId { get; set; }

        public string CurrencyName { get; set; }

        public string CurrencyFullName { get; set; }

        public string CurrencyCodeName { get; set; }

        public bool Active { get; set; }

        public DateTime CreatedDate { get; set; }

        public int RecordCreatorId { get; set; }
    }
}
