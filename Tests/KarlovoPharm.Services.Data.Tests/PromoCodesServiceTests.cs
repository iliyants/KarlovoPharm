namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.PromoCodes;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.InputModels.PromoCodes;
    using KarlovoPharm.Web.ViewModels.PromoCodes;
    using Xunit;

    public class PromoCodesServiceTests
    {
        [Fact]
        public async Task Add_FunctionsProperly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var validPromoCodeCreateInputModel = new PromoCodeCreateInputModel()
            {
                Name = "Unique",
                DiscountInPercentage = 2,
            };

            var inValidPromoCodeNameCreateInputModel = new PromoCodeCreateInputModel()
            {
                Name = "First",
                DiscountInPercentage = 2,
            };

            var inValidPromoCodeCreateInputModel = new PromoCodeCreateInputModel()
            {
                Name = null,
                DiscountInPercentage = -1,
            };

            var shouldBeTrue = await promoCodeService.AddAsync(validPromoCodeCreateInputModel);
            var shouldBeFalse = await promoCodeService.AddAsync(inValidPromoCodeNameCreateInputModel);

            Assert.True(shouldBeTrue);
            Assert.False(shouldBeFalse);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await promoCodeService.AddAsync(inValidPromoCodeCreateInputModel);
            });
        }

        [Fact]
        public async Task EditAsync_FunctionsProperly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var validPromoCodeCreateInputModel = new PromoCodeCreateInputModel()
            {
                Id = "FirstId",
                Name = "FirstChanged",
            };

            var invalidNamePromoCodeCreateInputModel = new PromoCodeCreateInputModel()
            {
                Id = "SecondId",
                Name = "FirstChanged",
            };

            var exceptionPromoCodeCreateInputModel = new PromoCodeCreateInputModel()
            {
                Id = "invalid",
                Name = "invalid",
            };

            var shouldBeTrue = await promoCodeService.EditAsync(validPromoCodeCreateInputModel);
            var shouldBeFalse = await promoCodeService.EditAsync(invalidNamePromoCodeCreateInputModel);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await promoCodeService.EditAsync(exceptionPromoCodeCreateInputModel);
            });
        }

        [Fact]
        public async Task DeleteAsync_FunctionsProperly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var shouldBeTrue = await promoCodeService.DeleteAsync("FirstId");

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await promoCodeService.DeleteAsync("invalid");
            });
        }

        [Fact]
        public async Task ExistsAsync_FunctionsProperly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var shouldBeTrue = await promoCodeService.ExistsAsync("First");
            var shouldBeFalse = await promoCodeService.ExistsAsync("invalid");

            Assert.True(shouldBeTrue);
            Assert.False(shouldBeFalse);
        }

        [Fact]
        public async Task DeductPercentageFromPrice_ShouldDecuctThePriceProperly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var actualResult = await promoCodeService.DeductPercentageFromPrice(100, "FirstId");
            var expectedResult = 90;

            Assert.Equal(expectedResult, actualResult);
        }

        [Fact]
        public async Task GetIdByNameAsync_FunctionsProperly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var promoCodeRepository = new EfDeletableEntityRepository<PromoCode>(context);
            var promoCodeService = new PromoCodeService(promoCodeRepository);

            var promoCodesTestSeeder = new PromoCodesTestSeeder();

            await promoCodesTestSeeder.SeedPromoCodesAsync(context);

            var actualResult = await promoCodeService.GetIdByNameAsync("First");
            var expectedResult = "FirstId";

            Assert.Equal(expectedResult, actualResult);
        }
    }
}
