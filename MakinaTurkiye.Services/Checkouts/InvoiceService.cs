using MakinaTurkiye.Core.Data;
using MakinaTurkiye.Entities.Tables.Checkouts;
using System;
using System.Collections.Generic;
using System.Linq;

namespace MakinaTurkiye.Services.Checkouts
{
    public class InvoiceService : IInvoiceService
    {

        //TODO bu class kullanilmiyor silinebilir
        #region Fields

        private readonly IRepository<Invoice> _invoiceRepository;

        #endregion

        #region Methods

        public IList<Invoice> GetAllInvoices()
        {
            var query = _invoiceRepository.Table;
            return query.ToList();
        }

        public Invoice GetInvoiceByInvoiceId(int invoiceId)
        {
            if (invoiceId == 0)
                throw new ArgumentException("invoiceId");

            var query = _invoiceRepository.Table;
            return query.FirstOrDefault(i => i.InvoiceId == invoiceId);

        }

        public void InsertInvoice(Invoice invoice)
        {
            if (invoice == null)
                throw new ArgumentNullException("invoice");

            _invoiceRepository.Insert(invoice);
        }

        #endregion

    }
}
