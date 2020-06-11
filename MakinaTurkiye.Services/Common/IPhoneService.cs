using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IPhoneService : ICachingSupported
    {
        IList<Phone> GetPhonesByMainPartyId(int mainPartyId);

        Phone GetPhoneByPhoneId(int phoneId);

        Phone GetPhoneByActivationCode(string activationCode);

        void InsertPhone(Phone phone);

        void UpdatePhone(Phone phone);

        void DeletePhone(Phone phone);

        Phone GetPhonesByMainPartyIdByPhoneType(int mainPartyId, PhoneTypeEnum phoneType);
       
        IList<Phone> GetPhonesAddressId(int addressId);
    }
}
