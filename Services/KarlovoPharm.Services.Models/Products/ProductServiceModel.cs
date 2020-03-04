using KarlovoPharm.Data.Models;
using KarlovoPharm.Services.Mapping;

namespace KarlovoPharm.Services.Models.Products
{
    public class ProductServiceModel : IMapFrom<Product>, IMapTo<Product>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public string Specification { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string SubCategoryId { get; set; }
    }
}
