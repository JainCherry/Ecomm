
using Ecommerce.Api.Orders.Model;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Orders.Interfaces
{
    public interface IOrdersProvider
    {
        public Task<(bool IsSuccess, IEnumerable< Model.Order> Orders, string ErrorMessage)> GetOrdersAsync(int customerId);
    }
}
