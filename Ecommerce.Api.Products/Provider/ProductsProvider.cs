using AutoMapper;
using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Interfaces;
using Ecommerce.Api.Products.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Api.Products.Provider
{
    public class ProductsProvider : IProductsProvider
    {
        private readonly ProductsDbContext dbContext;
        private readonly ILogger logger;
        private readonly IMapper mapper;

        public ProductsProvider(ProductsDbContext dbContext, ILogger<ProductsProvider> logger, IMapper mapper)
        {
            this.dbContext = dbContext;
            this.logger = logger;
            this.mapper = mapper;

            SeedData();
        }

        private void SeedData()
        {
            if (!dbContext.Products.Any())
            {
                dbContext.Products.Add(new Db.Product() { Id = 1, Name = "Keyboard", Price = 50.00m, Inventory = 3 });
                dbContext.Products.Add(new Db.Product() { Id = 2, Name = "Mouse", Price = 520.00m, Inventory = 43 });
                dbContext.Products.Add(new Db.Product() { Id = 3, Name = "Monitor", Price = 450.00m, Inventory =4 });
                dbContext.Products.Add(new Db.Product() { Id = 4, Name = "Camera", Price = 560.00m, Inventory = 2 });
                dbContext.Products.Add(new Db.Product() { Id = 5, Name = "Cable", Price = 60.00m, Inventory = 8});
                dbContext.Products.Add(new Db.Product() { Id = 6, Name = "plugs", Price = 15.00m, Inventory = 89 });

                dbContext.SaveChangesAsync();
            }
        }

        public async Task<(bool IsSuccess, Models.Product Product, string ErrorMessage)> GetProductAsync(int id)
        {
            try
            {
                var product = await dbContext.Products.FirstOrDefaultAsync(p => p.Id == id);
                if (product != null)
                {
                    var results = mapper.Map<Db.Product, Models.Product>(product);
                    return (true, results, "");
                }
                return (false, null, "Not found");
            }

            catch (Exception Ex)
            {
                logger?.LogError(Ex.ToString());
                return (false, null, Ex.Message);
            }
        }

        public async Task<(bool IsSuccess, IEnumerable<Models.Product> Products, string ErrorMessage)> GetProductsAsync()
        {
            try
            {
                var products = await dbContext.Products.ToListAsync();
                if (products != null && products.Any())
                {
                    var results = mapper.Map<IEnumerable<Db.Product>, IEnumerable<Models.Product>>(products);
                    return  (true, results, "");
                }
                return (false, null, "Not found");
            }

            catch (Exception Ex) {
                logger?.LogError(Ex.ToString());
                return (false, null, Ex.Message);
            }
        }
    }
}
