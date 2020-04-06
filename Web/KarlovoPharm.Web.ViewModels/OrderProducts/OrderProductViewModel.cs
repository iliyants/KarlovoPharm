namespace KarlovoPharm.Web.ViewModels.OrderProducts
{
    using System.ComponentModel.DataAnnotations.Schema;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;

    public class OrderProductViewModel : IMapFrom<OrderProduct>
    {
        public string ProductName { get; set; }

        public decimal ProductPrice { get; set; }

        public int Quantity { get; set; }

        [NotMapped]
        public decimal TotalPrice
            => this.ProductPrice * this.Quantity;
    }
}
