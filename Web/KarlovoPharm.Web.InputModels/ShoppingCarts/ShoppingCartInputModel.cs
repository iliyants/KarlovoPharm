namespace KarlovoPharm.Web.InputModels.ShoppingCarts
{
    using KarlovoPharm.Data.Models;
    using KarlovoPharm.Services.Mapping;
    using KarlovoPharm.Web.InputModels.ShoppingCartProducts;
    using System.Collections.Generic;
    public class ShoppingCartInputModel : IMapFrom<ShoppingCart>
    {
        public IEnumerable<ShoppingCartProductInputModel> ShoppingCartProducts { get; set; }
    }
}
