using Infosalons.Domain.Interfaces;
using Infosalons.Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Repository
{
    public class InvoiceRepository : GenericRepository<Invoice>, IInvoiceRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<InvoiceViewModel> GetInvoice(int id)
        {
            return await _context.Invoices.Select(x => new InvoiceViewModel()
            {
                Id = x.Id,
                ClientName = x.ClientName,
                InvoiceNo = x.InvoiceNo,
                DefaultNote = x.DefaultNote,
                InvoiceDue = x.InvoiceDue,
                PurchaseOrderNumber = x.PurchaseOrderNumber,
                TotalAmount = x.TotalAmount,
                DateAdded = x.DateAdded,
                InvoiceDate = x.InvoiceDate,
                DateUpdated = x.DateUpdated,
                InvoiceItems = _context.InvoiceItems.Where(i => i.InvoiceId == x.Id).ToList()
            }).Where(w => w.Id == id).FirstOrDefaultAsync();
        }

        public async Task<List<InvoiceViewModel>> GetInvoices()
        {
            return await _context.Invoices.Select(x => new InvoiceViewModel()
            {
                Id = x.Id,
                ClientName = x.ClientName,
                InvoiceNo = x.InvoiceNo,
                DefaultNote = x.DefaultNote,
                InvoiceDue = x.InvoiceDue,
                PurchaseOrderNumber = x.PurchaseOrderNumber,
                TotalAmount = x.TotalAmount,
                DateAdded = x.DateAdded,
                InvoiceDate = x.InvoiceDate,
                DateUpdated = x.DateUpdated,
                InvoiceItems = _context.InvoiceItems.Where(i => i.InvoiceId == x.Id).ToList()
            }).ToListAsync();
        }

        //public async Task<IEnumerable<Order>> GetOrdersByOrderName(string orderName)
        //{
        //    return await _context.Orders.Where(c => c.OrderDetails.Contains(orderName)).ToListAsync();
        //}
    }
}
