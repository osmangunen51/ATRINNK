using MakinaTurkiye.Entities.Tables.Messages;

namespace MakinaTurkiye.Services.Messages
{
    public interface IAutoMailRecordService
    {
        void InsertAutoMailRecord(AutoMailRecord autoMailRecord);
        void UpdateAutoMailRecord(AutoMailRecord autoMailRecord);
        AutoMailRecord GetAutoMailRecordServicesByStoreMainPartyId(int storeMainPartyId, int messagemtId);

    }
}
