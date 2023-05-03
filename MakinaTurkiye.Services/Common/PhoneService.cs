using MakinaTurkiye.Caching;
using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class PhoneService : BaseService, IPhoneService
    {
        #region Constants

        private const string PHONES_BY_MAINPARTY_ID_KEY = "phone.bymainpartyId-{0}";
        private const string PHONES_BY_ADDRESS_ID_KEY = "phone.byaddressid-{0}";

        #endregion

        #region Fields

        private readonly IRepository<Phone> _phoneRepository;
        private readonly ICacheManager _cacheManager;

        #endregion

        #region Ctor

        public PhoneService(IRepository<Phone> phoneRepository, ICacheManager cacheManager) : base(cacheManager)
        {
            _phoneRepository = phoneRepository;
            _cacheManager = cacheManager;
        }

        #endregion

        #region Utilities

        private void RemoveCache(Phone phone)
        {
            string mainPartyIdKey = string.Format(PHONES_BY_MAINPARTY_ID_KEY, phone.MainPartyId);
            string addressıdKey = string.Format(PHONES_BY_ADDRESS_ID_KEY, phone.AddressId);

            _cacheManager.Remove(mainPartyIdKey);
            _cacheManager.Remove(addressıdKey);
        }

        #endregion

        #region Methods

        public IList<Phone> GetPhonesByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                return new List<Phone>();

            string key = string.Format(PHONES_BY_MAINPARTY_ID_KEY, mainPartyId);
            return _cacheManager.Get(key, () =>
             {
                 var query = _phoneRepository.Table;

                 //query = query.Include(p => p.Member);
                 //query = query.Include(p => p.Address);

                 query = query.Where(p => p.MainPartyId == mainPartyId);

                 var phones = query.ToList();
                 return phones;
             });
        }

        public IList<Phone> GetPhonesByMainPartyIds(List<int> mainPartyIds)
        {
            if (mainPartyIds.Count == 0)
                return new List<Phone>();

            string key = string.Format(PHONES_BY_MAINPARTY_ID_KEY,String.Join("-", mainPartyIds));
            return _cacheManager.Get(key, () =>
             {
                 var query = _phoneRepository.Table;

                 //query = query.Include(p => p.Member);
                 //query = query.Include(p => p.Address);

                 query = query.Where(p => mainPartyIds.Contains((int)p.MainPartyId));

                 var phones = query.ToList();
                 return phones;
             });
        }
        public Phone GetPhonesByMainPartyIdByPhoneType(int mainPartyId, PhoneTypeEnum phoneType)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");

            var query = _phoneRepository.Table;
            return query.FirstOrDefault(x => x.MainPartyId == mainPartyId && x.PhoneType == (byte)phoneType);
        }

        public Phone GetPhoneByPhoneId(int phoneId)
        {
            if (phoneId == 0)
                throw new ArgumentNullException("phoneId");

            var query = _phoneRepository.Table;
            return query.FirstOrDefault(p => p.PhoneId == phoneId);
        }

        public IList<Phone> GetPhonesAddressId(int addresId)
        {
            if (addresId == 0)
                throw new ArgumentNullException("storeDealerId");

            string key = string.Format(PHONES_BY_ADDRESS_ID_KEY, addresId);
            return _cacheManager.Get(key, () =>
            {
                var query = _phoneRepository.Table;
                query = query.Where(x => x.AddressId == addresId);

                return query.ToList();
            });

        }
        public Phone GetPhoneByActivationCode(string activationCode)
        {
            if (string.IsNullOrEmpty(activationCode))
                throw new ArgumentNullException("activationCode");

            var query = _phoneRepository.Table;

            var phone = query.FirstOrDefault(p => p.ActivationCode == activationCode);
            return phone;
        }


        public void UpdatePhone(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException("phone");

            _phoneRepository.Update(phone);

            RemoveCache(phone);
        }

        public void DeletePhone(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException("phone");

            _phoneRepository.Delete(phone);

            RemoveCache(phone);
        }

        public void InsertPhone(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException("phone");

            _phoneRepository.Insert(phone);

            RemoveCache(phone);
        }


        #endregion

    }
}
