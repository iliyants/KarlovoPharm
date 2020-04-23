namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.Categories;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.InputModels.Categories.Edit;
    using KarlovoPharm.Web.ViewModels.Categories;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class CategoriesServiceTest
    {
        [Fact]
        public async Task CreateCategoryAsync_ThrowsException_IfNameIsNullOrWhiteSpace()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await categoryService.CreateCategoryAsync(null);
                await categoryService.CreateCategoryAsync(string.Empty);
            });
        }

        [Fact]
        public async Task CreateCategoryAsync_ReturnsFalse_IfNameIsNotUnique()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var actualResult = await categoryService.CreateCategoryAsync("TestCategory");

            Assert.False(actualResult);
        }

        [Fact]
        public async Task CreateCategoryAsync_ReturnsTrueAndCreates_IfNameIsUnique()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);

            var actualResult = await categoryService.CreateCategoryAsync("TestCategory");
            var category = categoryRepository.All().SingleOrDefaultAsync(x => x.Name == "TestCategory");

            Assert.True(actualResult);
            Assert.NotNull(category);
        }

        [Fact]
        public async Task GetNameByIdAsync_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var actualName = await categoryService.GetNameByIdAsync("1");

            Assert.Equal("TestCategory", actualName);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await categoryService.GetNameByIdAsync("invalid");
            });
        }

        [Fact]
        public async Task EditCategory_ReturnsTrueAndEdits_WhenIModelIsValid()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var inputModel = new CategoryEditInputModel()
            {
                Id = "1",
                Name = "Changed",
            };

            var booleanResult = await categoryService.EditCategory(inputModel);
            var category = await categoryRepository.All().SingleOrDefaultAsync(x => x.Id == "1");

            Assert.True(booleanResult);
            Assert.Equal("Changed", category.Name);
        }

        [Fact]
        public async Task EditCategory_ThrowException_WhenCategoryDoesntExist()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);

            var inputModel = new CategoryEditInputModel()
            {
                Id = "1",
                Name = "invalid",
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await categoryService.EditCategory(inputModel);
            });
        }

        [Fact]
        public async Task EditCategory_ReturnsFalse_IfNameIsNotUnique()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var inputModel = new CategoryEditInputModel()
            {
                Id = "1",
                Name = "TestCategory",
            };

            var result = await categoryService.EditCategory(inputModel);

            Assert.False(result);
        }

        [Fact]
        public async Task GetCategoryById_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var category = categoryService.GetCategoryById<CategoryViewModel>("1");

            Assert.NotNull(category);

            Assert.Throws<ArgumentNullException>(() =>
            {
                 categoryService.GetCategoryById<CategoryViewModel>("fake");
            });
        }

        [Fact]
        public async Task DeleteCategory_ReturnsTrue_WhenThereArenoSubCategoriesInThatCategory()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var result = await categoryService.DeleteCategory("2");

            Assert.True(result);
        }

        [Fact]
        public async Task DeleteCategory_ReturnsFalse_WhenThereAreSubCategoriesInThatCategory()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var result = await categoryService.DeleteCategory("1");

            Assert.False(result);
        }

        [Fact]
        public async Task GetAllAsync_ReturnsCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var categoryRepository = new EfDeletableEntityRepository<Category>(context);
            var categoryService = new CategoryService(categoryRepository);
            var categoryTestSeeder = new CategoryTestSeeder();

            await categoryTestSeeder.SeedCategories(context);

            var result = await categoryService.GetAllAsync<CategoryViewModel>();

            Assert.Equal(2, result.Count());
        }
    }
}
