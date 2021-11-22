using MakinaTurkiye.Entities.Tables.Cultures;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Cultures
{
    public class StaticDefinitionMap:EntityTypeConfiguration<StaticDefinition>
    {
        public StaticDefinitionMap()
        {
            this.ToTable("StaticDefinition");
        }
    }
}
