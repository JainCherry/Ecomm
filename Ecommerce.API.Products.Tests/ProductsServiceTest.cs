using Ecommerce.Api.Products.Db;
using Ecommerce.Api.Products.Provider;
using System;
using Xunit;
using Microsoft.EntityFrameworkCore;
using Ecommerce.Api.Products.Profiles;
using AutoMapper;
using System.Threading.Tasks;
using System.Linq;

namespace Ecommerce.API.Products.Tests
{
    public class ProductsServiceTest
    {
        public ProductsServiceTest()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
                .UseInMemoryDatabase(nameof(GetProductsResultAllProducts))
                .Options;
            var dbContext = new ProductsDbContext(options);
            if (!dbContext.Products.Any())
                CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
        }
        [Fact]
        public async Task GetProductsResultAllProducts()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
               .UseInMemoryDatabase(nameof(GetProductsResultAllProducts))
               .Options;
            var dbContext = new ProductsDbContext(options);
            if (!dbContext.Products.Any())
                CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);

            var product = await productsProvider.GetProductsAsync();
            Assert.True(product.IsSuccess);
            Assert.True(product.Products.Any());
            Assert.Empty(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsResultAllProductsUsingValidID()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
               .UseInMemoryDatabase(nameof(GetProductsResultAllProductsUsingValidID))
               .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(1);
            Assert.True(product.IsSuccess);
            Assert.NotNull(product.Product);
            Assert.Empty(product.ErrorMessage);
        }

        [Fact]
        public async Task GetProductsResultAllProductsUsingInValidID()
        {
            var options = new DbContextOptionsBuilder<ProductsDbContext>()
               .UseInMemoryDatabase(nameof(GetProductsResultAllProductsUsingInValidID))
               .Options;
            var dbContext = new ProductsDbContext(options);
            CreateProducts(dbContext);
            var productProfile = new ProductProfile();
            var configuration = new MapperConfiguration(cfg => cfg.AddProfile(productProfile));
            var mapper = new Mapper(configuration);

            var productsProvider = new ProductsProvider(dbContext, null, mapper);
            var product = await productsProvider.GetProductAsync(-1);
            Assert.False(product.IsSuccess);
            Assert.Null(product.Product);
            Assert.NotEmpty(product.ErrorMessage);
        }

        private void CreateProducts(ProductsDbContext dbContext)
        {
            for (int i = 1; i <= 10; i++)
            {
                dbContext.Products.Add(new Product()
                {
                    Id = i,
                    Name = Guid.NewGuid().ToString(),
                    Inventory = i + 10,
                    Price = (decimal)(i * 3.14)
                });
            }
            dbContext.SaveChanges();
        }
    }
}
