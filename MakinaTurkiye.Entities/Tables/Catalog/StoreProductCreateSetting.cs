using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MakinaTurkiye.Entities.Tables.Catalog
{
    public class StoreProductCreateSetting:BaseEntity
    {
        public int StoreProductCreateSettingId { get; set; }
        public int StoreMainPartyId { get; set; }
        public int StoreProductCreatePropertieId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
