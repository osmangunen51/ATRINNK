using MakinaTurkiye.Entities.Tables.Members;
using MakinaTurkiye.Entities.Tables.Stores;
using System;

namespace MakinaTurkiye.Entities.Tables.Settings
{
    public class MemberSetting : BaseEntity
    {
        public int MemberSettingId { get; set; }
        public int SettingId { get; set; }
        public int? MemberMainPartyId { get; set; }
        public int? StoreMainPartyId { get; set; }
        public string FirstValue { get; set; }
        public string SecondValue { get; set; }
        public DateTime RecordDate { get; set; }
        public DateTime? UpdateDate { get; set; }

        //public virtual Member Member { get; set; }

        //public virtual Store Store { get; set; }

        public virtual Setting Setting { get; set; }

    }
}
