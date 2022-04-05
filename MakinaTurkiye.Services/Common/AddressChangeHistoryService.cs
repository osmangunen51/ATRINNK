using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Common;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Common
{
    public class AddressChangeHistoryService : IAddressChangeHistoryService
    {

        #region Fields

        private readonly IRepository<AddressChangeHistory> _addressChangeHistoryRepository;

        #endregion

        #region Ctor

        public AddressChangeHistoryService(IRepository<AddressChangeHistory> addressChangeHistoryRepository)
        {
            this._addressChangeHistoryRepository = addressChangeHistoryRepository;
        }

        #endregion

        #region Methods

        public IList<AddressChangeHistory> GetAllAddressChangeHistory()
        {
            return _addressChangeHistoryRepository.Table.ToList();
        }

        public AddressChangeHistory GetAddressChangeHistory(int addressChangeHistoryId)
        {
            if (addressChangeHistoryId == 0)
                throw new ArgumentNullException("addressChangeHistoryId");
            var query = _addressChangeHistoryRepository.Table;
            return query.FirstOrDefault(a => a.AddressChangeHistoryId == addressChangeHistoryId);

        }

        public void AddAddressChangeHistory(AddressChangeHistory addressChangeHistory)
        {
            if (addressChangeHistory == null)
                throw new ArgumentNullException("addressChangeHistory");
            _addressChangeHistoryRepository.Insert(addressChangeHistory);
        }

        public void AddAddressChangeHistoryForAddress(Address address)
        {
            if (address == null)
                throw new ArgumentNullException("address");
            AddressChangeHistory addressChangeHistory = new AddressChangeHistory();
            addressChangeHistory.AddressDefault = address.AddressDefault;
            addressChangeHistory.AddressId = address.AddressId;
            addressChangeHistory.AddressTypeId = address.AddressTypeId;
            addressChangeHistory.ApartmentNo = address.ApartmentNo;
            addressChangeHistory.Avenue = address.Avenue;
            addressChangeHistory.CityId = address.CityId;
            addressChangeHistory.CountryId = address.CountryId;
            addressChangeHistory.DoorNo = address.DoorNo;
            addressChangeHistory.LocalityId = address.LocalityId;
            addressChangeHistory.MainPartyId = address.MainPartyId;
            addressChangeHistory.StoreDealerId = address.StoreDealerId;
            addressChangeHistory.Street = address.Street;
            addressChangeHistory.TownId = address.TownId;
            addressChangeHistory.UpdatedDate = DateTime.Now;
            _addressChangeHistoryRepository.Insert(addressChangeHistory);

        }

        public void DeleteAddressChangeHistory(AddressChangeHistory addressChangeHistory)
        {
            if (addressChangeHistory == null)
                throw new ArgumentNullException("addressChangeHistory");
            _addressChangeHistoryRepository.Delete(addressChangeHistory);
        }

        #endregion

    }
}
