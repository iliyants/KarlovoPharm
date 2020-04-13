namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class AddressTestSeeder
    {
        public async Task SeedThreeAddressesToUser(ApplicationDbContext context, string userId)
        {
            var firstAddress = new Address()
            {
                City = "FirstCity",
                Street = "FirstStreet",
                BuildingNumber = "1",
                PostCode = "1",
            };

            var secondAddress = new Address()
            {
                City = "SecondCity",
                Street = "SecondStreet",
                BuildingNumber = "2",
                PostCode = "2",
            };

            var thirdAddress = new Address()
            {
                City = "ThirdCity",
                Street = "ThirdStreet",
                BuildingNumber = "3",
                PostCode = "3",
            };

            var address1 = await context.Addresses.AddAsync(firstAddress);
            var address2 = await context.Addresses.AddAsync(secondAddress);
            var address3 = await context.Addresses.AddAsync(thirdAddress);

            await context.SaveChangesAsync();

            await context.UsersAddresses.AddAsync(new UserAddress()
            {
                UserId = userId,
                AddressId = address1.Entity.Id,
            });

            await context.UsersAddresses.AddAsync(new UserAddress()
            {
                UserId = userId,
                AddressId = address2.Entity.Id,
            });

            await context.UsersAddresses.AddAsync(new UserAddress()
            {
                UserId = userId,
                AddressId = address3.Entity.Id,
            });

            await context.SaveChangesAsync();
        }

        public async Task SeedOneAddress(ApplicationDbContext context)
        {
            var address = new Address()
            {
                City = "FourthCity",
                Street = "FourthStreet",
                BuildingNumber = "4",
                PostCode = "4",
            };

            await context.AddAsync(address);

            await context.SaveChangesAsync();
        }
    }
}
