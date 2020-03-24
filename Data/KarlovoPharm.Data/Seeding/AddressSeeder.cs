namespace KarlovoPharm.Data.Seeding
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Threading.Tasks;

    using KarlovoPharm.Data.Models;

    public class AddressSeeder : ISeeder
    {
        public async Task SeedAsync(ApplicationDbContext dbContext, IServiceProvider serviceProvider)
        {
            if (dbContext.Addresses.Any())
            {
                return;
            }

            var addresses = new List<(string City,
                                  string Street,
                                  string PostCode,
                                  string BuildingNumber,
                                  string Description)>
            {
                ("Sofia", "SofiaStreet", "123456", "10A", "Sofia Description something"),
                ("Plovdiv", "PlovdivStreet", "654321", "98C", "Plovdiv description hahahehe"),
            };


            foreach (var address in addresses)
            {
                await dbContext.Addresses.AddAsync(new Address
                {
                    City = address.City,
                    Street = address.Street,
                    PostCode = address.PostCode,
                    BuildingNumber = address.BuildingNumber,
                    Description = address.Description,
                });
            }
        }
    }
}
