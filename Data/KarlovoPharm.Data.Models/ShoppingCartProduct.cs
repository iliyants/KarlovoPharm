namespace KarlovoPharm.Data.Models
{
    using KarlovoPharm.Data.Common.Models;

    public class ShoppingCartProduct : BaseDeletableModel<string>
    {
        public string ShoppingCartId { get; set; }

        public virtual ShoppingCart ShoppingCart { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
