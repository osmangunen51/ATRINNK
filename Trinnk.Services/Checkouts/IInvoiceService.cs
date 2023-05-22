using Trinnk.Entities.Tables.Checkouts;
using System.Collections.Generic;

namespace Trinnk.Services.Checkouts
{
    public interface IInvoiceService
    {
        IList<Invoice> GetAllInvoices();
        Invoice GetInvoiceByInvoiceId(int invoiceId);
        void InsertInvoice(Invoice invoice);
    }
}
