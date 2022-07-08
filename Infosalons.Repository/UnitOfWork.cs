using Infosalons.Domain.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public IUserRepository Users { get; }
        public IInvoiceRepository Invoices { get; }
        public IInvoiceItemRepository InvoiceItems { get; }

        public UnitOfWork(
            ApplicationDbContext bookStoreDbContext,
            IUserRepository usersRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceItemRepository invoiceItemRepository
            )
        {
            _context = bookStoreDbContext;
            Users = usersRepository;
            Invoices = invoiceRepository;
            InvoiceItems = invoiceItemRepository;
        }
        public int Complete()
        {
            return _context.SaveChanges();
        }
        public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }
        protected virtual void Dispose(bool disposing)
        {
            if (disposing)
            {
                _context.Dispose();
            }
        }
    }
}
