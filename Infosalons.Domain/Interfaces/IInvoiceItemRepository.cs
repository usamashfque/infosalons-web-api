using Infosalons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Domain.Interfaces
{
    public interface IInvoiceItemRepository : IGenericRepository<InvoiceItem>
    {
        Task<List<InvoiceItem>> GetItemsByInvoiceId(int invoiceId);
    }
}
