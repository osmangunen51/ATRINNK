using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Data.Entity.ModelConfiguration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Data.Mapping.Tables.Common
{
    public class UrlRedirectMap : EntityTypeConfiguration<UrlRedirect>
    {
        public UrlRedirectMap()
        {
            this.ToTable("UrlRedirect");
            this.Ignore(x => x.Id);
            this.HasKey(x => x.UrlRedirectId);
        }
    }
}
