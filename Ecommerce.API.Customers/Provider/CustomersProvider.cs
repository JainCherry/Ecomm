using AutoMapper;
using Ecommerce.API.Customers.Db;
using Ecommerce.API.Customers.Interfaces;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.API.Customers.Provider
{
    public class CustomersProvider : ICustomersProvider
    {
        private readonly CustomerDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public CustomersProvider(CustomerDbContext dbContext, ILogger<CustomersProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        public async Task<(bool IsSuccess, Model.Customer Cutomer, string ErrorMessage)> GetCustomerAsync(int id)
        {
            try
            {
                var customer = await dbContext.Customers.FirstOrDefaultAsync(c=> c.Id == id);
                if (customer != null )
                {
                    var results = mapper.Map<Db.Customer, Model.Customer>(customer);
                    return (true, results, "");
                }
                else
                    return (false, null, "Not found");

            }
            catch (Exception ex)
            {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Model.Customer> Cutomers, string ErrorMessage)> GetCustomersAsync()
        {
            try {
                var customers = await dbContext.Customers.ToListAsync();
                if (customers != null && customers.Any())
                {
                    var results = mapper.Map<IEnumerable<Db.Customer>, IEnumerable<Model.Customer>>(customers);
                    return (true, results, "");
                }
                else
                    return (false, null, "Not found");

            }
            catch (Exception ex) {
                logger?.LogError(ex.ToString());
                return (false, null, ex.Message);
            }
        }

        private void SeedData()
        {
            if (!dbContext.Customers.Any())
            {
                dbContext.Customers.Add(new Customer { Id = 1, Name="John", Address="sdkf jhskldf jajfdk hs" });
                dbContext.Customers.Add(new Customer { Id = 2, Name="Sophie", Address="sdkfjhs kldfjajfd khs" });
                dbContext.Customers.Add(new Customer { Id = 3, Name="Mike", Address="sdkfjh skld fjaj fdkhs" });
                dbContext.Customers.Add(new Customer { Id = 4, Name="Jackson", Address= "sdkfjhs kldf jajf dkhs" });

                dbContext.SaveChangesAsync();
            }
        }
    }
}
