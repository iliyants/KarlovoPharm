
namespace KarlovoPharm.Data.Models
{
    using KarlovoPharm.Data.Common.Models;

    public class OrderProduct : BaseDeletableModel<string>
    {
        public string OrderId { get; set; }

        public virtual Order Order { get; set; }

        public string ProductId { get; set; }

        public virtual Product Product { get; set; }

        public int Quantity { get; set; }
    }
}
