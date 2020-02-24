namespace KarlovoPharm.Data.Models
{
    public class ShoppingCartProduct
    {
        public string ShoppingCartId { get; set; }

        public ShoppingCart ShoppingCart { get; set; }

        public string ProductId { get; set; }

        public Product Product { get; set; }
    }
}
