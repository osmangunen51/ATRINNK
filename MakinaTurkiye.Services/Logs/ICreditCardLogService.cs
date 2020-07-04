using MakinaTurkiye.Entities.Tables.Logs;

namespace MakinaTurkiye.Services.Logs
{
    public interface ICreditCardLogService
    {
        void InsertCreditCardLog(CreditCardLog creditCardLog);
    }
}
