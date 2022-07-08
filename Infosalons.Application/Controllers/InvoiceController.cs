using Infosalons.Domain.Interfaces;
using Infosalons.Domain.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Infosalons.Application.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    [Authorize]
    public class InvoiceController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IUserRepository _userRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IConfiguration _config;

        public InvoiceController(
            IUnitOfWork unitOfWork,
            IUserRepository userRepository,
            IInvoiceRepository invoiceRepository,
            IInvoiceItemRepository invoiceItemRepository,
            IConfiguration config
            )
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
            _invoiceRepository = invoiceRepository;
            _invoiceItemRepository = invoiceItemRepository;
            _config = config;
        }

        [HttpGet]
        public async Task<IEnumerable<InvoiceViewModel>> GetAll()
        {
            return await _unitOfWork.Invoices.GetInvoices();
        }

        [HttpGet("{id}")]
        public async Task<InvoiceViewModel> Get(int id)
        {
            return await _unitOfWork.Invoices.GetInvoice(id);
        }

        [HttpPost]
        public async Task<IActionResult> Post(InvoiceViewModel invoiceFormModel)
        {
            var _Invoice = new Invoice()
            {
                InvoiceNo = invoiceFormModel.InvoiceNo,
                ClientName = invoiceFormModel.ClientName,
                DefaultNote = invoiceFormModel.DefaultNote,
                InvoiceDate = invoiceFormModel.InvoiceDate,
                InvoiceDue = invoiceFormModel.InvoiceDue,
                PurchaseOrderNumber = invoiceFormModel.PurchaseOrderNumber,
                TotalAmount = invoiceFormModel.TotalAmount,
                DateAdded = DateTime.Now,
                DateUpdated = DateTime.Now
            };

            var result = await _unitOfWork.Invoices.Add(_Invoice);

            _unitOfWork.Complete();

            if (result is not null)
            {
                foreach (var item in invoiceFormModel.InvoiceItems)
                {
                    var _invoiceItem = new InvoiceItem()
                    {
                        InvoiceId = _Invoice.Id,
                        Description = item.Description,
                        Quantity = item.Quantity,
                        Rate = item.Rate,
                        DateAdded = DateTime.Now,
                        DateUpdated = DateTime.Now
                    };
                    var result2 = await _unitOfWork.InvoiceItems.Add(_invoiceItem);

                    if (result is null)
                    {
                        return BadRequest("Error in Creating Invoice Item");
                    }
                }

                _unitOfWork.Complete();
                return Ok("Invoice Created");
            }
            else
            {
                return BadRequest("Error in Creating Invoice");
            }
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateInvoice(int id, InvoiceViewModel invoiceFormModel)
        {
            var _Invoice = await _unitOfWork.Invoices.Get(invoiceFormModel.Id);


            if (_Invoice is not null)
            {

                _Invoice.InvoiceNo = invoiceFormModel.InvoiceNo;
                _Invoice.ClientName = invoiceFormModel.ClientName;
                _Invoice.DefaultNote = invoiceFormModel.DefaultNote;
                _Invoice.InvoiceDue = invoiceFormModel.InvoiceDue;
                _Invoice.PurchaseOrderNumber = invoiceFormModel.PurchaseOrderNumber;
                _Invoice.TotalAmount = invoiceFormModel.TotalAmount;
                _Invoice.InvoiceDate = invoiceFormModel.InvoiceDate;
                _Invoice.DateUpdated = DateTime.Now;
                _unitOfWork.Complete();

                foreach (var item in invoiceFormModel.InvoiceItems)
                {
                    var _invoiceItem = await _unitOfWork.InvoiceItems.Get(item.Id);

                    if (_invoiceItem is not null)
                    {

                        _invoiceItem.Description = item.Description;
                        _invoiceItem.Quantity = item.Quantity;
                        _invoiceItem.Rate = item.Rate;
                        _invoiceItem.DateUpdated = DateTime.Now;
                    }
                    else
                    {                        
                        await _unitOfWork.InvoiceItems.Add(new InvoiceItem()
                        {
                            InvoiceId = _Invoice.Id,
                            Description = item.Description,
                            Quantity = item.Quantity,
                            Rate = item.Rate,
                            DateAdded = DateTime.Now,
                            DateUpdated = DateTime.Now
                        });
                    }

                    _unitOfWork.Complete();
                }

                return Ok("Invoice updated successfully");
            }
            else
            {
                return BadRequest("Invoice not found");
            }
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(int id)
        {
            var invoice = await _unitOfWork.Invoices.Get(id);

            _unitOfWork.Invoices.Delete(invoice);

            var invoiceItems = await _unitOfWork.InvoiceItems.GetItemsByInvoiceId(invoice.Id);

            foreach (var item in invoiceItems)
            {
                _unitOfWork.InvoiceItems.Delete(item);
                //_unitOfWork.Complete();
            }

            _unitOfWork.Complete();
            return Ok("Invoice Deleted Successfully");
        }
    }
}
