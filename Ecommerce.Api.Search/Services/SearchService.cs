using Ecommerce.Api.Search.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Services
{
    public class SearchService : ISearchService
    {
        private readonly IOrderService orderService;
        private readonly IProductService productService;
        private readonly ICustomerService customerService;

        public SearchService(IOrderService orderService , IProductService productService, ICustomerService customerService)
        {
            this.orderService = orderService;
            this.productService = productService;
            this.customerService = customerService;
        }
        public async Task<(bool IsSuccess, dynamic SearchResult)> SearchAsync(int customerId)
        {
            var customerResult = await customerService.GetCustomerAsync(customerId);
            var orderResult = await orderService.GetOrderAsync(customerId);
            var ProductResult = await productService.GetProductsAsync(); 

            if (orderResult.IsSuccess)
            {
                foreach (var order in orderResult.Orders)
                {
                    foreach (var item in order.Items)
                    {
                        item.ProductName = ProductResult.IsSuccess ? ProductResult.products.FirstOrDefault(p => p.Id == item.ProductId)?.Name : "Product Information not available.";
                    }
                }
                var result = new
                {
                    Customer = customerResult.IsSuccess? customerResult.customer : new { Name = "Customer information not availoable"},
                    Orders = orderResult.Orders    
                };
                return (true, result);
            }
            return (false, null);
        }
    }
}
