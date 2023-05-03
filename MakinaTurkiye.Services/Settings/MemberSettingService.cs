using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Settings;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace MakinaTurkiye.Services.Settings
{
    public class MemberSettingService : BaseService, IMemberSettingService
    {

        #region Constants 

        private const string MEMBERSETTINGS_BY_SETTING_NAME_STORE_MAIN_PARTY_KEY = "membersetting.byname-{0}-{1}";
        private const string SETTINGS_BY_SETTING_NAME_KEY = "setting.byname-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Setting> _settingRepository;
        private readonly IRepository<MemberSetting> _memberSettingRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public MemberSettingService(IRepository<Setting> settingRepository,
            IRepository<MemberSetting> memberSettingRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            this._settingRepository = settingRepository;
            this._memberSettingRepository = memberSettingRepository;
            this._cacheManager = cacheManager;

        }

        #endregion

        #region Utilities

        private void RemoveMemberSettingCache(MemberSetting memberSetting)
        {
            var setting = GetSettingBySettingId(memberSetting.SettingId);
            string key = string.Format(MEMBERSETTINGS_BY_SETTING_NAME_STORE_MAIN_PARTY_KEY,
                                setting.SettingName, memberSetting.StoreMainPartyId);

            _cacheManager.Remove(key);
        }

        #endregion

        #region Methods

        public MemberSetting GetMemberSettingByMemberSettingId(int MemberSettingId)
        {
            if (MemberSettingId <= 0)
                throw new ArgumentNullException("MemberSettingId");

            var query = _memberSettingRepository.Table;
            return query.FirstOrDefault(x => x.MemberSettingId == MemberSettingId);
        }

        public Setting GetSettingBySettingId(int settingId)
        {
            if (settingId <= 0)
                throw new ArgumentNullException("settingId");

            var query = _settingRepository.Table;
            return query.FirstOrDefault(x => x.SettingId == settingId);
        }

        public IList<MemberSetting> GetMemberSettingsBySettingNameWithStoreMainPartyId(string settingName, int storeMainPartyId)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("settingName");

            if (storeMainPartyId <= 0)
                throw new ArgumentNullException("storeMainPartyId");

            string key = string.Format(MEMBERSETTINGS_BY_SETTING_NAME_STORE_MAIN_PARTY_KEY, settingName, storeMainPartyId);
            return _cacheManager.Get(key, () =>
            {
                var query = from s in _settingRepository.Table
                            join ms in _memberSettingRepository.Table on s.SettingId equals ms.SettingId
                            where ms.StoreMainPartyId == storeMainPartyId && s.SettingName == settingName
                            select ms;

                query = query.Include(ms => ms.Setting);

                return query.ToList();
            });
        }

        public Setting GetSettingBySettingName(string settingName)
        {
            if (string.IsNullOrEmpty(settingName))
                throw new ArgumentNullException("settingName");

            string key = string.Format(SETTINGS_BY_SETTING_NAME_KEY, settingName);
            return _cacheManager.Get(key, () =>
            {
                var query = _settingRepository.Table;
                return query.FirstOrDefault(x => x.SettingName == settingName);
            });
        }

        public void DeleteMemberSetting(MemberSetting memberSetting)
        {
            if (memberSetting == null)
                throw new ArgumentNullException("memberSetting");

            _memberSettingRepository.Delete(memberSetting);

            RemoveMemberSettingCache(memberSetting);
        }

        public void InsertMemberSetting(MemberSetting memberSetting)
        {
            if (memberSetting == null)
                throw new ArgumentNullException("memberSetting");

            _memberSettingRepository.Insert(memberSetting);


            RemoveMemberSettingCache(memberSetting);
        }

        public void UpdateMemberSetting(MemberSetting memberSetting)
        {
            if (memberSetting == null)
                throw new ArgumentNullException("memberSetting");

            _memberSettingRepository.Update(memberSetting);

            RemoveMemberSettingCache(memberSetting);
        }


        public void InsertSetting(Setting setting)
        {
            if (setting == null)
                throw new ArgumentNullException("setting");

            _settingRepository.Update(setting);

        }

        #endregion

    }
}
