namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.SubCategories;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.InputModels.SubCategories.Create;
    using KarlovoPharm.Web.InputModels.SubCategories.Edit;
    using KarlovoPharm.Web.ViewModels.Categories;
    using Xunit;

    public class SubCategoriesServiceTests
    {
        [Fact]
        public async Task CreateAsync_ThrowsException_IfNameIsNullOrWhiteSpace()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);


            var subcategoryCreateInputModel = new SubCategoryCreateInputModel()
            {
                Name = null,
                CategoryId = null,
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await subCategoryService.CreateAsync(subcategoryCreateInputModel);
            });

        }

        [Fact]
        public async Task CreateAsync_ReturnsFalse_IfNameIsNotUnique()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var subCategoryCreateInputModel = new SubCategoryCreateInputModel()
            {
                Name = "SubCategory1",
                CategoryId = "categoryId1",
            };

            var shouldReturnfalse = await subCategoryService.CreateAsync(subCategoryCreateInputModel);

            Assert.False(shouldReturnfalse);
        }

        [Fact]
        public async Task CreateCategoryAsync_ReturnsTrueAndCreates_IfNameIsUnique()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);

            var subCategoryCreateInputModel = new SubCategoryCreateInputModel()
            {
                Name = "SubCategory1",
                CategoryId = "categoryId1",
            };

            var shouldReturnTrue = await subCategoryService.CreateAsync(subCategoryCreateInputModel);

            Assert.True(shouldReturnTrue);
        }

        [Fact]
        public async Task GetNameByIdAsync_ReturnsTheNameOrThrowsException()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var actualName = await subCategoryService.GetNameByIdAsync("1");

            Assert.Equal("SubCategory1", actualName);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await subCategoryService.GetNameByIdAsync("invalid");
            });
        }

        [Fact]
        public async Task EditSubCategory_ThrowsExceotion_IfMOdelIsIncorect()
        {

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var subCategoryEditInputModel = new SubCategoryEditInputModel()
            {
                Id = "Incorrect",
                Name = "SubCategory2",
            };

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await subCategoryService.EditSubCategory(subCategoryEditInputModel);
            });
        }

        [Fact]
        public async Task EditSubCategory_ReturnsFalse_IfNameIsNotUnique()
        {

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var subCategoryEditInputModel = new SubCategoryEditInputModel()
            {
                Id = "1",
                Name = "SubCategory2",
            };

            var shouldBeFalse = await subCategoryService.EditSubCategory(subCategoryEditInputModel);

            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task EditSubCategory_ReturnsTrue_IfNameIsUnique()
        {

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var subCategoryEditInputModel = new SubCategoryEditInputModel()
            {
                Id = "1",
                Name = "Unique",
            };

            var shouldBetrue = await subCategoryService.EditSubCategory(subCategoryEditInputModel);

            Assert.True(shouldBetrue);
        }


        [Fact]
        public async Task GetMainCategoryNameBySubCategoryId_ThrowsExceptionIfIdIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
             {
                 await subCategoryService.GetMainCategoryNameBySubCategoryId(null);
             });
        }

        [Fact]
        public async Task GetMainCategegoryIdBySubCategoryId_ThrowsExceptionIfIdIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await subCategoryService.GetMainCategoryNameBySubCategoryId(null);
            });
        }

        [Fact]
        public async Task DeleteSubCategory_ThrowsExceptionIfTheIdIsNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await subCategoryService.DeleteSubCategory(null);
            });
        }

        [Fact]
        public async Task DeleteSubCategory_ReturnsTrue_WhenTheIdISCorect()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var subCategoryRepository = new EfDeletableEntityRepository<SubCategory>(context);
            var subCategoryService = new SubCategoryService(subCategoryRepository);
            var subCategoryTestSeeder = new SubCategoryTestSeeder();

            await subCategoryTestSeeder.SeedSubCategories(context);

            var shouldBeTrue = await subCategoryService.DeleteSubCategory("1");

            Assert.True(shouldBeTrue);
        }
    }
}
