using System;
using System.Threading.Tasks;

namespace Ecommerce.Api.Search.Interfaces
{
    public interface ICustomerService
    {
        public Task<(bool IsSuccess, dynamic customer, String ErrorMessage)> GetCustomerAsync(int id);
    }
}
