namespace KarlovoPharm.Web.ViewModels.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class ProductSingleViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }
    }
}
