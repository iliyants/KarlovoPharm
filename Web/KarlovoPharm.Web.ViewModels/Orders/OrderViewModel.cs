namespace KarlovoPharm.Web.ViewModels.Orders
{
    using System.Collections.Generic;

    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.ViewModels.OrderProducts;

    public class OrderViewModel : IMapFrom<ShoppingCart>
    {
        public string UserId { get; set; }

        

    }
}
