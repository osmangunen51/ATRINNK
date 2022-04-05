using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IAddressChangeHistoryService
    {
        IList<AddressChangeHistory> GetAllAddressChangeHistory();
        AddressChangeHistory GetAddressChangeHistory(int addressChangeHistoryId);
        void AddAddressChangeHistory(AddressChangeHistory addressChangeHistory);
        void AddAddressChangeHistoryForAddress(Address address);
        void DeleteAddressChangeHistory(AddressChangeHistory addressChangeHistory);

    }
}
