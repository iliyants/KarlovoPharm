namespace KarlovoPharm.Web.InputModels.ShoppingCartProducts
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using System.ComponentModel.DataAnnotations.Schema;

    public class ShoppingCartProductInputModel : IMapFrom<ShoppingCartProduct>
    {
        public string ProductId { get; set; }
        public string ProductPicture { get; set; }
        public string ProductName { get; set; }
        public decimal ProductPrice { get; set; }
        public int Quantity { get; set; } 

        [NotMapped]
        public decimal TotalPrice =>
            this.Quantity * this.ProductPrice;
    }
}
