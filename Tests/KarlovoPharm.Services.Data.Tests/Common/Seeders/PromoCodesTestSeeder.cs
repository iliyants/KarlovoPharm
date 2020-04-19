namespace KarlovoPharm.Services.Data.Tests.Common.Seeders
{
    using System.Threading.Tasks;

    using KarlovoPharm.Data;
    using KarlovoPharm.Data.Models;

    public class PromoCodesTestSeeder
    {
        public async Task SeedPromoCodesAsync (ApplicationDbContext context)
        {
            var promoCode = new PromoCode()
            {
                Id = "FirstId",
                Name = "First",
                DiscountInPercentage = 10,
            };

            var promoCode2 = new PromoCode()
            {
                Id = "SecondId",
                Name = "Second",
                DiscountInPercentage = 3,
            };

            await context.PromoCodes.AddAsync(promoCode);
            await context.PromoCodes.AddAsync(promoCode2);

            await context.SaveChangesAsync();
        }
    }
}
