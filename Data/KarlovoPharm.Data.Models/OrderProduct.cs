namespace KarlovoPharm.Data.Models
{
    public class OrderProduct
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }
    }
}
