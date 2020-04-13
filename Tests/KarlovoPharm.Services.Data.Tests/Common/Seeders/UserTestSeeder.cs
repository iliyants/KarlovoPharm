namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Data.Models.Common;

    public class UserTestSeeder
    {
        public async Task SeedUsersWithAddressesAsync(ApplicationDbContext context)
        { 
            var userWithoutAddresses = new ApplicationUser()
            {
                UserName = "UserWithoutAddresses",
            };

            var userWithThreeAdresses = new ApplicationUser()
            {
                UserName = "UserWithThreeAddresses",
            };

            var user1 = await context.Users.AddAsync(userWithoutAddresses);
            var user2 = await context.Users.AddAsync(userWithThreeAdresses);

            await context.SaveChangesAsync();

            userWithoutAddresses.ShoppingCart = new ShoppingCart()
            {
                UserId = user1.Entity.Id,
            };

            userWithoutAddresses.ShoppingCart = new ShoppingCart()
            {
                UserId = user2.Entity.Id,
            };

            await context.SaveChangesAsync();

            var addressTestSeeder = new AddressTestSeeder();

            await addressTestSeeder.SeedThreeAddressesToUser(context, user2.Entity.Id);
        }
    }
}
