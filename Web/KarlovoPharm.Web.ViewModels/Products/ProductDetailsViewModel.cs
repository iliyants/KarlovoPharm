namespace KarlovoPharm.Web.ViewModels.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class ProductDetailsViewModel : IMapFrom<Product>
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public decimal Price { get; set; }

        public string Picture { get; set; }

        public string Designation { get; set; }

        public string Effect { get; set; }

        public string Composition { get; set; }

        public string WayOfuse { get; set; }

        public string Specification { get; set; }

        public string Manufacturer { get; set; }

        public string CountryOfOrigin { get; set; }

        public string SubCategoryId { get; set; }
    }
}
