using Trinnk.Entities.Tables.Logs;

namespace Trinnk.Services.Logs
{
    public interface ICreditCardLogService
    {
        void InsertCreditCardLog(CreditCardLog creditCardLog);
    }
}
