using MakinaTurkiye.Entities.Tables.Cultures;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Cultures
{
    public class LanguageMap:EntityTypeConfiguration<Language>
    {
        public LanguageMap()
        {
            this.ToTable("Language");
            this.Ignore(b => b.Id);
            this.HasKey(b => b.LanguageId);
        }
 
    }
}
