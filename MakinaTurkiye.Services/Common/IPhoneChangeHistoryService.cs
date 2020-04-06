using MakinaTurkiye.Entities.Tables.Common;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Common
{
    public interface IPhoneChangeHistoryService
    {
        IList<PhoneChangeHistory> GetAllPhoneChangeHistory();

        PhoneChangeHistory GetPhoneChangeHistoryByPhoneChangeHistoryId(int phoneChangeHistoryId);

        void InsertPhoneChange(PhoneChangeHistory phoneChangeHistory);

        void InsertPhoneChangeHistoryForPhone(Phone phone);

        void DeletePhoneChangeHistory(PhoneChangeHistory phoneChangeHistory); 
    }
}
