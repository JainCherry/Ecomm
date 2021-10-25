using Ecommerce.API.Customers.Interfaces;
using Ecommerce.API.Customers.Provider;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Customers.Controllers
{
    [ApiController]
    [Route("api/customers")]
    public class CustomersController : ControllerBase
    {
        private readonly ICustomersProvider customersProvider;
        public CustomersController(ICustomersProvider customersProvider)
        {
            this.customersProvider = customersProvider;
        }

        [HttpGet]
        public async Task<IActionResult> GetCustomersAsync()
        {
            var results =await customersProvider.GetCustomersAsync();
            if (results.IsSuccess)
            {
                return Ok(results.Cutomers);
            }
            return NotFound();

           
        }
        [HttpGet("{id}")]
        public async Task<IActionResult> GetCustomersAsync(int id)
        {
            var results = await customersProvider.GetCustomerAsync(id);
            if (results.IsSuccess)
            {
                return Ok(results.Cutomer);
            }
            return NotFound();


        }
    }
}
