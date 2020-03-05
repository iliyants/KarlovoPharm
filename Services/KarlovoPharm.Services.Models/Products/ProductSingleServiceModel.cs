namespace KarlovoPharm.Services.Models.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    public class ProductSingleServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public bool Available { get; set; }
    }
}
