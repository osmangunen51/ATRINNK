using MakinaTurkiye.Entities.Tables.Settings;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Settings
{
    public interface IMemberSettingService : ICachingSupported
    {
        void InsertSetting(Setting setting);

        Setting GetSettingBySettingName(string settingName);

        Setting GetSettingBySettingId(int settingId);

        IList<MemberSetting> GetMemberSettingsBySettingNameWithStoreMainPartyId(string settingName, int storeMainPartyId);

        void InsertMemberSetting(MemberSetting memberSetting);

        void UpdateMemberSetting(MemberSetting memberSetting);

        MemberSetting GetMemberSettingByMemberSettingId(int MemberSettingId);

        void DeleteMemberSetting(MemberSetting memberSetting);
    }
}
