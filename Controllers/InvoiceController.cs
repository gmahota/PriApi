using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriApi.Data;
using PriApi.Model;
using PriApi.Model.Helper;
using PriApi.Services;

namespace PriApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class InvoiceController : ControllerBase
    {
        private readonly IInvoicesServices _invoice;

        public InvoiceController(IInvoicesServices invoice)
        {
            _invoice = invoice;
        }

        // GET: api/Invoice
        [HttpGet]
        public IActionResult Get([FromBody] InvoiceParams parmas)
        {            
            var invoices = _invoice.GetAllAsync(parmas);
            
            return Ok(invoices);
        }


        // GET: api/Invoice/5
        [HttpGet("{id}", Name = "Get")]
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Invoice
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT: api/Invoice/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE: api/ApiWithActions/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
    }
}
