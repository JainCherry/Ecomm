using Ecommerce.Api.Products.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Controllers
{
    [ApiController]
    [Route("api/products")]
    public class ProductController : ControllerBase

    {
        private readonly IProductsProvider productsProvider;
        public ProductController(IProductsProvider productsProvider)
        {
            this.productsProvider = productsProvider;
        }
        [HttpGet]
        public async Task<IActionResult> GetProductsAsync()
        {
            var results = await productsProvider.GetProductsAsync();
            if (results.IsSuccess)
            {
                return Ok(results.Products);
            }
            return NotFound();
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetProductAsync(int id)
        {
            var results = await productsProvider.GetProductAsync(id);
            if (results.IsSuccess)
            {
                return Ok(results.Product);
            }
            return NotFound();
        }
    }
}
