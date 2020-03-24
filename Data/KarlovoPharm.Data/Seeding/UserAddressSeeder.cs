namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;

    public class UserAddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.UsersAddresses.Any())
            {
                return;
            }

            var firstUser = dbContext.Users.Where(x => x.UserName == "FirstUser").SingleOrDefault();
            var secondUser = dbContext.Users.Where(x => x.UserName == "SecondUser").SingleOrDefault();

            var firstAddress = dbContext.Addresses.Where(x => x.City == "Sofia").SingleOrDefault();
            var secondAddress = dbContext.Addresses.Where(x => x.City == "Plovdiv").SingleOrDefault();

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
