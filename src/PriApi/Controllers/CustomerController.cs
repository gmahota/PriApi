using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using PriApi.Model.Helper;
using PriApi.Services;

namespace PriApi.Controllers
{
    [Authorize]
    [Route("api/[controller]")]
    [ApiController]
    public class CustomerController : ControllerBase
    {
        private readonly ICustomerServices _customer;

        public CustomerController(ICustomerServices customer)
        {
            _customer = customer;
        }

        // GET: api/Invoice
        [HttpGet]
        public IActionResult Get([FromBody] CustomerParams parmas)
        {
            var invoices = _customer.GetAllAsync(parmas);

            return Ok(invoices);
        }


        // GET: api/Invoice/5
        [HttpGet("{id}")]
        public IActionResult Get(string id)
        {
            var customer = _customer.GetByCodeAsync(id);

            return Ok(customer);
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