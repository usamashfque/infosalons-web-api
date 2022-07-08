using Infosalons.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Domain.Interfaces
{
    public interface IInvoiceRepository : IGenericRepository<Invoice>
    {
        Task<List<InvoiceViewModel>> GetInvoices();
        Task<InvoiceViewModel> GetInvoice(int id);
    }
}
