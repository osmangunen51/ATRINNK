using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Cultures
{
   public class Language:BaseEntity
    {
        public int LanguageId { get; set; }
        public string LanguageCode { get; set; }

    }
}
