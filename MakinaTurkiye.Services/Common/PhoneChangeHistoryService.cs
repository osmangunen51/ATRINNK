using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class PhoneChangeHistoryService : IPhoneChangeHistoryService
    {
        #region Fields

        private readonly IRepository<PhoneChangeHistory> _phoneChangeHistoryRepository;

        #endregion

        #region Ctor

        public PhoneChangeHistoryService(IRepository<PhoneChangeHistory> phoneChangeHistoryRepository)
        {
            this._phoneChangeHistoryRepository = phoneChangeHistoryRepository;
        }

        #endregion

        #region Methods

        public IList<PhoneChangeHistory> GetAllPhoneChangeHistory()
        {
            return _phoneChangeHistoryRepository.Table.ToList();
        }

        public PhoneChangeHistory GetPhoneChangeHistoryByPhoneChangeHistoryId(int phoneChangeHistoryId)
        {
            if (phoneChangeHistoryId == 0)
                throw new ArgumentNullException("phoneChangeHistoryId");

            var query = _phoneChangeHistoryRepository.Table;
            return query.FirstOrDefault(p => p.PhoneChangeHistoryId == phoneChangeHistoryId);

        }

        public void InsertPhoneChange(PhoneChangeHistory phoneChangeHistory)
        {
            if (phoneChangeHistory == null)
                throw new ArgumentNullException("phoneChangeHistory");
            _phoneChangeHistoryRepository.Insert(phoneChangeHistory);
        }

        public void InsertPhoneChangeHistoryForPhone(Phone phone)
        {
            if (phone == null)
                throw new ArgumentNullException("phone");
            PhoneChangeHistory model = new PhoneChangeHistory();
            model.MainPartyId = phone.MainPartyId;
            model.GsmType = phone.GsmType;
            model.PhoneAreaCode = phone.PhoneAreaCode;
            model.PhoneCulture = phone.PhoneCulture;
            model.PhoneId = phone.PhoneId;
            model.PhoneNumber = phone.PhoneNumber;
            model.PhoneType = phone.PhoneType;
            model.ActivationCode = phone.ActivationCode;
            model.active = phone.active;
            model.UpdatedDate = DateTime.Now;
            _phoneChangeHistoryRepository.Insert(model);
        }

        public void DeletePhoneChangeHistory(PhoneChangeHistory phoneChangeHistory)
        {
            if (phoneChangeHistory == null)
                throw new ArgumentNullException("phoneChangeHistory");
            _phoneChangeHistoryRepository.Delete(phoneChangeHistory);
        }

        #endregion 
    }
}
