using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Cultures
{
    public class StaticDefinition:BaseEntity
    {
        public string Key { get; set; }
        public string Value { get; set; }
        public int LanguageId { get; set; }
    }
}
