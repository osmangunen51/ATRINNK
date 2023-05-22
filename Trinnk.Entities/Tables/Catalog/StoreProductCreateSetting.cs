using System;

namespace Trinnk.Entities.Tables.Catalog
{
    public class StoreProductCreateSetting : BaseEntity
    {
        public int StoreProductCreateSettingId { get; set; }
        public int StoreMainPartyId { get; set; }
        public int StoreProductCreatePropertieId { get; set; }
        public string Value { get; set; }
        public DateTime CreatedDate { get; set; }
    }
}
