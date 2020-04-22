namespace KarlovoPharm.Web.ViewModels.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class ProductSingleViewModel : IMapFrom<Product>
    {
        public string Id { get; set; }

        public string Name { get; set; }

        public decimal OldPrice { get; set; }

        public decimal Price { get; set; }

        public bool Available { get; set; }

        public string Picture { get; set; }

        public override bool Equals(object obj)
        {
            return this.Name == ((ProductSingleViewModel)obj).Name;
        }
    }
}
