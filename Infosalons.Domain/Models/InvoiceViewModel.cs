using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infosalons.Domain.Models
{
    public class InvoiceViewModel
    {
        public int Id { get; set; }
        public String InvoiceNo { get; set; }
        public String ClientName { get; set; }
        public String PurchaseOrderNumber { get; set; }
        public string InvoiceDue { get; set; }
        public string? DefaultNote { get; set; }
        public decimal TotalAmount { get; set; }
        public DateTime InvoiceDate { get; set; }
        public DateTime DateAdded { get; set; }
        public DateTime DateUpdated { get; set; }
        public List<InvoiceItem> InvoiceItems { get; set; }
    }
}
