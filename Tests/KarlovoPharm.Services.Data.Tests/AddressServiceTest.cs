namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.Addresses;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Web.InputModels.Addresses.Create;
    using KarlovoPharm.Web.InputModels.Addresses.Edit;
    using KarlovoPharm.Web.ViewModels.Addresses;
    using Microsoft.EntityFrameworkCore;
    using Xunit;

    public class AddressServiceTest
    {

        [Fact]
        public async Task CreateAsync_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var addressRepository = new EfDeletableEntityRepository<Address>(context);
            var addressService = new AddressService(addressRepository);

            var addressCreateInputModel = new AddressCreateInputModel()
            {
                City = "TestCity",
                Street = "TestStreet",
                BuildingNumber = "123",
                PostCode = "223",
            };

            var emptyAddressCreateInputModel = new AddressCreateInputModel();

            var address = await addressService.CreateAsync(addressCreateInputModel);

            var actual = await context.Addresses.SingleOrDefaultAsync(x => x.City == "TestCity");

            Assert.NotNull(actual);
            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await addressService.CreateAsync(emptyAddressCreateInputModel);
            });
        }

        [Fact]
        public async Task DeleteAsync_WorksCorectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var addressRepository = new EfDeletableEntityRepository<Address>(context);
            var addressService = new AddressService(addressRepository);

            var address = await context.Addresses.SingleOrDefaultAsync(x => x.City == "FourthCity");

            var booleanResult = await addressService.DeleteAsync(address.Id);

            var addressesCount = await context.Addresses.CountAsync();

            Assert.True(booleanResult, "Servcie did not return true with correct data to be deleten");
            Assert.Equal(0, addressesCount);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await addressService.DeleteAsync(null);
            });
        }

        [Fact]
        public async Task Edit_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var addressRepository = new EfDeletableEntityRepository<Address>(context);
            var addressService = new AddressService(addressRepository);
            var addressSeeder = new AddressTestSeeder();
            await addressSeeder.SeedOneAddress(context);

            var address = await context.Addresses.SingleOrDefaultAsync(x => x.City == "FourthCity");

            var addressEditInputModel = new AddressEditInputModel()
            {
                Id = address.Id,
                City = "FourthCityEdit",
                Street = "TestStreet",
                BuildingNumber = "123",
                PostCode = "223",
            };

            var addressEditInputModelNullInt = new AddressEditInputModel()
            {
                City = "FourthCityEdit",
                Street = "TestStreet",
                BuildingNumber = "123",
                PostCode = "223",
            };

            var booleanResult = await addressService.Edit(addressEditInputModel);

            var edited = await context.Addresses.SingleOrDefaultAsync(x => x.City == "FourthCityEdit");

            Assert.True(booleanResult);

            Assert.NotNull(edited);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await addressService.Edit(addressEditInputModelNullInt);
            });
        }

        [Fact]
        public async Task GetById_WorksCorectly()
        {
            MapperInitializer.InitializeMapper();
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var addressRepository = new EfDeletableEntityRepository<Address>(context);
            var addressService = new AddressService(addressRepository);
            var addressSeeder = new AddressTestSeeder();
            await addressSeeder.SeedOneAddress(context);

            var address = await context.Addresses.SingleOrDefaultAsync(x => x.City == "FourthCity");

            Assert.Throws<ArgumentNullException>(() =>
            {
                 addressService.GetById<AddressViewModel>(null);
            });

            var actualAddress = addressService.GetById<AddressViewModel>(address.Id);

            Assert.Equal(address.City, actualAddress.City);
        }
    }
}
