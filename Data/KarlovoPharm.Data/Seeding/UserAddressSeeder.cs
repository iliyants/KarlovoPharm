namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;
    using Microsoft.EntityFrameworkCore;

    public class UserAddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UsersAddresses.Any())
            {
                return;
            }

            var firstUser = await dbContext.Users.Where(x => x.UserName == "FirstUser").SingleOrDefaultAsync();
            var secondUser = await dbContext.Users.Where(x => x.UserName == "SecondUser").SingleOrDefaultAsync();

            var firstAddress = await dbContext.Addresses.Where(x => x.City == "Sofia").SingleOrDefaultAsync();
            var secondAddress = await dbContext.Addresses.Where(x => x.City == "Plovdiv").SingleOrDefaultAsync();

            var firstCurrentAddress = new UserAddress
            {
                User = firstUser,
                Address = firstAddress,
            };

            var firstCurrentAddress2 = new UserAddress
            {
                User = firstUser,
                Address = secondAddress,
            };

            await dbContext.UsersAddresses.AddAsync(firstCurrentAddress);
            await dbContext.UsersAddresses.AddAsync(firstCurrentAddress2);

            var secondCurrentAddress = new UserAddress
            {
                User = secondUser,
                Address = secondAddress,
            };

            await dbContext.UsersAddresses.AddAsync(secondCurrentAddress);
        }
    }
}
