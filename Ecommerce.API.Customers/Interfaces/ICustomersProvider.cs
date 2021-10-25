using Ecommerce.API.Customers.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Customers.Interfaces
{
    public interface ICustomersProvider
    {
        public Task<(bool IsSuccess, IEnumerable<Customer> Cutomers, string ErrorMessage)> GetCustomersAsync();
        public Task<(bool IsSuccess, Customer Cutomer, string ErrorMessage)> GetCustomerAsync(int id);
    }
}
