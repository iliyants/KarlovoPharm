namespace KarlovoPharm.Web.ViewModels.Products
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class ProductFavouriteViewModel : IMapFrom<UserFavouriteProduct>
    {
        public string UserId { get; set; }

        public string ProductId { get; set; }

        public bool ProductAvailable { get; set; }

        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public string ProductPicture { get; set; }
    }
}
