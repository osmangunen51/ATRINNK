using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Stores
{
    public class WhatsappLogService : IWhatsappLogService
    {
        IRepository<WhatsappLog> _whatsappLogResository;

        public WhatsappLogService(IRepository<WhatsappLog> whatsappLogResository)
        {
            _whatsappLogResository = whatsappLogResository;
        }

        public void DeleteWhatsappLog(WhatsappLog whatsappLog)
        {
            if (whatsappLog == null)
                throw new ArgumentNullException("whatsappLog");
            _whatsappLogResository.Delete(whatsappLog);
        }

        public List<WhatsappLog> GetWhatpLogs()
        {
            var query = _whatsappLogResository.Table;
            return query.ToList();
        }

        public WhatsappLog GetWhatsappLogByMainPartyIdAndRecordDate(int mainPartyId, DateTime dateTime)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");
            var query = _whatsappLogResository.Table;
            return query.FirstOrDefault(x => x.MainPartyId == mainPartyId && x.RecordDate == dateTime);

        }

        public List<WhatsappLog> GetWhatsappLogsByMainPartyId(int mainPartyId)
        {
            if (mainPartyId == 0)
                throw new ArgumentNullException("mainPartyId");
            var query = _whatsappLogResository.Table;
            query = query.Where(x => x.MainPartyId == mainPartyId);
            return query.ToList();
        }

        public void InsertWhatsappLog(WhatsappLog whatsappLog)
        {
            if (whatsappLog == null)
                throw new ArgumentNullException("whatsappLog");
            _whatsappLogResository.Insert(whatsappLog);
        }

        public void UpdateWhatsappLog(WhatsappLog whatsappLog)
        {
            if (whatsappLog == null)
                throw new ArgumentNullException("whatsappLog");
            _whatsappLogResository.Update(whatsappLog);
        }
    }
}
