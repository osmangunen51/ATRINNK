using Trinnk.Entities.Tables.Messages;

namespace Trinnk.Services.Messages
{
    public interface IAutoMailRecordService
    {
        void InsertAutoMailRecord(AutoMailRecord autoMailRecord);
        void UpdateAutoMailRecord(AutoMailRecord autoMailRecord);
        AutoMailRecord GetAutoMailRecordServicesByStoreMainPartyId(int storeMainPartyId, int messagemtId);

    }
}
