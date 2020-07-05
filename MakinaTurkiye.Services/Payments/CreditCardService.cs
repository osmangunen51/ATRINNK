using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Payments;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Payments
{
    public class CreditCardService : ICreditCardService
    {

        #region Fileds

        private readonly IRepository<CreditCard> _creditCardRepository;
        private readonly IRepository<CreditCardInstallment> _creditCardInstallmentRepository;

        #endregion

        #region Ctor

        public CreditCardService(IRepository<CreditCard> creditCardRepository, 
                                IRepository<CreditCardInstallment> creditCardInstallmentRepository)
        {
            _creditCardRepository = creditCardRepository;
            _creditCardInstallmentRepository = creditCardInstallmentRepository;
        }

        #endregion


        public IList<CreditCard> GetAllCreditCards(bool showHidden = false)
        {
            var query = _creditCardRepository.Table;
            if (!showHidden)
                query = query.Where(cc => cc.Active);

            var creditCards = query.ToList();
            return creditCards;
        }

        public CreditCard GetCreditCardByCreditCardId(int creditCardId)
        {
            if (creditCardId == 0)
                throw new ArgumentNullException("creditCardId");

            var query = _creditCardRepository.Table;
            var creditCard = query.FirstOrDefault(cc => cc.CreditCardId == creditCardId);
            return creditCard;
        }

        public IList<CreditCardInstallment> GetCreditCardInstallmentsByCreditCardId(int creditCardId)
        {
            if (creditCardId == 0)
                throw new ArgumentNullException("creditCardId");

            var query = _creditCardInstallmentRepository.Table;
            query = query.Where(ccl => ccl.CreditCardId == creditCardId);
            var creditCardInstallments = query.ToList();
            return creditCardInstallments;

        }

        public CreditCardInstallment GetCreditCardInstallmentByCreditCardInstallmentId(int creditCardInstallmentId)
        {
            if (creditCardInstallmentId == 0)
                throw new ArgumentNullException("creditCardInstallmentId");

            var query = _creditCardInstallmentRepository.Table;
            var creditCardInstallment = query.FirstOrDefault(cc => cc.CreditCardInstallmentId == creditCardInstallmentId);
            return creditCardInstallment;
        }

    }
}
