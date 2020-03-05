namespace KarlovoPharm.Web.ViewModels.Products
{
    using System.Collections.Generic;

    public class ProductAllViewModel 
    {
        public IEnumerable<ProductSingleViewModel> Products { get; set; }
         = new HashSet<ProductSingleViewModel>();
    }
}
