namespace KarlovoPharm.Web.ViewModels.Display
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class ProductDisplayViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Picture { get; set; }

        public string Name { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string SubCategoryCategoryName { get; set; }

        public string SubCategoryName { get; set; }
    }
}
