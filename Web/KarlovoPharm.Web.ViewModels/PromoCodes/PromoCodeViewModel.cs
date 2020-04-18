namespace KarlovoPharm.Web.ViewModels.PromoCodes
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class PromoCodeViewModel : IMapFrom<PromoCode>
    {

        public string Id { get; set; }

        public string Name { get; set; }

        public decimal DiscountInPercentage { get; set; }
    }
}
