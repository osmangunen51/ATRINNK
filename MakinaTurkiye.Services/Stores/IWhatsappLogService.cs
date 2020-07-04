using MakinaTurkiye.Entities.Tables.Stores;
using System;
using System.Collections.Generic;

namespace MakinaTurkiye.Services.Stores
{
    public interface IWhatsappLogService
    {
        WhatsappLog GetWhatsappLogByMainPartyIdAndRecordDate(int mainPartyId, DateTime dateTime);
        List<WhatsappLog> GetWhatsappLogsByMainPartyId(int mainPartyId);
        void InsertWhatsappLog(WhatsappLog whatsappLog);
        void UpdateWhatsappLog(WhatsappLog whatsappLog);
        void DeleteWhatsappLog(WhatsappLog whatsappLog);
        List<WhatsappLog> GetWhatpLogs();
    }
}
