namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.Products;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.Products.Create;
    using KarlovoPharm.Web.InputModels.Products.Edit;
    using KarlovoPharm.Web.ViewModels.Products;
    using Microsoft.AspNetCore.Http;
    using Microsoft.AspNetCore.Http.Internal;
    using Moq;
    using Xunit;

    public class ProductsServiceTests
    {
        [Fact]
        public async Task CreateAsync_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            IFormFile picture = new FormFile(new MemoryStream(Encoding.UTF8.GetBytes("dummy image")), 0, 0, "Data", "image.png");

            var validProductCreateInputModel = new ProductCreateInputModel()
            {
                Name = "asd",
                Price = 1,
                Picture = picture,
                Description = "some description",
            };

            var invalidProductCreateInputModel = new ProductCreateInputModel()
            {
                Name = null,
                Price = 0,
                Picture = null,
            };

            var shouldBeTrue = await productsService.CreateAsync(validProductCreateInputModel);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await productsService.CreateAsync(invalidProductCreateInputModel);
            });

            Assert.True(shouldBeTrue);
        }

        [Fact]
        public async Task GetAll_ReturnsProducts_WhichAreBeingSearchedByTheSearchString()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var products = productsService.GetAll<ProductSingleViewModel>("testProduct1").ToList();

            Assert.Collection(products, product => Assert.Contains("testProduct1", product.Name));
        }

        [Fact]
        public async Task GetAllBySubCategory_ReturnsProducts_WhichAreBeingSearchedByTheSearchStringInASubcategory()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var products = productsService.GetAllBySubCategory<ProductSingleViewModel>("subId1", "testProduct2").ToList();

            Assert.Collection(products, product => Assert.Contains("testProduct2", product.Name));
        }

        [Fact]
        public async Task OrderProducts_OrdersProductsCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var products = productRepository.All().To<ProductSingleViewModel>();

            var priceDesc = "price-highest-to-lowest";
            var priceAsc = "price-lowest-to-highest";

            var productsActualDesc = productsService.OrderProducts(priceDesc, products);
            var productsActualAsc = productsService.OrderProducts(priceAsc, products);

            var productExpectedDesc = productRepository.All().OrderByDescending(x => x.Price).To<ProductSingleViewModel>();
            var productExpectedAsc = productRepository.All().OrderBy(x => x.Price).To<ProductSingleViewModel>();

            Assert.Equal(productExpectedDesc, productsActualDesc);
            Assert.Equal(productExpectedAsc, productsActualAsc);
        }

        [Fact]
        public async Task GetProductDetailsById_ReturnsCorrectResult_ThrowsExceptionIfProductIsNull()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedOneProductForDetails(context);

            var product = productsService.GetProductDetailsById<ProductDetailsViewModel>("1");

            Assert.NotNull(product);

            Assert.Throws<ArgumentNullException>(() =>
            {
                productsService.GetProductDetailsById<ProductDetailsViewModel>("invalidId");
            });
        }

        [Fact]
        public async Task EditProductAsync_ReturnsTrueOrFalse_DependingOnTheInputData()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var validProductEditInputModel = new ProductEditInputModel()
            {
                Id = "1",
                Price = 1,
            };

            var inValidProductIdEditInputModel = new ProductEditInputModel()
            {
                Id = "invalid",
                Price = 1,
            };

            var inValidProductNameEditInputModel = new ProductEditInputModel()
            {
                Id = "1",
                Name = "testProduct2",
                Price = 1,
            };

            var resultShouldBeTrue = await productsService.EditProductAsync(validProductEditInputModel);
            var resultShouldBeFalse = await productsService.EditProductAsync(inValidProductNameEditInputModel);

            Assert.True(resultShouldBeTrue);
            Assert.False(resultShouldBeFalse);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await productsService.EditProductAsync(inValidProductIdEditInputModel);
            });
        }

        [Fact]
        public async Task DeleteProductAsync_ShouldReturnTrueOrFalse_DependingOnIfTheProductExists()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var shouldBeTrue = await productsService.DeleteProductAsync("1");

            Assert.True(shouldBeTrue);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await productsService.DeleteProductAsync("invalid");
            });
        }

        [Fact]
        public async Task GetNewest_ReturnsProductsCreatedINTheLast3Days()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var products = await productsService.GetNewest<ProductSingleViewModel>();

            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task GetAllByIds_ReturnsAllProductsByTheirIds()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var products = await productsService.GetAllByIds<ProductSingleViewModel>(new List<string> { "1", "2" });

            Assert.Equal(2, products.Count());
        }

        [Fact]
        public async Task ChangeAvailability_ChangesProductAvailabilityCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var productRepository = new EfDeletableEntityRepository<Product>(context);
            var productsTestSeeder = new ProductTestSeeder();
            var productsService = this.GetProductsService(productRepository, context);

            await productsTestSeeder.SeedProducts(context);

            var isNotAvailableModel = new ProductSingleViewModel() { Id = "2" };
            var nonExistantId = new ProductSingleViewModel() { Id = "asd" };

            var shouldBeTrue = await productsService.ChangeAvailability(isNotAvailableModel);

            Assert.True(shouldBeTrue.Available);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await productsService.ChangeAvailability(nonExistantId);
            });
        }

        private ProductService GetProductsService(EfDeletableEntityRepository<Product> productRepository, ApplicationDbContext context)
        {
            var cloudinaryService = new Mock<ICloudinaryService>();

            var orderProductService = new ProductService(
               productRepository,
               cloudinaryService.Object);

            return orderProductService;
        }
    }
}


