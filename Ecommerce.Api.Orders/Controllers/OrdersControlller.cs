using Ecommerce.Api.Orders.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Controllers
{
    [ApiController]
    [Route("api/orders")]
    public class OrdersControlller : ControllerBase
    {
        private readonly IOrdersProvider ordersProvider;
        public OrdersControlller(IOrdersProvider ordersProvider)
        {
            this.ordersProvider = ordersProvider;
        }

        [HttpGet("{customerId}")]
        public async Task<IActionResult> GetOrdersAsync(int customerId)
        {
            var results = await ordersProvider.GetOrdersAsync(customerId);
            if (results.IsSuccess)
            {
                return Ok(results.Orders);
            }
            return NotFound();

        }
    }
}
