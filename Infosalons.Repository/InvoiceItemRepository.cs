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
    public class InvoiceItemRepository : GenericRepository<InvoiceItem>, IInvoiceItemRepository
    {
        private readonly ApplicationDbContext _context;
        public InvoiceItemRepository(ApplicationDbContext context) : base(context)
        {
            _context = context;
        }

        public async Task<List<InvoiceItem>> GetItemsByInvoiceId(int invoiceId)
        {
            return await _context.InvoiceItems.Where(c => c.InvoiceId == invoiceId).ToListAsync();
        }

        //public async Task<IEnumerable<Order>> GetOrdersByOrderName(string orderName)
        //{
        //    return await _context.Orders.Where(c => c.OrderDetails.Contains(orderName)).ToListAsync();
        //}
    }
}
