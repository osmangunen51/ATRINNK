using System.Collections.Generic;

namespace MakinaTurkiye.Entities.Tables.Settings
{
    public class Setting:BaseEntity
    {

        private ICollection<MemberSetting> _memberSettings;

        public int SettingId { get; set; }
        public string SettingName { get; set; }
        public byte SettingType { get; set; }


        public virtual ICollection<MemberSetting> MemberSettings
        {
            get { return _memberSettings ?? (_memberSettings = new List<MemberSetting>()); }
            protected set { _memberSettings = value; }
        }

    }
}
