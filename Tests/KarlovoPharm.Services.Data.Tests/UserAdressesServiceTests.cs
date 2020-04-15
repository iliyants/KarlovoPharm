namespace KarlovoPharm.Services.Data.Tests
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Repositories;
    using KarlovoPharm.Services.Data.Tests.Common;
    using KarlovoPharm.Services.Data.Tests.Common.Seeders;
    using KarlovoPharm.Services.Data.UsersAddresses;
    using Xunit;

    public class UserAdressesServiceTests
    {

        [Fact]
        public async Task CreateAsync_ThrowsException_IfEitheruserIdorAdressIdAreNull()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userAddressRepository = new EfDeletableEntityRepository<UserAddress>(context);
            var userAddressService = new UsersAddressesService(userAddressRepository);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userAddressService.CreateAsync(null, null);
            });
        }

        [Fact]
        public async Task CreateAsync_AddsAddress_ReturnsTrueAndAddsAddress_WithCorrectData()
        {
            var errorMessage = "Method did not return true upon adding an address to user;";

            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userAddressRepository = new EfDeletableEntityRepository<UserAddress>(context);
            var userAddressService = new UsersAddressesService(userAddressRepository);
            var userSeeder = new UserTestSeeder();
            await userSeeder.SeedUsersWithAddressesAsync(context);
            var addressSeeder = new AddressTestSeeder();
            await addressSeeder.SeedOneAddress(context);

            var user = context.Users.First(x => x.UserName == "UserWithoutAddresses");
            var address = context.Addresses.First(x => x.City == "FourthCity");

            var booleanResult = await userAddressService.CreateAsync(user.Id, address.Id);
            var actualAddressesCount = user.UserAddresses.Count();

            Assert.True(booleanResult, errorMessage);
            Assert.Equal(1, actualAddressesCount);
        }

        [Fact]
        public async Task DeleteAsync_FunctionsCorrectly()
        {
            var context = ApplicationDbContextInMemoryFactory.InitializeContext();
            var userAddressRepository = new EfDeletableEntityRepository<UserAddress>(context);
            var userAddressService = new UsersAddressesService(userAddressRepository);
            var userSeeder = new UserTestSeeder();
            await userSeeder.SeedUsersWithAddressesAsync(context);

            var user = context.Users.First(x => x.UserName == "UserWithThreeAddresses");
            var address = context.Addresses.First(x => x.City == "FirstCity");

            var booleanResult = await userAddressService.DeleteAsync(address.Id, user.Id);
            var actualAddressesCount = user.UserAddresses.Count();

            Assert.True(booleanResult, "Method should return true after deleting the address");
            Assert.Equal(2, actualAddressesCount);

            await Assert.ThrowsAsync<ArgumentNullException>(async () =>
            {
                await userAddressService.DeleteAsync(null, null);
            });
        }
    }
}
