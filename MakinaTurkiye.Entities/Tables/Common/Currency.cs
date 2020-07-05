using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Common
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
