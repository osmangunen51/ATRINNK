using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Messages;
using System;
using System.Linq;

namespace MakinaTurkiye.Services.Messages
{
    public class AutoMailRecordService : IAutoMailRecordService
    {
        IRepository<AutoMailRecord> _autoMailRecordRepository;

        public AutoMailRecordService(IRepository<AutoMailRecord> autoMailRecordRepository)
        {
            _autoMailRecordRepository = autoMailRecordRepository;
        }


        public AutoMailRecord GetAutoMailRecordServicesByStoreMainPartyId(int storeMainPartyId, int messagemtId)
        {
            if (storeMainPartyId == 0)
                throw new ArgumentNullException("storeMainPartyId");

            var query = _autoMailRecordRepository.Table;
            return query.FirstOrDefault(x => x.StoreMainPartyId == storeMainPartyId && x.MessagesMTId == messagemtId);

        }

        public void InsertAutoMailRecord(AutoMailRecord autoMailRecord)
        {
            if (autoMailRecord == null)
                throw new ArgumentNullException("autoMailRecord");
            _autoMailRecordRepository.Insert(autoMailRecord);
        }

        public void UpdateAutoMailRecord(AutoMailRecord autoMailRecord)
        {
            if (autoMailRecord == null)
                throw new ArgumentNullException("autoMailRecord");
            _autoMailRecordRepository.Update(autoMailRecord);
        }
    }
}
